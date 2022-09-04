import '../../../css/potManagement.css';
import '../../../css/form.css';
import AddPot from './AddPot';
import EditPot from './EditPot';

interface PotManagementProps {
    setData: (data: []) => void;
}

const PotManagement = (props: PotManagementProps) => {
    return ( 
        <>
            <h1>Pot Management</h1>
            <AddPot setData={props.setData}/>
            <EditPot setData={props.setData}/>
        </>
     );
}

export default PotManagement;