import { Helmet } from "react-helmet";
import { Box, Container, Grid } from "@material-ui/core";
import AccountProfile from "./details/AccountProfile";
import AccountProfileDetails from "./details/AccountProfileDetails";

import { makeStyles } from "@material-ui/core/styles";

const useStyles = makeStyles((theme) => ({
  box: {
    backgroundColor: "background.default",
    minHeight: "100%",
    py: 3,
  },
}));

const Account = () => {
  const classes = useStyles();
  return (
    <>
      <Helmet>
        <title>Account | Material Kit</title>
      </Helmet>
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
