import React, { useEffect, useState } from "react";
import axios from 'axios';
import Styled from "styled-components";
import ReportItem from "./ReportItem";

const Wrapper = Styled.div`
    justify-content: center;
    align-items: center;
    display: flex;
    border-color: #FF0364;
    border-style: solid;
    width: 1100px;
`;

const Container = Styled.div`
    border-style: solid;
    padding: 10px 10px 10px 10px;
    width: 4500px;
    margin: 10px;
    border-color: #FF0364;
    border-radius: 10px;
`;

const ReportComponent= () => {
  const [reportData, setReportData] = useState([]);

  useEffect(() => {
    const fetchData = async () => {
      const result = await axios(
        'http://localhost:56182/Home/index',
      );
      setReportData(result.data);
    };
    fetchData();
  }, [reportData]);

  return (
    <>
    <Wrapper>
      <Container>
      Reporte de sobrepeso
        <hr />
        {reportData.map((element, index) => (
          <ReportItem key={index} {...element} />
        ))}
      </Container>
    </Wrapper>
    </>
  );
}


export default ReportComponent;
