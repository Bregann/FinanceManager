import { Burger, Drawer, Group, UnstyledButton, Container } from '@mantine/core'
import { useDisclosure } from '@mantine/hooks'
import classes from '../styles/Nav.module.css'
import { useRouter } from 'next/router'
import Link from 'next/link'
import { IconCalendarPlus } from '@tabler/icons-react'
import { useState } from 'react'
import AddNewMonthModal from './AddNewMonthModal'

const Navigation = () => {
  const [opened, { toggle }] = useDisclosure()
  const router = useRouter()
  const [addMonthDisplayed, setAddMonthDisplayed] = useState(false)

  return (
    <>
      <header className={classes.header}>
      <Container size='100%' className={classes.inner}>
        <Group visibleFrom='xs'>
          <Link href='/'>
            <UnstyledButton className={router.pathname === '/' ? classes.linkActive : classes.link}>Home</UnstyledButton>
          </Link>
          <Link href='/this-month'>
            <UnstyledButton className={router.pathname === '/this-month' ? classes.linkActive : classes.link}>This Month</UnstyledButton>
          </Link>
          <Link href='/management' >
            <UnstyledButton className={router.pathname === '/management' ? classes.linkActive : classes.link}>Management</UnstyledButton>
          </Link>
          <IconCalendarPlus className={classes.addMonthIco} onClick={() => { setAddMonthDisplayed(true) }}/>
        </Group>
        <Burger
          className={classes.burger}
          opened={opened}
          onClick={toggle}
          hiddenFrom="xs"
          size="sm" />
          </Container>
      </header>

      <AddNewMonthModal displayModal={addMonthDisplayed} hideModal={() => { setAddMonthDisplayed(false) }}/>

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
