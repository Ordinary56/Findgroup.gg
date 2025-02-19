import Cookies from "js-cookie";

export const tokenService = {
    getToken: (): string | undefined => Cookies.get("accessToken"),
}