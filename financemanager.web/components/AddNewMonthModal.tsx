'use client'

import {
  Modal,
  Button,
  Group,
  Grid,
  Text,
  Title,
  Divider,
  Stack,
  Paper,
  Checkbox,
  NumberInput
} from '@mantine/core'
import { useState, useEffect } from 'react'

export interface AddNewMonthModalProps {
  displayModal: boolean
  hideModal: () => void
}

interface PotData {
  potId: number
  potName: string
  potAmount: number
  isSavingsPot: boolean
  rolloverAmount?: number
  hasRollover?: boolean
}

const AddNewMonthModal = (props: AddNewMonthModalProps) => {
  // Dummy pot data
  const [potList, setPotList] = useState<PotData[]>([])
  const [totalPotAmount, setTotalPotAmount] = useState(0)
  const [incomeForMonth, setIncomeForMonth] = useState<number | string>('')
  const [potRollovers, setPotRollovers] = useState<{ [key: number]: boolean }>({})

  // Initialize dummy data when modal opens
  useEffect(() => {
    if (props.displayModal) {
      const dummyPots: PotData[] = [
        {
          potId: 1,
          potName: 'Groceries',
          potAmount: 400,
          isSavingsPot: false,
          rolloverAmount: 25.30,
          hasRollover: true
        },
        {
          potId: 2,
          potName: 'Transport',
          potAmount: 200,
          isSavingsPot: false,
          rolloverAmount: 15.75,
          hasRollover: true
        },
        {
          potId: 3,
          potName: 'Entertainment',
          potAmount: 150,
          isSavingsPot: false,
          rolloverAmount: 8.45,
          hasRollover: true
        },
        {
          potId: 4,
          potName: 'Emergency Fund',
          potAmount: 500,
          isSavingsPot: true,
          rolloverAmount: 0,
          hasRollover: false
        },
        {
          potId: 5,
          potName: 'Holiday Fund',
          potAmount: 300,
          isSavingsPot: true,
          rolloverAmount: 0,
          hasRollover: false
        },
        {
          potId: 6,
          potName: 'Bills',
          potAmount: 800,
          isSavingsPot: false,
          rolloverAmount: 12.20,
          hasRollover: true
        }
      ]

      const totalAmount = dummyPots.reduce((sum, pot) => sum + pot.potAmount, 0)
      setPotList(dummyPots)
      setTotalPotAmount(totalAmount)

      // Initialize rollover states
      const initialRollovers: { [key: number]: boolean } = {}
      dummyPots.forEach(pot => {
        initialRollovers[pot.potId] = false
      })
      setPotRollovers(initialRollovers)
    } else {
      // Reset form when modal closes
      setPotList([])
      setTotalPotAmount(0)
      setIncomeForMonth('')
      setPotRollovers({})
    }
  }, [props.displayModal])

  const togglePotRollover = (potId: number) => {
    setPotRollovers(prev => ({
      ...prev,
      [potId]: !prev[potId]
    }))
  }

  const calculateTotalRollover = (): number => {
    return potList.reduce((total, pot) => {
      if (potRollovers[pot.potId] && pot.rolloverAmount) {
        return total + pot.rolloverAmount
      }
      return total
    }, 0)
  }

  const calculateSparedMoney = (): number => {
    const income = typeof incomeForMonth === 'number' ? incomeForMonth : 0
    const totalRollover = calculateTotalRollover()
    return (income + totalRollover) - totalPotAmount
  }

  const handleAddMonth = async () => {
    const totalRollover = calculateTotalRollover()
    const monthData = {
      income: incomeForMonth,
      totalPotAmount,
      potRollovers,
      totalRolloverAmount: totalRollover,
      sparedMoney: calculateSparedMoney()
    }

    console.log('Adding new month with data:', monthData)

    // Simulate API call
    setTimeout(() => {
      console.log('Month added successfully')
      props.hideModal()
    }, 500)
  }

  const isValidInput = typeof incomeForMonth === 'number' && incomeForMonth > 0

  return (
    <Modal
      opened={props.displayModal}
      onClose={props.hideModal}
      title={<Title order={3}>Add New Month</Title>}
      size="lg"
      centered
      closeOnClickOutside={false}
    >
      <Stack gap="md">
        {/* Income Input */}
        <NumberInput
          label="Income This Month"
          placeholder="Enter your income"
          value={incomeForMonth}
          onChange={setIncomeForMonth}
          min={0}
          decimalScale={2}
          fixedDecimalScale
          thousandSeparator=","
          prefix="£"
          size="md"
        />

        {/* Spending Pots */}
        <div>
          <Title order={4} mb="sm">Spending Pots</Title>
          <Grid gutter="sm">
            {potList.filter(pot => !pot.isSavingsPot).map((pot) => (
              <Grid.Col span={6} key={pot.potId}>
                <Paper withBorder p="md" radius="md" ta="center">
                  <Stack gap="xs">
                    <div>
                      <Text fw={500} size="md">{pot.potName}</Text>
                      <Text size="lg" fw={700} c="dark">
                        £{pot.potAmount}
                      </Text>
                    </div>

                    {pot.hasRollover && pot.rolloverAmount && pot.rolloverAmount > 0 && (
                      <div>
                        <Text size="sm" c="dimmed" mb="xs">
                          Rollover available: £{pot.rolloverAmount.toFixed(2)}
                        </Text>
                        <Checkbox
                          checked={potRollovers[pot.potId] || false}
                          onChange={() => togglePotRollover(pot.potId)}
                          label="Include rollover"
                          size="sm"
                        />
                      </div>
                    )}
                  </Stack>
                </Paper>
              </Grid.Col>
            ))}
          </Grid>
        </div>

        {/* Savings Pots */}
        <div>
          <Title order={4} mb="sm">Savings Pots</Title>
          <Grid gutter="sm">
            {potList.filter(pot => pot.isSavingsPot).map((pot) => (
              <Grid.Col span={6} key={pot.potId}>
                <Paper withBorder p="md" radius="md" ta="center">
                  <Text fw={500} size="sm" truncate>
                    {pot.potName}
                  </Text>
                  <Text size="lg" fw={700} c="blue">
                    £{pot.potAmount}
                  </Text>
                  <Text size="xs" c="blue">
                    Savings
                  </Text>
                </Paper>
              </Grid.Col>
            ))}
          </Grid>
        </div>

        <Divider />

        {/* Summary */}
        <Paper withBorder p="md" radius="md">
          <Stack gap="xs">
            <Group justify="space-between">
              <Text>Monthly Income:</Text>
              <Text fw={500}>
                {typeof incomeForMonth === 'number' ? `£${incomeForMonth.toFixed(2)}` : '£0.00'}
              </Text>
            </Group>

            {calculateTotalRollover() > 0 && (
              <Group justify="space-between">
                <Text>Total Rollover from selected pots:</Text>
                <Text fw={500} c="green">
                  £{calculateTotalRollover().toFixed(2)}
                </Text>
              </Group>
            )}

            <Group justify="space-between">
              <Text>Total Pot Allocations:</Text>
              <Text fw={500}>
                £{totalPotAmount.toFixed(2)}
              </Text>
            </Group>

            <Divider />

            <Group justify="space-between">
              <Text fw={600} size="lg">Spare Money:</Text>
              <Text
                fw={700}
                size="lg"
                c={calculateSparedMoney() >= 0 ? 'green' : 'red'}
              >
                {isValidInput || calculateTotalRollover() > 0
                  ? `£${calculateSparedMoney().toFixed(2)}`
                  : 'Invalid input'
                }
              </Text>
            </Group>

            {calculateSparedMoney() < 0 && isValidInput && (
              <Text size="sm" c="red" ta="center">
                Warning: You are allocating more money than you have available!
              </Text>
            )}
          </Stack>
        </Paper>

        {/* Action Buttons */}
        <Group justify="center" mt="md">
          <Button
            color="green"
            onClick={handleAddMonth}
            disabled={!isValidInput}
            size="md"
          >
            Add Month
          </Button>
          <Button
            color="red"
            variant="outline"
            onClick={props.hideModal}
            size="md"
          >
            Cancel
          </Button>
        </Group>
      </Stack>
    </Modal>
  )
}

export default AddNewMonthModal
