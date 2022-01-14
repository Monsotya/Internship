CREATE DATABASE Planetarium

CREATE TABLE Performance(
	id int IDENTITY(1,1) NOT NULL PRIMARY KEY,
	title varchar(100) NOT NULL,
	description_event varchar(max),
	duration time NOT NULL
)

CREATE TABLE Hall(
	id int IDENTITY(1,1) NOT NULL PRIMARY KEY,
	name_hall varchar(30) NOT NULL,
	capacity tinyint NOT NULL
)

CREATE TABLE Poster(
	id int IDENTITY(1,1) NOT NULL PRIMARY KEY,
	performance_id int NOT NULL FOREIGN KEY REFERENCES Performance(id)
        ON DELETE CASCADE
        ON UPDATE CASCADE,
	date_of_event datetime NOT NULL,
	hall_id int NOT NULL FOREIGN KEY REFERENCES Hall(id)
        ON DELETE CASCADE
        ON UPDATE CASCADE,
)

CREATE TABLE Orders(
	id int IDENTITY(1,1) NOT NULL PRIMARY KEY,
	date_of_order datetime NOT NULL,
	name_client varchar(30) NOT NULL,
	surname_client varchar(30) NOT NULL,
	email varchar(30) NOT NULL
)

CREATE TABLE Ticket(
	id int IDENTITY(1,1) NOT NULL PRIMARY KEY,
	poster_id int NOT NULL FOREIGN KEY REFERENCES Poster(id)
        ON DELETE CASCADE
        ON UPDATE CASCADE,
	order_id int FOREIGN KEY REFERENCES Orders(id)
        ON DELETE CASCADE
        ON UPDATE CASCADE,
	status_ticket varchar(15) DEFAULT 'available',
	price decimal NOT NULL,
	place tinyint NOT NULL
)

