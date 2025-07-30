'use client'

import {
  Container,
  Grid,
  Paper,
  Text,
  Title,
  Table,
  Input,
  Button,
  Group,
  Checkbox,
  Select,
  Modal,
  Stack
} from '@mantine/core'
import { useState } from 'react'

interface PotData {
  potId: number
  potName: string
  potAmount: number
  isSavingsPot: boolean
}

interface AutomaticTransactionData {
  id: number
  merchantName: string
  potId: string
}

interface PotOption {
  value: string
  label: string
}

export default function Management() {
  // Dummy pot data
  const [potData, setPotData] = useState<PotData[]>([
    {
      potId: 1,
      potName: 'Groceries',
      potAmount: 400,
      isSavingsPot: false
    },
    {
      potId: 2,
      potName: 'Transport',
      potAmount: 200,
      isSavingsPot: false
    },
    {
      potId: 3,
      potName: 'Entertainment',
      potAmount: 150,
      isSavingsPot: false
    },
    {
      potId: 4,
      potName: 'Emergency Fund',
      potAmount: 500,
      isSavingsPot: true
    },
    {
      potId: 5,
      potName: 'Holiday Fund',
      potAmount: 300,
      isSavingsPot: true
    }
  ])

  // Dummy automatic transaction data
  const [automaticTransactionData, setAutomaticTransactionData] = useState<AutomaticTransactionData[]>([
    {
      id: 1,
      merchantName: 'Netflix',
      potId: '3'
    },
    {
      id: 2,
      merchantName: 'Spotify',
      potId: '3'
    },
    {
      id: 3,
      merchantName: 'British Gas',
      potId: '4'
    },
    {
      id: 4,
      merchantName: 'Tesco Clubcard',
      potId: '1'
    }
  ])

  // Dummy pot options for dropdowns
  const potOptions: PotOption[] = [
    { value: '1', label: 'Groceries' },
    { value: '2', label: 'Transport' },
    { value: '3', label: 'Entertainment' },
    { value: '4', label: 'Emergency Fund' },
    { value: '5', label: 'Holiday Fund' }
  ]

  // Form states for adding new items
  const [addPotName, setAddPotName] = useState('')
  const [addPotAmount, setAddPotAmount] = useState('')
  const [addPotIsSavings, setAddPotIsSavings] = useState(false)
  const [addMerchantName, setAddMerchantName] = useState('')
  const [addMerchantPotId, setAddMerchantPotId] = useState<string | null>(null)

  // Modal state
  const [showDeleteConfirmation, setShowDeleteConfirmation] = useState(false)
  const [deleteType, setDeleteType] = useState<'pot' | 'transaction'>('pot')
  const [deleteId, setDeleteId] = useState<number>(0)

  // Placeholder functions
  const handleAddPot = () => {
    console.log('Add pot:', { addPotName, addPotAmount, addPotIsSavings })
    setAddPotName('')
    setAddPotAmount('')
    setAddPotIsSavings(false)
  }

  const handleAddAutomaticTransaction = () => {
    console.log('Add automatic transaction:', { addMerchantName, addMerchantPotId })
    setAddMerchantName('')
    setAddMerchantPotId(null)
  }

  const handleSavePot = (pot: PotData) => {
    console.log('Save pot:', pot)
  }

  const handleDeletePot = (potId: number) => {
    setDeleteType('pot')
    setDeleteId(potId)
    setShowDeleteConfirmation(true)
  }

  const handleSaveAutomaticTransaction = (transaction: AutomaticTransactionData) => {
    console.log('Save automatic transaction:', transaction)
  }

  const handleDeleteAutomaticTransaction = (transactionId: number) => {
    setDeleteType('transaction')
    setDeleteId(transactionId)
    setShowDeleteConfirmation(true)
  }

  const confirmDelete = () => {
    if (deleteType === 'pot') {
      console.log('Delete pot:', deleteId)
    } else {
      console.log('Delete automatic transaction:', deleteId)
    }
    setShowDeleteConfirmation(false)
  }

  const updatePotField = (index: number, field: keyof PotData, value: string | number | boolean) => {
    const newData = [...potData]
    newData[index] = { ...newData[index], [field]: value }
    setPotData(newData)
  }

  const updateTransactionField = (index: number, field: keyof AutomaticTransactionData, value: string) => {
    const newData = [...automaticTransactionData]
    newData[index] = { ...newData[index], [field]: value }
    setAutomaticTransactionData(newData)
  }

  return (
    <Container size="95%">
      {/* Page Title */}
      <Title order={1} mb="xl" ta="center">
        Pot/Automatic Transactions Management
      </Title>

      <Grid gutter="lg">
        {/* Pots Management Section */}
        <Grid.Col span={{ base: 12, md: 6 }}>
          <Paper withBorder p="md" radius="md" shadow="sm">
            <Title order={2} mb="lg" ta="center">
              Pots
            </Title>

            {/* Add New Pot Form */}
            <Group justify="center" mb="lg">
              <Input.Wrapper label="Name" style={{ width: '40%' }}>
                <Input
                  value={addPotName}
                  onChange={(e) => setAddPotName(e.target.value)}
                  placeholder="Enter pot name"
                />
              </Input.Wrapper>
              <Input.Wrapper label="Amount" style={{ width: '15%' }}>
                <Input
                  value={addPotAmount}
                  onChange={(e) => setAddPotAmount(e.target.value)}
                  placeholder="0"
                />
              </Input.Wrapper>
              <Checkbox
                checked={addPotIsSavings}
                onChange={(e) => setAddPotIsSavings(e.currentTarget.checked)}
                label="Savings pot?"
                mt="xl"
              />
              <Button
                onClick={handleAddPot}
                disabled={!addPotName || !addPotAmount}
                mt="xl"
              >
                Add
              </Button>
            </Group>

            {/* Pots Table */}
            <Table striped withTableBorder>
              <Table.Thead>
                <Table.Tr>
                  <Table.Th>Pot Name</Table.Th>
                  <Table.Th>Amount (£)</Table.Th>
                  <Table.Th>Savings?</Table.Th>
                  <Table.Th ta="right" w="25%">Actions</Table.Th>
                </Table.Tr>
              </Table.Thead>
              <Table.Tbody>
                {potData.map((pot, index) => (
                  <Table.Tr key={pot.potId}>
                    <Table.Td>
                      <Input
                        value={pot.potName}
                        onChange={(e) => updatePotField(index, 'potName', e.target.value)}
                      />
                    </Table.Td>
                    <Table.Td w="15%">
                      <Input
                        value={pot.potAmount}
                        onChange={(e) => updatePotField(index, 'potAmount', Number(e.target.value))}
                      />
                    </Table.Td>
                    <Table.Td>
                      <Checkbox
                        checked={pot.isSavingsPot}
                        onChange={(e) => updatePotField(index, 'isSavingsPot', e.currentTarget.checked)}
                        style={{ paddingLeft: 15 }}
                      />
                    </Table.Td>
                    <Table.Td ta="right">
                      <Group gap="xs" justify="flex-end">
                        <Button
                          variant="filled"
                          color="green"
                          size="xs"
                          onClick={() => handleSavePot(pot)}
                        >
                          Save
                        </Button>
                        <Button
                          variant="filled"
                          color="red"
                          size="xs"
                          onClick={() => handleDeletePot(pot.potId)}
                        >
                          Delete
                        </Button>
                      </Group>
                    </Table.Td>
                  </Table.Tr>
                ))}
              </Table.Tbody>
            </Table>
          </Paper>
        </Grid.Col>

        {/* Automatic Transactions Management Section */}
        <Grid.Col span={{ base: 12, md: 6 }}>
          <Paper withBorder p="md" radius="md" shadow="sm">
            <Title order={2} mb="lg" ta="center">
              Automatic Transactions
            </Title>

            {/* Add New Automatic Transaction Form */}
            <Group justify="center" mb="lg">
              <Input.Wrapper label="Merchant Name" style={{ width: '45%' }}>
                <Input
                  value={addMerchantName}
                  onChange={(e) => setAddMerchantName(e.target.value)}
                  placeholder="Enter merchant name"
                />
              </Input.Wrapper>
              <Input.Wrapper label="Pot Name" style={{ width: '25%' }}>
                <Select
                  value={addMerchantPotId}
                  onChange={setAddMerchantPotId}
                  placeholder="Pick pot"
                  data={potOptions}
                  comboboxProps={{
                    transitionProps: { transition: 'pop', duration: 200 }
                  }}
                />
              </Input.Wrapper>
              <Button
                onClick={handleAddAutomaticTransaction}
                disabled={!addMerchantName || !addMerchantPotId}
                mt="xl"
              >
                Add
              </Button>
            </Group>

            {/* Automatic Transactions Table */}
            <Table striped withTableBorder>
              <Table.Thead>
                <Table.Tr>
                  <Table.Th>Merchant Name</Table.Th>
                  <Table.Th w="30%">Pot</Table.Th>
                  <Table.Th ta="right" w="25%">Actions</Table.Th>
                </Table.Tr>
              </Table.Thead>
              <Table.Tbody>
                {automaticTransactionData.map((transaction, index) => (
                  <Table.Tr key={transaction.id}>
                    <Table.Td>
                      <Input
                        value={transaction.merchantName}
                        onChange={(e) => updateTransactionField(index, 'merchantName', e.target.value)}
                      />
                    </Table.Td>
                    <Table.Td>
                      <Select
                        value={transaction.potId}
                        onChange={(value) => updateTransactionField(index, 'potId', value || '')}
                        data={potOptions}
                        comboboxProps={{
                          transitionProps: { transition: 'pop', duration: 200 }
                        }}
                      />
                    </Table.Td>
                    <Table.Td ta="right">
                      <Group gap="xs" justify="flex-end">
                        <Button
                          variant="filled"
                          color="green"
                          size="xs"
                          onClick={() => handleSaveAutomaticTransaction(transaction)}
                        >
                          Save
                        </Button>
                        <Button
                          variant="filled"
                          color="red"
                          size="xs"
                          onClick={() => handleDeleteAutomaticTransaction(transaction.id)}
                        >
                          Delete
                        </Button>
                      </Group>
                    </Table.Td>
                  </Table.Tr>
                ))}
              </Table.Tbody>
            </Table>
          </Paper>
        </Grid.Col>
      </Grid>

      {/* Delete Confirmation Modal */}
      <Modal
        opened={showDeleteConfirmation}
        onClose={() => setShowDeleteConfirmation(false)}
        title={`Delete ${deleteType === 'pot' ? 'Pot' : 'Automatic Transaction'}`}
        centered
      >
        <Stack gap="md">
          <Text>
            Are you sure you want to delete this {deleteType === 'pot' ? 'pot' : 'automatic transaction'}?
            This action cannot be undone.
          </Text>
          <Group justify="center">
            <Button color="red" onClick={confirmDelete}>
              Yes, Delete
            </Button>
            <Button variant="outline" onClick={() => setShowDeleteConfirmation(false)}>
              Cancel
            </Button>
          </Group>
        </Stack>
      </Modal>
    </Container>
  )
}
