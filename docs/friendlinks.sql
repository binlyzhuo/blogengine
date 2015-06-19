use Blog
GO
create table be_FriendLink
(
   LinkID int identity primary key,
   Name nvarchar(100) not null default '',
   Url nvarchar(500) not null default '',
   Keywords nvarchar(500) not null default '',
   Contact nvarchar(500) not null default '',
   AddDate datetime not null default getdate(),
   BlogGuid nvarchar(500) not null default '',
   LinkGuid nvarchar(500) not null default ''
)
GO