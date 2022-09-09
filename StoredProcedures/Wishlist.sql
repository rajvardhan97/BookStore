USE BookStoreDB

CREATE TABLE Wishlist(
	WishlistId int identity (1,1) primary key,
	UserId int not null foreign key (UserId) references Users(UserId),
	BookId int not null foreign key (BookId) references Books(BookId)
	)

SELECT * FROM Wishlist;

CREATE PROCEDURE AddWishlist
	@BookId int,
	@UserId int
AS
BEGIN
		INSERT INTO Wishlist (UserId,BookId) VALUES (@UserId,@BookId)
END

CREATE PROCEDURE RemoveWishlist
	@WishlistId int
AS
BEGIN
	DELETE FROM Wishlist WHERE WishlistId = @WishlistId
END