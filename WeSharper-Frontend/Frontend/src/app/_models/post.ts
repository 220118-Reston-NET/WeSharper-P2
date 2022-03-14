export interface Post{
    postId: string,
    userId: string,
    postContent: string,
    postPhoto: string,
    isDeleted: boolean,
    createdAt: Date,
    postComments: []
    postReacts: []
}