USE BookStoreDB

CREATE TABLE Orders(
	OrderId int identity(1,1) primary key,
	OrderQty int not null,
	TotalPrice float not null,
	OrderDate Date not null,
	UserId INT NOT NULL FOREIGN KEY REFERENCES Users(UserId),
	BookId INT NOT NULL FOREIGN KEY REFERENCES Books(BookId),
	AddressId int not null FOREIGN KEY REFERENCES Address(AddressId)
	)

SELECT * FROM Orders

CREATE PROCEDURE AddOrder
	@UserId int,
	@BookId int,
	@AddressId int,
	@TotalPrice int,
	@OrderQty int
AS
BEGIN
	SET @TotalPrice = (SELECT DiscountPrice FROM Books WHERE BookId = @BookId); 
	SET @OrderQty = (SELECT BooksInCart FROM Cart WHERE BookId = @BookId);
	BEGIN	
		BEGIN TRY
			BEGIN Transaction
				IF((SELECT Quantity FROM Books WHERE BookId = @BookId)>= @OrderQty)
				BEGIN
					INSERT INTO Orders VALUES(@OrderQty,@TotalPrice*@OrderQty,GETDATE(),@UserId,@BookId,@AddressId);
					UPDATE Books SET Quantity = (Quantity - @OrderQty) WHERE BookId = @BookId;
					DELETE FROM Cart WHERE BookId = @BookId and UserId = @UserId; 
				END
			COMMIT TRANSACTION
		End try

		BEGIN Catch
				ROLLBACK;
		End Catch
	End
End

CREATE PROCEDURE RemoveOrder
	@OrderId int
AS
BEGIN
	DELETE FROM Orders WHERE OrderId=@OrderId
END

CREATE PROCEDURE GetOrder
	@OrderId int
AS
BEGIN
	SELECT * FROM Orders WHERE OrderId=@OrderId
END