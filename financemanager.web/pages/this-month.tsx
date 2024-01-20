import { type ComboboxItem, Container, Grid, Tooltip, Input, Modal, InputLabel, Button, Group } from '@mantine/core'
import classes from '../styles/ThisMonth.module.css'
import TransactionsTable, { type TransactionTableData } from '@/components/TransactionsTable'
import { type GetServerSideProps } from 'next'
import backendFetchHelper from '@/helpers/BackendFetchHelper'
import { IconCircleX, IconPencil } from '@tabler/icons-react'
import { useState } from 'react'
import fetchHelper from '@/helpers/FetchHelper'
import notificationHelper from '@/helpers/NotificationHelper'
import { type UpdateSavingsPotDto } from './api/pots/UpdateSavingsPot'

interface PageProps {
  potDropdownOptions: ComboboxItem[]
  transactionData: TransactionTableData[]
  potsStatsData: PotsStats[]
}

interface PotsStats {
  potId: number
  potName: string
  isSavingsPot: boolean
  amountAllocated: string
  amountLeft: number
  amountSpent: string
}

const ThisMonth = (props: PageProps): JSX.Element => {
  const [openEditSavingsModal, setOpenEditSavingsModal] = useState(false)
  const [currentEditedSavingsPot, setCurrentEditedSavingsPot] = useState<PotsStats | undefined>(undefined)
  const [potsStatsData, setPotsStatsData] = useState(props.potsStatsData)

  const updateSavingsPot = async (): Promise<void> => {
    if (currentEditedSavingsPot !== undefined) {
      const data = {
        potId: currentEditedSavingsPot.potId,
        savingsAmount: currentEditedSavingsPot.amountLeft
      }

      const fetchResult = await fetchHelper.doPost('/pots/UpdateSavingsPot', data)

      if (fetchResult.errored) {
        notificationHelper.showErrorNotification('Error', 'There has been an error with trying to update the savings', 5000, <IconCircleX />)
      } else {
        const data: UpdateSavingsPotDto = fetchResult.data

        if (!data.success) {
          notificationHelper.showErrorNotification('Error', data.reason, 5000, <IconCircleX />)
        } else {
          setPotsStatsData(pots => pots.map((pot) => {
            if (pot.potId === currentEditedSavingsPot.potId) {
              return { ...pot, amountLeft: data.amount }
            } else {
              return pot
            }
          }))
        }
      }
    }
  }

  const onPotAmountChanged = (amount: string): void => {
    const convertedNumber = Number.parseFloat(amount)

    if (!isNaN(convertedNumber) && currentEditedSavingsPot !== undefined) {
      setCurrentEditedSavingsPot({ ...currentEditedSavingsPot, amountLeft: convertedNumber })
    }
  }

  return (
    <>
      <h1>This Month</h1>
      <Container size='xl'>
        <Grid gutter={5}>
          {potsStatsData.filter(x => !x.isSavingsPot).map((pot) => {
            return (
              <Grid.Col className={classes.potsCol} span={potsStatsData.filter(x => !x.isSavingsPot).length > 3 ? 'auto' : 3} key={pot.potId}>
                <div className={classes.box}>
                  <h3 className={classes.header}>{pot.potName}</h3>
                  <p className={classes.amount}>Allocated: {pot.amountAllocated}</p>
                  <p className={classes.amount}>Amount left: &pound;{pot.amountLeft.toFixed(2)}</p>
                  <p className={classes.amount}>Spent: {pot.amountSpent}</p>
                </div>
              </Grid.Col>
            )
          })}
        </Grid>
      </Container>

      <h2>Savings Breakdown</h2>
      <Container size='lg'>
        <Grid gutter="lg" justify="center">
          {potsStatsData.filter(x => x.isSavingsPot).map((pot) => {
            return (
              <Grid.Col span="content" key={pot.potId}>
                <div className={classes.savingsBox}>
                  <h3 className={classes.header}>{pot.potName}</h3>
                  <p className={classes.amount}>&pound;{pot.amountLeft.toFixed(2)}</p>
                </div>
                <Tooltip label="Edit savings amount">
                  <IconPencil className={classes.savingsPencilEdit} onClick={() => { setOpenEditSavingsModal(true); setCurrentEditedSavingsPot(pot) }}/>
                </Tooltip>
              </Grid.Col>
            )
          })}
        </Grid>
      </Container>

      <h2>Transactions This Month</h2>
      <Container size='xl'>
        <TransactionsTable removeRow={false} rows={props.transactionData} dropdownValues={props.potDropdownOptions} />
      </Container>

      <h2>Stats Breakdown</h2>
      <Container size='xl'>
        <Grid>
          <Grid.Col span={{ base: 12, md: 4 }}>
            <div className={classes.statsBox}>
              <h4>Spent Per Pot</h4>
            </div>
          </Grid.Col>
          <Grid.Col span={{ base: 12, md: 4 }}>
            <div className={classes.statsBox}>
              <h4>Top Places Money Spent</h4>
            </div>
          </Grid.Col>
          <Grid.Col span={{ base: 12, md: 4 }}>
            <div className={classes.statsBox}>
              <h4>Most Expensive Purchases</h4>
            </div>
          </Grid.Col>
        </Grid>
        <Grid style={{ marginTop: 30 }}>
          <Grid.Col span={{ base: 12, md: 4 }}>
            <div className={classes.statsBox}>
              <h4>Spent Per Pot</h4>
            </div>
          </Grid.Col>
          <Grid.Col span={{ base: 12, md: 4 }}>
            <div className={classes.statsBox}>
              <h4>Top Places Money Spent</h4>
            </div>
          </Grid.Col>
          <Grid.Col span={{ base: 12, md: 4 }}>
            <div className={classes.statsBox}>
              <h4>Most Expensive Purchases</h4>
            </div>
          </Grid.Col>
        </Grid>

        <div className={classes.statsBoxBig}>
          <h4>Daily Money Spent</h4>
        </div>
      </Container>
      <br />

      <Modal opened={openEditSavingsModal} onClose={() => { setOpenEditSavingsModal(false) }} withCloseButton size='xs' title={`Editing ${currentEditedSavingsPot?.potName}`}>
        <InputLabel>Savings Amount</InputLabel>
        <Input defaultValue={currentEditedSavingsPot?.amountLeft} onChange={(e) => { onPotAmountChanged(e.target.value) }}/>
        <Group justify='center' style={{ paddingTop: 20 }}>
          <Button color='green' onClick={async () => { await updateSavingsPot() }}>Save</Button>
          <Button color='red' onClick={() => { setOpenEditSavingsModal(false) }}>Cancel</Button>
        </Group>
      </Modal>
    </>
  )
}

export const getServerSideProps: GetServerSideProps<PageProps> = async () => {
  const potValuesFetchResponse = await backendFetchHelper.doGet('/Pots/GetPotDropdownValues')
  const transactionsFetchResponse = await backendFetchHelper.doGet('/Transactions/GetProcessedTransactionsForMonth')
  const potsStatsResponse = await backendFetchHelper.doGet('/Pots/GetPotsStats')

  const pageProps: PageProps = {
    potDropdownOptions: potValuesFetchResponse.data,
    transactionData: transactionsFetchResponse.data,
    potsStatsData: potsStatsResponse.data
  }

  return {
    props: pageProps
  }
}

export default ThisMonth
