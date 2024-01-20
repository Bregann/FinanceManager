import type { AppProps } from 'next/app'
import { MantineProvider } from '@mantine/core'
import '@mantine/core/styles.css'
import '@mantine/notifications/styles.css'
import Navigation from '@/components/Navigation'
import '../styles/global.css'
import { Notifications } from '@mantine/notifications'

export default function App ({ Component, pageProps }: AppProps): JSX.Element {
  if (process.env.NODE_ENV === 'development') {
    process.env.NODE_TLS_REJECT_UNAUTHORIZED = '0'
  }

  return (
    <MantineProvider defaultColorScheme="dark">
      <Notifications />
      <Navigation />
      <Component {...pageProps} />
    </MantineProvider>
  )
}
