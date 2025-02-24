import { useEffect, useState } from "react"
import { Group } from "../api/Models/Group";
import { AxiosResponse } from "axios";
import axiosInstance from "../api/axiosInstance";

const useGroup = (id: number) => {
    const [group, setGroup] = useState<Group>();
    const [error, setError] = useState<Error | undefined>();
    useEffect(() => {
        const fetchGroup = async () => {
            const {data, status, statusText} : AxiosResponse = await axiosInstance.get("/Group");
            if(status !== 200 || statusText)
        }
    });

    return {group, error};
}