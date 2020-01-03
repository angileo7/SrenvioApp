import React from "react";
import { BrowserRouter as Router, Switch, Route, Link } from "react-router-dom";
import ReportWrapper from "./component/ReportWrapper";
import { TextSkydropx, Container } from "./style/style";

function App() {
  return (
    <div>
      <TextSkydropx>Skydropx</TextSkydropx>
      <Router>
        <Container>
          <Switch>
            <Route path="/">
              <ReportWrapper />
            </Route>
          </Switch>
        </Container>
      </Router>
    </div>
  );
}

export default App;
