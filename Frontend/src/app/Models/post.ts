export class Post {
  constructor(
    public id: number,
    public userId: number,
    public content: string,
    public imageUrl?: string,
    public creationDate?: string,
    public categoryId?: number,
    public categoryName?: string,
    public userName?: string,
    public userProfile?: string
  ) {}
}
