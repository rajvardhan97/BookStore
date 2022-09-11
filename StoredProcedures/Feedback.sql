USE BookStoreDB

CREATE TABLE Feedback
(
	FeedbackI int identity(1,1) primary key,
	Rating int Not Null,
	Review varchar(Max) Not Null,
	UserId int Not Null,
	BookId int Not Null
)

SELECT * FROM Feedback

USE [BookStoreDB]
GO
/****** Object:  StoredProcedure [dbo].[AddFeedback]    Script Date: 9/11/2022 12:56:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[AddFeedback]
		@Rating int,
		@Review varchar(max),
		@UserId int,
		@BookId int
AS
BEGIN
			INSERT INTO Feedback(Rating,Review,UserId,BookId)
			VALUES(@Rating, @Review, @UserId, @BookId)
END

USE [BookStoreDB]
GO
/****** Object:  StoredProcedure [dbo].[GetFeedback]    Script Date: 9/11/2022 12:57:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[GetFeedback]
AS
BEGIN
		SELECT * FROM Feedback
END