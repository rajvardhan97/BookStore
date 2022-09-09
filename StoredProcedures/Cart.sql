USE BookStoreDB

CREATE TABLE Cart(
	CartId int identity(1,1) primary key,
	BooksInCart int default 1,
	UserId int not null foreign key (UserId) references Users(UserId),
	BookId int not null foreign key (BookId) references Books(BookId)
)

SELECT * FROM Cart

CREATE PROCEDURE AddToCart
	@BookId int,
	@BooksInCart int
AS
BEGIN
		INSERT INTO Cart(BookId,BooksInCart)VALUES(@BookId,@BooksInCart)
END

CREATE PROCEDURE UpdateCart
	@CartId int,
	@BooksInCart int,
	@UserId int
AS
BEGIN
	UPDATE Cart SET BooksInCart=@BooksInCart WHERE CartId=@CartId AND UserId=@UserId
END

CREATE PROCEDURE DeleteCart
	@CartId int,
	@UserId int
AS
BEGIN
	DELETE FROM Cart WHERE CartId=@CartId AND UserId=@UserId
END