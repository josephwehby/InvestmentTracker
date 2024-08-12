import { createContext, ReactNode, useContext, useState } from "react";
import authApiClient from "../api/authClient";
import { jwtDecode, JwtPayload } from "jwt-decode";
import { setAuthData } from "../utils/authUtils";

interface AuthContextProps {
  isAuthenticated: boolean;
  setIsAuthenticated: (value: boolean) => void;
  login: (username: string, password: string) => Promise<void>;
  register: (username: string, password: string) => Promise<void>;
}

export const AuthContext = createContext<AuthContextProps | undefined>(undefined);

export function AuthProvider({children}: {children: ReactNode}) {
  const [isAuthenticated, setIsAuthenticated] = useState<boolean>(false);

  const login = async (username: string, password: string) => {
    
    try {
      const response = await authApiClient.post("/login", {
        username,
        password,
      });

      const token = response.data;
      setAuthData(token);
      setIsAuthenticated(true);
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
    <AuthContext.Provider value={{ isAuthenticated, setIsAuthenticated, login, register }}>
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
