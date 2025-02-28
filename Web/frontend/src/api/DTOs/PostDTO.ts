import {Guid} from "js-guid";
export type PostDTO = {
    id?: number | undefined,
    title: string,
    content: string,
    createdDate : Date,
    userId : string, 
    categoryId: number,
    groupId: Guid | string
}

