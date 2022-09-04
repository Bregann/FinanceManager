import { Route, Routes } from "react-router-dom";
import NavigationBar from "./js/Shared/NavigationBar";
import './css/index.css';
import { useEffect } from "react";
import Home from "./js/Components/Home";
import MonthlyBreakdown from "./js/Components/MonthlyBreakdown";
import NotFound from "./js/Components/NotFound";
import PotsHeader from "./js/Shared/PotsHeader";
import PotManagement from "./js/Components/PotManagement/PotManagement";
import { useFetchGET, UseFetchGETResult } from "./js/Helpers/fetchHelper";
import { ToastContainer } from "react-toastify";
import AutomaticTransactions from "./js/Components/AutomaticTransactions";

function App() {

  useEffect(() => {
    document.body.style.backgroundColor = 'rgb(48 45 45)';
  }, []);

  //Get the top pots data and pass it to the potsHeader and PotManagement comps
  const useFetchData: UseFetchGETResult = useFetchGET('/api/Pots/GetPotsHeader');

  return (
    <div className="App">
      <NavigationBar setPotsData={useFetchData.setData}/>
      <PotsHeader pots={useFetchData.data} isPending={useFetchData.isPending} error={useFetchData.error}/>
      <div className="content">
        <Routes>
          <Route path='/' element={<Home setPotsData={useFetchData.setData}/>}/>
          <Route path='/monthlybreakdown' element={ <MonthlyBreakdown /> }/>
          <Route path='/automatictransactions' element= {<AutomaticTransactions />}></Route>
          <Route path='/potmanagement' element= { <PotManagement setData={useFetchData.setData} /> }/>
          <Route path='*' element={ <NotFound />}/>
        </Routes>
      </div>

      <ToastContainer />
    </div>
    
  );
}

export default App;