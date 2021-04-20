import React, { useState, useEffect } from "react";
import { makeStyles, Theme, createStyles } from "@material-ui/core/styles";
import {
  HttpTransportType,
  HubConnection,
  HubConnectionBuilder,
} from "@microsoft/signalr";
import { Badge, Button, Container } from "@material-ui/core";
// import { MailIcon } from "@material-ui/icons";
import MailIcon from "@material-ui/icons/Mail";

import "./App.css";

import NavBar from "./components/navbar/NavBar";

const useStyles = makeStyles((theme: Theme) =>
  createStyles({
    root: {
      "& > *": {
        margin: theme.spacing(1),
      },
    },
  })
);

const App: React.FC = () => {
  // const classes = useStyles();

  const hubConnection = new HubConnectionBuilder()
    .withUrl("https://localhost:5001/notify", {
      skipNegotiation: true,
      transport: HttpTransportType.WebSockets,
      accessTokenFactory: () =>
        "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJlbWFpbCI6ImRhQGRhLmNvbSIsIm5hbWVpZCI6ImI1YWMyN2I3LTA4NjctNGNjOC05MTM5LWE3MmZiMTRlZGEzNSIsInJvbGUiOiIiLCJuYmYiOjE2MTg4MjAyOTgsImV4cCI6MTYxOTQyNTA5OCwiaWF0IjoxNjE4ODIwMjk4fQ.FHLA__BXBuSHy4qbqigXskzwt8I6QO35_RjwYRz2hYU",
    })
    // .configureLogging(LogLevel.Trace)
    .build();

  hubConnection.start();

  var list: string[] = [];

  interface MessageProps {
    HubConnection: HubConnection;
  }

  const Messages: React.FC<MessageProps> = (messageProps) => {
    const [date, setDate] = useState<Date>();

    useEffect(() => {
      messageProps.HubConnection.on("rest", (message) => {
        list.push(message);
        setDate(new Date());
      });
      // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);

    const classes = useStyles();

    return (
      <>
        <div className={classes.root}>
          <Badge badgeContent={4} color='primary'>
            <MailIcon />
          </Badge>
          <Badge badgeContent={4} color='secondary'>
            <MailIcon />
          </Badge>
          <Badge badgeContent={4} color='error'>
            <MailIcon />
          </Badge>
        </div>
        <Badge badgeContent={4} color='error'>
          <MailIcon />
        </Badge>
        {list.map((message, index) => (
          <p key={`message${index}`}>{message}</p>
        ))}
      </>
    );
  };

  const SendMessage: React.FC = () => {
    const [message, setMessage] = useState("");

    const messageChange = (event: React.ChangeEvent<HTMLInputElement>) => {
      if (event && event.target) {
        setMessage(event.target.value);
      }
    };

    const messageSubmit = (event: React.MouseEvent) => {
      if (event) {
        hubConnection.invoke("notify", message);

        // fetch("https://localhost:5001/api/Appointment", {
        //   "method": "POST",
        //   "headers": {
        //     Accept: "application/json",
        //     "Content-Type": "application/json",
        //     Authorization: "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJlbWFpbCI6ImRhQGRhLmNvbSIsIm5hbWVpZCI6ImI1YWMyN2I3LTA4NjctNGNjOC05MTM5LWE3MmZiMTRlZGEzNSIsInJvbGUiOiIiLCJuYmYiOjE2MTg4MjAyOTgsImV4cCI6MTYxOTQyNTA5OCwiaWF0IjoxNjE4ODIwMjk4fQ.FHLA__BXBuSHy4qbqigXskzwt8I6QO35_RjwYRz2hYU"
        //   },
        //   body: JSON.stringify({
        //     message: message
        //   })
        // });

        setMessage("");
      }
    };

    return (
      <>
        <label>Enter your Message</label>
        <input type='text' onChange={messageChange} value={message} />
        <Button onClick={messageSubmit}>Add Message</Button>
      </>
    );
  };

  // return (
  //   <>
  //     <SendMessage />
  //     <Messages HubConnection={hubConnection}></Messages>
  //   </>
  // );
  return (
    <Container>
      <NavBar />
    </Container>
  );
};

export default App;

// import React from 'react';
// import './App.css';
// import { Notify } from './hubs/notifyHub'

// function App() {
//   return (
//     <div className="App">

//       <Notify />
//       {/* <header className="App-header">
//         <img src={logo} className="App-logo" alt="logo" />
//         <p>
//           Edit <code>src/App.tsx</code> and save to reload.
//         </p>
//         <a
//           className="App-link"
//           href="https://reactjs.org"
//           target="_blank"
//           rel="noopener noreferrer"
//         >
//           Learn React
//         </a>
//       </header> */}
//     </div>
//   );
// }

// export default App;
