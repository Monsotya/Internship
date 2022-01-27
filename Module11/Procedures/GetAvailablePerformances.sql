CREATE PROCEDURE GetAvailablePerformances
	@from DATETIME,
	@to DATETIME
AS
	SELECT DISTINCT DateOfEvent, HallName, Price, Title, Duration, EventDescription 
    FROM Poster Po
	INNER JOIN Hall H on (H.Id = Po.HallId)
	INNER JOIN Performance Pe on (Pe.Id = Po.PerformanceId)
	WHERE Po.DateOfEvent BETWEEN @from AND @to;