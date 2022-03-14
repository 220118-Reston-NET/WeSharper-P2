using Microsoft.EntityFrameworkCore;
using WeSharper.DatabaseManagement.Interfaces;
using WeSharper.Models;

namespace WeSharper.DatabaseManagement.Implements
{
    public class FriendManagementDL : IFriendManagementDL
    {
        private readonly WeSharperContext _context;

        public FriendManagementDL(WeSharperContext context)
        {
            _context = context;
        }

        public async Task<Friend> AcceptFriend(string p_userID, string p_friendID)
        {
            Friend _currentRelationship = await _context.Friends.FirstOrDefaultAsync(p => p.AcceptedUserId.Equals(p_userID)
                                                                                && p.RequestedUserId.Equals(p_friendID));
            if (_currentRelationship != null)
            {
                _currentRelationship.IsAccepted = true;
                _currentRelationship.CreatedAt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"));
                await _context.SaveChangesAsync();
            }
            return _currentRelationship;
        }

        public async Task<Friend> AddFriend(string p_userID, string p_friendID)
        {
            Friend _currentRelationship = await _context.Friends.FirstOrDefaultAsync(p => p.AcceptedUserId.Equals(p_userID)
                                                                                && p.RequestedUserId.Equals(p_friendID)
                                                                            || p.AcceptedUserId.Equals(p_friendID)
                                                                                && p.RequestedUserId.Equals(p_userID));
            if (_currentRelationship != null)
            {
                _currentRelationship.RequestedUserId = p_userID;
                _currentRelationship.AcceptedUserId = p_friendID;
                _currentRelationship.Relationship = "Friend";
                _currentRelationship.CreatedAt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"));
                await _context.SaveChangesAsync();
            }
            else
            {
                Friend _newFriendShip = new Friend()
                {
                    RelationshipId = Guid.NewGuid().ToString(),
                    RequestedUserId = p_userID,
                    AcceptedUserId = p_friendID,
                    IsAccepted = false,
                    Relationship = "Friend",
                    CreatedAt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"))
                };
                await _context.AddAsync(_newFriendShip);
                await _context.SaveChangesAsync();

                return _newFriendShip;
            }

            return _currentRelationship;
        }

        public async Task<List<Friend>> GetAllFriends()
        {
            List<Friend> _listOfAllRelationship = new List<Friend>();
            _listOfAllRelationship = await _context.Friends
                                        .Select(f => new Friend
                                        {
                                            RelationshipId = f.RelationshipId,
                                            RequestedUserId = f.RequestedUserId,
                                            AcceptedUserId = f.AcceptedUserId,
                                            IsAccepted = f.IsAccepted,
                                            Relationship = f.Relationship,
                                            CreatedAt = f.CreatedAt,
                                            AcceptedUser = new ApplicationUser
                                            {
                                                Profiles = f.AcceptedUser.Profiles
                                                            .Select(p => new Profile
                                                            {
                                                                FirstName = p.FirstName,
                                                                LastName = p.LastName,
                                                                ProfilePictureUrl = p.ProfilePictureUrl
                                                            }).ToList()
                                            },
                                            RequestedUser = new ApplicationUser
                                            {
                                                Profiles = f.RequestedUser.Profiles
                                                            .Select(p => new Profile
                                                            {
                                                                FirstName = p.FirstName,
                                                                LastName = p.LastName,
                                                                ProfilePictureUrl = p.ProfilePictureUrl
                                                            }).ToList()
                                            },
                                        }).ToListAsync();

            return _listOfAllRelationship;
        }

        public async Task<List<Profile>> GetAllRecommenedFriendByUserID(string p_userID)
        {
            List<Profile> _listAllProfiles = await _context.Profiles.Where(p => p.UserId != p_userID).ToListAsync();

            List<Profile> _result = new List<Profile>();

            //Get List Of Friends, IncomingFriends and OutcomingFriends
            List<Friend> _listOfFriends = await _context.Friends.Where(p => p.AcceptedUserId.Equals(p_userID) && p.IsAccepted
                                                                || p.RequestedUserId.Equals(p_userID) && p.IsAccepted
                                                                || p.AcceptedUserId.Equals(p_userID)
                                                                    && p.IsAccepted.Equals(false)
                                                                    && p.Relationship.Equals("Friend")
                                                                || p.RequestedUserId.Equals(p_userID)
                                                                    && p.IsAccepted.Equals(false)
                                                                    && p.Relationship.Equals("Friend")).ToListAsync();

            foreach (var item in _listAllProfiles)
            {
                if (_listOfFriends.All(p => p.AcceptedUserId != item.UserId) && _listOfFriends.All(p => p.RequestedUserId != item.UserId))
                {
                    _result.Add(item);
                }
            }

            return _result;
        }

        public async Task<List<Post>> GetFriendPostsByFriendID(string p_friendID)
        {
            var _result = await _context.Posts
                                .Select(p => new Post
                                {
                                    PostId = p.PostId,
                                    UserId = p.UserId,
                                    PostContent = p.PostContent,
                                    PostPhoto = p.PostPhoto,
                                    IsDeleted = p.IsDeleted,
                                    CreatedAt = p.CreatedAt,
                                    PostComments = p.PostComments
                                                    .Select(pc => new PostComment
                                                    {
                                                        CommentId = pc.CommentId,
                                                        UserId = pc.UserId,
                                                        PostComment1 = pc.PostComment1,
                                                        IsDeleted = pc.IsDeleted,
                                                        CreatedAt = pc.CreatedAt,
                                                        PostCommentReacts = pc.PostCommentReacts
                                                                                .Select(pcr => new PostCommentReact
                                                                                {
                                                                                    PostCommentReactId = pcr.PostCommentReactId,
                                                                                    UserId = pcr.UserId,
                                                                                    ReactId = pcr.ReactId
                                                                                }).ToList()
                                                    }).ToList(),
                                    PostReacts = p.PostReacts
                                                    .Select(pr => new PostReact
                                                    {
                                                        PostReactId = pr.PostReactId,
                                                        UserId = pr.UserId,
                                                        ReactId = pr.ReactId
                                                    }).ToList()
                                })
                                .Where(p => p.UserId.Equals(p_friendID) && p.IsDeleted.Equals(false))
                                .OrderByDescending(p => p.CreatedAt)
                                .ToListAsync();

            return _result;
        }

        public async Task<Profile> GetFriendProfileByFriendID(string p_friendID)
        {
            return await _context.Profiles.Where(p => p.UserId.Equals(p_friendID))
                                    .Select(p => new Profile
                                    {
                                        UserId = p.UserId,
                                        FirstName = p.FirstName,
                                        LastName = p.LastName,
                                        ProfilePictureUrl = p.ProfilePictureUrl,
                                        Bio = p.Bio,
                                        CreatedAt = p.CreatedAt,
                                    }).FirstAsync();
        }

        public async Task<string> GetRelationshipByFriendID(string p_userID, string p_friendID)
        {
            if (_context.Friends.Any(p => p.AcceptedUserId == p_userID))
            {
                if (_context.Friends.Any(p => p.RequestedUserId == p_friendID))
                {
                    var relationship = _context.Friends.FirstOrDefault(p => p.AcceptedUserId.Equals(p_userID)
                                                                       && p.RequestedUserId.Equals(p_friendID)
                                                                       && p.Relationship != null);

                    if (relationship != null)
                    {
                        if (relationship.IsAccepted)
                        {
                            return "Friend";
                        }
                        else
                        {
                            return "Incoming";
                        }
                    }
                }
                else if (_context.Friends.Any(p => p.RequestedUserId == p_userID))
                {
                    if (_context.Friends.Any(p => p.AcceptedUserId == p_friendID))
                    {
                        var relationship = await _context.Friends.FirstOrDefaultAsync(p => p.RequestedUserId.Equals(p_userID)
                                                                           && p.AcceptedUserId.Equals(p_friendID)
                                                                           && p.Relationship != null);

                        if (relationship != null)
                        {
                            if (relationship.IsAccepted)
                            {
                                return "Friend";
                            }
                            else
                            {
                                return "Outcoming";
                            }
                        }
                        else
                        {
                            return "NotFriend";
                        }
                    }
                    else
                    {
                        return "NotFriend";
                    }
                }
            }
            if (_context.Friends.Any(p => p.RequestedUserId == p_userID))
            {
                if (_context.Friends.Any(p => p.AcceptedUserId == p_friendID))
                {
                    var relationship = await _context.Friends.FirstOrDefaultAsync(p => p.RequestedUserId.Equals(p_userID)
                                                                       && p.AcceptedUserId.Equals(p_friendID)
                                                                       && p.Relationship != null);

                    if (relationship != null)
                    {
                        if (relationship.IsAccepted)
                        {
                            return "Friend";
                        }
                        else
                        {
                            return "Outcoming";
                        }
                    }
                    else
                    {
                        return "NotFriend";
                    }
                }
                else
                {
                    return "NotFriend";
                }
            }
            return "NotFriend";
        }

        public async Task<string> GetUserNameForFriendID(string p_friendID)
        {
            var user = await _context.Users.FirstOrDefaultAsync(p => p.Id.Equals(p_friendID));
            return user.UserName;
        }

        public async Task<Friend> RemoveFriend(string p_userID, string p_friendID)
        {
            Friend _currentRelationship = await _context.Friends.FirstOrDefaultAsync(p => p.AcceptedUserId.Equals(p_userID)
                                                                                && p.RequestedUserId.Equals(p_friendID)
                                                                            || p.AcceptedUserId.Equals(p_friendID)
                                                                                && p.RequestedUserId.Equals(p_userID));
            if (_currentRelationship != null)
            {
                _currentRelationship.IsAccepted = false;
                _currentRelationship.Relationship = null;
                _currentRelationship.CreatedAt = null;
                await _context.SaveChangesAsync();
            }

            return _currentRelationship;
        }
    }
}