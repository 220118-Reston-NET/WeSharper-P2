export interface Profile {
    profileId: string,
    userId: string,
    firstName: string,
    lastName: string,
    profilePictureUrl: string,
    bio: string,
    createdAt: Date,
    user: {
        userName: string
    }
}