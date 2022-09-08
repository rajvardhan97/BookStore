USE BookStoreDB

CREATE TABLE Admin(
AdminId int identity(1,1) primary key,
AdminName varchar(100) not null,
AdminEmail varchar(100) not null UNIQUE,
AdminPassword varchar(100) not null,
MobileNumber bigint not null,
AdminAddress varchar(300) not null);

SELECT * FROM Admin

CREATE PROCEDURE AdminLogin
		@AdminEmail varchar(100),
		@AdminPassword varchar(100)
AS
BEGIN
	SELECT * FROM Admin WHERE AdminEmail = @AdminEmail AND AdminPassword = @AdminPassword
END
