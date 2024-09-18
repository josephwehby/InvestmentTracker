import  axios from "axios";

const apiClient = axios.create({
  baseURL: "/investments",
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
        const refresh = await axios.get("/auth/refresh", { 
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
