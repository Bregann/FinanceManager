import { Table, Select } from '@mantine/core'
import { IconTrash } from '@tabler/icons-react'
import classes from '../styles/TransactionsTable.module.css'

const TransactionsTable = (): JSX.Element => {
  return (
    <>
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
                <IconTrash className={classes.binIco} />
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
                <IconTrash className={classes.binIco} />
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
                <IconTrash className={classes.binIco} />
              </Table.Td>
            </Table.Tr>
          </Table.Tbody>
        </Table>
      </div>
    </>
  )
}

export default TransactionsTable
