import { type ReactNode } from 'react'
import { AppShell, Burger, Group } from '@mantine/core'
import '@mantine/core/styles.css'
import { useDisclosure } from '@mantine/hooks'

interface MyAppShellProps {
  children: ReactNode
}

const Navigation = ({ children }: MyAppShellProps): JSX.Element => {
  const [opened, { toggle }] = useDisclosure()

  return (
    <AppShell
        header={{ height: 48 }}
        navbar={{
          width: 200,
          breakpoint: 'sm',
          collapsed: { mobile: !opened }
        }}>
        <AppShell.Header>
            <Group h="100%" px="md">
                <Burger opened={opened} onClick={toggle} hiddenFrom="sm" size="sm" />
            </Group>
        </AppShell.Header>
        <AppShell.Navbar p="md">Navbar</AppShell.Navbar>
        <AppShell.Main>
            {children}
        </AppShell.Main>
    </AppShell>
  )
}

export default Navigation
