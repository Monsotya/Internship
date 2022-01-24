CREATE PROCEDURE GetAvailablePlaces
	@from DATETIME,
	@to DATETIME,
	@performance varchar(max)
AS
	SELECT HallName, DateOfEvent, Price, Count(*) as AvailablePlacesNumber
    FROM Poster, Performance, Ticket, Hall
	WHERE Title = @performance AND [Performance].Id = PerformanceId AND [Poster].Id = PosterId AND [Hall].Id = HallId AND DateOfEvent BETWEEN @from AND @to
	GROUP BY HallName, DateOfEvent, Price