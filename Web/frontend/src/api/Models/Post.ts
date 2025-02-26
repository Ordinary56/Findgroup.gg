import { Category } from "./Category"
import { Group } from "./Group"

export type Post = {
    id: number
    title : string, 
    content: string,
    category : Category
    createdDate : string,
    userId : string,
    group:Group,
}