import { createContext, ReactNode, useContext, useState } from "react";
import authApiClient from "../api/authClient";
import { jwtDecode, JwtPayload } from "jwt-decode";

interface AuthContextProps {
  jwt: string;
  setJwt: (value: string) => void;
  login: (username: string, password: string) => Promise<void>;
  register: (username: string, password: string) => Promise<void>;
}

export const AuthContext = createContext<AuthContextProps | undefined>(undefined);

export function AuthProvider({children}: {children: ReactNode}) {
  const [jwt, setJwt] = useState<string>("");

  const login = async (username: string, password: string) => {
    try {
      const response = await authApiClient.post("/login", {
        username,
        password,
      });

      const token = response.data;
      console.log(token);
      setJwt(token);
      
      const decode = jwtDecode<JwtPayload>(token);
      const name = decode.sub || "Default";
      localStorage.setItem("username", name);
    } catch (error) {
      throw new Error("Invalid login credentials or network error.");
    }
  };

  const register = async (username: string, password: string) => {
    try {
      const response = await authApiClient("/register", { 
        username,
        password,
      });
    } catch (error) {
      throw new Error("Username already exists or network error");
    }
  };

  return (
    <AuthContext.Provider value={{ jwt, setJwt, login, register }}>
      {children}
    </AuthContext.Provider>
  );
}

export function useAuthContext() {
  const auth = useContext(AuthContext);
  if (auth == undefined) {
    throw new Error("useAuthContext must be used with a AuthContext");
  }
  return auth;
}
