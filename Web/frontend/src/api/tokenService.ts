import { useState } from "react"

const [accessToken, setAccessToken] = useState("");
export const tokenService = {
    getToken : () : string => accessToken,
    setToken: (token: string) => setAccessToken(token)
}