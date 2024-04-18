export class User {
  constructor(
    public id?: number,
    public firstName?: string,
    public lastName?: string,
    public username?: string,
    public password?: string,
    public email?: string,
    public designation?: string,
    public departament?: string,
    public profilePicture?: string,
    public location?: string,
    public phone?: string
  ) {}
}
