import { useState } from "react";
import { Navbar, Container, Nav } from "react-bootstrap";
import '../../css/nav.css'
import AddManualTransaction from "../Components/AddManualTransaction";
import AddMonth from "../Components/AddMonth";

interface NavigationBarProps{
    setPotsData: (data: []) => void;
}

const NavigationBar = (props: NavigationBarProps) => {
    const[renderMonthModal, setRenderMonthModal] = useState(false);
    const[renderAddManualTransactionModal, setRenderAddManualTransactionModal] = useState(false);

    return ( 
        <>
            <Navbar expand="lg" className="navbar-custom" sticky="top">
                <Container fluid>
                    <Navbar.Brand href="/">
                        <img
                        src="./favicon.ico"
                        width="30"
                        height="30"
                        className="d-inline-block align-top"
                        alt="FinanceManager Logo">
                        </img>
                        Finance Manager
                    </Navbar.Brand>
                    <Navbar.Toggle aria-controls="basic-navbar-nav"/>
                    <Navbar.Collapse id="basic-navbar-nav">
                        <Nav className="me-auto">
                            <Nav.Link href='/'>Home</Nav.Link>
                            <Nav.Link href='/monthlybreakdown'>Monthly breakdown</Nav.Link>
                            <Nav.Link href='/automatictransactions'>Automatic Transactions</Nav.Link>
                            <Nav.Link href='/potmanagement'>Edit/Add pots</Nav.Link>
                            <Nav.Link onClick={() => setRenderMonthModal(true)}>Add Month</Nav.Link>
                            <Nav.Link onClick={() => setRenderAddManualTransactionModal(true)}>Add manual transaction</Nav.Link>
                        </Nav>
                    </Navbar.Collapse>
                </Container>
            </Navbar>

            { renderMonthModal && <AddMonth showModal={renderMonthModal} setRenderMonthModal={setRenderMonthModal} setPotsData={props.setPotsData}/> }
            { renderAddManualTransactionModal && <AddManualTransaction showModal={renderAddManualTransactionModal} setManualTransactionModal={setRenderAddManualTransactionModal} setPotsData={props.setPotsData}/>}
        </>

     );
}
 
export default NavigationBar;