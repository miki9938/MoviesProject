USE [master]
GO
/****** Object:  Database [Movies]    Script Date: 2014-01-30 01:46:41 ******/
CREATE DATABASE [Movies]
 CONTAINMENT = NONE
GO
ALTER DATABASE [Movies] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Movies].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Movies] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Movies] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Movies] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Movies] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Movies] SET ARITHABORT OFF 
GO
ALTER DATABASE [Movies] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Movies] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [Movies] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Movies] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Movies] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Movies] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Movies] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Movies] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Movies] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Movies] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Movies] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Movies] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Movies] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Movies] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Movies] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Movies] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Movies] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Movies] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Movies] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Movies] SET  MULTI_USER 
GO
ALTER DATABASE [Movies] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Movies] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Movies] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Movies] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
EXEC sys.sp_db_vardecimal_storage_format N'Movies', N'ON'
GO
USE [Movies]
GO
/****** Object:  StoredProcedure [dbo].[update_score]    Script Date: 2014-01-30 01:46:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[update_score] (@relationId int)
as

begin 

declare @yes as int
declare @no as int
declare @sum as int
declare @average as int

set @yes = (select count(*) from users_vote where relation_id = @relationId and vote = 1)
set @no =  (select count(*) from users_vote where relation_id = @relationId and vote = 0)
set @sum = @yes + @no
set @average = @sum - @no

update movie_relation
set vote_count = @average
where id = @relationId

end




GO
/****** Object:  UserDefinedFunction [dbo].[get_score]    Script Date: 2014-01-30 01:46:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create function [dbo].[get_score] (@relation_id int)
returns int
as

begin

declare @const int
declare @average int
declare @vote_count int
declare @massive_vote_count int
declare @massive_count int
declare @massive_average int
declare @movie_id_1 int
declare @movie_id_2 int

set @const = 10
set @average = (select vote_count from movie_relation where id = @relation_id) 
set @vote_count = (select COUNT(*) from users_vote where relation_id = @relation_id)
set @movie_id_1 = (select movie_1_id from movie_relation where id = @relation_id)
set @movie_id_2 = (select movie_2_id from movie_relation where id = @relation_id)

set @massive_vote_count = (select SUM(vote_count) from movie_relation where 
						(movie_1_id = @movie_id_1 and movie_2_id = @movie_id_2) or
						(movie_1_id = @movie_id_2 and movie_2_id = @movie_id_1))

set @massive_count = (select count(*) from movie_relation where 
						(movie_1_id = @movie_id_1 and movie_2_id = @movie_id_2) or
						(movie_1_id = @movie_id_2 and movie_2_id = @movie_id_1))

set @massive_average = @massive_vote_count / @massive_count

declare @result int

set @result = ((@vote_count / (@vote_count + @const)) * @average + (@const / (@vote_count + @const)) * @massive_average)

return @result

end
GO
/****** Object:  Table [dbo].[cast]    Script Date: 2014-01-30 01:46:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[cast](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[movie_id] [int] NOT NULL,
	[person_id] [int] NOT NULL,
	[role] [int] NOT NULL,
	[character_name] [nvarchar](50) NULL,
 CONSTRAINT [PK_cast] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_cast] UNIQUE NONCLUSTERED 
(
	[movie_id] ASC,
	[person_id] ASC,
	[role] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[comment]    Script Date: 2014-01-30 01:46:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[comment](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[user_id] [int] NOT NULL,
	[text] [text] NOT NULL,
	[movie_id] [int] NULL,
	[date] [datetime] NOT NULL,
	[person_id] [int] NULL,
 CONSTRAINT [PK_comment] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[image_movie]    Script Date: 2014-01-30 01:46:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[image_movie](
	[movie_id] [int] NOT NULL,
	[is_poster] [bit] NOT NULL,
	[source] [nvarchar](50) NULL,
	[id] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_image_movie_1] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[image_person]    Script Date: 2014-01-30 01:46:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[image_person](
	[perosn_id] [int] NOT NULL,
	[is_portrait] [bit] NOT NULL,
	[source] [nvarchar](50) NULL,
	[id] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_image_person_1] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[image_relation]    Script Date: 2014-01-30 01:46:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[image_relation](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[person_id] [int] NOT NULL,
	[image_id] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_image_relation] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[movie]    Script Date: 2014-01-30 01:46:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[movie](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[title] [nvarchar](50) NOT NULL,
	[release_date] [date] NOT NULL,
	[description] [text] NULL,
	[trailer_link] [nvarchar](50) NULL,
 CONSTRAINT [PK_movie] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_movie] UNIQUE NONCLUSTERED 
(
	[release_date] ASC,
	[title] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[movie_relation]    Script Date: 2014-01-30 01:46:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[movie_relation](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[movie_1_id] [int] NOT NULL,
	[movie_2_id] [int] NOT NULL,
	[auto_created] [bit] NOT NULL,
	[vote_count] [int] NULL,
 CONSTRAINT [PK_movie_relation] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[person]    Script Date: 2014-01-30 01:46:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[person](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[description] [text] NULL,
	[birth_date] [date] NULL,
	[birth_place] [nvarchar](50) NULL,
 CONSTRAINT [PK_person] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_person] UNIQUE NONCLUSTERED 
(
	[name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[user]    Script Date: 2014-01-30 01:46:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[user](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[login] [nvarchar](25) NOT NULL,
	[password] [nvarchar](200) NOT NULL,
	[admin] [bit] NOT NULL,
	[email] [varchar](100) NOT NULL,
 CONSTRAINT [PK_user] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_user] UNIQUE NONCLUSTERED 
(
	[login] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_user_1] UNIQUE NONCLUSTERED 
(
	[email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[users_vote]    Script Date: 2014-01-30 01:46:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[users_vote](
	[user_id] [int] NOT NULL,
	[relation_id] [int] NOT NULL,
	[vote] [bit] NOT NULL,
 CONSTRAINT [PK_users_vote] PRIMARY KEY CLUSTERED 
(
	[user_id] ASC,
	[relation_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Index [IX_movie_relation]    Script Date: 2014-01-30 01:46:42 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_movie_relation] ON [dbo].[movie_relation]
(
	[movie_1_id] ASC,
	[movie_2_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[cast]  WITH CHECK ADD  CONSTRAINT [FK_cast_movie] FOREIGN KEY([movie_id])
REFERENCES [dbo].[movie] ([id])
GO
ALTER TABLE [dbo].[cast] CHECK CONSTRAINT [FK_cast_movie]
GO
ALTER TABLE [dbo].[cast]  WITH CHECK ADD  CONSTRAINT [FK_cast_person] FOREIGN KEY([person_id])
REFERENCES [dbo].[person] ([id])
GO
ALTER TABLE [dbo].[cast] CHECK CONSTRAINT [FK_cast_person]
GO
ALTER TABLE [dbo].[comment]  WITH CHECK ADD  CONSTRAINT [FK_comment_movie1] FOREIGN KEY([movie_id])
REFERENCES [dbo].[movie] ([id])
GO
ALTER TABLE [dbo].[comment] CHECK CONSTRAINT [FK_comment_movie1]
GO
ALTER TABLE [dbo].[comment]  WITH CHECK ADD  CONSTRAINT [FK_comment_person1] FOREIGN KEY([person_id])
REFERENCES [dbo].[person] ([id])
GO
ALTER TABLE [dbo].[comment] CHECK CONSTRAINT [FK_comment_person1]
GO
ALTER TABLE [dbo].[comment]  WITH CHECK ADD  CONSTRAINT [FK_comment_user] FOREIGN KEY([user_id])
REFERENCES [dbo].[user] ([id])
GO
ALTER TABLE [dbo].[comment] CHECK CONSTRAINT [FK_comment_user]
GO
ALTER TABLE [dbo].[image_movie]  WITH CHECK ADD  CONSTRAINT [FK_image_movie_movie] FOREIGN KEY([movie_id])
REFERENCES [dbo].[movie] ([id])
GO
ALTER TABLE [dbo].[image_movie] CHECK CONSTRAINT [FK_image_movie_movie]
GO
ALTER TABLE [dbo].[image_person]  WITH CHECK ADD  CONSTRAINT [FK_image_person_person] FOREIGN KEY([perosn_id])
REFERENCES [dbo].[person] ([id])
GO
ALTER TABLE [dbo].[image_person] CHECK CONSTRAINT [FK_image_person_person]
GO
ALTER TABLE [dbo].[image_relation]  WITH CHECK ADD  CONSTRAINT [FK_image_relation_image_movie1] FOREIGN KEY([image_id])
REFERENCES [dbo].[image_movie] ([id])
GO
ALTER TABLE [dbo].[image_relation] CHECK CONSTRAINT [FK_image_relation_image_movie1]
GO
ALTER TABLE [dbo].[image_relation]  WITH CHECK ADD  CONSTRAINT [FK_image_relation_person] FOREIGN KEY([person_id])
REFERENCES [dbo].[person] ([id])
GO
ALTER TABLE [dbo].[image_relation] CHECK CONSTRAINT [FK_image_relation_person]
GO
ALTER TABLE [dbo].[movie_relation]  WITH CHECK ADD  CONSTRAINT [FK_movie_relation_movie] FOREIGN KEY([movie_1_id])
REFERENCES [dbo].[movie] ([id])
GO
ALTER TABLE [dbo].[movie_relation] CHECK CONSTRAINT [FK_movie_relation_movie]
GO
ALTER TABLE [dbo].[movie_relation]  WITH CHECK ADD  CONSTRAINT [FK_movie_relation_movie1] FOREIGN KEY([movie_2_id])
REFERENCES [dbo].[movie] ([id])
GO
ALTER TABLE [dbo].[movie_relation] CHECK CONSTRAINT [FK_movie_relation_movie1]
GO
ALTER TABLE [dbo].[users_vote]  WITH CHECK ADD  CONSTRAINT [FK_users_vote_movie_relation] FOREIGN KEY([relation_id])
REFERENCES [dbo].[movie_relation] ([id])
GO
ALTER TABLE [dbo].[users_vote] CHECK CONSTRAINT [FK_users_vote_movie_relation]
GO
ALTER TABLE [dbo].[users_vote]  WITH CHECK ADD  CONSTRAINT [FK_users_vote_user] FOREIGN KEY([user_id])
REFERENCES [dbo].[user] ([id])
GO
ALTER TABLE [dbo].[users_vote] CHECK CONSTRAINT [FK_users_vote_user]
GO
/****** Object:  Trigger [dbo].[poster]    Script Date: 2014-01-30 01:46:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create trigger [dbo].[poster] on [dbo].[image_movie]
for insert
as

declare @countPosters INT
set @countPosters = (select COUNT(*) from image_movie where is_poster = 1)

if @countPosters > 1
begin
	rollback
end

GO
/****** Object:  Trigger [dbo].[portrait]    Script Date: 2014-01-30 01:46:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create trigger [dbo].[portrait] on [dbo].[image_person]
for insert
as

declare @countPortraits INT
set @countPortraits = (select COUNT(*) from image_person where is_portrait = 1)

if @countPortraits > 1
begin
	rollback
end

GO
/****** Object:  Trigger [dbo].[update_vote_count]    Script Date: 2014-01-30 01:46:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create trigger [dbo].[update_vote_count] on [dbo].[users_vote]
for insert
as 

begin

declare @id int
set @id = (select relation_id from inserted)

execute dbo.update_score @id

end 
GO
USE [master]
GO
ALTER DATABASE [Movies] SET  READ_WRITE 
GO
