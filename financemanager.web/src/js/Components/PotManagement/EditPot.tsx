import { useState } from 'react';
import { Col, Container, Row } from 'react-bootstrap';
import Select from 'react-select';
import { DoDelete, DoPost, DoGet, useFetchGET, UseFetchGETResult } from '../../Helpers/fetchHelper';
import { SubmitHandler, useForm } from "react-hook-form";
import { toast } from 'react-toastify';

type FormValues = {
    potNameField: string;
    potAmountField: number;
}

interface DropdownOption {
    label: string;
    value: number;
}

interface PotResponse {
    success: boolean;
    error: string | null;
    potName: string;
    potAmount: number;
}

interface EditPotProps {
    setData: (data: []) => void;
}

const EditPot = (props: EditPotProps) => {
    const useFetchData: UseFetchGETResult = useFetchGET('/api/Pots/GetPotValuesDropdown');
    const [potName, setPotName] = useState<string | null>(null);
    const [potAmount, setPotAmount] = useState(0);
    const [potId, setPotId] = useState(1);
    const { register, handleSubmit, setValue } = useForm<FormValues>();

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

    const handleDropdownChange = (data: DropdownOption | null) => {
        if(data){
            //Set the pot ID used for deleting/editing pots
            setPotId(data.value);

            DoPost('/api/Pots/GetSpecificPotData', {potId: data.value}).then((res: PotResponse) => {
                if(res.success === true){
                    //set potName and potAmount and render below. Save and delete button
                    setPotName(res.potName);
                    setPotAmount(res.potAmount);

                    //Set the values for the form
                    setValue("potNameField", res.potName);
                    setValue("potAmountField", res.potAmount);
                }
            });
        }
    }

    const deletePot = () => {
        DoDelete('/api/Pots/DeletePot', {potId: potId}).then((res) => {
            
            //If succesful hide the edit pot and clear it from the pot nav
            if(res.status === 200){
                setPotName(null);

                //get the data after deleting the pot
                const newPotList = DoGet('/api/Pots/GetPotsHeader');

                newPotList.then(res => {
                    props.setData(res);
                });

                toast.success('Pot has been deleted', {
                    position: 'bottom-right',
                    closeOnClick: true,
                    theme: 'colored'
                });
            }
            else if(res.status === 404){
                toast.warning('Pot not found', {
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

    const onSubmit: SubmitHandler<FormValues> = data => {
        //check if it's a name change, changes the api request
        let nameChanged = false;

        if(data.potNameField !== potName){
            nameChanged = true;
        }

        const editPotObj = {
            potId: potId,
            potName: data.potNameField,
            potAmount: data.potAmountField,
            nameChanged: nameChanged
        };

        DoPost('/api/Pots/EditPot', editPotObj).then((res: PotResponse) => {
            if(res.success === true){
                toast.success('Pot has been edited', {
                    position: 'bottom-right',
                    closeOnClick: true,
                    theme: 'colored'
                });

                //get the data after editing the pot
                const newPotList = DoGet('/api/Pots/GetPotsHeader');

                newPotList.then(res => {
                    props.setData(res);
                });
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

    return (
        <>
            <h1>Edit Pot</h1>

            <Container>
                <Row>
                    <Col>
                        <Select options={useFetchData.data} maxMenuHeight={300} styles={customFontColour} className="single-select" onChange={handleDropdownChange}/>
                    </Col>
                </Row>
                { potName && 
                
                <Row>
                    <Col className='d-flex justify-content-center' style={ { paddingTop: '20px' } }>
                    <form onSubmit={handleSubmit(onSubmit)}>
                        <input {...register('potNameField', {required: true})} className='formInput' size={50} placeholder='Pot Name' defaultValue={potName}/>
                        <input {...register('potAmountField', {required: true, maxLength: 10, valueAsNumber: true, min: 1})} className='formInput' size={10} type='number' placeholder='Pot Amount' defaultValue={potAmount}/>
                        <input type='submit' className='formInput' value='Save' style={ { backgroundColor: '#39e327' } }/>
                    </form>
                    <input type='submit' className='formInput' value='Delete' style={ { backgroundColor: '#d52b2b' } } onClick={() => deletePot()}/>
                    </Col>
                </Row>}
            </Container>
            <div style={{paddingBottom: '50px'}} />

        </>
     );
}
 
export default EditPot;