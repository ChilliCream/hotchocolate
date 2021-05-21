import React, {
  createContext,
  FC,
  useCallback,
  useContext,
  useEffect,
  useMemo,
  useState,
} from "react";

interface GroupMap {
  [groupId: string]: string | undefined;
}

type SetGroup = (groupId: string, value: string) => void;

interface TabGroupContextShape {
  readonly groups: GroupMap;
  setGroup: SetGroup;
}

const TabGroupContext = createContext<TabGroupContextShape>({
  groups: {},
  setGroup: () => {},
});

const getLocalStorageKey = (groupId: string) => `tab-${groupId}`;

export const TabGroupProvider: FC = ({ children }) => {
  const [groups, setGroups] = useState<GroupMap>({});

  const handleSetGroup: SetGroup = (groupId, value) => {
    if (groups[groupId] === value) {
      return;
    }

    setGroups({ ...groups, [groupId]: value });

    if (typeof window !== "undefined" && window.localStorage) {
      window.localStorage.setItem(getLocalStorageKey(groupId), value);
    }
  };

  return (
    <TabGroupContext.Provider value={{ groups, setGroup: handleSetGroup }}>
      {children}
    </TabGroupContext.Provider>
  );
};

type TabGroupReturn = [string, (value: string) => void];

export function useActiveTab(
  defaultValue: string,
  groupId?: string
): TabGroupReturn {
  const { groups, setGroup } = useContext(TabGroupContext);
  const localTabState = useState(defaultValue);

  if (!groupId) {
    return localTabState;
  }

  const activeGroupTab = useMemo(() => {
    let value: string | null | undefined = groups[groupId];

    if (!value && typeof window !== "undefined" && window.localStorage) {
      value = window.localStorage.getItem(getLocalStorageKey(groupId));
    }

    return value ?? defaultValue;
  }, [groups]);

  const handleSetGroupTab = useCallback(
    (value: string) => {
      setGroup(groupId, value);
    },
    [groupId]
  );

  return [activeGroupTab, handleSetGroupTab];
}

export function useIsClient() {
  const [isClient, setClient] = useState(false);

  useEffect(() => {
    setClient(true);
  }, []);

  return { isClient, key: isClient ? "client" : "server" };
}
