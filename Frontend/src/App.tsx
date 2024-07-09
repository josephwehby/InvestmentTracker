import Home from "./pages/Home.tsx";
import Portfolio from "./pages/Portfolio.tsx"
import { BrowserRouter, Route, Routes } from "react-router-dom";

function App() {

  return (
    <>
      <div className="App">
        <div id="Pages">
          <BrowserRouter>
            <Routes>
              <Route path="/portfolio" element={<Portfolio />} />
              <Route path="/" element={<Home />} />
            </Routes>
          </BrowserRouter>
        </div>
      </div>
    </>
  );
}

export default App;
