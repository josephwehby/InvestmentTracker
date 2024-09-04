import  axios from "axios";
import apiClient from "../api/apiClient";
import { useAuthContext } from "../contexts/AuthContext";
import { useNavigate } from "react-router-dom";
import { useState } from "react";


function useAxios() {
  const { setIsAuthenticated } = useAuthContext();
  const navigate = useNavigate();
  const [axiosInstance] = useState(() => {
  const instance = apiClient;

  instance.interceptors.request.use(
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

  instance.interceptors.response.use(
    (response) => response,
    async (error) => {
      
      const original = error.config;
      if (error.response?.status === 401 && !original._retry) {
        original._retry = true;
       
        try {   
          const refresh = await axios.get("https://backend:5000/auth/refresh", { 
            withCredentials: true, 
          });

          const newjwt = refresh.data;
          console.log("Refreshed my jwt!");
           
          localStorage.setItem("jwt", newjwt);
          
          setIsAuthenticated(true);

          instance.defaults.headers.Authorization = `Bearer ${newjwt}`;
          original.headers.Authorization = `Bearer ${newjwt}`;
          
          return instance(original);

        } catch (refresh_error) {
            console.error("Response error: ", refresh_error);
            setIsAuthenticated(false);
            navigate("/");
            return Promise.reject(refresh_error);
        }
      }
     
      return Promise.reject(error);
    }
  );
  
  return instance;

  });

  return axiosInstance;
}

export default useAxios;
