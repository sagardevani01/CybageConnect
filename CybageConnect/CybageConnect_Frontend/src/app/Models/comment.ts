export class Comment{
    constructor(
        public id:number,
        public userId:number,
        public postId:number,
        public content:string,
        public creationDate:string,
        public userName?:string
    ){}
}