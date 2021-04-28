import { makeStyles } from "@material-ui/core/styles";

const useStyles = makeStyles((theme) => ({
  paper: {
    marginTop: theme.spacing(8),
    display: "flex",
    flexDirection: "column",
    alignItems: "center",
    position: "relative",
    transformStyle: "preserve-3d",
    transition: "1s",
  },
  submit: {
    margin: theme.spacing(3, 0, 2),
  },
}));

export default useStyles;
