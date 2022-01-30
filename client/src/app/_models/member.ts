import { Photo } from "./photo";

export interface Member {
    id:         number;
    username:   string;
    photoUrl:   string;
    age:        number;
    alias:      string;
    created:    Date;
    gender:     string;
    lookingFor: string;
    interests:  string;
    country:    string;
    photos:     Photo[];
}


