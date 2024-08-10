import  axios from "axios";
import { useEffect } from "react";

const apiClient = axios.create({
  baseURL: "https://localhost:7274/investments",
  withCredentials: true,
  headers: {
    "Content-Type": "application/json",
  }
});

apiClient.interceptors.request.use(
  (config) => {
    const jwt = localStorage.getItem("jwt");
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
        const refresh = await axios.get("https://localhost:7274/auth/refresh", { 
          withCredentials: true, 
        });

        const newjwt = refresh.data;
        console.log("Refreshed my jwt!");
        
        localStorage.setItem("jwt", newjwt);

        apiClient.defaults.headers.Authorization = `Bearer ${newjwt}`;
        original.headers.Authorization = `Bearer ${newjwt}`;
        
        return apiClient(original);

      } catch (refresh_error) {
          console.error("Response error: ", refresh_error);
          return Promise.reject(refresh_error);
      }
    }
  return Promise.reject(error);
  }
);

export default apiClient;
