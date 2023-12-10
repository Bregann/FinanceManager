import { Container, Grid } from '@mantine/core'
import classes from '../styles/Home.module.css'
import TransactionsTable from '@/components/TransactionsTable'

export default function Home (): JSX.Element {
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
