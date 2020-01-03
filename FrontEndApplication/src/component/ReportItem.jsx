import React from "react";
import { GeneralContainer } from "../style/style";

const ReportItem = ({ ticketWeight, realWeight, sobrePeso, hasSobrePeso }) => {
  return (
    <GeneralContainer color="#a9a9ec">
      <strong>{`peso usado (obtenido del JSON): ${ticketWeight}`}</strong>
      <div>
        <strong>{`peso real del paquete (obtenido del API de Fedex: ${realWeight}`}</strong>
      </div>
      <strong>{`Sobrepeso: ${sobrePeso}`}</strong>
    </GeneralContainer>
  );
}

export default ReportItem;
