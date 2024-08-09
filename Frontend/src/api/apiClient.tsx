import  axios from "axios";
import { useAuthContext } from "../contexts/AuthContext";

const apiClient = axios.create({
  baseURL: "https://localhost:7274/investments",
  withCredentials: true,
});

export configureAxiosInterceptors = () => {
  const { jwt, setJwt } = useAuthContext();
  apiClient.interceptors.request.use(
    (config) => {
      if (jwt) {
        config.headers.Authorization = `Bearer ${jwt}`;  
      }
       return config
    },
    (error) => {
      return Promise.reject(error);
    }
  );

  apiClient.interceptors.response.use(
    (response) => response,
    async (error) => {
    
    const original = error.config;
    if (error.response?.status === 401 && !original._retry) {
      original._retry = true;
     
      try {   
        const refresh = await axios.post("https://localhost:7274/auth/refresh", { 
          withCredentials: true, 
        });

        const newjwt = refresh.data;
        setJwt(newjwt);
        
        original.headers.Authorization = `Bearer ${jwt}`;
        return apiClient(original);

      } catch (error) {
        console.log(error);
        return Promise.reject(error);
      }
    }
    return Promise.reject(error);
   }
  );
};

export default apiClient;
