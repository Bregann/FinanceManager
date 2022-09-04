/* eslint-disable react-hooks/exhaustive-deps */
import { Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Paper } from "@mui/material";
import { Chart as ChartJS, ArcElement, Tooltip, Legend, CategoryScale, LinearScale, PointElement, LineElement, Title } from 'chart.js';
import { useEffect, useState } from "react";
import { Col, Container, Row } from "react-bootstrap";
import { Line, Pie } from "react-chartjs-2";
import '../../css/monthlyBreakdown.css';
import { apiUrl } from "../config";

interface MonthlyTable {
    moneySpentPerPotData: MonthlyBreakdownTableData[];
    topPlacesMoneySpentData: MonthlyBreakdownTableData[];
    mostExpensivePurchasesData: MonthlyBreakdownTableData[];
}

interface MonthlyBreakdownTableData {
    name: string;
    amount: string;
}

interface MonthlyTableChart {
    moneySpentPerPotData: MonthlyBreakdownChartData[];
    topPlacesMoneySpentData: MonthlyBreakdownChartData[];
    mostExpensivePurchasesData: MonthlyBreakdownChartData[];
}

interface MonthlyBreakdownChartData {
    name: string;
    amount: number;
}

interface DailySpendChart{
    data: DailySpendData[]
}

interface DailySpendData {
    dayMonth: string;
    amount: number;
}

//Register the chart data
ChartJS.register(
    ArcElement, 
    Tooltip, 
    Legend,
    CategoryScale,
    LinearScale,
    PointElement,
    LineElement,
    Title);

const ChartColours = [
    'rgba(255, 99, 132, 0.2)',
    'rgba(54, 162, 235, 0.2)',
    'rgba(255, 206, 86, 0.2)',
    'rgba(75, 192, 192, 0.2)',
    'rgba(153, 102, 255, 0.2)',
    'rgba(255, 159, 64, 0.2)',
    'rgba(202, 182, 209, 0.2)',
    'rgba(56, 99, 40, 0.2)',
    'rgba(110, 88, 49, 0.2)',
    'rgba(104, 49, 110, 0.2)'
  ];

  const ChartBorderColours = [
    'rgba(255, 99, 132, 1)',
    'rgba(54, 162, 235, 1)',
    'rgba(255, 206, 86, 1)',
    'rgba(75, 192, 192, 1)',
    'rgba(153, 102, 255, 1)',
    'rgba(255, 159, 64, 1)',
    'rgba(202, 182, 209, 1)',
    'rgba(56, 99, 40, 1)',
    'rgba(110, 88, 49, 1)',
    'rgba(104, 49, 110, 1)'
  ];

const MonthlyBreakdown = () => {
    //For the top 3 tables
    const[isTableDataPending, setIsTableDataPending] = useState(true);
    const[tableData, setTableData] = useState<MonthlyTable | null>(null);
    const[tableError, setTableError] = useState<string | null>(null);

    //For the charts just below the tables
    const[isTableChartDataPending, setIsTableChartDataPending] = useState(true);
    const[tableChartData, setTableChartData] = useState<MonthlyTableChart | null>(null);
    const[tableChartError, setTableChartError] = useState<string | null>(null);

    //For the spending each day data
    const[isDailySpentChartDataPending, setIsDailySpentChartDataPending] = useState(true);
    const[dailySpendChartData, setDailySpendChartData] = useState<DailySpendChart | null>(null);
    const[dailySpendChartError, setDailySpendChartError] = useState<string | null>(null);

    useEffect(() => {
        //Get the data for the tables
        fetch(apiUrl + '/api/MonthlyBreakdown/GetMonthlyBreakdownTableData').then(res => {
            if(!res.ok){
                throw Error(res.status.toString() + ' ' + res.statusText);
            }

            return res.json();
        }).then((data: MonthlyTable) => {
            setTableData(data);
            setIsTableDataPending(false);
        }).catch(err => {
            setTableError(err.message);
            setIsTableDataPending(false);
        });

        //Get the data for the charts
        fetch(apiUrl + '/api/MonthlyBreakdown/GetMonthlyBreakdownTableChartData').then(res => {
            if(!res.ok){
                throw Error(res.status.toString() + ' ' + res.statusText);
            }

            return res.json();
        }).then((data: MonthlyTableChart) => {
            setTableChartData(data);
            setIsTableChartDataPending(false);
        }).catch(err => {
            setTableChartError(err.message);
            setIsTableChartDataPending(false);
        });

        //Get the daily spend chart
        fetch(apiUrl + '/api/MonthlyBreakdown/GetMoneySpentPerDayChartData').then(res => {
            if(!res.ok){
                throw Error(res.status.toString() + ' ' + res.statusText);
            }

            return res.json();
        }).then((data: DailySpendChart) => {
            setDailySpendChartData(data);
            setIsDailySpentChartDataPending(false);
        }).catch(err => {
            setDailySpendChartError(err.message);
            setIsDailySpentChartDataPending(false);
        });
    }, []);

    return ( 
        <> 
            <h1>Monthly Breakdown</h1>

            {!tableError && isTableDataPending && <h2>Loading table data...</h2>}
            {tableError && <h3>Error loading table data - {tableError}</h3>}
            <Container>
                {/* only render if theres no error and its not pending */!isTableDataPending && !tableError && <Row style={{paddingTop: 20}}>
                    <Col>
                        <Paper className="box" elevation={4} style={{height: 650}}>
                            <h4>Money Spent Per Pot</h4>
                            <TableContainer component={Paper} elevation={5} style={{height: 590}}>
                                <Table>
                                    <TableHead>
                                        <TableRow>
                                            <TableCell>Pot</TableCell>
                                            <TableCell>Amount Spent</TableCell>
                                        </TableRow>
                                    </TableHead>
                                    <TableBody>
                                        {tableData?.moneySpentPerPotData.map(data => (
                                        <TableRow>
                                            <TableCell>{data.name}</TableCell>
                                            <TableCell>{data.amount}</TableCell>
                                        </TableRow>
                                        ))}
                                    </TableBody>
                                </Table>
                            </TableContainer>
                        </Paper>
                    </Col>
                    <Col>
                        <Paper className="box" elevation={4} style={{height: 650}}>
                            <h4>Top Places Money Spent</h4>
                            <TableContainer component={Paper} elevation={5} style={{height: 590}}>
                                <Table>
                                    <TableHead>
                                        <TableRow>
                                            <TableCell>Pot</TableCell>
                                            <TableCell>Amount Spent</TableCell>
                                        </TableRow>
                                    </TableHead>
                                    <TableBody>
                                        {tableData?.topPlacesMoneySpentData.map(data => (
                                        <TableRow>
                                            <TableCell>{data.name}</TableCell>
                                            <TableCell>{data.amount}</TableCell>
                                        </TableRow>
                                            ))}
                                    </TableBody>
                                </Table>
                            </TableContainer>
                        </Paper>
                    </Col>
                    <Col>
                        <Paper className="box" elevation={4} style={{height: 650}}>
                            <h4>Most Expensive Purchases</h4>
                            <TableContainer component={Paper} elevation={5}  style={{height: 590}}>
                                <Table>
                                    <TableHead>
                                        <TableRow>
                                            <TableCell>Pot</TableCell>
                                            <TableCell>Amount Spent</TableCell>
                                        </TableRow>
                                    </TableHead>
                                    <TableBody>
                                        {tableData?.mostExpensivePurchasesData.map(data => (
                                            <TableRow>
                                                <TableCell>{data.name}</TableCell>
                                                <TableCell>{data.amount}</TableCell>
                                            </TableRow>
                                                ))}
                                    </TableBody>
                                </Table>
                            </TableContainer>
                        </Paper>
                    </Col>
                </Row>}

                {!tableChartError && isTableChartDataPending && <h2>Loading table data...</h2>}
                {tableChartError && <h3>Error loading table data - {tableChartError}</h3>}
                {/* only render if theres no error and its not pending */!isTableChartDataPending && !tableChartError && <Row  style={{paddingTop: 20}}>
                    <Col>
                        <Paper className="box" elevation={4}>
                            <h3>Money Spent Per Pot</h3>
                            <Paper elevation={4} style={{paddingBottom: 20, paddingTop: 10}}>
                                <Pie 
                                    data={{
                                        labels: tableChartData?.moneySpentPerPotData.map(data => data.name),
                                        datasets: [
                                        {
                                            label: 'Money spent per pot',
                                            data: tableChartData?.moneySpentPerPotData.map(data => data.amount),
                                            backgroundColor: ChartColours,
                                            borderColor: ChartBorderColours
                                            }
                                        ]
                                    }} />
                            </Paper>
                        </Paper>
                    </Col>
                    <Col>
                        <Paper className="box" elevation={4}>
                            <h3>Top Places Money Spent</h3>
                            <Paper elevation={4} style={{paddingBottom: 20, paddingTop: 10}}>
                                <Pie 
                                    data={{
                                        labels: tableChartData?.topPlacesMoneySpentData.map(data => data.name),
                                        datasets: [
                                        {
                                            label: 'Top places money spent data',
                                            data: tableChartData?.topPlacesMoneySpentData.map(data => data.amount),
                                            backgroundColor: ChartColours,
                                            borderColor: ChartBorderColours
                                            }
                                        ]
                                    }} />
                            </Paper>
                        </Paper>
                    </Col>
                    <Col>
                        <Paper className="box" elevation={4}>
                            <h3>Most Expensive Purchases</h3>
                            <Paper elevation={4} style={{paddingBottom: 20, paddingTop: 10}}>
                                <Pie 
                                    data={{
                                        labels: tableChartData?.mostExpensivePurchasesData.map(data => data.name),
                                        datasets: [
                                        {
                                            label: 'Most expensive purchases data',
                                            data: tableChartData?.mostExpensivePurchasesData.map(data => data.amount),
                                            backgroundColor: ChartColours,
                                            borderColor: ChartBorderColours
                                        }
                                        ]
                                    }} />
                            </Paper>
                        </Paper>
                    </Col>
                </Row>}

                {!dailySpendChartError && isDailySpentChartDataPending && <h2>Loading table data...</h2>}
                {dailySpendChartError && <h3>Error loading table data - {dailySpendChartError}</h3>}
                {/* only render if theres no error and its not pending */!isDailySpentChartDataPending && !dailySpendChartError && <Row style={{paddingTop: 20}}>
                    <Col>
                        <Paper className="box" elevation={4} style={{marginBottom: 30}}>
                            <h3>Daily Money Spent</h3>
                            <Paper elevation={4} style={{paddingTop: 10, paddingLeft: 10, paddingBottom: 10, paddingRight: 10}}>
                                <Line 
                                    data={{
                                        labels: dailySpendChartData?.data.map(data => data.dayMonth),
                                        datasets: [
                                        {
                                            label: 'Amount spent',
                                            data: dailySpendChartData?.data.map(data => data.amount),
                                            borderColor: 'rgb(3, 165, 252)',
                                            backgroundColor: 'rgba(3, 165, 252, 0.5)'
                                        }
                                        ]
                                    }}
                                    options={{
                                        scales: {
                                            x: {
                                                title: {
                                                    display: true,
                                                    text: 'Date'
                                                }
                                            },
                                            y: {
                                                title: {
                                                    display: true,
                                                    text: 'Amount Spent'
                                                }
                                            }
                                        }
                                    }}/>
                            </Paper>
                        </Paper>
                    </Col>
                </Row>}
            </Container>
        </>
     );
}
 
export default MonthlyBreakdown;