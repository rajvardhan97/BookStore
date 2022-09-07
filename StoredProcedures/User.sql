create database BookStoreDB
use BookStoreDB

create table Users(
UserId int identity(1,1) primary key,
FullName varchar(100) not null,
EmailId varchar(100) not null UNIQUE,
Password varchar(100) not null,
MobileNumber bigint not null);

select * from Users

CREATE PROCEDURE Register
	@FullName varchar(100),
	@EmailId varchar(100),
	@Password varchar(100),
	@MobileNumber bigint
AS
BEGIN
	INSERT INTO Users 
	VALUES(@FullName, @EmailId, @Password, @MobileNumber);
END


CREATE PROCEDURE Login
	@EmailId varchar(100),
	@Password varchar(100)
AS
BEGIN
	SELECT * FROM Users WHERE EmailId = @EmailId AND Password = @Password;
END


create procedure ForgetPassword
	@EmailId varchar(100)
AS
BEGIN
	SELECT * FROM Users WHERE EmailId = @EmailId;
END


create procedure ResetPassword
	@EmailId varchar(100),
	@Password varchar(100)
AS
BEGIN
	UPDATE Users SET Password = @Password WHERE EmailId = @EmailId;
END