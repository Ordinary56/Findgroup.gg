import Cookies from "js-cookie";

export const tokenService = {
    getToken: (): string | undefined => Cookies.get("accessToken"),
    setToken: (token: string) => Cookies.set("accessToken", token, {
        sameSite: "strict",
        secure: true,
        expires : 1 / 1440
    }),
    clearTokens: (): void => {
        Cookies.remove("accessToken");
    }
}