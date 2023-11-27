import type { AppProps } from 'next/app'
import { MantineProvider } from '@mantine/core'
import '@mantine/core/styles.css'
import Navigation from '@/components/Navigation'

export default function App ({ Component, pageProps }: AppProps): JSX.Element {
  return (

    <MantineProvider defaultColorScheme="dark">
      <Navigation>
        <Component {...pageProps} />
      </Navigation>
    </MantineProvider>
  )
}
