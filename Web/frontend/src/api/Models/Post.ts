import { Category } from "./Category"

export type Post = {
    id: number
    title : string, 
    content: string,
    category : Category
    createdDate : string,
    userId : string,
}