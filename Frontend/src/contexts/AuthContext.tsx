import { createContext, ReactNode, useContext, useState } from "react";

interface AuthContextProps {
  jwt: string;
  setJwt: (value: string) => void;
}

export const AuthContext = createContext<AuthContextProps | undefined>(undefined);

export function AuthProvider({children}: {children: ReactNode}) {
  const [jwt, setJwt] = useState<string>("");
  return (
    <AuthContext.Provider value={{ jwt, setJwt }}>
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