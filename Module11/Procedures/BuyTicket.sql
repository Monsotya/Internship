CREATE PROC BuyTicket 
	@ticketId int
AS
	IF (Select TicketStatus from ticket where id = @ticketId) = 'available'
		BEGIN
		UPDATE Ticket 
		set ticketstatus = 'bought' 
		where id = @ticketId
		RETURN (1)
		END
	ELSE 
		BEGIN
		RETURN (0)
		END