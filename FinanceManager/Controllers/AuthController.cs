using FinanceManager.Domain.DTOs.Auth.Requests;
using FinanceManager.Domain.DTOs.Auth.Responses;
using FinanceManager.Domain.Interfaces.Api;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Data;

namespace FinanceManager.Core.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult> RegisterUser([FromBody] RegisterUserRequest request)
        {
            try
            {
                await authService.RegisterUser(request);
            }
            catch (DuplicateNameException ex)
            {
                Log.Warning(ex, "Error attempting to register user");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Unknown error attempting to register user");
                return BadRequest();
            }

            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult<LoginUserResponse>> LoginUser([FromBody] LoginUserRequest request)
        {
            try
            {
                var response = await authService.LoginUser(request);
                return Ok(response);
            }
            catch (KeyNotFoundException ex)
            {
                Log.Warning(ex, "Error attempting to login user");
                return Unauthorized(ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                Log.Warning(ex, "Error attempting to login user");
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Unknown error attempting to login user");
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<ActionResult<LoginUserResponse>> RefreshToken([FromBody] RefreshTokenRequest request)
        {
            try
            {
                var response = await authService.RefreshToken(request.RefreshToken);
                return Ok(response);
            }
            catch (KeyNotFoundException ex)
            {
                Log.Warning(ex, "Error attempting to refresh token");
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Unknown error attempting to refresh token");
                return BadRequest();
            }
        }
    }
}
