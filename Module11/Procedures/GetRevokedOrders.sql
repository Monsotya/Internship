CREATE PROCEDURE GetRevokedOrders
	@to DATETIME,
	@from DATETIME,
	@hallId int,
	@places varchar(max)
AS
	SELECT Place, Price * Surcharge as Price, O.Id, ClientName, Email 
		FROM Ticket T
		INNER JOIN Orders O ON(T.OrderId = O.Id) 
		INNER JOIN Poster P ON(T.PosterId = P.Id) 
		INNER JOIN Tier Ti ON(T.TierId = Ti.Id) 
		WHERE T.place in (SELECT CAST(value AS int) FROM STRING_SPLIT(@places, ',')) AND P.HallId = @hallid AND P.DateOfEvent BETWEEN @from AND @to
