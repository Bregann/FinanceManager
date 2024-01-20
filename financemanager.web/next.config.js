/** @type {import('next').NextConfig} */
const nextConfig = {
  reactStrictMode: true,
  output: "standalone",
  images: {
    remotePatterns: [
      {
        protocol: 'https',
        hostname: 'merchant-logos.monzo.com',
      }
    ],
  },
}

module.exports = nextConfig
