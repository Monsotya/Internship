CREATE PROCEDURE GetAvailablePerformances
	@from DATETIME,
	@to DATETIME
AS
	SELECT DISTINCT DateOfEvent, HallName, Price, Title, Duration, EventDescription, 
    FROM Poster, Hall, Performance, Ticket
	WHERE[Performance].Id = PerformanceId AND[Hall].Id = HallId AND DateOfEvent BETWEEN @from AND @to;