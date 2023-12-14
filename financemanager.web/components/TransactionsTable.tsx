import { Table, Select, type ComboboxItem } from '@mantine/core'
import { IconCircleCheck, IconTrash } from '@tabler/icons-react'
import classes from '../styles/TransactionsTable.module.css'
import Image from 'next/image'
import { useState } from 'react'
import fetchHelper from '@/helpers/FetchHelper'
import notificationHelper from '@/helpers/NotificationHelper'

export interface TransactionTableData {
  id: string
  merchantName: string
  iconUrl: string
  transactionAmount: number
  transactionDate: string
  potId?: string
}

interface TransactionsTableProps {
  rows: TransactionTableData[]
  dropdownValues: ComboboxItem[]
  removeRow: boolean
}

const TransactionsTable = (props: TransactionsTableProps): JSX.Element => {
  const [tableRows, setTableRows] = useState<TransactionTableData[]>(props.rows)

  const UpdateDropdownChange = async (row: TransactionTableData, potId: number): Promise<void> => {
    // only want to remove a row on the homepage
    if (props.removeRow) {
      const data = {
        transactionId: row.id,
        potId
      }

      const fetchResult = await fetchHelper.doPost('/transactions/UpdateTransaction', data)

      if (fetchResult.data === false || fetchResult.errored) {
        console.log('error')
      } else {
        notificationHelper.showSuccessNotification('Success', 'message', <IconCircleCheck />)
      }
    }
  }

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
            {tableRows.map((row) => {
              return (
                <>
                  <Table.Tr key={row.id}>
                    <Table.Td>
                      <Image
                        className={classes.logo}
                        src={row.iconUrl}
                        width={48}
                        height={48}
                        alt='logo'
                      />
                    </Table.Td>
                    <Table.Td>{row.merchantName}</Table.Td>
                    <Table.Td>{row.transactionAmount}</Table.Td>
                    <Table.Td>{row.transactionDate}</Table.Td>
                    <Table.Td>
                      <Select
                        onChange={async (e) => { await UpdateDropdownChange(row, Number(e)) }}
                        defaultValue={row.potId !== null ? row.potId : undefined}
                        placeholder='Pick value'
                        data={props.dropdownValues}
                        comboboxProps={{ transitionProps: { transition: 'pop', duration: 200 } }}
                      />
                    </Table.Td>
                    <Table.Td>
                      <IconTrash className={classes.binIco} />
                    </Table.Td>
                  </Table.Tr>
                  <Table.Tr></Table.Tr>
                </>
              )
            })}
        </Table.Tbody>
      </Table>
    </div >
    </>
  )
}

export default TransactionsTable
