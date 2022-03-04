# We#er - Proposal - Social Media App

# Table of Contents
- [We#er - Proposal - Social Media App](#weer---proposal---social-media-app)
- [Table of Contents](#table-of-contents)
- [Overview](#overview)
- [Tables](#tables)
- [User Stories](#user-stories)
- [Scope Goals](#scope-goals)
- [Tech Stack](#tech-stack)
  - [Backend](#backend)
  - [Frontend](#frontend)
  - [Others](#others)
  
# Overview
> The social media app is a web application for connecting and communicating with friends. When users register for the app, users will specify their interests and hobbies. The app will be recommended to users based on their interests where they can connect with others of similar interests.

# Tables
- User: For storing user information(username, password, etc..).
- Profile: For storing user profile information(name, address?, profile picture, etc..).
- ProfilePicReact: For storing the profile reaction.
- ProfilePicComment: For storing the profile comment.
- ProfilePicCommentReact: For storing the profile comment reaction.
- Hobbie: For storing the interests/hobbies.
- Userhobbie: For storing user interests/hobbies.
- Friend: For storing the relationship between user (friend, family, etc...).
- Post: For storing the user posts(text, image, text + image, etc…).
- PostReact: For storing the user post reaction.
- PostShare: For storing the user post shared.
- Comment: For storing comments of user posts.
- CommentReact: For storing post comment reactions.
- Group: For storing group information.
- GroupUser: For storing group users information(joined date, isBanned, isApproved, isManager, etc..)
- GroupPost: For storing group posts.
- GroupPostShare: For storing group post shared.
- GroupPostReact: For storing group post reactions.
- GroupPostComment: For storing group posts comments.
- GroupPostCommentReact: For storing group post comment reactions.

# User Stories
- As a user, I should be able to create an account
- As a user, I should be able to login into my account
- As a user, I should be able to edit my profile information
- As a user, I should be able to set my hobbies
- As a user, I should be able to update my profile picture
- As a user, I should be able to react to my/my friend profile picture
- As a user, I should be able to comment to my/my friend profile picture
- As a user, I should be able to post to my timeline
- As a user, I should be able to delete my post
- As a user, I should be able to update my post
- As a user, I should be able to find a new friend
- As a user, I should be able to add a new friend
- As a user, I should be able to check the new feed
- As a user, I should be able to comment to my friend post
- As a user, I should be able to react to my friend post
- As a user, I should be able to reply to my friend comment
- As a user, I should be able to react to my friend comment reply
- As a user, I should be able to create/join a group
- As a user, I should be able to share my post in a group.
- As a group manager, I should be able to approve joining request
- As a group manager, I should be able to manage the group posts.
- As a group manager, I should be able to manage group users.
- As a group user, I should be able to create a new post.
- As a group user, I should be able to react to a group post.
- As a group user, I should be able to comment to group post.
- As a group user, I should be able to react to group post comment.
- As a group user, I should be able to share the group post to my timeline.


# Scope Goals
- Realtime Messenger(additional hidden message, group chat)
- Stories(keep up 24h, addtional save to collection after 24h options)
- Marketplace

# Tech Stack
## Backend
- [C#](https://docs.microsoft.com/en-us/dotnet/csharp/tour-of-csharp/)
- [LINQ](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/linq/)
- [JSON](https://www.json.org/json-en.html)
- [ADO.NET](https://docs.microsoft.com/en-us/dotnet/framework/data/adonet/ado-net-overview)
- [xUnit](https://xunit.net)
- [SeriLog](https://serilog.net)
- [Azure SQL Server](https://azure.microsoft.com/en-us/services/sql-database/campaign/)
- [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/)
- [ASP.NET Core Web APIs](https://dotnet.microsoft.com/en-us/apps/aspnet/apis)
- [ASP.NET Identity](https://docs.microsoft.com/en-us/aspnet/identity/overview/getting-started/introduction-to-aspnet-identity)
- [Swagger](https://swagger.io)
## Frontend
- [Angular](https://angular.io)
- [Typescript](https://www.typescriptlang.org)
- [Javascript](https://www.javascript.com)
- [HTML](https://www.w3schools.com/html/)
- [CSS](https://www.w3schools.com/css/)
- [Bootstrap](https://getbootstrap.com)
- [JSON](https://www.json.org/json-en.html)
## Others
- [Visual Studio Code](https://code.visualstudio.com)
- [DBeaver](https://dbeaver.io)
- [Git](https://git-scm.com)
- [GitHub](https://github.com)
- [SonarQube](https://www.sonarqube.org)
- [Markdown](https://daringfireball.net/projects/markdown/)
- [Moqups](https://moqups.com)

[Back To Top](#weer---proposal---social-media-app)