const useIdentityVisual = () => {
  let paper: HTMLElement | null;

  const loadPaper = () => (paper = document.getElementById("paper"));

  const openLogin = () => {
    if (paper) {
      paper.style.transform = "rotateY(0deg)";
    }
  };

  const openRegister = () => {
    if (paper) {
      paper.style.transform = "rotateY(180deg)";
    }
  };

  return {
    openRegister,
    openLogin,
    loadPaper,
  };
};

export default useIdentityVisual;
