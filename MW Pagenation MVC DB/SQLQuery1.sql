--CREATE TABLE Pagenation(
--ID int PRIMARY KEY IDENTITY,
--[Name] nvarchar(255) NOT NULL,
--[Surname] nvarchar(255) NOT NULL,
--[Age] int NOT NULL
--)

--INSERT INTO Pagenation(Name, Surname, Age)
--VALUES (N'Alex37', N'Bob23', 21)

--DELETE FROM Pagenation

SELECT COUNT(ID) FROM Pagenation

SELECT * FROM Pagenation
ORDER BY ID
    OFFSET 2 ROWS
    FETCH NEXT 3 ROWS ONLY;