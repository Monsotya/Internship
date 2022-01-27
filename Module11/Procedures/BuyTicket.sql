CREATE PROCEDURE BuyTicket 
	@ticketId int
AS
	IF (SELECT TicketStatus FROM ticket WHERE id = @ticketId) = 'available'
		UPDATE Ticket 
		SET ticketstatus = 'bought' 
		WHERE id = @ticketId