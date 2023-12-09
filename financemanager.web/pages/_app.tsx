import type { AppProps } from 'next/app'
import { MantineProvider } from '@mantine/core'
import '@mantine/core/styles.css'
import Navigation from '@/components/Navigation'
import '../styles/global.css'

export default function App ({ Component, pageProps }: AppProps): JSX.Element {
  return (

    <MantineProvider defaultColorScheme="dark">
      <Navigation />
      <Component {...pageProps} />
    </MantineProvider>
  )
}
