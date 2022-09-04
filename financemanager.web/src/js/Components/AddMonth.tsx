import { useEffect, useState } from 'react';
import { Container, Row, Modal, Col } from 'react-bootstrap';
import { useForm, useWatch, SubmitHandler } from 'react-hook-form';
import { toast } from 'react-toastify';
import '../../css/modal.css';
import { DoGet, DoPost } from '../Helpers/fetchHelper';

interface AddMonthProps {
    showModal: boolean;
    setRenderMonthModal: (data: boolean) => void;
    setPotsData: (data: []) => void;
}

type FormData = {
    income: number;
}

interface MonthBreakdownData{
    success: boolean;
    error: string | null;
    pots: [PotData];
    total: string;
    spareMoney: string;
}

interface PotData{
    potName: string;
    amountAllocated: string;
}

const AddMonth = (props: AddMonthProps) => {
    const closeModal = () => props.setRenderMonthModal(false);
    const {register, handleSubmit, control } = useForm<FormData>();
    const [monthBreakdownData, setmonthBreakdownData] = useState<MonthBreakdownData | null>(null);
    const [displayMonthBreakdownData, setDisplayMonthBreakdownData] = useState(false);

    const income = useWatch<FormData>({ name: "income", control: control });

    const onSubmit: SubmitHandler<FormData> = data => {
        DoPost('/api/NewMonth/CreateNewMonth', {income: data.income}).then(res => {
            if(res.success === true){
                toast.success('Month has been added', {
                    position: 'bottom-right',
                    closeOnClick: true,
                    theme: 'colored'
                });

                //Update the pots with the new month
                DoGet('/api/Pots/GetPotsHeader').then(res => {
                    props.setPotsData(res);
                });

                //Close the modal
                props.setRenderMonthModal(false);
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

    //this is for doing the api request to get the monthly breakdown stuff
    useEffect(() => {
        //Want to wait a second before doing the API request to prevent db spam
        const incomeApiDelay = setTimeout(() => {
            if(income === undefined){
                return;
            }

            DoPost('/api/NewMonth/GetAddMonthBreakdownAndTotal', { income: income}).then(res => {
                setmonthBreakdownData(res);
                setDisplayMonthBreakdownData(true);
            });
        }, 500);

        return () => clearTimeout(incomeApiDelay);

    }, [income]);

    return (
        <>
            <Modal show={props.showModal} onHide={closeModal} backdrop="static">
                <Modal.Header closeButton >
                    <Modal.Title>Add New Month</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <Container className="ModalBody">
                        <form onSubmit={handleSubmit(onSubmit)}>
                            <label className="modalLabel">Income for month</label>
                            <Row>
                                <Col xs={5} md={{ offset: 2}}>
                                    <input className="modalInput" {...register("income", {required: true, maxLength: 10, valueAsNumber: true, min: 1})} name="income" type="number" step="0.01"/>
                                </Col>
                                <Col xs={2}>
                                    <input type='submit' className="modalInput" style={ { backgroundColor: '#39e327', marginLeft: '10px'} }/>
                                </Col>
                            </Row>
                        </form>
                        { displayMonthBreakdownData &&
                        <>
                            {monthBreakdownData?.success && <h2 style={ { paddingTop: "10px", paddingBottom: "10px" }}>Money Breakdown</h2> /* only render the breakdown if its been succesful */ }
                            {monthBreakdownData?.success === false && <h4 style={ {color: 'orange'} }>Error - {monthBreakdownData.error}</h4>}
                            <Container>
                                <Row>
                                    {monthBreakdownData?.success && monthBreakdownData?.pots.map( potBreakdown => (
                                        <Col xs={4}>
                                            <h4><b>{potBreakdown.potName}</b></h4>
                                            <h6>{potBreakdown.amountAllocated}</h6>
                                        </Col>
                                    ))}
                                </Row>
                                <hr style={ { border: '2px solid white'}}/>
                                <Row>
                                    <Col>
                                        <h4><b>Total Allocated</b></h4>
                                        <h6>{monthBreakdownData?.total}</h6>
                                    </Col>
                                    <Col>
                                        <h4><b>Total Spare</b></h4>
                                        <h6>{monthBreakdownData?.spareMoney}</h6>
                                    </Col>
                                </Row>
                            </Container>

                        </>
                        }
                    </Container>
                </Modal.Body>

            </Modal>
        </>
     );
}
 
export default AddMonth;