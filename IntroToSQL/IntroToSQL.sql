---------------------------------------------------------------------
SELECT * FROM Departments
---------------------------------------------------------------------
SELECT Name FROM Departments
---------------------------------------------------------------------
SELECT FirstName, Salary FROM Employees
SELECT Salary FROM Employees ORDER BY Salary
---------------------------------------------------------------------
SELECT (FirstName + ' ' + LastName) AS [FullName] FROM Employees
---------------------------------------------------------------------
SELECT (FirstName + '.' + LastName + '@telerik.com') AS [Full Email Addresses] FROM Employees
---------------------------------------------------------------------
SELECT DISTINCT Salary FROM Employees ORDER BY Salary
---------------------------------------------------------------------
SELECT FirstName + ' ' + LastName AS [Full Name]
FROM Employees
WHERE JobTitle='Sales Representative' 
---------------------------------------------------------------------
SELECT (FirstName + ' ' + LastName) AS [Full Name]
FROM Employees
WHERE FirstName LIKE 'SA%'
---------------------------------------------------------------------
SELECT (FirstName + ' ' + LastName) AS [Full Name]
FROM Employees
WHERE LastName LIKE '%ei%'
---------------------------------------------------------------------
SELECT (FirstName + ' ' + LastName) AS [Full Name], Salary
FROM Employees
WHERE Salary BETWEEN 20000 AND 30000
---------------------------------------------------------------------
SELECT (FirstName + ' ' + LastName) AS [Full Name], Salary
FROM Employees
WHERE (Salary = 25000 OR Salary = 14000 OR Salary = 12500 OR Salary = 23600)
---------------------------------------------------------------------
SELECT (FirstName + ' ' + LastName) AS [Full Name], Salary, ManagerID
FROM Employees
WHERE ManagerID IS NULL
---------------------------------------------------------------------
SELECT (FirstName + ' ' + LastName) AS [Full Name], Salary
FROM Employees
WHERE Salary>=50000 ORDER BY Salary DESC
---------------------------------------------------------------------
SELECT TOP 5 *
FROM Employees
ORDER BY Salary DESC

SELECT TOP 5 Salary
FROM Employees
ORDER BY Salary DESC
---------------------------------------------------------------------
SELECT (e.FirstName + ' ' + e.LastName) AS [Full Name], a.AddressText
FROM Employees e
	JOIN Addresses a
	ON e.AddressID=a.AddressID
---------------------------------------------------------------------
SELECT (e.FirstName + ' ' + e.LastName) AS [Full Name], a.AddressText
FROM Employees e, Addresses a
WHERE e.AddressID=a.AddressID
---------------------------------------------------------------------
SELECT (e.FirstName + ' ' + e.LastName) AS [Employee],
	(m.FirstName + ' ' + m.LastName) AS [Manager]
FROM Employees e LEFT OUTER JOIN Employees m
	ON e.ManagerID=m.EmployeeID
---------------------------------------------------------------------
SELECT (e.FirstName + ' ' + e.LastName) AS [Employee],
	(m.FirstName + ' ' + m.LastName) AS [Manager],
	a.AddressText AS [Address]
FROM Employees e
	LEFT OUTER JOIN Employees m
		ON e.ManagerID=m.EmployeeID
	JOIN Addresses a
		ON e.AddressID=a.AddressID
---------------------------------------------------------------------
SELECT Name AS TownsAndDepartments
FROM Towns
UNION
SELECT Name AS TownsAndDepartments
FROM Departments
---------------------------------------------------------------------
SELECT e.FirstName + ' ' + e.LastName AS [Employee],
	m.FirstName + ' ' + m.LastName AS [Manager]
FROM Employees m RIGHT OUTER JOIN Employees e
		ON e.ManagerID = m.EmployeeID
---------------------------------------------------------------------
SELECT e.FirstName, e.LastName
FROM Employees e JOIN Departments d
	ON e.DepartmentID=d.DepartmentID
	WHERE d.Name IN ('Sales', 'Finance') 
	AND DATEPART(YEAR, e.HireDate) BETWEEN 1995 AND 2005
	ORDER BY FirstName ASC , LastName ASC
---------------------------------------------------------------------
SELECT e.FirstName + ' ' + e.LastName AS [Employee]
FROM Employees e JOIN Departments d
	ON e.DepartmentID = d.DepartmentID
	WHERE d.Name IN ('Sales', 'Finance') AND
	DATEPART(YEAR, e.HireDate) BETWEEN 1995 AND 2005
	ORDER BY FirstName ASC , LastName ASC
