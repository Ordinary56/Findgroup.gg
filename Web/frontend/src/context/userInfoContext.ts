import { createContext, useEffect, useState, useContext } from "react";
import { UserInfo } from "../api/Models/UserInfo"
import { apiService } from "../api/apiService";

type UserInfoContextProps = {
	user: UserInfo | null;
	loading: boolean;
	error: string | null;
	refreshUser: () => void;
}
type UserInfoProviderProps = {
	children: React.ReactNode
}

const UserInfoContext = createContext<UserInfoContextProps | undefined>({});

export const UserProvider = ({ children }: UserInfoProviderProps) => {
	const [user, setUser] = useState<UserInfo | undefined>();
	const [loading, setLoading] = useState(true);
	const [error, setError] = useState<string | null>(null);

	const fetchUser = async () => {
		setLoading(true);
		try {
			const data = await apiService.getUserInfo();
			setUser(data);
			setError(null);
		} catch (err) {
			setError((err as Error).message);
		} finally {
			setLoading(false);
		}
	};
	useEffect(() => {
		fetchUser();
	}, []);

	return (
		<UserInfoContext.Provider value= { user, loading, error } >
		{ children }
		</UserInfoContext.Provider>
	)
}

export const useUserInfo = () => {
	return useContext(UserInfoContext);
}
