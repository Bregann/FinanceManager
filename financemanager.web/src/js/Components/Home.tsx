import { useEffect, useState } from "react";
import { apiUrl } from "../config";
import { DoPost } from "../Helpers/fetchHelper";
import { toast } from "react-toastify";
import 'react-toastify/dist/ReactToastify.css';
import { Table } from "react-bootstrap";
import { useFetchGET, UseFetchGETResult, DoGet } from '../Helpers/fetchHelper';
import Select from 'react-select';
import '../../css/home.css';

interface HomeProps{
    setPotsData: (data: []) => void;
}

interface TransactionData{
    id: string;
    imgUrl: string;
    merchantName: string;
    transactionAmount: string;
    transactionDate: string;
}

export interface Transaction {
    id: string;
    imgUrl: string;
    merchantName: string;
    transactionAmount: string;
    transactionDate: string;
}

const Home = (props: HomeProps) => {
    const[isPending, setIsPending] = useState(true);
    const[transactionData, setTransactionData] = useState<TransactionData[]>();
    const[message, setMessage] = useState<string | null>(null);
    const useFetchData: UseFetchGETResult = useFetchGET('/api/Pots/GetPotValuesDropdown');

    /* get the transaction data */
    useEffect(() => {
        fetch(apiUrl + '/api/Transactions/GetUnprocessedTransactions').then(res => {
                if(res.status === 404){
                    setMessage('No transactions found');
                    setIsPending(false);
                }
                else if(!res.ok){
                    setMessage('Error loading transaction data');
                    setIsPending(false);
                }
                else{
                    return res.json();
                }
            }).then((data: TransactionData[]) => {
                setTransactionData(data);
                setIsPending(false);
            });
    }, []);

    const handleTransactionUpdate = (transactionId: string, potId: number) => {
        DoPost('/api/Transactions/UpdateTransaction', { 
            transactionId: transactionId,
            potId: potId
        }).then(res => {
            if(res === true){
                setTransactionData(transactionData?.filter(transaction => transaction.id !== transactionId));

                //if theres no transactions left to process then hide the table
                if(transactionData?.length === 1){
                    setMessage('All Done! No transactions left to process');
                }

                DoGet('/api/Pots/GetPotsHeader').then(res => {
                    props.setPotsData(res);
                });

                toast.success('Transaction updated!', {
                    position: 'bottom-right',
                    closeOnClick: true,
                    theme: 'colored'
                });
            }
            else{
                toast.warning('Error removing transaction', {
                    position: 'bottom-right',
                    closeOnClick: true,
                    theme: 'colored'
                });
            }
        });
    }

    //For the dropdown
    const customFontColour = {
        menuList: () => ({
            color: 'black'
        }),
        dropdownIndicator: () => ({
            'svg': {
                fill: 'black'
              }
        })
    };

    return (
        <>
        <h1 style={{textAlign: "center", marginTop: "20px"}}>Home</h1>

        { isPending && !message && <h3 style={ {textAlign: 'center'} }>loading...</h3> /*Loading but no error message*/ }
        { message && <h3 style={ {textAlign: 'center'} }> { message } </h3> /* error message - could be an actual error or the no transactions found */}

        {!message && !isPending && <Table striped hover responsive variant="dark" className='transTable'/* only render the table when it isn't pending and there's been no error */>
            <thead>
                <tr>
                    <th>Icon</th>
                    <th>Merchant</th>
                    <th>Transaction Amount</th>
                    <th>Transaction Date</th>
                    <th>Pot type</th>
                </tr>
            </thead>
            <tbody>
                { transactionData?.map( (transaction: Transaction) => (
                    <tr key={ transaction.id }>
                        <td><img src= {transaction.imgUrl} width='64' height='64' alt={`${transaction.merchantName} icon`}></img></td>
                        <td> { transaction.merchantName } </td>
                        <td> { transaction.transactionAmount } </td>
                        <td> { transaction.transactionDate } </td>
                        <td>
                            <Select menuPosition='fixed' options={useFetchData.data} styles={customFontColour} onChange={(e: any) => handleTransactionUpdate(transaction.id, e.value)}/>
                        </td>
                    </tr>
                ))}

            </tbody>
        </Table>}
        </>
     );
}
 
export default Home;