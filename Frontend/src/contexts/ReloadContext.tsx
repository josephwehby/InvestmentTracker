import { useState, createContext, ReactNode, useContext } from "react";

interface ReloadContextProps {
  reload: boolean;
  setReload: (value: boolean) => void;
}

const ReloadContext = createContext<ReloadContextProps | undefined >(undefined);

interface ReloadContextProviderProps {
  children: ReactNode;
}

export function ReloadContextProvider ({children} : ReloadContextProviderProps) {
  const [reload, setReload] = useState<boolean>(false);

  return (
    <ReloadContext.Provider value={{ reload, setReload }}>
      {children}
    </ReloadContext.Provider>
  );
}


export function useReloadContext() {
  const reload = useContext(ReloadContext);
  if (reload == undefined) {
    throw new Error("useAuthContext must be used with a AuthContext");
  }
  return reload;
}

export default ReloadContext;