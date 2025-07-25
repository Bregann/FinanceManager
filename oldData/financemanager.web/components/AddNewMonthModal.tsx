import { Button, Divider, Grid, Group, Input, Modal } from '@mantine/core'
import classes from '../styles/AddNewMonthModal.module.css'
import { useEffect, useState } from 'react'
import fetchHelper from '@/helpers/FetchHelper'
import notificationHelper from '@/helpers/NotificationHelper'
import { IconCircleCheck, IconCircleX } from '@tabler/icons-react'
import { type PotList } from '@/pages/api/pots/GetPotList'

export interface AddNewMonthModalProps {
  displayModal: boolean
  hideModal: () => void
}

const AddNewMonthModal = (props: AddNewMonthModalProps) => {
  const [potList, setPotList] = useState<PotList[] | undefined>(undefined)
  const [totalPotAmount, setTotalPotAmount] = useState(0)
  const [incomeForMonth, setIncomeForMonth] = useState(0)

  useEffect(() => {
    if (props.displayModal) {
      void (async () => {
        const fetchResponse = await fetchHelper.doGet('/pots/GetPotList')

        if (fetchResponse.errored) {
          notificationHelper.showErrorNotification('Error', 'Error getting pots from database', 5000, <IconCircleX />)
        } else {
          const data: PotList[] = fetchResponse.data
          const summedPotAmounts = data.reduce((sum, { potAmount }) => sum + potAmount, 0)
          setTotalPotAmount(summedPotAmounts)
          setPotList(data)
        }
      })()
    } else {
      setPotList(undefined)
      setTotalPotAmount(0)
      setIncomeForMonth(0)
    }
  }, [props.displayModal])

  const addMonth = async (): Promise<void> => {
    const fetchResult = await fetchHelper.doPost('/newMonth/AddNewMonth', { income: incomeForMonth })

    if (fetchResult.errored || fetchResult.data === false) {
      notificationHelper.showErrorNotification('Error', 'Failed to add month', 5000, <IconCircleX />)
    } else {
      notificationHelper.showSuccessNotification('Success', 'Month added successfully', 2000, <IconCircleCheck />)
      setPotList(undefined)
      setTotalPotAmount(0)
      setIncomeForMonth(0)
      props.hideModal()
    }
  }

  return (
    <>
      <Modal opened={props.displayModal} onClose={() => { props.hideModal() }} closeOnClickOutside={false} title="Add Month" classNames={{ title: classes.modalTitle }}>
        <Input.Wrapper label="Income This Month" className={classes.inputWrapper}>
          <Input onChange={(e) => { setIncomeForMonth(Number.parseFloat(e.target.value)) }} />
        </Input.Wrapper>
        <h4 className={classes.h4}>Pot Breakdown</h4>
        <Grid>
          {potList?.map((pot) => {
            return (
              <Grid.Col span={4} key={pot.potId}>
              <h5>{pot.potName}</h5>
              <p>&pound;{pot.potAmount}</p>
            </Grid.Col>
            )
          })
        }
        </Grid>
        <Divider style={{ paddingBottom: 20 }} />
        <h5>Spare Money</h5>
        <p>{isNaN(incomeForMonth) ? 'Invalid input' : `£${(incomeForMonth - totalPotAmount).toFixed(2)}`}</p>
        <Group justify='center'>
          <Button color='green' onClick={async () => { await addMonth() }}>Add Month</Button>
          <Button color='red' onClick={() => { props.hideModal() }}>Cancel</Button>
        </Group>

      </Modal>
    </>
  )
}

export default AddNewMonthModal
