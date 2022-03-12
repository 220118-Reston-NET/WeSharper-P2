import { Profile } from "./profile";

export interface Friend {
    relationshipId: string,
    requestUserId: string,
    acceptedUserId: string,
    isAccepted: boolean,
    createdAt: Date,
    acceptedUser: [
        profiles: Profile
    ],
    requestedUser: [
        profiles: Profile
    ],
}