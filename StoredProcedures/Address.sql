USE BookStoreDB

CREATE TABLE AddressType(
	TypeId int identity(1,1) primary key,
	AddType varchar(100)
	)

INSERT INTO AddressType VALUES('Home')
INSERT INTO AddressType VALUES('Work')
INSERT INTO AddressType VALUES('Other')

SELECT * FROM AddressType

CREATE TABLE Address(
	AddressId int identity(1,1) primary key,
	Address varchar(max) not null,
	City varchar(100) not null,
	State varchar(100) not null,
	TypeId int not null foreign key (TypeId) references AddressType(TypeId),
	UserId int not null foreign key (UserId) references Users(UserId)
	)

SELECT * FROM Address

CREATE PROCEDURE AddAddress
	@Address varchar(max),
	@City varchar(100),
	@State varchar(100),
	@TypeId int,
	@UserId int
AS
BEGIN
	INSERT INTO Address(Address, City, State, TypeId, UserId) VALUES(@Address,@City,@State,@TypeId,@UserId)
END

CREATE PROCEDURE UpdateAddress
	@AddressId int,
	@Address varchar(max),
	@City varchar(100),
	@State varchar(100),
	@TypeId int,
	@UserId int
AS
BEGIN
	UPDATE Address SET
	Address=@Address,City=@City,State=@State,TypeId=@TypeId WHERE UserId=@UserId AND AddressId=@AddressId;
END

CREATE PROCEDURE GetAllAddress
	@UserId int
AS
BEGIN
	SELECT * FROM Address WHERE UserId=@UserId
END