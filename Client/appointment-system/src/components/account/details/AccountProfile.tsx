import {
  Avatar,
  Box,
  Button,
  Card,
  CardActions,
  CardContent,
  Divider,
  Typography,
} from "@material-ui/core";

import { makeStyles } from "@material-ui/core/styles";

const useStyles = makeStyles((theme) => ({
  box: {
    alignItems: "center",
    display: "flex",
    flexDirection: "column",
  },
  avatar: {
    height: 100,
    width: 100,
  },
}));

const user = {
  avatar: "/static/images/avatars/avatar_6.png",
  city: "Los Angeles",
  country: "USA",
  jobTitle: "Senior Developer",
  name: "Katarina Smith",
  timezone: "GTM-7",
};

interface Props {}

const AccountProfile = (props: Props) => {
  const classes = useStyles();
  return (
    <Card {...props}>
      <CardContent>
        <Box className={classes.box}>
          <Avatar className={classes.avatar} src={user.avatar} />
          <Typography color='textPrimary' gutterBottom variant='h3'>
            {user.name}
          </Typography>
          <Typography color='textSecondary' variant='body1'>
            {`${user.city} ${user.country}`}
          </Typography>
          <Typography color='textSecondary' variant='body1'>
            {`TIME`}
          </Typography>
        </Box>
      </CardContent>
      <Divider />
      <CardActions>
        <Button color='primary' fullWidth variant='text'>
          Upload picture
        </Button>
      </CardActions>
    </Card>
  );
};

export default AccountProfile;
