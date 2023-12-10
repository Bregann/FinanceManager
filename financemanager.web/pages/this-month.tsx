import { Container, Grid } from '@mantine/core'
import classes from '../styles/ThisMonth.module.css'
import TransactionsTable from '@/components/TransactionsTable'

const ThisMonth = (): JSX.Element => {
  return (
    <>
      <h1>This Month</h1>
      <Container size='xl'>
        <Grid gutter={5}>
          <Grid.Col span="auto">
            <div className={classes.box}>
              <h3 className={classes.header}>Pot 1</h3>
              <p className={classes.amount}>Allocated: £100</p>
              <p className={classes.amount}>Amount left: £100</p>
              <p className={classes.amount}>Spent: £100</p>
            </div>
          </Grid.Col>
          <Grid.Col span="auto">
            <div className={classes.box}>
              <h3 className={classes.header}>Pot 2</h3>
              <p className={classes.amount}>Allocated: £100</p>
              <p className={classes.amount}>Amount left: £100</p>
              <p className={classes.amount}>Spent: £100</p>
            </div>
          </Grid.Col>
          <Grid.Col span="auto">
            <div className={classes.box}>
              <h3 className={classes.header}>Pot 3</h3>
              <p className={classes.amount}>Allocated: £100</p>
              <p className={classes.amount}>Amount left: £100</p>
              <p className={classes.amount}>Spent: £100</p>
            </div>
          </Grid.Col>
          <Grid.Col span="auto">
            <div className={classes.box}>
              <h3 className={classes.header}>Pot 4</h3>
              <p className={classes.amount}>Allocated: £100</p>
              <p className={classes.amount}>Amount left: £100</p>
              <p className={classes.amount}>Spent: £100</p>
            </div>
          </Grid.Col>
          <Grid.Col span="auto">
            <div className={classes.box}>
              <h3 className={classes.header}>Pot 5</h3>
              <p className={classes.amount}>Allocated: £100</p>
              <p className={classes.amount}>Amount left: £100</p>
              <p className={classes.amount}>Spent: £100</p>
            </div>
          </Grid.Col>
        </Grid>
      </Container>

      <h2>Savings Breakdown</h2>
      <Container size='lg'>
        <Grid gutter="lg" justify="center">
          <Grid.Col span="content">
            <div className={classes.savingsBox}>
              <h3 className={classes.header}>Savings Pot 1</h3>
              <p className={classes.amount}>Amount: £100</p>
              <p className={classes.amount}>Change: £100</p>
            </div>
          </Grid.Col>
          <Grid.Col span="content">
            <div className={classes.savingsBox}>
              <h3 className={classes.header}>Savings Pot 2</h3>
              <p className={classes.amount}>Amount: £100</p>
              <p className={classes.amount}>Change: £100</p>
            </div>
          </Grid.Col>
          <Grid.Col span="content">
            <div className={classes.savingsBox}>
              <h3 className={classes.header}>Savings Pot 3</h3>
              <p className={classes.amount}>Amount: £100</p>
              <p className={classes.amount}>Change: £100</p>
            </div>
          </Grid.Col>
        </Grid>
      </Container>

      <h2>Transactions This Month</h2>
      <Container size='xl'>
        <TransactionsTable />
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

export default ThisMonth
