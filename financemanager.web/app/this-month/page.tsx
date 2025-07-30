'use client'

import {
  Container,
  Grid,
  Paper,
  Text,
  Title,
  Input,
  Modal,
  Button,
  Group,
  Stack,
  ActionIcon,
  Tooltip
} from '@mantine/core'
import { IconPencil } from '@tabler/icons-react'
import { useState } from 'react'
import TransactionsTable, { type TransactionData, type PotOption } from '../../components/TransactionsTable'

interface PotsStats {
  potId: number
  potName: string
  isSavingsPot: boolean
  amountAllocated: string
  amountLeft: number
  amountSpent: string
  monthlyAddition?: number // For savings pots
}

export default function ThisMonth() {
  // Dummy pot stats data
  const [potsStatsData] = useState<PotsStats[]>([
    {
      potId: 1,
      potName: 'Groceries',
      isSavingsPot: false,
      amountAllocated: '£400.00',
      amountLeft: 245.50,
      amountSpent: '£154.50'
    },
    {
      potId: 2,
      potName: 'Transport',
      isSavingsPot: false,
      amountAllocated: '£200.00',
      amountLeft: 87.30,
      amountSpent: '£112.70'
    },
    {
      potId: 3,
      potName: 'Entertainment',
      isSavingsPot: false,
      amountAllocated: '£150.00',
      amountLeft: 98.25,
      amountSpent: '£51.75'
    },
    {
      potId: 4,
      potName: 'Bills',
      isSavingsPot: false,
      amountAllocated: '£800.00',
      amountLeft: 125.00,
      amountSpent: '£675.00'
    },
    {
      potId: 5,
      potName: 'Emergency Fund',
      isSavingsPot: true,
      amountAllocated: '£500.00',
      amountLeft: 2450.75,
      amountSpent: '£0.00',
      monthlyAddition: 500.00
    },
    {
      potId: 6,
      potName: 'Holiday Fund',
      isSavingsPot: true,
      amountAllocated: '£300.00',
      amountLeft: 1250.00,
      amountSpent: '£0.00',
      monthlyAddition: 300.00
    },
    {
      potId: 7,
      potName: 'House Deposit',
      isSavingsPot: true,
      amountAllocated: '£1000.00',
      amountLeft: 15500.25,
      amountSpent: '£0.00',
      monthlyAddition: 1000.00
    }
  ])

  // Dummy transaction data for this month
  const [transactions] = useState<TransactionData[]>([
    {
      id: '1',
      merchantName: 'Tesco Superstore',
      iconUrl: '',
      transactionAmount: '£45.67',
      transactionDate: '2025-01-28',
      potId: '1'
    },
    {
      id: '2',
      merchantName: 'Shell Fuel Station',
      iconUrl: '',
      transactionAmount: '£52.30',
      transactionDate: '2025-01-27',
      potId: '2'
    },
    {
      id: '3',
      merchantName: 'Amazon UK',
      iconUrl: '',
      transactionAmount: '£23.99',
      transactionDate: '2025-01-26',
      potId: '3'
    },
    {
      id: '4',
      merchantName: 'British Gas',
      iconUrl: '',
      transactionAmount: '£125.00',
      transactionDate: '2025-01-25',
      potId: '4'
    },
    {
      id: '5',
      merchantName: 'Netflix',
      iconUrl: '',
      transactionAmount: '£15.99',
      transactionDate: '2025-01-24',
      potId: '3'
    }
  ])

  // Dummy pot options
  const potOptions: PotOption[] = [
    { value: '1', label: 'Groceries' },
    { value: '2', label: 'Transport' },
    { value: '3', label: 'Entertainment' },
    { value: '4', label: 'Bills' },
    { value: '5', label: 'Emergency Fund' },
    { value: '6', label: 'Holiday Fund' },
    { value: '7', label: 'House Deposit' }
  ]

  // Modal state for editing savings
  const [openEditSavingsModal, setOpenEditSavingsModal] = useState(false)
  const [currentEditedSavingsPot, setCurrentEditedSavingsPot] = useState<PotsStats | undefined>(undefined)
  const [editAmount, setEditAmount] = useState('')

  const handleEditSavings = (pot: PotsStats) => {
    setCurrentEditedSavingsPot(pot)
    setEditAmount(pot.amountLeft.toString())
    setOpenEditSavingsModal(true)
  }

  const handleSaveSavings = () => {
    console.log(`Updating ${currentEditedSavingsPot?.potName} to £${editAmount}`)
    setOpenEditSavingsModal(false)
  }

  const handlePotChange = (transactionId: string, potId: string) => {
    console.log(`Updated transaction ${transactionId} to pot ${potId}`)
  }

  const handleDeleteTransaction = (transactionId: string) => {
    console.log(`Delete transaction ${transactionId}`)
  }

  // Filter pots
  const regularPots = potsStatsData.filter(x => !x.isSavingsPot)
  const savingsPots = potsStatsData.filter(x => x.isSavingsPot)

  const PotCard = ({ pot }: { pot: PotsStats }) => (
    <Paper withBorder p="md" radius="md" shadow="sm">
      <Stack gap="sm">
        <Text fw={600} size="lg" ta="center">{pot.potName}</Text>
        <Stack gap="xs">
          <Text size="md" ta="center">Allocated: {pot.amountAllocated}</Text>
          <Text size="md" fw={500} c="green" ta="center">Amount Left: £{pot.amountLeft.toFixed(2)}</Text>
          <Text size="md" c="red" ta="center">Spent: {pot.amountSpent}</Text>
        </Stack>
      </Stack>
    </Paper>
  )

  const SavingsCard = ({ pot }: { pot: PotsStats }) => (
    <Paper withBorder p="md" radius="md" shadow="sm" style={{ minWidth: '200px', position: 'relative' }}>
      <Stack gap="sm">
        <Text fw={600} size="lg" ta="center" pr="md">{pot.potName}</Text>
        <Stack gap="xs">
          <Text size="xl" fw={700} c="green" ta="center">£{pot.amountLeft.toFixed(2)}</Text>
          <Text size="md" c="blue" ta="center">+£{pot.monthlyAddition?.toFixed(2)} this month</Text>
        </Stack>
        <Tooltip label="Edit savings amount">
          <ActionIcon
            variant="light"
            color="blue"
            onClick={() => handleEditSavings(pot)}
            style={{ position: 'absolute', top: 8, right: 8 }}
          >
            <IconPencil size={16} />
          </ActionIcon>
        </Tooltip>
      </Stack>
    </Paper>
  )

  const StatsPlaceholder = ({ title }: { title: string }) => (
    <Paper withBorder p="md" radius="md" shadow="sm" h={200}>
      <Stack align="center" justify="center" h="100%">
        <Text fw={600} size="lg">{title}</Text>
        <Text size="sm" c="dimmed">Chart/Graph will go here</Text>
      </Stack>
    </Paper>
  )

  return (
    <Container size="xl">
      {/* Page Title */}
      <Title order={1} mb="xl" ta="center">
        This Month
      </Title>

      {/* Regular Pots Section */}
      <Title order={2} mb="lg" ta="center">
        Monthly Budget Breakdown
      </Title>
      <Grid gutter="md" mb="xl">
        {regularPots.map((pot) => (
          <Grid.Col span={{ base: 12, sm: 6, md: regularPots.length > 3 ? 3 : 4 }} key={pot.potId}>
            <PotCard pot={pot} />
          </Grid.Col>
        ))}
      </Grid>

      {/* Savings Section */}
      <Title order={2} mb="lg" ta="center">
        Savings Breakdown
      </Title>
      <Grid gutter="lg" justify="center" mb="xl">
        {savingsPots.map((pot) => (
          <Grid.Col span="content" key={pot.potId}>
            <SavingsCard pot={pot} />
          </Grid.Col>
        ))}
      </Grid>

      {/* Savings Tracking Over Time */}
      <Title order={2} mb="lg" ta="center">
        Savings Progress Over Time
      </Title>
      <Grid gutter="md" mb="xl">
        <Grid.Col span={{ base: 12, md: 6 }}>
          <StatsPlaceholder title="Emergency Fund - 12 Month Trend" />
        </Grid.Col>
        <Grid.Col span={{ base: 12, md: 6 }}>
          <StatsPlaceholder title="Holiday Fund - 12 Month Trend" />
        </Grid.Col>
        <Grid.Col span={12}>
          <StatsPlaceholder title="All Savings Pots - Monthly Comparison" />
        </Grid.Col>
      </Grid>

      {/* Transactions Section */}
      <Title order={2} mb="lg" ta="center">
        Transactions This Month
      </Title>
      <TransactionsTable
        transactions={transactions}
        potOptions={potOptions}
        onPotChange={handlePotChange}
        onDeleteTransaction={handleDeleteTransaction}
        showActions={false}
      />

      {/* Stats Breakdown */}
      <Title order={2} mt="xl" mb="lg" ta="center">
        Stats Breakdown
      </Title>
      <Grid gutter="md" mb="xl">
        <Grid.Col span={{ base: 12, md: 4 }}>
          <StatsPlaceholder title="Spent Per Pot" />
        </Grid.Col>
        <Grid.Col span={{ base: 12, md: 4 }}>
          <StatsPlaceholder title="Top Places Money Spent" />
        </Grid.Col>
        <Grid.Col span={{ base: 12, md: 4 }}>
          <StatsPlaceholder title="Most Expensive Purchases" />
        </Grid.Col>
      </Grid>

      <Grid gutter="md" mb="xl">
        <Grid.Col span={{ base: 12, md: 4 }}>
          <StatsPlaceholder title="Weekly Spending Trend" />
        </Grid.Col>
        <Grid.Col span={{ base: 12, md: 4 }}>
          <StatsPlaceholder title="Category Comparison" />
        </Grid.Col>
        <Grid.Col span={{ base: 12, md: 4 }}>
          <StatsPlaceholder title="Budget vs Actual" />
        </Grid.Col>
      </Grid>

      <Paper withBorder p="md" radius="md" shadow="sm" h={250} mb="xl">
        <Stack align="center" justify="center" h="100%">
          <Text fw={600} size="xl">Daily Money Spent</Text>
          <Text size="sm" c="dimmed">Daily spending chart will go here</Text>
        </Stack>
      </Paper>

      {/* Edit Savings Modal */}
      <Modal
        opened={openEditSavingsModal}
        onClose={() => setOpenEditSavingsModal(false)}
        title={`Editing ${currentEditedSavingsPot?.potName}`}
        size="xs"
        centered
      >
        <Stack gap="md">
          <div>
            <Text size="sm" fw={500} mb="xs">
              Savings Amount
            </Text>
            <Input
              value={editAmount}
              onChange={(e) => setEditAmount(e.target.value)}
              placeholder="Enter amount"
            />
          </div>
          <Group justify="center">
            <Button color="green" onClick={handleSaveSavings}>
              Save
            </Button>
            <Button color="red" variant="outline" onClick={() => setOpenEditSavingsModal(false)}>
              Cancel
            </Button>
          </Group>
        </Stack>
      </Modal>
    </Container>
  )
}
