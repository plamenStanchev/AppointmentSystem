import { useEffect, useState } from "react";

import {
  HubConnection,
  HubConnectionBuilder,
  LogLevel,
} from "@microsoft/signalr";

import Constants from "../constants";

const useHub = (path: string) => {
  const hubConnectionBuilder = new HubConnectionBuilder()
    .withUrl(`${Constants.ApiUrl}/${path}`)
    .configureLogging(LogLevel.Error)
    .withAutomaticReconnect()
    .build();

  const [hubConnection, setHubConnection] =
    useState<HubConnection>(hubConnectionBuilder);

  useEffect(() => {
    const newConnection = hubConnectionBuilder;

    setHubConnection(newConnection);
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  return { hubConnection };
};

export default useHub;
