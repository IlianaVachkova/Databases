SELECT FirstName + ' ' + LastName AS Employee, Salary
FROM Employees
WHERE Salary=
		(SELECT MIN(Salary) FROM Employees) 
---------------------------------------------------------------------
SELECT FirstName + ' ' + LastName AS Employee, Salary
FROM Employees
WHERE Salary<=
		(SELECT MIN(Salary) FROM Employees)*1.1
---------------------------------------------------------------------
SELECT FirstName + ' ' + LastName AS Employee, Salary, e.DepartmentID
FROM Employees e
WHERE Salary=
		(SELECT MIN(Salary) FROM Employees
		WHERE DepartmentID=e.DepartmentID)
---------------------------------------------------------------------
SELECT AVG(Salary)
FROM Employees
WHERE DepartmentID=1
---------------------------------------------------------------------
SELECT AVG(Salary)
FROM Employees
WHERE DepartmentID IN
		(SELECT DepartmentID FROM Departments
		WHERE Name='Sales')
---------------------------------------------------------------------
SELECT COUNT(*)
FROM Employees
WHERE DepartmentID IN
		(SELECT DepartmentID FROM Departments
		WHERE Name='Sales')	
---------------------------------------------------------------------
SELECT COUNT(*)
FROM Employees
WHERE ManagerID IS NOT NULL		
---------------------------------------------------------------------
SELECT COUNT(*)
FROM Employees
WHERE ManagerID IS NULL	
---------------------------------------------------------------------
SELECT d.Name AS Department,
		AVG(e.Salary) AS [Average Salary]
FROM Employees e JOIN Departments d
			ON e.DepartmentID=d.DepartmentID
GROUP BY d.Name
---------------------------------------------------------------------
SELECT d.Name AS Department,
		COUNT(e.EmployeeID) AS [Employee's Count]
FROM Employees e
	JOIN Departments d
			ON e.DepartmentID=d.DepartmentID
GROUP BY d.Name
ORDER BY COUNT(e.EmployeeID) DESC

SELECT t.Name AS Town,
	COUNT(e.EmployeeID) AS [Employee's Count]
FROM Employees e
	JOIN Addresses a
		ON e.AddressID=a.AddressID
	JOIN Towns t
		ON a.TownID=t.TownID
GROUP BY t.Name
ORDER BY COUNT(e.EmployeeID) DESC
---------------------------------------------------------------------
SELECT m.FirstName, m.LastName,
	COUNT(m.EmployeeID) AS [Employee's Count]
FROM Employees e, Employees m
WHERE e.ManagerID=m.EmployeeID
GROUP BY m.FirstName, m.LastName
HAVING COUNT(m.EmployeeID)=5			
---------------------------------------------------------------------
SELECT e.FirstName + ' ' + e.LastName AS Employee,
	ISNULL((m.FirstName + ' ' + m.LastName), 'no manager') AS Manager
FROM Employees e LEFT OUTER JOIN Employees m
	ON e.ManagerID=m.EmployeeID
---------------------------------------------------------------------
SELECT LastName
FROM Employees
WHERE LEN(LastName)=5
---------------------------------------------------------------------
SELECT 
	CONVERT(VARCHAR(25), GETDATE(), 131)
		AS [Current Date]
---------------------------------------------------------------------
CREATE TABLE Users(
	UserID int IDENTITY,
	Username nvarchar(50) NOT NULL,
	Pass nvarchar(50) NOT NULL,
	FullName nvarchar(100),
	LastLoginTime datetime,
	CONSTRAINT PK_Users PRIMARY KEY(UserID), 
	CONSTRAINT chk_Username UNIQUE(Username),
	CONSTRAINT chk_Pass CHECK(LEN(Pass)>4)
)
---------------------------------------------------------------------
CREATE VIEW UsersView
AS 
SELECT FullName, LastLoginTime 
FROM Users
WHERE DAY(LastLoginTime) = DAY(GETDATE())
---------------------------------------------------------------------
CREATE TABLE Groups (
		GroupID int IDENTITY,
		Name varchar(50),
		CONSTRAINT PK_Groups PRIMARY KEY(GroupID),
		CONSTRAINT chk_Name UNIQUE (Name)
	)
---------------------------------------------------------------------
ALTER TABLE Users
	ADD GroupID int

	ALTER TABLE Users
	ADD CONSTRAINT FK_Users_Groups
		FOREIGN KEY (GroupID)
		REFERENCES Groups (GroupID)
---------------------------------------------------------------------
INSERT INTO Groups(Name)
	VALUES('Go')

	INSERT INTO Users(Username, Pass, FullName, LastLoginTime, GroupID)
	VALUES('gosho', 'gosho', 'gosho', GETDATE(), 4)
---------------------------------------------------------------------
UPDATE Users
	SET FullName = 'Pesho', Username = 'Pesho'
	WHERE FullName = 'Gosho'

	UPDATE Groups
	SET Name = 'Pesho'
	WHERE Name = 'Go'
---------------------------------------------------------------------
	DELETE FROM Users
	WHERE FullName = 'Pesho'

	DELETE FROM Groups
	WHERE Name = 'Pesho'
---------------------------------------------------------------------
	INSERT INTO Users(Username, Pass, FullName, LastLoginTime)
	SELECT FirstName + ' ' + LastName, 
		   LOWER(SUBSTRING(FirstName, 0, 1) + LastName + 'salt'), 
		   LOWER(SUBSTRING(FirstName, 0, 1) + LastName),
		   getdate()
	FROM Employees
---------------------------------------------------------------------
	UPDATE Users
	SET Pass = 'default'
	WHERE LastLoginTime <= CAST('10.03.2010 00:00:00' AS smalldatetime)
---------------------------------------------------------------------
	DELETE FROM Users
	WHERE Pass != 'default'
---------------------------------------------------------------------
	SELECT d.Name, em.JobTitle, AVG(em.Salary) as [Max Salary] FROM Employees em
	JOIN Departments d on d.DepartmentID = em.DepartmentID
	GROUP BY d.Name, em.JobTitle
---------------------------------------------------------------------
	SELECT d2.Name, e.JobTitle, e.FirstName + ' ' + e.LastName as Name, e.Salary 
	FROM Employees e
	JOIN Departments d2 ON d2.DepartmentID = e.DepartmentID
	WHERE e.Salary IN (SELECT MIN(em.Salary) 
					   FROM Employees em
					   JOIN Departments d on d.DepartmentID = em.DepartmentID
					   WHERE d2.DepartmentID = d.DepartmentID AND e.JobTitle = em.JobTitle
					   GROUP BY d.Name, em.JobTitle)
---------------------------------------------------------------------
	SELECT TOP 1 t.Name, COUNT(*)
	FROM Employees e
	JOIN Addresses a ON a.AddressID = e.AddressID
	JOIN Towns t ON t.TownID = a.TownID
	GROUP BY t.Name
	ORDER BY COUNT(*) DESC
---------------------------------------------------------------------
	SELECT t.Name as Town, COUNT(e.ManagerID) AS ManagersCount
	FROM Employees e
	JOIN Addresses a ON e.AddressID = a.AddressID
	join Towns t ON t.TownID = a.TownID
	WHERE e.EmployeeID in (SELECT DISTINCT ManagerID FROM Employees)
	GROUP BY t.Name
---------------------------------------------------------------------
	CREATE TABLE WorkHours(
		EmployeeID int IDENTITY,
		[Date] datetime,
		Task nvarchar(50),
		[Hours] int,
		Comment nvarchar(50),
		CONSTRAINT PK_WorkHours PRIMARY KEY(EmployeeID),
		CONSTRAINT FK_WorkHours_Employees FOREIGN KEY(EmployeeID) REFERENCES Employees(EmployeeID)
	)

	INSERT INTO WorkHours(Date, Task, Hours)
	VALUES
		(getdate(), 'Sample Task1', 23),
		(getdate(), 'Sample Task2', 3)

	DELETE FROM WorkHours
	WHERE Task LIKE '%Task1'

	UPDATE WorkHours
	SET HOURS = 10
	WHERE Task = 'Sample Task2'

	CREATE TABLE WorkHoursLog(
		Id int IDENTITY,
		OldRecord nvarchar(100) NOT NULL,
		NewRecord nvarchar(100) NOT NULL,
		Command nvarchar(10) NOT NULL,
		EmployeeId int NOT NULL,
		CONSTRAINT PK_WorkHoursLog PRIMARY KEY(Id),
		CONSTRAINT FK_WorkHoursLogs_WorkHours FOREIGN KEY(EmployeeId) REFERENCES WorkHours(EmployeeID)
	)

	ALTER TRIGGER tr_WorkHoursInsert ON WorkHours FOR INSERT
	AS
		INSERT INTO WorkHoursLog(OldRecord, NewRecord, Command, EmployeeId)
		Values(' ',
			   (SELECT 'Day: ' + CAST(Date AS nvarchar(50)) + ' ' + ' Task: ' + Task + ' ' + ' Hours: ' + CAST([Hours] AS nvarchar(50)) + ' ' + Comment 
				FROM Inserted),
			   'INSERT',
			   (SELECT EmployeeID FROM Inserted))
	GO

	ALTER TRIGGER tr_WorkHoursUpdate ON WorkHours FOR UPDATE
	AS
		INSERT INTO WorkHoursLog(OldRecord, NewRecord, Command, EmployeeId)
		Values((SELECT 'Day: ' + CAST(Date AS nvarchar(50)) + ' ' + ' Task: ' + Task + ' ' + ' Hours: ' + CAST([Hours] AS nvarchar(50)) + ' ' + Comment FROM Deleted),
			   (SELECT 'Day: ' + CAST(Date AS nvarchar(50)) + ' ' + ' Task: ' + Task + ' ' + ' Hours: ' + CAST([Hours] AS nvarchar(50)) + ' ' + Comment FROM Inserted),
			   'UPDATE',
			   (SELECT EmployeeID FROM Inserted))
	GO

	ALTER TRIGGER tr_WorkHoursDeleted ON WorkHours FOR DELETE
	AS
		INSERT INTO WorkHoursLog(OldRecord, NewRecord, Command, EmployeeId)
		Values((SELECT 'Day: ' + CAST(Date AS nvarchar(50)) + ' ' + ' Task: ' + Task + ' ' + ' Hours: ' + CAST([Hours] AS nvarchar(50)) + ' ' + Comment FROM Deleted),
			   ' ',
			   'DELETE',
			   (SELECT EmployeeID FROM Deleted))
	GO

	INSERT INTO WorkHours([Date], Task, Hours, Comment)
	VALUES(GETDATE(), 'Random task4', 12, 'Comment4')

	DELETE FROM WorkHours
	WHERE Task = 'Random task3'

	UPDATE WorkHours
	SET Task = 'Random task12'
	WHERE EmployeeID = 8
---------------------------------------------------------------------
	BEGIN TRAN
	DELETE FROM Employees
		SELECT d.Name
		FROM Employees e JOIN Departments d
		ON e.DepartmentID = d.DepartmentID
		WHERE d.Name = 'Sales'
		GROUP BY d.Name
	ROLLBACK TRAN
---------------------------------------------------------------------
	BEGIN TRAN
	DROP TABLE EmployeesProjects
	ROLLBACK TRAN
---------------------------------------------------------------------
	CREATE TABLE #TemporaryTable(
		EmployeeID int NOT NULL,
		ProjectID int NOT NULL
	)

	INSERT INTO #TemporaryTable
		SELECT EmployeeID, ProjectID
		FROM EmployeesProjects

	DROP TABLE EmployeesProjects

	CREATE TABLE EmployeesProjects(
		EmployeeID int NOT NULL,
		ProjectID int NOT NULL,
		CONSTRAINT PK_EmployeesProjects PRIMARY KEY(EmployeeID, ProjectID),
		CONSTRAINT FK_EP_Employee FOREIGN KEY(EmployeeID) REFERENCES Employees(EmployeeID),
		CONSTRAINT FK_EP_Project FOREIGN KEY(ProjectID) REFERENCES Projects(ProjectID)
	)

	INSERT INTO EmployeesProjects
	SELECT EmployeeID, ProjectID
	FROM #TemporaryTable
------------------------------------------------------------------------
