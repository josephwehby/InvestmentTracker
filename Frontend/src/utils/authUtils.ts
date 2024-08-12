import { jwtDecode, JwtPayload } from "jwt-decode";

export function setAuthData(token: string) {
  localStorage.setItem("jwt", token);
  console.log(token);
  try {
    const decode = jwtDecode<JwtPayload>(token);
    const name = decode.sub || "Default";
    localStorage.setItem("username", name);
  } catch (error) {
    console.error("Unable to decode jwt");
  }
}
