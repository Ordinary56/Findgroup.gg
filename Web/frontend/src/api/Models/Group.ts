import { Message } from "./Message"
import { User } from "./User"

export type Group = {
    id: string,
    groupName: string,
    description: string,
    memberLimit: number,
    users: User[],
    creator: User | undefined,
    messages: Message[]
}
