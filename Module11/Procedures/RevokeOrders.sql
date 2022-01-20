CREATE PROCEDURE RevokeOrders
	@to DATETIME,
	@from DATETIME,
	@hallId int,
	@places varchar(max)
AS
	SELECT Place, Price * Surcharge as Price, [Orders].Id, ClientName, Email 
		FROM Ticket 
		INNER JOIN Orders ON([Ticket].OrderId = [Orders].Id) 
		INNER JOIN Poster ON([Ticket].PosterId = [Poster].Id) 
		INNER JOIN Tier ON([Ticket].TierId = [Tier].Id) 
		WHERE [Ticket].place in (SELECT CAST(value AS int) from STRING_SPLIT(@places, ', ')) AND (SELECT HallId FROM Poster WHERE Id = PosterId) = @hallid AND (SELECT DateOfEvent FROM Poster WHERE Id = PosterId) BETWEEN @from AND @to

     UPDATE Ticket 
	 SET TicketStatus = 'unavailable', OrderId = NULL 
	 WHERE [Ticket].place in (SELECT CAST(value AS int) from STRING_SPLIT(@places, ', ')) AND (SELECT HallId FROM Poster WHERE Id = PosterId) = @hallid AND (SELECT DateOfEvent FROM Poster WHERE Id = PosterId) BETWEEN @from AND @to
	
	