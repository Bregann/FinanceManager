import { Table, Select, type ComboboxItem } from '@mantine/core'
import { IconCircleCheck, IconCircleX, IconTrash, IconPhotoX } from '@tabler/icons-react'
import classes from '../styles/TransactionsTable.module.css'
import Image from 'next/image'
import { useState } from 'react'
import fetchHelper from '@/helpers/FetchHelper'
import notificationHelper from '@/helpers/NotificationHelper'
import YesNoModal from './Shared/YesNoModal'

export interface TransactionTableData {
  id: string
  merchantName: string
  iconUrl: string
  transactionAmount: string
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
  const [showYesNoModal, setShowYesNoModal] = useState(false)
  const [clickedRow, setClickedRow] = useState<TransactionTableData | null>(null)
  const UpdateDropdownChange = async (row: TransactionTableData, potId: number): Promise<void> => {
    const data = {
      transactionId: row.id,
      potId
    }

    const fetchResult = await fetchHelper.doPost('/transactions/UpdateTransaction', data)

    if (fetchResult.data === false || fetchResult.errored) {
      notificationHelper.showErrorNotification('Error', 'Transaction failed to update', 5000, <IconCircleX />)
    } else {
      if (props.removeRow) {
        setTableRows(tableRows.filter(x => x !== row))
      }

      notificationHelper.showSuccessNotification('Success', 'Transaction processed successfully', 2000, <IconCircleCheck />)
    }
  }

  const DeleteTransaction = async (): Promise<void> => {
    if (clickedRow !== null) {
      const fetchResult = await fetchHelper.doPost(`/transactions/DeleteTransaction/${clickedRow.id}`, null)

      if (fetchResult.data === false || fetchResult.errored) {
        notificationHelper.showErrorNotification('Error', 'Transaction failed to remove', 5000, <IconCircleX />)
      } else {
        setTableRows(tableRows.filter(x => x !== clickedRow))
        notificationHelper.showSuccessNotification('Success', 'Transaction removed successfully', 2000, <IconCircleCheck />)
      }

      setShowYesNoModal(false)
      setClickedRow(null)
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
                      {row.iconUrl !== ''
                        ? <Image
                        className={classes.logo}
                        src={row.iconUrl}
                        width={48}
                        height={48}
                        alt='logo'
                      />
                        : <IconPhotoX className={classes.logoMissing} /> }
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
                      <IconTrash className={classes.binIco} onClick={() => { setClickedRow(row); setShowYesNoModal(true) }}/>
                    </Table.Td>
                  </Table.Tr>
                </>
              )
            })}
        </Table.Tbody>
      </Table>
    </div>

    <YesNoModal
      text='Are you sure you want to delete this transaction? This cannot be undone!'
      displayModal={showYesNoModal}
      onHideModal={setShowYesNoModal}
      onYesClicked={async () => { await DeleteTransaction() }} />
    </>
  )
}

export default TransactionsTable
