import Styled from "styled-components";

export const GeneralContainer = Styled.div`
    border-color: ${props => props.color};
    border-radius: 10px;
    border-style: solid;
    margin-bottom: 10px;
    padding: 10px;
`;

export const Container = Styled.div`
  justify-content: center;
  align-items: center;
  display: flex;
  margin-top: 10px;
  
`;

export const TextSkydropx = Styled.div`
  text-align: center;
  margin-top: 40px;
  font-size: 50px;
  color: #FF0364;
`;
