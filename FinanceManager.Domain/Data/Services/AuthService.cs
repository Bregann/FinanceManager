﻿using FinanceManager.Domain.DTOs.Auth.Requests;
using FinanceManager.Domain.DTOs.Auth.Responses;
using FinanceManager.Domain.Interfaces.Api;
using FinanceManager.Domain.Database.Context;
using FinanceManager.Domain.Database.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace FinanceManager.Domain.Data.Services
{
    public class AuthService(AppDbContext context) : IAuthService
    {
        private readonly PasswordHasher<User> _passwordHasher = new();

        public async Task RegisterUser(RegisterUserRequest request)
        {
            Log.Information($"Registering user {request.Username}");

            var existingUser = await context.Users.FirstOrDefaultAsync(u => u.Username == request.Username);

            if (context.Users.Any(x => x.Username == request.Username || x.Email == request.Email))
            {
                Log.Information($"User already exists {request.Username}");
                throw new DuplicateNameException("User already exists");
            }

            var newUser = new User
            {
                Username = request.Username.ToLower().Trim(),
                FirstName = request.FirstName.Trim(),
                Email = request.Email.Trim(),
                PasswordHash = _passwordHasher.HashPassword(new User(), request.Password.Trim())
            };

            context.Users.Add(newUser);
            await context.SaveChangesAsync();

            Log.Information($"User registered {request.Username}");
        }

        public async Task<LoginUserResponse> LoginUser(LoginUserRequest request)
        {
            Log.Information($"Logging in user {request.Username}");

            var user = await context.Users.FirstOrDefaultAsync(u => u.Username == request.Username.ToLower().Trim());

            if (user == null)
            {
                Log.Information($"User not found {request.Username}");
                throw new KeyNotFoundException("User not found");
            }

            if (_passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password) == PasswordVerificationResult.Failed)
            {
                Log.Information($"Invalid password for user {request.Username}");
                throw new UnauthorizedAccessException("Invalid password");
            }

            var token = GenerateJwtToken(user);
            var refreshToken = GenerateRefreshToken();

            await SaveRefreshToken(refreshToken, user.Id);

            Log.Information($"User logged in {request.Username}");

            return new LoginUserResponse
            {
                AccessToken = token,
                RefreshToken = refreshToken
            };
        }

        public async Task<LoginUserResponse> RefreshToken(string userRefreshToken)
        {
            Log.Information($"Refreshing token {userRefreshToken}");
            var refreshToken = await context.UserRefreshTokens.FirstOrDefaultAsync(t => t.Token == userRefreshToken);

            if (refreshToken == null)
            {
                Log.Information($"Token not found for refresh token {userRefreshToken}");
                throw new KeyNotFoundException("Token not found");
            }

            if (refreshToken.ExpiresAt < DateTime.UtcNow)
            {
                Log.Information($"Token expired for user {refreshToken.UserId}");
                throw new UnauthorizedAccessException("Refresh token expired");
            }

            var user = await context.Users.FirstOrDefaultAsync(u => u.Id == refreshToken.UserId);

            if (user == null)
            {
                Log.Information($"User not found for token {refreshToken.UserId}");
                throw new KeyNotFoundException("User not found");
            }

            var token = GenerateJwtToken(user);
            var newRefreshToken = GenerateRefreshToken();

            await SaveRefreshToken(newRefreshToken, user.Id);

            refreshToken.IsRevoked = true;
            await context.SaveChangesAsync();

            Log.Information($"Token refreshed for user {refreshToken.UserId}");

            return new LoginUserResponse
            {
                AccessToken = token,
                RefreshToken = newRefreshToken
            };
        }

        private static string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JwtKey")!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: Environment.GetEnvironmentVariable("JwtValidIssuer"),
                audience: Environment.GetEnvironmentVariable("JwtValidAudience"),
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private static string GenerateRefreshToken()
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(128));
        }

        private async Task SaveRefreshToken(string token, string userId)
        {
            var refreshToken = new UserRefreshToken
            {
                Token = token,
                UserId = userId,
                ExpiresAt = DateTime.UtcNow.AddDays(7)
            };

            context.UserRefreshTokens.Add(refreshToken);
            await context.SaveChangesAsync();
        }
    }
}
