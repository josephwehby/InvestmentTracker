import axios from "axios";

const authApiClient = axios.create({
  baseURL: "/auth",
  headers: {
    "Accept": "application/json",
    "Content-Type": "application/json",
  },
  withCredentials: true,
});

export default authApiClient;
