import { Button, Checkbox, type ComboboxItem, Container, Grid, Group, Input, Select, Table } from '@mantine/core'
import classes from '../styles/Management.module.css'
import backendFetchHelper from '@/helpers/BackendFetchHelper'
import { type GetServerSideProps } from 'next'
import { useState } from 'react'
import fetchHelper from '@/helpers/FetchHelper'
import notificationHelper from '@/helpers/NotificationHelper'
import { IconCircleCheck, IconCircleX } from '@tabler/icons-react'
import { type BoolReason } from '@/types/Shared/BoolReason'
import { type AddNewPotResponse } from './api/pots/AddPot'

interface PageProps {
  potDropdownOptions: ComboboxItem[]
  potData: PotList[]
  automaticTransactionData: AutomaticTransactionList[]
}

export interface PotList {
  potId: number
  potName: string
  potAmount: number
  isSavingsPot: boolean
}

export interface AutomaticTransactionList {
  id: number
  merchantName: string
  potId: string
}

const Page = (props: PageProps): JSX.Element => {
  const [potData, setPotData] = useState(props.potData)
  const [addPotName, setAddPotName] = useState('')
  const [addPotAmount, setAddPotAmount] = useState(0)
  const [addPotIsSavings, setAddPotIsSavings] = useState(false)
  const [addButtonDisabled, setAddButtonDisabled] = useState(false)

  const updatePotDataField = (value: string, index: number, fieldName: string): void => {
    const newArr = [...potData]
    const parsedNum = Number(value)

    switch (fieldName) {
      case 'name':
        newArr[index].potName = value
        break
      case 'amount':
        if (!isNaN(parsedNum)) {
          newArr[index].potAmount = parsedNum
        }
        break
      default:
        break
    }

    setPotData(newArr)
  }

  const savePotChange = async (pot: PotList): Promise<void> => {
    const potPostData = {
      potId: pot.potId,
      potName: pot.potName,
      potAmount: pot.potAmount
    }
    const fetchResult = await fetchHelper.doPost('/pots/UpdatePot', potPostData)

    if (fetchResult.errored) {
      notificationHelper.showErrorNotification('Error Submitting Request', 'There has been an error submitting the request. Please try again', 5000, <IconCircleX />)
      return
    }

    const data: BoolReason = fetchResult.data

    if (!data.success) {
      notificationHelper.showErrorNotification('Error Updating', data.reason, 5000, <IconCircleX />)
    } else {
      notificationHelper.showSuccessNotification('Update Successful', 'The pot has been updated successfully', 3000, <IconCircleCheck />)
    }
  }

  const addNewPot = async (): Promise<void> => {
    setAddButtonDisabled(true)

    if (addPotName === '' || addPotAmount === 0) {
      notificationHelper.showErrorNotification('Error', 'The pot name or pot amount is missing', 5000, <IconCircleX />)
      return
    }

    const data = {
      potName: addPotName,
      amount: addPotAmount,
      isSavingsPot: addPotIsSavings
    }

    const fetchResult = await fetchHelper.doPost('/pots/AddPot', data)

    if (fetchResult.errored) {
      notificationHelper.showErrorNotification('Error Submitting Request', 'There has been an error submitting the request. Please try again', 5000, <IconCircleX />)
      return
    }

    const responseData: AddNewPotResponse = fetchResult.data

    if (!responseData.success) {
      notificationHelper.showErrorNotification('Error Adding', responseData.reason, 5000, <IconCircleX />)
    } else {
      setPotData(potData => [...potData, {
        potId: responseData.potId ?? -1, // this will always have a pot id number so if -1 is being hit then there is some sort of issue with the api
        potName: addPotName,
        potAmount: addPotAmount,
        isSavingsPot: addPotIsSavings
      }])
      notificationHelper.showSuccessNotification('Add Pot Successful', 'The pot has been added successfully', 3000, <IconCircleCheck />)
      setAddPotName('')
      setAddPotAmount(0)
      setAddPotIsSavings(false)
      setAddButtonDisabled(false)
    }
  }

  const deletePot = async (potId: number): Promise<void> => {
    const fetchResult = await fetchHelper.doDelete(`/pots/DeletePot/${potId}`)

    if (fetchResult.errored || fetchResult.data === false) {
      notificationHelper.showErrorNotification('Error', 'There has been an error deleting the pot. Please try again', 5000, <IconCircleX />)
    } else {
      setPotData(potData.filter(x => x.potId !== potId))
      notificationHelper.showSuccessNotification('Delete Pot Successful', 'The pot has been deleted successfully', 3000, <IconCircleCheck />)
    }
  }

  return (
    <>
      <h1>Pot/Automatic Transactions Management</h1>
      <Container size="90%">
        <Grid>
          <Grid.Col span={{ base: 12, md: 6 }}>
            <div className={classes.box}>
              <h2>Pots</h2>
              <Group justify='center'>
                <Input.Wrapper label="Name" className={classes.inputWrapper} style={{ width: '50%' }}>
                  <Input defaultValue={addPotName} onChange={(e) => { setAddPotName(e.target.value) }}/>
                </Input.Wrapper>
                <Input.Wrapper label="Amount" className={classes.inputWrapper} style={{ width: '10%' }}>
                  <Input defaultValue={addPotAmount} onChange={(e) => { setAddPotAmount(Number(e.target.value)) }}/>
                </Input.Wrapper>
                <Checkbox
                  className={classes.checkBoxButton}
                  checked={addPotIsSavings}
                  onChange={(e) => { setAddPotIsSavings(e.currentTarget.checked) }}
                  label="Savings pot?"
                />
                <Button className={classes.checkBoxButton} disabled={addButtonDisabled} onClick={async () => { await addNewPot() }}>Add</Button>
              </Group>

              <Table className={classes.table} striped withTableBorder>
                <Table.Thead>
                  <Table.Tr>
                    <Table.Th>Pot Name</Table.Th>
                    <Table.Th>Amount (&pound;)</Table.Th>
                    <Table.Th>Savings?</Table.Th>
                    <Table.Th style={{ textAlign: 'right', width: '25%' }}>Actions</Table.Th>
                  </Table.Tr>
                </Table.Thead>
                <Table.Tbody>
                  {potData.map((pot, index) => {
                    return (
                      <Table.Tr key={pot.potId}>
                        <Table.Td>
                          <Input defaultValue={pot.potName} onChange={(e) => { updatePotDataField(e.target.value, index, 'name') }}/>
                        </Table.Td>
                        <Table.Td width="15%">
                          <Input defaultValue={pot.potAmount} onChange={(e) => { updatePotDataField(e.target.value, index, 'amount') }}/>
                        </Table.Td>
                        <Table.Td>
                          <Checkbox style={{ paddingLeft: 15 }} color="blue" checked={pot.isSavingsPot}/>
                        </Table.Td>
                        <Table.Td style={{ textAlign: 'right' }}>
                          <Button variant='filled' color='green' className={classes.actionButton} onClick={async () => { await savePotChange(pot) }}>Save</Button>
                          <Button variant='filled' color='red' onClick={async () => { await deletePot(pot.potId) }}>Delete</Button>
                        </Table.Td>
                      </Table.Tr>
                    )
                  })}
                </Table.Tbody>
              </Table>
            </div>
          </Grid.Col>
          <Grid.Col span={{ base: 12, md: 6 }}>
            <div className={classes.box}>
              <h2>Automatic Transactions</h2>
              <Group justify='center'>
                <Input.Wrapper label="Merchant Name" className={classes.inputWrapper} style={{ width: '50%' }}>
                  <Input placeholder="Input inside Input.Wrapper" />
                </Input.Wrapper>
                <Input.Wrapper label="Pot Name" className={classes.inputWrapper} style={{ width: '20%' }}>
                  <Select
                    placeholder='Pick value'
                    data={['Savings', 'None']}
                    comboboxProps={{ transitionProps: { transition: 'pop', duration: 200 } }}
                  />
                </Input.Wrapper>
                <Button className={classes.automaticTransactionsButton}>Add</Button>
              </Group>
              <Table className={classes.table} striped withTableBorder>
                <Table.Thead>
                  <Table.Tr>
                    <Table.Th>Merchant Name</Table.Th>
                    <Table.Th style={{ width: '30%' }}>Pot</Table.Th>
                    <Table.Th style={{ textAlign: 'right', width: '25%' }}>Actions</Table.Th>
                  </Table.Tr>
                </Table.Thead>
                <Table.Tbody>
                  <Table.Tr>
                    <Table.Td>
                      <Input defaultValue="Tilly" />
                    </Table.Td>
                    <Table.Td>
                      <Select
                        placeholder='Pick value'
                        data={['Savings', 'None']}
                        comboboxProps={{ transitionProps: { transition: 'pop', duration: 200 } }}
                      />
                    </Table.Td>
                    <Table.Td style={{ textAlign: 'right' }}>
                      <Button variant='filled' color='green' className={classes.actionButton}>Save</Button>
                      <Button variant='filled' color='red'>Delete</Button>
                    </Table.Td>
                  </Table.Tr>
                  <Table.Tr>
                    <Table.Td>
                      <Input defaultValue="Tilly" />
                    </Table.Td>
                    <Table.Td>
                      <Select
                        placeholder='Pick value'
                        data={['Savings', 'None']}
                        comboboxProps={{ transitionProps: { transition: 'pop', duration: 200 } }}
                      />
                    </Table.Td>
                    <Table.Td style={{ textAlign: 'right' }}>
                      <Button variant='filled' color='green' className={classes.actionButton}>Save</Button>
                      <Button variant='filled' color='red'>Delete</Button>
                    </Table.Td>
                  </Table.Tr>
                  <Table.Tr>
                    <Table.Td>
                      <Input defaultValue="Tilly" />
                    </Table.Td>
                    <Table.Td>
                      <Select
                        placeholder='Pick value'
                        data={['Savings', 'None']}
                        comboboxProps={{ transitionProps: { transition: 'pop', duration: 200 } }}
                      />
                    </Table.Td>
                    <Table.Td style={{ textAlign: 'right' }}>
                      <Button variant='filled' color='green' className={classes.actionButton}>Save</Button>
                      <Button variant='filled' color='red'>Delete</Button>
                    </Table.Td>
                  </Table.Tr>
                  <Table.Tr>
                    <Table.Td>
                      <Input defaultValue="Tilly" />
                    </Table.Td>
                    <Table.Td>
                      <Select
                        placeholder='Pick value'
                        data={['Savings', 'None']}
                        comboboxProps={{ transitionProps: { transition: 'pop', duration: 200 } }}
                      />
                    </Table.Td>
                    <Table.Td style={{ textAlign: 'right' }}>
                      <Button variant='filled' color='green' className={classes.actionButton}>Save</Button>
                      <Button variant='filled' color='red'>Delete</Button>
                    </Table.Td>
                  </Table.Tr>
                </Table.Tbody>
              </Table>
            </div>
          </Grid.Col>
        </Grid>
      </Container>
    </>
  )
}

export const getServerSideProps: GetServerSideProps<PageProps> = async (ctx) => {
  const potValuesFetchResponse = await backendFetchHelper.doGet('/Pots/GetPotDropdownValues')
  const potsListFetchResponse = await backendFetchHelper.doGet('/Pots/GetPotList')
  const automaticTransactionListFetchResponse = await backendFetchHelper.doGet('/AutomaticTransactions/GetAutomaticTransactions')

  const pageProps: PageProps = {
    potDropdownOptions: potValuesFetchResponse.data,
    potData: potsListFetchResponse.data,
    automaticTransactionData: automaticTransactionListFetchResponse.data
  }

  return {
    props: pageProps
  }
}

export default Page
