import { useState, createContext, ReactNode } from "react";

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

export default ReloadContext;