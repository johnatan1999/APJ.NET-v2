export interface BaseModel {
    id: string;   
}

export interface StateModel extends BaseModel {
    state: number;
}
export interface ApjTest extends StateModel {
    name?: string;
    description?: string;
    age?: number;
    data?: Date;
    child?: any[];
}

export interface Menu extends BaseModel {
    url: string;
    label: string;
    rank: number;
    idParent?: string;
}

export interface Users extends StateModel {
    login: string;
    username: string;
    token?: string;
}

export interface Role extends BaseModel {
    description: string;
}