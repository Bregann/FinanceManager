import { type ComboboxItem, Container, Grid } from '@mantine/core'
import classes from '../styles/Home.module.css'
import TransactionsTable, { type TransactionTableData } from '@/components/TransactionsTable'
import { type GetServerSideProps } from 'next/types'
import backendFetchHelper from '@/helpers/BackendFetchHelper'

interface PageProps {
  homeData: HomeTopStats
  potDropdownOptions: ComboboxItem[]
  transactionData: TransactionTableData[]
}

interface HomeTopStats {
  moneyIn: string
  moneySpent: string
  moneyLeft: string
  totalSavings: string
}

export default function Home (props: PageProps) {
  return (
    <>
      <Container size='lg'>
        <Grid gutter={5}>
          <Grid.Col span="auto">
            <div className={classes.box}>
              <h3 className={classes.header}>Money In</h3>
              <p className={classes.amount}>{props.homeData.moneyIn}</p>
            </div>
          </Grid.Col>
          <Grid.Col span="auto">
            <div className={classes.box}>
              <h3 className={classes.header}>Money Spent</h3>
              <p className={classes.amount}>{props.homeData.moneySpent}</p>
            </div>
          </Grid.Col>
          <Grid.Col span="auto">
            <div className={classes.box}>
              <h3 className={classes.header}>Money Left</h3>
              <p className={classes.amount}>{props.homeData.moneyLeft}</p>
            </div>
          </Grid.Col>
          <Grid.Col span="auto">
            <div className={classes.box}>
              <h3 className={classes.header}>Total in Savings</h3>
              <p className={classes.amount}>{props.homeData.totalSavings}</p>
            </div>
          </Grid.Col>
        </Grid>

        <h1>Home</h1>
        <TransactionsTable
          rows={props.transactionData}
          dropdownValues={props.potDropdownOptions}
          removeRow={true} />
      </Container>
    </>
  )
}

export const getServerSideProps: GetServerSideProps<PageProps> = async () => {
  const unprocessedTransactionsFetchResponse = await backendFetchHelper.doGet('/Transactions/GetUnprocessedTransactionsFromDatabase')
  const potValuesFetchResponse = await backendFetchHelper.doGet('/Pots/GetPotDropdownValues')
  const homeDataStatsFetchResponse = await backendFetchHelper.doGet('/Home/GetHomeData')

  const pageProps: PageProps = {
    homeData: homeDataStatsFetchResponse.data,
    potDropdownOptions: potValuesFetchResponse.data,
    transactionData: unprocessedTransactionsFetchResponse.data
  }

  return {
    props: pageProps
  }
}
