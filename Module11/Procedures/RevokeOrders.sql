CREATE PROCEDURE RevokeOrders
	@to DATETIME,
	@from DATETIME,
	@hallId int,
	@places varchar(max)
AS
     UPDATE Ticket 
	 SET TicketStatus = 'unavailable', OrderId = NULL 
	 WHERE [Ticket].place in (SELECT CAST(value AS int) from STRING_SPLIT(@places, ',')) AND (SELECT HallId FROM Poster WHERE Id = PosterId) = @hallid AND (SELECT DateOfEvent FROM Poster WHERE Id = PosterId) BETWEEN @from AND @to
	
	