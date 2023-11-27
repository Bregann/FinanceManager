import { Burger, Container, Drawer, Button, Group } from '@mantine/core'
import { useDisclosure } from '@mantine/hooks'
import classes from '../styles/Nav.module.css'

const Navigation = (): JSX.Element => {
  const [opened, { toggle }] = useDisclosure()

  return (
    <>
      <header className={classes.header}>
        <Container className={classes.inner} size='100%'>
          <Group visibleFrom='xs'>
            <Button>test</Button>
            <Button>test2</Button>
          </Group>
          <Burger
            className={classes.burger}
            opened={opened}
            onClick={toggle}
            hiddenFrom="xs"
            size="sm" />
        </Container>
      </header>

      <Drawer
        opened={opened}
        onClose={toggle}
        size='100%'
      >

      </Drawer>
    </>

  )
}

export default Navigation
