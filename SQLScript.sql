
/* create tables */

CREATE TABLE Roles (
	Id int IDENTITY(1,1) NOT NULL PRIMARY KEY,
	Name NVARCHAR(30) NOT NULL UNIQUE
)

CREATE TABLE Users (
	Id int IDENTITY(1,1) NOT NULL PRIMARY KEY,
	LastName NVARCHAR(50) NOT NULL,
	FirstName NVARCHAR(50) NOT NULL,
	Email NVARCHAR(100) NOT NULL UNIQUE,
	PasswordHash NVARCHAR(300) NOT NULL,
	Phone NVARCHAR(22) NULL,
)

CREATE TABLE UserRoles (
	RoleId INT NOT NULL FOREIGN KEY REFERENCES Roles(Id) ON DELETE CASCADE,
	UserId INT NOT NULL FOREIGN KEY REFERENCES Users(Id) ON DELETE CASCADE,
	CONSTRAINT PK_UserRoleCompositeKey PRIMARY KEY (RoleId, UserId)
)


/* insert some roles */
INSERT INTO Roles (Name) VALUES
('Super User'),
('General User')

