import {Guid} from "js-guid";
export type GroupDTO = {
    id?: number | Guid | string,
    groupName: string,
    description: string,
    memberLimit? : number,
    postId: number,
    userId: number,
    
}