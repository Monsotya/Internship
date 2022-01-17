CREATE DATABASE Planetarium

CREATE TABLE Performance(
	Id int IDENTITY(1,1) NOT NULL PRIMARY KEY,
	Title varchar(100) NOT NULL,
	EventDescription varchar(max),
	Duration time NOT NULL
)

CREATE TABLE Hall(
	Id int IDENTITY(1,1) NOT NULL PRIMARY KEY,
	HallName varchar(30) NOT NULL,
	Capacity tinyint NOT NULL
)

CREATE TABLE Tier(
	Id int IDENTITY(1,1) NOT NULL PRIMARY KEY,
	TierName varchar(30) NOT NULL,
	Surcharge decimal NOT NULL	
)

CREATE TABLE Poster(
	Id int IDENTITY(1,1) NOT NULL PRIMARY KEY,
	PerformanceId int NOT NULL FOREIGN KEY REFERENCES Performance(Id)
        ON DELETE CASCADE
        ON UPDATE CASCADE,
	DateOfEvent datetime NOT NULL,
	HallId int NOT NULL FOREIGN KEY REFERENCES Hall(Id)
        ON DELETE CASCADE
        ON UPDATE CASCADE,
	
	Price decimal NOT NULL,
)

CREATE TABLE Orders(
	Id int IDENTITY(1,1) NOT NULL PRIMARY KEY,
	DateOfOrder datetime NOT NULL,
	ClientName varchar(30) NOT NULL,
	ClientSurname varchar(30) NOT NULL,
	Email varchar(30) NOT NULL
)

CREATE TABLE Ticket(
	Id int IDENTITY(1,1) NOT NULL PRIMARY KEY,
	PosterId int NOT NULL FOREIGN KEY REFERENCES Poster(Id)
        ON DELETE CASCADE
        ON UPDATE CASCADE,
	OrderId int FOREIGN KEY REFERENCES Orders(Id)
        ON DELETE CASCADE
        ON UPDATE CASCADE,
	TicketStatus varchar(15) DEFAULT 'available',
	Place tinyint NOT NULL,
	TierId int NOT NULL FOREIGN KEY REFERENCES Tier(Id)
        ON DELETE CASCADE
        ON UPDATE CASCADE,
)

