import { type ComboboxItem, Container, Grid } from '@mantine/core'
import classes from '../styles/ThisMonth.module.css'
import TransactionsTable, { type TransactionTableData } from '@/components/TransactionsTable'
import { type GetServerSideProps } from 'next'
import backendFetchHelper from '@/helpers/BackendFetchHelper'

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
  amountLeft: string
  amountSpent: string
}

const ThisMonth = (props: PageProps): JSX.Element => {
  return (
    <>
      <h1>This Month</h1>
      <Container size='xl'>
        <Grid gutter={5}>
          {props.potsStatsData.filter(x => !x.isSavingsPot).map((pot) => {
            return (
              <Grid.Col className={classes.potsCol} span={props.potsStatsData.filter(x => !x.isSavingsPot).length > 3 ? 'auto' : 3} key={pot.potId}>
                <div className={classes.box}>
                  <h3 className={classes.header}>{pot.potName}</h3>
                  <p className={classes.amount}>Allocated: {pot.amountAllocated}</p>
                  <p className={classes.amount}>Amount left: {pot.amountLeft}</p>
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
          {props.potsStatsData.filter(x => x.isSavingsPot).map((pot) => {
            return (
              <Grid.Col span="content" key={pot.potId}>
                <div className={classes.savingsBox}>
                  <h3 className={classes.header}>{pot.potName}</h3>
                  <p className={classes.amount}>{pot.amountAllocated}</p>
                </div>
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
    </>
  )
}

export const getServerSideProps: GetServerSideProps<PageProps> = async (ctx) => {
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
