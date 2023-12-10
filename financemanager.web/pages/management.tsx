import { Button, Checkbox, Container, Grid, Group, Input, Table } from '@mantine/core'
import classes from '../styles/Management.module.css'

const AutomaticTransactions = (): JSX.Element => {
  return (
    <>
      <h1>Pot/Automatic Transactions Management</h1>
      <Container size="90%">
        <Grid>
          <Grid.Col span={{ base: 12, md: 6 }}>
            <div className={classes.box}>
              <h2>Pots</h2>
              <Group justify='center'>
                <Input.Wrapper label="Name" className={classes.inputWrapper} style={{ width: '50%' }}>
                  <Input placeholder="Input inside Input.Wrapper" />
                </Input.Wrapper>
                <Input.Wrapper label="Amount" className={classes.inputWrapper} style={{ width: '10%' }}>
                  <Input placeholder="Input inside Input.Wrapper" />
                </Input.Wrapper>
                <Checkbox
                  className={classes.checkBoxButton}
                  defaultChecked
                  label="Savings pot?"
                />
                <Button className={classes.checkBoxButton}>Add</Button>
              </Group>

              <Table className={classes.potTable} striped withTableBorder>
                <Table.Thead>
                  <Table.Tr>
                    <Table.Th>Pot Name</Table.Th>
                    <Table.Th>Amount</Table.Th>
                    <Table.Th style={{ textAlign: 'right' }}>Actions</Table.Th>
                  </Table.Tr>
                </Table.Thead>
                <Table.Tbody>
                  <Table.Tr>
                    <Table.Td>
                      <Input defaultValue="Tilly" />
                    </Table.Td>
                    <Table.Td width="15%">
                      <Input defaultValue="£500" />
                    </Table.Td>
                    <Table.Td style={{ textAlign: 'right' }}>
                      <Button variant='filled' color='green' style={{ marginRight: 10 }}>Save</Button>
                      <Button variant='filled' color='red'>Delete</Button>
                    </Table.Td>
                  </Table.Tr>
                  <Table.Tr>
                    <Table.Td>
                      <Input defaultValue="Tilly" />
                    </Table.Td>
                    <Table.Td>
                      <Input defaultValue="£500" />
                    </Table.Td>
                    <Table.Td style={{ textAlign: 'right' }}>
                      <Button variant='filled' color='green' style={{ marginRight: 10 }}>Save</Button>
                      <Button variant='filled' color='red'>Delete</Button>
                    </Table.Td>
                  </Table.Tr>
                  <Table.Tr>
                    <Table.Td>
                      <Input defaultValue="Tilly" />
                    </Table.Td>
                    <Table.Td>
                      <Input defaultValue="£500" />
                    </Table.Td>
                    <Table.Td style={{ textAlign: 'right' }}>
                      <Button variant='filled' color='green' style={{ marginRight: 10 }}>Save</Button>
                      <Button variant='filled' color='red'>Delete</Button>
                    </Table.Td>
                  </Table.Tr>
                  <Table.Tr>
                    <Table.Td>
                      <Input defaultValue="Tilly" />
                    </Table.Td>
                    <Table.Td>
                      <Input defaultValue="£500" />
                    </Table.Td>
                    <Table.Td style={{ textAlign: 'right' }}>
                      <Button variant='filled' color='green' style={{ marginRight: 10 }}>Save</Button>
                      <Button variant='filled' color='red'>Delete</Button>
                    </Table.Td>
                  </Table.Tr>
                </Table.Tbody>
              </Table>
            </div>
          </Grid.Col>
          <Grid.Col span={{ base: 12, md: 6 }}>
            <h2>Automatic Transactions</h2>
            <div className={classes.box}>

            </div>
          </Grid.Col>
        </Grid>
      </Container>
    </>
  )
}

export default AutomaticTransactions
