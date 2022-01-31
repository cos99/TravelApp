import { User } from "./user";

export class UserParams {
  gender: string;
  minAge = 18;
  maxAge = 150;
  pageNumber = 1;
  pageSize = 5;
  orderBy='lookingFor'

  constructor(user:User) {
    this.gender = user.gender === "male" ? "female" : "male";
  }
}
