/** @type {import('next').NextConfig} */
const nextConfig = {
  reactStrictMode: true,
  images: {
    remotePatterns: [
      {
        protocol: 'https',
        hostname: 'merchant-logos.monzo.com',
      }
    ],
  }
}

module.exports = nextConfig
