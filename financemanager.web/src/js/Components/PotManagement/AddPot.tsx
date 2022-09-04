import { Container, Row, Col } from "react-bootstrap";
import { SubmitHandler, useForm } from "react-hook-form";
import { DoGet, DoPost } from "../../Helpers/fetchHelper";
import { toast } from "react-toastify";

type FormValues = {
    potName: string;
    potAmount: number;
}

interface AddPotProps {
    setData: (data: []) => void;
}

const AddPot = (props: AddPotProps) => {
    const { register, handleSubmit, formState: { errors } } = useForm<FormValues>();

    const onSubmit: SubmitHandler<FormValues> = data => {
        const addPotRes = DoPost('/api/Pots/AddNewPot', data);

        addPotRes.then(res => {
            if(res.success){
                toast.success('Pot has been added', {
                    position: 'bottom-right',
                    closeOnClick: true,
                    theme: 'colored'
                });

                //get the data and set it here
                DoGet('/api/Pots/GetPotsHeader').then(res => {
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
        <h2>Add New Pot</h2>

        <Container>
            <Row>
                <Col className='d-flex justify-content-center'>
                    <form onSubmit={handleSubmit(onSubmit)}>
                        <input {...register('potName', {required: true})} className='formInput' size={50} placeholder='Pot Name'/>
                        <input {...register('potAmount', {required: true, maxLength: 10, valueAsNumber: true, min: 1})} className='formInput' size={10} type='number' step={0.01} placeholder='Pot Amount'/>
                        <input type='submit' className='formInput'/>
                    </form>
                </Col>
            </Row>
            <Row>
                <Col md={{ offset: 3 }}>
                    {errors.potName?.type === 'required' && <p>Pot name is required</p>}
                    {errors.potAmount?.type === 'required' && <p>Pot amount is required</p>}
                    {errors.potAmount?.type === 'min' && <p>Pot value cannot be 0p</p>}
                </Col>
            </Row>
        </Container>
        </>
     );
}
 
export default AddPot;