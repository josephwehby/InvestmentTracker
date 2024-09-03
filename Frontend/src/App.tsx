import { useEffect } from "react";
import { Route, Routes, useNavigate } from "react-router-dom";
import { setAuthData } from "./utils/authUtils";
import { useAuthContext } from "./contexts/AuthContext";
import LoginPage from "./pages/LoginPage.tsx";
import Portfolio from "./pages/Portfolio.tsx"
import Registration from "./pages/Register.tsx";
import Home from "./pages/Home.tsx";
import ProtectedRoute from "./components/ProtectedRoute";
import authApiClient from "./api/authClient";

function App() {
  
  const navigate = useNavigate();
  const { setIsAuthenticated } = useAuthContext();
  
  async function checkRefresh() {
    try {
      const response = await authApiClient.get("/refresh");
      if (response.status != 200) {
        throw new Error("Unable to process refresh token");
      }

      const token = response.data;
      setAuthData(token);
      setIsAuthenticated(true);
      navigate("/portfolio");
    
    } catch (error) {
      setIsAuthenticated(false);
      console.error("No refresh token. going to home page", error);
      navigate("/");
    }
  }
  useEffect(() => {
    checkRefresh();
  }, []);

  return (
    <>
      <div className="App">
        <div id="Pages">
          <Routes>
            <Route path="/" element={<Home />} />
            <Route path="/portfolio" element={<ProtectedRoute><Portfolio /> </ProtectedRoute>} />
            <Route path="/register" element={<Registration />} />
            <Route path="/login" element={<LoginPage />} />
          </Routes>
        </div>
      </div>
    </>
  );
}

export default App;
