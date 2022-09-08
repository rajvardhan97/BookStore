use BookStoreDB

CREATE TABLE Books(
BookId int identity(1,1) primary key,
BookName varchar(100) not null,
AuthorName varchar(100) not null,
Description varchar(500) not null,
Quantity int not null,
TotalRating float,
Rating float,
OriginalPrice int not null,
DiscountPrice int not null,
BookImage varchar(500)
)

SELECT * FROM Books

CREATE PROCEDURE AddBook
	@BookName varchar(100),
	@AuthorName varchar(100),
	@Description varchar(500),
	@Quantity int,
	@TotalRating float,
	@Rating float,
	@OriginalPrice int,
	@DiscountPrice int,
	@BookImage varchar(500)
AS
BEGIN
	INSERT INTO Books (BookName, AuthorName, Description, Quantity, TotalRating, Rating, OriginalPrice, DiscountPrice, BookImage)
	VALUES(@BookName, @AuthorName, @Description, @Quantity, @TotalRating, @Rating, @OriginalPrice, @DiscountPrice, @BookImage)
END

CREATE PROCEDURE UpdateBook
	@BookId int,
	@BookName varchar(100),
	@AuthorName varchar(100),
	@Description varchar(500),
	@Quantity int,
	@TotalRating float,
	@Rating float,
	@OriginalPrice int,
	@DiscountPrice int,
	@BookImage varchar(500)

AS
BEGIN
	UPDATE Books SET
	BookName=@BookName,
	AuthorName=@AuthorName,
	Description=@Description,
	Quantity=@Quantity,
	TotalRating=@TotalRating,
	Rating=@Rating,
	@OriginalPrice=@OriginalPrice,
	DiscountPrice=@DiscountPrice,
	BookImage=@BookId
	WHERE BookId=@BookId
END

CREATE PROCEDURE DeleteBook
	@BookId int
AS
BEGIN
	DELETE FROM Books WHERE BookId = @BookId
END

CREATE PROCEDURE GetAllBooks
AS
BEGIN
	SELECT * FROM Books;
END

CREATE PROCEDURE GetBookById
	@BookId int
AS
BEGIN
	SELECT * FROM Books WHERE BookId = @BookId;
END
