import { Container, Grid } from '@mantine/core'
import classes from '../styles/Home.module.css'

export default function Home (): JSX.Element {
  return (
    <>

      <Container size='xl'>
        <Grid>
          <Grid.Col span={{ base: 12, xl: 3, lg: 3, md: 6, sm: 6, xs: 6 }}>
            <div className={classes.box}>
              <h3 className={classes.header}>Money In</h3>
              <p className={classes.amount}>£100</p>
            </div>
          </Grid.Col>
          <Grid.Col span={{ base: 12, xl: 3, lg: 3, md: 6, sm: 6, xs: 6 }}>
            <div className={classes.box}>
              <h3 className={classes.header}>Money Spent</h3>
              <p className={classes.amount}>£100</p>
            </div>
          </Grid.Col>
          <Grid.Col span={{ base: 12, xl: 3, lg: 3, md: 6, sm: 6, xs: 6 }}>
            <div className={classes.box}>
              <h3 className={classes.header}>Money Left</h3>
              <p className={classes.amount}>£100</p>
            </div>
          </Grid.Col>
          <Grid.Col span={{ base: 12, xl: 3, lg: 3, md: 6, sm: 6, xs: 6 }}>
            <div className={classes.box}>
              <h3 className={classes.header}>Total in Savings</h3>
              <p className={classes.amount}>£100</p>
            </div>
          </Grid.Col>
        </Grid>

        <h1>Home</h1>
      </Container>
    </>
  )
}
