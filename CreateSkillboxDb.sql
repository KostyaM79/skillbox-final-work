USE master;
GO
-- ==================================================================================
-- = Проверяем наличие базы данных skillboxDb и, если она не существует, создаём её =
-- ==================================================================================
IF NOT EXISTS(SELECT name FROM sys.databases WHERE (name=N'skillboxDb'))
	BEGIN
		CREATE DATABASE skillboxDb;
	END
GO

USE skillboxDb;
GO

-- ==================================================================================
-- =   Проверяем наличие таблицы LastNames и, если она не существует, создаём её    =
-- ==================================================================================
IF NOT EXISTS(SELECT name FROM sys.tables WHERE(name=N'LastNames'))
	BEGIN
		CREATE TABLE LastNames
		(
			Id INT IDENTITY NOT NULL PRIMARY KEY,
			LastName NVarChar(25) NOT NULL UNIQUE
		)
	END
GO

-- ==================================================================================
-- =   Проверяем наличие таблицы FirstNames и, если она не существует, создаём её   =
-- ==================================================================================
IF NOT EXISTS(SELECT name FROM sys.tables WHERE(name=N'FirstNames'))
	BEGIN
		CREATE TABLE FirstNames
		(
			Id INT IDENTITY NOT NULL PRIMARY KEY,
			FirstName NVarChar(25) NOT NULL UNIQUE
		)
	END
GO

-- ==================================================================================
-- =    Проверяем наличие таблицы Emails и, если она не существует, создаём её      =
-- ==================================================================================
IF NOT EXISTS(SELECT name FROM sys.tables WHERE(name=N'Emails'))
	BEGIN
		CREATE TABLE Emails
		(
			Id INT IDENTITY NOT NULL PRIMARY KEY,
			Email NVarChar(25) NOT NULL UNIQUE
		)
	END
GO

-- ==================================================================================
-- = Проверяем наличие таблицы OrderStatuses и, если она не существует, создаём её  =
-- =                       и заполняем её начальными данными                        =
-- ==================================================================================
IF NOT EXISTS(SELECT name FROM sys.tables WHERE(name=N'OrderStatuses'))
	BEGIN
		CREATE TABLE OrderStatuses
		(
			Id INT IDENTITY NOT NULL PRIMARY KEY,
			OrderStatus NVarChar(10) NOT NULL
		)
	END
GO

INSERT INTO OrderStatuses(OrderStatus) VALUES(N'Получена'), (N'В работе'), (N'Выполнена'), (N'Отклонена'), (N'Отменена');
GO

-- ==================================================================================
-- =     Проверяем наличие таблицы Clients и, если она не существует, создаём её     =
-- ==================================================================================
IF NOT EXISTS(SELECT name FROM sys.tables WHERE(name=N'Clients'))
	BEGIN
		CREATE TABLE Clients
		(
			Id INT IDENTITY NOT NULL PRIMARY KEY,
			LastNameId INT NOT NULL,
			FirstNameId INT NOT NULL,
			EmailId INT NOT NULL,
			CONSTRAINT U_Clients UNIQUE (LastNameId, FirstNameId, EmailId),
			CONSTRAINT FK_LastNames_Clients FOREIGN KEY (LastNameId) REFERENCES LastNames (Id) ON UPDATE CASCADE ON DELETE CASCADE,
			CONSTRAINT FK_FirstNames_Clients FOREIGN KEY (FirstNameId) REFERENCES FirstNames (Id) ON UPDATE CASCADE ON DELETE CASCADE,
			CONSTRAINT FK_Emails_Clients FOREIGN KEY (EmailId) REFERENCES Emails (Id) ON UPDATE CASCADE ON DELETE CASCADE
		)
	END
GO

-- ==================================================================================
-- =    Проверяем наличие таблицы Orders и, если она не существует, создаём её     =
-- ==================================================================================
IF NOT EXISTS(SELECT name FROM sys.tables WHERE(name=N'Orders'))
	BEGIN
		CREATE TABLE Orders
		(
			Id INT IDENTITY NOT NULL PRIMARY KEY,
			ClientId INT NOT NULL,
			CreatingDate DATETIME NOT NULL,
			OrderText TEXT NOT NULL,
			OrderStatusId INT NOT NULL,
			CONSTRAINT FK_Clients_Orders FOREIGN KEY (ClientId) REFERENCES Clients (Id) ON UPDATE CASCADE ON DELETE CASCADE,
			CONSTRAINT FK_OrderStatuses_Orders FOREIGN KEY (OrderStatusId) REFERENCES OrderStatuses (Id) ON UPDATE CASCADE ON DELETE CASCADE
		)
	END
GO

-- ==================================================================================
-- =    Проверяем наличие таблицы Articles и, если она не существует, создаём её    =
-- ==================================================================================
IF NOT EXISTS(SELECT name FROM sys.tables WHERE(name=N'Articles'))
	BEGIN
		CREATE TABLE Articles
		(
			Id INT IDENTITY NOT NULL PRIMARY KEY,
			ArticleCaption NVarChar(100) NOT NULL,
			ArticlePublishDate DATETIME NOT NULL,
			ArticleText TEXT NOT NULL,
			ArticleImageFileName NVarChar(100) NOT NULL
		)
	END
GO

-- ==================================================================================
-- =    Проверяем наличие таблицы Projects и, если она не существует, создаём её    =
-- ==================================================================================
IF NOT EXISTS(SELECT name FROM sys.tables WHERE(name=N'Projects'))
	BEGIN
		CREATE TABLE Projects
		(
			Id INT IDENTITY NOT NULL PRIMARY KEY,
			ProjectCaption NVarChar(25) NOT NULL,
			ProjectDescription TEXT NOT NULL,
			ProjectImageFileName NVarChar(100)
		)
	END
GO

-- ==================================================================================
-- =    Проверяем наличие таблицы Services и, если она не существует, создаём её    =
-- ==================================================================================
IF NOT EXISTS(SELECT name FROM sys.tables WHERE(name=N'Services'))
	BEGIN
		CREATE TABLE Services
		(
			Id INT IDENTITY NOT NULL PRIMARY KEY,
			ServiceCaption NVarChar(100) NOT NULL,
			ServiceDescription TEXT NOT NULL
		)
	END
GO

-- ==================================================================================
-- =    Проверяем наличие таблицы Socials и, если она не существует, создаём её     =
-- ==================================================================================
IF NOT EXISTS(SELECT name FROM sys.tables WHERE(name=N'Socials'))
	BEGIN
		CREATE TABLE Socials
		(
			Id INT IDENTITY NOT NULL PRIMARY KEY,
			Link NVarChar(50) NOT NULL,
			ImageFileName NVarChar(100) NOT NULL
		)
	END
GO

-- ========================================================================================================================

-- ==================================================================================
-- =                          Процедура добавления фамилии                          =
-- ==================================================================================
CREATE PROCEDURE AddLastNameProc
@lastName NVarChar(25)
AS
BEGIN
	DECLARE @id INT
	IF NOT EXISTS(SELECT LastName FROM LastNames WHERE(LastName=@lastName))
	BEGIN
		INSERT INTO LastNames(LastName) VALUES(@lastName);
		SET @id = @@IDENTITY;
	END
	ELSE
		SET @id = (SELECT TOP 1 Id FROM LastNames WHERE(LastName=@lastName))
END
RETURN @id;
GO

-- ==================================================================================
-- =                           Процедура добавления имени                           =
-- ==================================================================================
CREATE PROCEDURE AddFirstNameProc
@FirstName NVarChar(25)
AS
BEGIN
	DECLARE @id INT
	IF NOT EXISTS(SELECT FirstName FROM FirstNames WHERE(FirstName=@FirstName))
	BEGIN
		INSERT INTO FirstNames(FirstName) VALUES(@FirstName);
		SET @id = @@IDENTITY;
	END
	ELSE
		SET @id = (SELECT TOP 1 Id FROM FirstNames WHERE(FirstName=@FirstName))
END
RETURN @id;
GO

-- ==================================================================================
-- =                           Процедура добавления email                           =
-- ==================================================================================
CREATE PROCEDURE AddEmailProc
@email NVarChar(25)
AS
BEGIN
	DECLARE @id INT
	IF NOT EXISTS(SELECT Email FROM Emails WHERE(Email=@email))
	BEGIN
		INSERT INTO Emails(Email) VALUES(@email);
		SET @id = @@IDENTITY;
	END
	ELSE
		SET @id = (SELECT TOP 1 Id FROM Emails WHERE(Email=@email))
END
RETURN @id;
GO

-- ==================================================================================
-- =                          Процедура добавления клиента                          =
-- ==================================================================================
CREATE PROCEDURE AddClientProc
@lastName NVARCHAR(25),
@firstName NVARCHAR(25),
@email NVARCHAR(25)

AS
BEGIN
	DECLARE @id INT, @lastNameId INT, @firstNameId INT, @emailId INT
	EXEC @lastNameId = AddLastNameProc @lastName;
	EXEC @firstNameId = AddFirstNameProc @firstName;
	EXEC @emailId = AddEmailProc @email;

	IF NOT EXISTS(SELECT 1 FROM Clients WHERE(LastNameId=@lastNameId AND FirstNameId=@firstNameId AND EmailId=@emailId))
	BEGIN
		INSERT INTO Clients(LastNameId, FirstNameId, EmailId) VALUES(@lastNameId, @firstNameId, @emailId);
		SET @id = @@IDENTITY;
	END
	ELSE
		SET @id = (SELECT TOP 1 Id FROM Clients WHERE(LastNameId=@lastNameId AND FirstNameId=@firstNameId AND EmailId=@emailId));
END
RETURN @id;
GO

-- ==================================================================================
-- =                          Процедура добавления заявки                           =
-- ==================================================================================
CREATE PROCEDURE AddOrderProc
@lastName NVARCHAR(25),
@firstName NVARCHAR(25),
@email NVARCHAR(25),
@oderText NVARCHAR(500)
AS
BEGIN
	DECLARE @id INT
	EXEC @id = AddClientProc @lastName, @firstName, @email;
	INSERT INTO Orders(ClientId, CreatingDate, OrderText, OrderStatusId)
		VALUES(@id, GETDATE(), @oderText, (SELECT TOP 1 Id FROM OrderStatuses WHERE(OrderStatus=N'Получена')));
	SET @id = @@IDENTITY;
END
RETURN @id;
GO