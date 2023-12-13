import { Container, Grid } from '@mantine/core'
import classes from '../styles/Home.module.css'
import TransactionsTable from '@/components/TransactionsTable'
import { type GetServerSideProps } from 'next/types'
import backendFetchHelper from '@/helpers/BackendFetchHelper'
import type DropdownOptions from '@/types/DropdownOptions'

interface PageProps {
  homeData: HomeTopStats
  potDropdownOptions: DropdownOptions
}

interface HomeTopStats {
  moneyIn: string
  moneySpent: string
  moneyLeft: string
  totalSavings: string
}

export default function Home (props: PageProps): JSX.Element {
  return (
    <>
      <Container size='lg'>
        <Grid gutter={5}>
          <Grid.Col span="auto">
            <div className={classes.box}>
              <h3 className={classes.header}>Money In</h3>
              <p className={classes.amount}>£100</p>
            </div>
          </Grid.Col>
          <Grid.Col span="auto">
            <div className={classes.box}>
              <h3 className={classes.header}>Money Spent</h3>
              <p className={classes.amount}>£100</p>
            </div>
          </Grid.Col>
          <Grid.Col span="auto">
            <div className={classes.box}>
              <h3 className={classes.header}>Money Left</h3>
              <p className={classes.amount}>£100</p>
            </div>
          </Grid.Col>
          <Grid.Col span="auto">
            <div className={classes.box}>
              <h3 className={classes.header}>Total in Savings</h3>
              <p className={classes.amount}>£100</p>
            </div>
          </Grid.Col>
        </Grid>

        <h1>Home</h1>
        <TransactionsTable />
      </Container>
    </>
  )
}

export const getServerSideProps: GetServerSideProps<PageProps> = async () => {
  const unprocessedTransactionsFetchResponse = await backendFetchHelper.doGet('/api/Transactions/GetUnprocessedTransactionsFromDatabase')
  const potValuesFetchResponse = await backendFetchHelper.doGet('/api/Pots/GetPotDropdownValues')
  const homeDataStatsFetchResponse = await backendFetchHelper.doGet('/api/Home/GetHomeData')
  return {
    props: {

    }
  }
}
