import SignIn from "features/sign-in";

import PAGES from "navigation/pages";

const publicRoutes = [
  {
    exact: true,
    name: "Sign In",
    key: PAGES.signIn,
    route: PAGES.signIn,
    component: <SignIn />,
  },
];

export default publicRoutes;
