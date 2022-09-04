import { Paper } from '@mui/material';
import '../../css/potHeader.css';

interface PotsHeaderProps{
    pots: [];
    isPending: boolean; 
    error: string | null;
}

interface Pot{
    potName: string;
    amountAllocated: number;
    amountLeftForMonth: number;
}

const PotsHeader = (props: PotsHeaderProps) => {
    return (
        <div className="potsHeader" style={{ marginTop: "20px"}}>
            <h4 style={{textAlign: "center"}}>Pots</h4>
            { props.isPending && <div>Loading...</div>}
            { props.error && <div>Error {props.error} </div>}

            <div className="flex">
                {props.pots.map( (pot: Pot) => (
                    <Paper elevation={4} className="paperStyle" key={pot.potName}>
                        <h4><b>{ pot.potName }</b></h4>
                        Allocated: { pot.amountAllocated }<br />
                        Amount left: { pot.amountLeftForMonth } <br />
                    </Paper>
                ))}
            </div>
        </div>
     );
}

export default PotsHeader;