'use client'

import {
  Container,
  Grid,
  Paper,
  Text,
  Title,
  Stack
} from '@mantine/core'
import { useState } from 'react'
import TransactionsTable, { type TransactionData, type PotOption } from '../components/TransactionsTable'

interface HomeTopStats {
  moneyIn: string
  moneySpent: string
  moneyLeft: string
  totalSavings: string
}

export default function Home() {
  // Dummy data for stats
  const homeData: HomeTopStats = {
    moneyIn: '£3,250.00',
    moneySpent: '£1,845.50',
    moneyLeft: '£1,404.50',
    totalSavings: '£12,350.75'
  }

  // Dummy transaction data
  const [transactions] = useState<TransactionData[]>([
    {
      id: '1',
      merchantName: 'Tesco Superstore',
      iconUrl: '',
      transactionAmount: '£45.67',
      transactionDate: '2025-01-28',
      potId: undefined
    },
    {
      id: '2',
      merchantName: 'Shell Fuel Station',
      iconUrl: '',
      transactionAmount: '£52.30',
      transactionDate: '2025-01-27',
      potId: undefined
    },
    {
      id: '3',
      merchantName: 'Amazon UK',
      iconUrl: '',
      transactionAmount: '£23.99',
      transactionDate: '2025-01-26',
      potId: undefined
    },
    {
      id: '4',
      merchantName: 'Costa Coffee',
      iconUrl: '',
      transactionAmount: '£4.95',
      transactionDate: '2025-01-26',
      potId: undefined
    },
    {
      id: '5',
      merchantName: 'Netflix',
      iconUrl: '',
      transactionAmount: '£15.99',
      transactionDate: '2025-01-25',
      potId: undefined
    }
  ])

  // Dummy pot options
  const potOptions: PotOption[] = [
    { value: '1', label: 'Groceries' },
    { value: '2', label: 'Transport' },
    { value: '3', label: 'Entertainment' },
    { value: '4', label: 'Bills' },
    { value: '5', label: 'Savings' }
  ]

  const handlePotChange = (transactionId: string, potId: string) => {
    console.log(`Updated transaction ${transactionId} to pot ${potId}`)
  }

  const handleDeleteTransaction = (transactionId: string) => {
    console.log(`Delete transaction ${transactionId}`)
  }

  const StatCard = ({ title, amount }: { title: string, amount: string }) => (
    <Paper withBorder p="md" radius="md" shadow="sm">
      <Stack gap="xs">
        <Text size="sm" c="dimmed" fw={500}>
          {title}
        </Text>
        <Text size="xl" fw={700}>
          {amount}
        </Text>
      </Stack>
    </Paper>
  )

  return (
    <Container size="lg">
      {/* Top Stats Grid */}
      <Grid gutter="md" mb="xl">
        <Grid.Col span={{ base: 12, sm: 6, md: 3 }}>
          <StatCard title="Money In" amount={homeData.moneyIn} />
        </Grid.Col>
        <Grid.Col span={{ base: 12, sm: 6, md: 3 }}>
          <StatCard title="Money Spent" amount={homeData.moneySpent} />
        </Grid.Col>
        <Grid.Col span={{ base: 12, sm: 6, md: 3 }}>
          <StatCard title="Money Left" amount={homeData.moneyLeft} />
        </Grid.Col>
        <Grid.Col span={{ base: 12, sm: 6, md: 3 }}>
          <StatCard title="Total in Savings" amount={homeData.totalSavings} />
        </Grid.Col>
      </Grid>

      {/* Page Title */}
      <Title order={1} mb="lg">
        Recent Transactions
      </Title>

      {/* Transactions Table */}
      <TransactionsTable
        transactions={transactions}
        potOptions={potOptions}
        onPotChange={handlePotChange}
        onDeleteTransaction={handleDeleteTransaction}
      />
    </Container>
  )
}
