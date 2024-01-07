import { Button, Divider, Grid, Group, Input, Modal } from '@mantine/core'
import classes from '../styles/AddNewMonthModal.module.css'

export interface AddNewMonthModalProps {
  displayModal: boolean
  hideModal: () => void
}

const AddNewMonthModal = (props: AddNewMonthModalProps): JSX.Element => {
  return (
    <>
      <Modal opened={props.displayModal} onClose={() => { props.hideModal() }} closeOnClickOutside={false} title="Add Month" classNames={{ title: classes.modalTitle }}>
        <Input.Wrapper label="Income This Month" className={classes.inputWrapper}>
          <Input onChange={(e) => { console.log('lol') }} />
        </Input.Wrapper>
        <h4>Pot Breakdown</h4>
        <Grid>
          <Grid.Col span={4}>
            <h5>Savings</h5>
            <p>£500</p>
          </Grid.Col>
          <Grid.Col span={4}>
            <h5>Savings</h5>
            <p>£500</p>
          </Grid.Col>
          <Grid.Col span={4}>
            <h5>Savings</h5>
            <p>£500</p>
          </Grid.Col>
          <Grid.Col span={4}>
            <h5>Savings</h5>
            <p>£500</p>
          </Grid.Col>
          <Grid.Col span={4}>
            <h5>Savings</h5>
            <p>£500</p>
          </Grid.Col>
          <Grid.Col span={4}>
            <h5>Savings</h5>
            <p>£500</p>
          </Grid.Col>
          <Grid.Col span={4}>
            <h5>Savings</h5>
            <p>£500</p>
          </Grid.Col>
        </Grid>
        <Divider style={{ paddingBottom: 20 }} />
        <h5>Spare Money</h5>
        <p>£500</p>
        <Group justify='center'>
          <Button color='green'>Add Month</Button>
          <Button color='red' onClick={() => { props.hideModal() }}>Cancel</Button>
        </Group>

      </Modal>
    </>
  )
}

export default AddNewMonthModal
