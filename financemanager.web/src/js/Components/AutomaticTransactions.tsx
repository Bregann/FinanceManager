import { Col, Container, Row, Table } from "react-bootstrap";
import { SubmitHandler, useForm } from "react-hook-form";
import Select from 'react-select';
import { DoDelete, DoPost, useFetchGET, UseFetchGETResult } from "../Helpers/fetchHelper";
import { useEffect, useState } from "react";
import '../../css/form.css';
import { toast } from "react-toastify";
import { apiUrl } from "../config";

type FormValues = {
    automaticTransactionName: string;
}

interface DropdownOption {
    label: string;
    value: number;
}

interface PotResponse {
    success: boolean;
    error: string | null;
}

interface AutomaticTransactionsTableData{
    automaticTransactions: AutomaticTransactionData[];
}

interface AutomaticTransactionData {
    name: string
    potName: string
  }

const AutomaticTransactions = () => {
    const {register, handleSubmit} = useForm<FormValues>();
    const selectDropdownData: UseFetchGETResult = useFetchGET('/api/Pots/GetPotValuesDropdown');
    const [selectedPotId, setSelectedPotId] = useState(0);
    const [automaticTransactionData, setAutomaticTransactionData] = useState<null | AutomaticTransactionsTableData>(null);
    const [getAutomaticTransactionsDataError, setGetAutomaticTransactionsDataError] = useState<null | string>(null);
    const [getAutomaticTransactionsDataIsPending, setGetAutomaticTransactionsDataIsPending] = useState(true);

    useEffect(() => {
        getAutomaticTransactionDataForTable();
    }, []);

    const getAutomaticTransactionDataForTable = () => {
        fetch(apiUrl + '/api/AutomaticTransactions/GetAutomaticTransactions').then(res => {
            //Not found returned when there are no automatic transactions added
            if(res.status === 404){
                throw Error("No automatic transactions found");
            }
            else if(!res.ok){
                throw Error(res.status + ' ' + res.statusText);
            }
            
            return res.json();
        }).then((res: AutomaticTransactionsTableData) => {
            setAutomaticTransactionData(res);
            setGetAutomaticTransactionsDataIsPending(false);
            setGetAutomaticTransactionsDataError(null);
        }).catch(err => {
            setGetAutomaticTransactionsDataError(err.message);
            setGetAutomaticTransactionsDataIsPending(false);
        });
    }

    const onSubmit: SubmitHandler<FormValues> = data => {
        DoPost('/api/AutomaticTransactions/AddNewAutomaticTransaction', { transactionName: data.automaticTransactionName, potId: selectedPotId }).then((res: PotResponse) => {
            if(res.success === true){
                toast.success('Automatic transaction has been added', {
                    position: 'bottom-right',
                    closeOnClick: true,
                    theme: 'colored'
                });

                //Update the table
                getAutomaticTransactionDataForTable();
            }
            else{
                toast.warning(res.error, {
                    position: 'bottom-right',
                    closeOnClick: true,
                    theme: 'colored'
                });
            }
        });
    }

    const onDeleteRow = (autoTransactionName: string) => {
        DoDelete('/api/AutomaticTransactions/DeleteAutomaticTransaction', {name: autoTransactionName}).then(res => {
            if(res.ok){
                getAutomaticTransactionDataForTable();
                toast.success('Automatic transaction has been deleted', {
                    position: 'bottom-right',
                    closeOnClick: true,
                    theme: 'colored'
                });
            }
            else if(res.status === 404){
                toast.warning('automatic transaction not found', {
                    position: 'bottom-right',
                    closeOnClick: true,
                    theme: 'colored'
                });
            }
            else{
                toast.warning('There has been an unexpected error', {
                    position: 'bottom-right',
                    closeOnClick: true,
                    theme: 'colored'
                });
            }
        });
    }

    const handleDropdownChange = (data: DropdownOption | null) => {
        if(data){
            setSelectedPotId(data.value);
        }
    }

    //For the dropdown
    const customFontColour = {
        option: (provided: any) => ({
            ...provided,
            color: 'black',
        }),
        dropdownIndicator: () => ({
            'svg': {
                fill: 'black'
              }
        })
    };

    return ( 
        <>
            <h1>Automatic Transactions</h1>

            <Container>
                <h2 style={{textAlign: 'center', paddingTop: 20}}>Add Automatic Transaction</h2>
                <form onSubmit={handleSubmit(onSubmit)}>
                    <Row style={{paddingLeft: 200, paddingTop: 20}}>
                        <Col>
                            <input {...register('automaticTransactionName', {required: true})} name="automaticTransactionName" className="formInput" style={{width: 400}}/>
                        </Col>
                        <Col>
                            <Select options={selectDropdownData.data} maxMenuHeight={300} styles={customFontColour} className="single-select" onChange={handleDropdownChange}></Select>
                        </Col>
                        <Col>
                            <input type='submit' className="formInput" style={ { backgroundColor: '#39e327'} }/>
                        </Col>
                    </Row>
                </form>
            </Container>
            
            <Container>
                <h3 style={{textAlign: 'center', paddingTop: 20, paddingBottom: 20}}>Added Automatic Transactions</h3>
                { getAutomaticTransactionsDataError && <h5 style={{textAlign: 'center', paddingTop: 20, paddingBottom: 20}}>{ getAutomaticTransactionsDataError }</h5> }
                { !getAutomaticTransactionsDataError && getAutomaticTransactionsDataIsPending && <h5 style={{textAlign: 'center', paddingTop: 20, paddingBottom: 20}}>Loading...</h5>}
                { !getAutomaticTransactionsDataError && !getAutomaticTransactionsDataIsPending &&
                    <Table striped hover responsive variant="dark" >
                        <thead>
                            <tr>
                                <th>Merchant</th>
                                <th>Pot Type</th>
                                <th>Delete?</th>
                            </tr>
                        </thead>
                        <tbody>
                        { automaticTransactionData?.automaticTransactions.map( (transaction : AutomaticTransactionData) => (
                            <tr key={ transaction.name }>
                                <td> { transaction.name } </td>
                                <td> { transaction.potName } </td>
                                <td><button onClick={(() => onDeleteRow(transaction.name))} className="formInput" style={{ backgroundColor: '#d52b2b' }} >Delete</button></td>
                            </tr>
                        ))}
                        </tbody>
                    </Table>}

            </Container>
        </>
     );
}
 
export default AutomaticTransactions;