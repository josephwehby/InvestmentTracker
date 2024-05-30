import React from "react";
import Home from "./pages/Home.tsx";
import { Route, Routes } from "react-router-dom";

function App() {

  return (
    <>
      <div className="App">
        <div id="Pages">
          <Routes>
            <Route path="/" Component={Home} />
          </Routes>
        </div>
      </div>
    </>
  )
}

export default App
