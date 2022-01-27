CREATE PROCEDURE CreatePoster
	@performanceId int,
	@dateOfEvent DATETIME,
	@hallId int,
	@price decimal
AS 
	INSERT INTO Poster(PerformanceId, DateOfEvent, HallId, Price)
	VALUES (@performanceId, @dateOfEvent, @hallId, @price)
	DECLARE @cnt INT = 0;

WHILE @cnt < (SELECT Capacity FROM Hall WHERE Id = @hallId)
BEGIN
	SET @cnt = @cnt + 1
   INSERT INTO Ticket(PosterId, TierId, Place)
	VALUES ((SELECT TOP 1 Id FROM Poster ORDER BY ID DESC), 1, @cnt)
END;