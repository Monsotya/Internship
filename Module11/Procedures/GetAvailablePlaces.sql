CREATE PROCEDURE GetAvailablePlaces
	@from DATETIME,
	@to DATETIME,
	@performance varchar(max)
AS
	SELECT HallName, DateOfEvent, Price as PriceFrom, Count(1) as AvailablePlacesNumber
    FROM Poster Po
	INNER JOIN Performance Pe on (Pe.Id = Po.PerformanceId)
	INNER JOIN Ticket T on (T.PosterId = Po.Id)
	INNER JOIN Hall H on (H.Id = Po.HallId)
	WHERE Title = @performance AND Po.DateOfEvent BETWEEN @from AND @to AND T.TicketStatus = 'available'
	GROUP BY HallName, DateOfEvent, Price