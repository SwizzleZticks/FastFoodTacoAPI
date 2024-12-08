drop table if exists Combo;
drop table if exists Taco;
drop table if exists Drink;

CREATE TABLE Taco(
    ID int IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(255),
    Cost REAL,
    SoftShell BIT,
    Chips BIT
);
CREATE TABLE Drink(
    ID int IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(255),
    Cost REAL,
    Slushie BIT
);

INSERT INTO Taco(Name, Cost, SoftShell, Chips)
VALUES ('Double Layer Taco', 2.49, 0, 0),
	   ('Soft Taco', 1.69, 1, 0),
       ('Potato Taco', 1.00, 1, 0),
       ('Nacho Cheese Chips Tacos', 2.39, 0, 1),
       ('Chicken Taco', 3.49, 1, 0);

INSERT INTO Drink(Name, Cost, Slushie)
VALUES ('Fiesta Blast', 2.00, 0),
       ('Gamer Drink', 3.50, 0),
       ('Fiesta Blast Freeze', 2.00, 1),
       ('Blue Raspberry Strawberry Slushie', 2.50, 1),
       ('Water', 0, 0);

SELECT * FROM Taco;
SELECT * FROM Drink;

CREATE TABLE Combo(
	ID INT IDENTITY(1,1) Primary Key,
	Name NVARCHAR(255),
	TacoId INT FOREIGN KEY REFERENCES Taco(ID),
	DrinkId INT FOREIGN KEY REFERENCES Drink(ID),
	Cost REAL
);

INSERT INTO Combo(Name, TacoId, DrinkId, Cost)
VALUES ('Double Taco Blast',  1, 1, 4.50),
	   ('Soft Taco Gamer',    2, 2, 3.00),
	   ('Potato Swizz Combo', 3, 3, 2.50),
	   ('Blue Nachos',        4, 4, 3.75),
	   ('Chicken Water',      5, 5, 3.49);

SELECT * FROM Combo
JOIN Taco ON Combo.TacoId = Taco.ID
JOIN Drink ON Combo.DrinkId = Drink.ID;
