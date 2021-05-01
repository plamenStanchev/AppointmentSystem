import { useEffect } from "react";
import { Box, Container, Grid } from "@material-ui/core";
import AccountProfile from "./details/AccountProfile";
import AccountProfileDetails from "./details/AccountProfileDetails";
import Doctor from "./create/doctor/Doctor";

import useAccount from "./hooks/useAccount";

import { makeStyles } from "@material-ui/core/styles";
import useAccountDetails from "./hooks/useAccountDetails";

const useStyles = makeStyles((theme) => ({
  box: {
    backgroundColor: "background.default",
    minHeight: "100%",
    py: 3,
  },
}));

const Account = () => {
  const classes = useStyles();
  const { getDoctor } = useAccount();

  const { accountId } = useAccountDetails();

  useEffect(() => {
    (async () => {
      await getDoctor(accountId);
    })();
  });

  if (accountId) {
    return <Doctor />;
  }
  return (
    <>
      <Box className={classes.box}>
        <Container maxWidth='lg'>
          <Grid container spacing={3}>
            <Grid item lg={4} md={6} xs={12}>
              <AccountProfile />
            </Grid>
            <Grid item lg={8} md={6} xs={12}>
              <AccountProfileDetails />
            </Grid>
          </Grid>
        </Container>
      </Box>
    </>
  );
};

export default Account;
