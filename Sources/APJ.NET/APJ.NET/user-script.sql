CREATE TABLE [dbo].[Roles]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Description] VARCHAR(200) NULL
);

CREATE TABLE [dbo].[Utilisateur]
(
	[Id] VARCHAR(100) NOT NULL PRIMARY KEY, 
    [Login] VARCHAR(100) NOT NULL, 
    [Password] VARCHAR(500) NOT NULL, 
    [Username] VARCHAR(100) NOT NULL, 
    [State] INT NULL DEFAULT 0, 
    [IdRole] VARCHAR(100) NOT NULL REFERENCES Roles(Id)
);

CREATE TABLE [dbo].[Menu]
(
	[Id] VARCHAR(100) NOT NULL PRIMARY KEY, 
    [Url] VARCHAR(500) NULL DEFAULT '#', 
    [Label] VARCHAR(100) NOT NULL, 
    [Rank] INT NULL, 
    [IdParent] VARCHAR(100) NULL REFERENCES Menu(Id) 
);

CREATE TABLE [dbo].[Permission]
(
	[Id] VARCHAR(100) NOT NULL PRIMARY KEY, 
    [ApiUrl] VARCHAR(500) NOT NULL, 
    [Description] VARCHAR(200) NULL
);

CREATE TABLE [dbo].[RolePermission]
(
	[Id] VARCHAR(100) NOT NULL PRIMARY KEY, 
    [IdRole] VARCHAR(100) NOT NULL, 
    [IdPermission] VARCHAR(100) NOT NULL, 
    [ApiUrl] VARCHAR(500) NOT NULL
);

CREATE TABLE [dbo].[Historique]
(
	[Id] VARCHAR(100) NOT NULL PRIMARY KEY, 
    [IdUser] VARCHAR(100) NOT NULL references Utilisateur(Id), 
    [RefObject] VARCHAR(100) NOT NULL, 
    [Action] VARCHAR(500) NOT NULL, 
    [Object] VARCHAR(100) NOT NULL,
    [DateAction] DATE NOT NULL, 
    [TimeAction] VARCHAR(50) NOT NULL
)


create sequence menu_seq start with 1 increment by 1;

create sequence permission_seq start with 1 increment by 1;

create sequence rolepermission_seq start with 1 increment by 1;

create sequence roles_seq start with 1 increment by 1;

create sequence utilisateur_seq start with 1 increment by 1;

create sequence historique_seq start with 1 increment by 1;