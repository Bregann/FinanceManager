import { Container, Grid, Select, Table } from '@mantine/core'
import classes from '../styles/Home.module.css'
import { IconTrash } from '@tabler/icons-react'

export default function Home (): JSX.Element {
  return (
    <>

      <Container size='lg'>
        <Grid gutter={0}>
          <Grid.Col span="auto">
            <div className={classes.box}>
              <h3 className={classes.header}>Money In</h3>
              <p className={classes.amount}>£100</p>
            </div>
          </Grid.Col>
          <Grid.Col span="auto">
            <div className={classes.box}>
              <h3 className={classes.header}>Money Spent</h3>
              <p className={classes.amount}>£100</p>
            </div>
          </Grid.Col>
          <Grid.Col span="auto">
            <div className={classes.box}>
              <h3 className={classes.header}>Money Left</h3>
              <p className={classes.amount}>£100</p>
            </div>
          </Grid.Col>
          <Grid.Col span="auto">
            <div className={classes.box}>
              <h3 className={classes.header}>Total in Savings</h3>
              <p className={classes.amount}>£100</p>
            </div>
          </Grid.Col>
        </Grid>

        <h1>Home</h1>

        <div className={classes.tableBox}>
          <Table striped>
            <Table.Thead>
              <Table.Tr>
                <Table.Th>Icon</Table.Th>
                <Table.Th>Merchant</Table.Th>
                <Table.Th>Transaction Amount</Table.Th>
                <Table.Th>Date</Table.Th>
                <Table.Th>Pot Type</Table.Th>
                <Table.Th></Table.Th>
              </Table.Tr>
            </Table.Thead>
            <Table.Tbody>
              <Table.Tr>
                <Table.Td>Ico</Table.Td>
                <Table.Td>Sainsburys</Table.Td>
                <Table.Td>£500</Table.Td>
                <Table.Td>Today</Table.Td>
                <Table.Td>
                  <Select
                    placeholder='Pick value'
                    data={['Savings', 'None']}
                    comboboxProps={{ transitionProps: { transition: 'pop', duration: 200 } }}
                  />
                </Table.Td>
                <Table.Td>
                  <IconTrash className={classes.binIco}/>
                </Table.Td>
              </Table.Tr>
              <Table.Tr>
                <Table.Td>Ico</Table.Td>
                <Table.Td>Sainsburys</Table.Td>
                <Table.Td>£500</Table.Td>
                <Table.Td>Today</Table.Td>
                <Table.Td>
                  <Select
                    placeholder='Pick value'
                    data={['Savings', 'None']}
                    comboboxProps={{ transitionProps: { transition: 'pop', duration: 200 } }}
                  />
                </Table.Td>
                <Table.Td>
                  <IconTrash className={classes.binIco}/>
                </Table.Td>
              </Table.Tr>
              <Table.Tr>
                <Table.Td>Ico</Table.Td>
                <Table.Td>Sainsburys</Table.Td>
                <Table.Td>£500</Table.Td>
                <Table.Td>Today</Table.Td>
                <Table.Td>
                  <Select
                    placeholder='Pick value'
                    data={['Savings', 'None']}
                    comboboxProps={{ transitionProps: { transition: 'pop', duration: 200 } }}
                  />
                </Table.Td>
                <Table.Td>
                  <IconTrash className={classes.binIco}/>
                </Table.Td>
              </Table.Tr>
            </Table.Tbody>
          </Table>
        </div>
      </Container>
    </>
  )
}
