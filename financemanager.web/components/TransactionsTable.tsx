'use client'

import {
  Table,
  Select,
  ActionIcon,
  Image,
  Text,
  Paper
} from '@mantine/core'
import {
  IconTrash,
  IconPhotoX
} from '@tabler/icons-react'

export interface TransactionData {
  id: string
  merchantName: string
  iconUrl: string
  transactionAmount: string
  transactionDate: string
  potId?: string
}

export interface PotOption {
  value: string
  label: string
}

interface TransactionsTableProps {
  transactions: TransactionData[]
  potOptions: PotOption[]
  onPotChange?: (transactionId: string, potId: string) => void
  onDeleteTransaction?: (transactionId: string) => void
  showActions?: boolean
}

export default function TransactionsTable({
  transactions,
  potOptions,
  onPotChange,
  onDeleteTransaction,
  showActions = true
}: TransactionsTableProps) {

  const handlePotChange = (transactionId: string, potId: string | null) => {
    if (potId && onPotChange) {
      onPotChange(transactionId, potId)
    }
  }

  const handleDeleteClick = (transactionId: string) => {
    if (onDeleteTransaction) {
      onDeleteTransaction(transactionId)
    }
  }

  return (
    <Paper withBorder radius="md" p="md" shadow="sm">
      <Table striped highlightOnHover>
        <Table.Thead>
          <Table.Tr>
            <Table.Th>Icon</Table.Th>
            <Table.Th>Merchant</Table.Th>
            <Table.Th>Amount</Table.Th>
            <Table.Th>Date</Table.Th>
            <Table.Th>Pot Type</Table.Th>
            {showActions && <Table.Th>Actions</Table.Th>}
          </Table.Tr>
        </Table.Thead>
        <Table.Tbody>
          {transactions.map((transaction) => (
            <Table.Tr key={transaction.id}>
              <Table.Td>
                {transaction.iconUrl ? (
                  <Image
                    src={transaction.iconUrl}
                    alt="Merchant icon"
                    width={32}
                    height={32}
                    radius="sm"
                  />
                ) : (
                  <IconPhotoX size={32} color="gray" />
                )}
              </Table.Td>
              <Table.Td>
                <Text fw={500}>{transaction.merchantName}</Text>
              </Table.Td>
              <Table.Td>
                <Text fw={500}>{transaction.transactionAmount}</Text>
              </Table.Td>
              <Table.Td>
                <Text c="dimmed">{transaction.transactionDate}</Text>
              </Table.Td>
              <Table.Td>
                <Select
                  placeholder="Pick pot"
                  data={potOptions}
                  defaultValue={transaction.potId}
                  comboboxProps={{
                    transitionProps: { transition: 'pop', duration: 200 }
                  }}
                  onChange={(value) => handlePotChange(transaction.id, value)}
                />
              </Table.Td>
              {showActions && (
                <Table.Td>
                  <ActionIcon
                    variant="light"
                    color="red"
                    onClick={() => handleDeleteClick(transaction.id)}
                  >
                    <IconTrash size={16} />
                  </ActionIcon>
                </Table.Td>
              )}
            </Table.Tr>
          ))}
        </Table.Tbody>
      </Table>
    </Paper>
  )
}
