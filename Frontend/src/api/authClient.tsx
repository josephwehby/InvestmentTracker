import axios from "axios";

const authApiClient = axios.create({
  baseURL: "https://backend:5000/auth",
  headers: {
    "Accept": "application/json",
    "Content-Type": "application/json",
  },
  withCredentials: true,
});

export default authApiClient;
