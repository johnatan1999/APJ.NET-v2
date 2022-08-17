export default interface ApiResponse<T> {
    success: boolean;
    message: string;   
    content: T;
    objectModel: any;
}

export interface ApiListResponse<T> {
    success: boolean;
    message: string;   
    content: {
        totalCount: number;
        limit: number;
        page: number;
        data: T[];
    };
    objectModel: any;
}