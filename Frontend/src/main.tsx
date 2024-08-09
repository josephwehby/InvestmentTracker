import ReactDOM from 'react-dom/client'
import App from './App.tsx'
import { AuthProvider } from './contexts/AuthContext.tsx'
import { configureAxiosInterceptors } "api/apiClient";

configureAxiosInterceptors();

ReactDOM.createRoot(document.getElementById('root')!).render(
  <AuthProvider> 
    <App />
  </AuthProvider>
)
