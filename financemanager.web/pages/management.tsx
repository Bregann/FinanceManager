import { Button, Checkbox, Container, Grid, Group, Input, Table } from '@mantine/core'
import classes from '../styles/Management.module.css'

const AutomaticTransactions = (): JSX.Element => {
  return (
    <>
      <h1>Pot/Automatic Transactions Management</h1>
      <Container size="90%">
        <Grid>
          <Grid.Col span={{ base: 12, md: 6 }}>
            <div className={classes.box}>
              <h2>Pots</h2>
              <Group>
              <Input.Wrapper label="Name" className={classes.inputWrapper} style={{ width: '50%' }}>
                <Input placeholder="Input inside Input.Wrapper"/>
              </Input.Wrapper>
              <Input.Wrapper label="Amount" className={classes.inputWrapper} style={{ width: '10%' }}>
                <Input placeholder="Input inside Input.Wrapper"/>
              </Input.Wrapper>
              <Checkbox
                className={classes.checkBoxButton}
                defaultChecked
                label="Savings pot?"
              />
              <Button className={classes.checkBoxButton}>Add</Button>
              </Group>

              <Table>

              </Table>
            </div>
          </Grid.Col>
          <Grid.Col span={{ base: 12, md: 6 }}>
            <h2>Automatic Transactions</h2>
            <div className={classes.box}>

            </div>
          </Grid.Col>
        </Grid>
      </Container>
    </>
  )
}

export default AutomaticTransactions
