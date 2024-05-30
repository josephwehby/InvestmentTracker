import Home from "./pages/Home.tsx";
import ReactDOM from "react-dom/client";
import { BrowserRouter, Route, Routes } from "react-router-dom";

function App() {

  return (
    <>
      <div className="App">
        <div id="Pages">
          <BrowserRouter>
            <Routes>
              <Route path="/portfolio" element={<Home />} />
            </Routes>
          </BrowserRouter>
        </div>
      </div>
    </>
  );
}

export default App;
