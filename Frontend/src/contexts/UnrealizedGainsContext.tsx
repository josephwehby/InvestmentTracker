import {useState, createContext, ReactNode } from 'react';

interface UnrealizedGainsContextProps {
  unrealizedGains: number;
  setUnrealizedGains: (value: number) => void;
}

const UnrealizedGainsContext = createContext<UnrealizedGainsContextProps | undefined>(undefined);

interface UnrealizedGainsProviderProps {
  children: ReactNode;
}

export function UnrealizedGainsProvider({children}: UnrealizedGainsProviderProps) {
  const [unrealizedGains, setUnrealizedGains] = useState<number>(0);

  return (
    <UnrealizedGainsContext.Provider value={{ unrealizedGains, setUnrealizedGains}}>
      {children}
    </UnrealizedGainsContext.Provider>
  );
}

export default UnrealizedGainsContext;
