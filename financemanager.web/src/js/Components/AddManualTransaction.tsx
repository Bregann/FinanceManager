import { useState } from "react";
import { Col, Container, Modal, Row } from "react-bootstrap";
import { SubmitHandler, useForm } from "react-hook-form";
import Select from 'react-select';
import { toast } from "react-toastify";
import '../../css/modal.css';
import { DoGet, DoPost, useFetchGET, UseFetchGETResult } from "../Helpers/fetchHelper";

type FormData = {
    transactionAmount: number;
    merchantName: string;
}

interface AddManualTransactionProps {
    showModal: boolean;
    setManualTransactionModal: (data: boolean) => void;
    setPotsData: (data: []) => void;
}

interface DropdownOption {
    label: string;
    value: number;
}

const AddManualTransaction = (props: AddManualTransactionProps) => {
    const closeModal = () => props.setManualTransactionModal(false);
    const {register, handleSubmit} = useForm<FormData>();
    const selectDropdownData: UseFetchGETResult = useFetchGET('/api/Pots/GetPotValuesDropdown');
    const [selectedPotId, setSelectedPotId] = useState(0);

    const onSubmit: SubmitHandler<FormData> = data => {
        //0 is default therefore no option has been selected
        if(selectedPotId === 0){
            toast.warning('Please select a pot', {
                position: 'bottom-right',
                closeOnClick: true,
                theme: 'colored'
            });
            return;
        }

        DoPost('/api/Transactions/AddManualTransaction', { transactionAmount: data.transactionAmount, merchantName: data.merchantName ,potId: selectedPotId}).then(res => {
            if(res === true){
                toast.success('Manual transaction added!', {
                    position: 'bottom-right',
                    closeOnClick: true,
                    theme: 'colored'
                });

                //Update the pots with the new month
                DoGet('/api/Pots/GetPotsHeader').then(res => {
                    props.setPotsData(res);
                });

                //Close the modal
                props.setManualTransactionModal(false);
            }
            else{
                toast.warning('Error adding manual transaction', {
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
            <Modal show={props.showModal} onHide={closeModal} backdrop="static" contentClassName="manualTransactionModal">
                <Modal.Header closeButton>
                    <Modal.Title>Add Manual Transaction</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <Container className="ModalBody">
                        <form onSubmit={handleSubmit(onSubmit)}>
                            <Row>
                                <Col xs={2}>
                                    <input className="modalInput" {...register("transactionAmount", {required: true, maxLength: 10, valueAsNumber: true, min: 0.01})} name="transactionAmount" type="number" step="0.01" placeholder="Amount" style={ {maxWidth: '100px'} } />
                                </Col>
                                <Col>
                                    <input className="modalInput" {...register("merchantName", {required: true})} name="merchantName" placeholder="Merchant Name" style={{maxWidth: '200px'}} ></input>
                                </Col>
                                <Col>
                                    <Select options={selectDropdownData.data} maxMenuHeight={300} styles={customFontColour} className="single-select" onChange={handleDropdownChange} ></Select>
                                </Col>
                                <Col xs={1}>
                                    <input type='submit' className="modalInput" style={ { backgroundColor: '#39e327'} }/>
                                </Col>
                            </Row>
                        </form>
                    </Container>
                </Modal.Body>
            </Modal>
        </>
     );
}
 
export default AddManualTransaction;