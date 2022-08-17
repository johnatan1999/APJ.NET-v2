create user apj# identified by apj#;
grant dba to apj#;

CREATE TABLE Roles
(
	Id VARCHAR(100) NOT NULL PRIMARY KEY, 
    Description VARCHAR(200) NULL
);

CREATE TABLE Utilisateur
(
	Id VARCHAR(100) NOT NULL PRIMARY KEY, 
    Login VARCHAR(100) NOT NULL, 
    Password VARCHAR(500) NOT NULL, 
    Username VARCHAR(100) NOT NULL, 
    State INT DEFAULT 0, 
    IdRole VARCHAR(100) NOT NULL REFERENCES Roles(Id)
);

CREATE TABLE Menu
(
	Id VARCHAR(100) NOT NULL PRIMARY KEY, 
    Url VARCHAR(500) DEFAULT '#', 
    Label VARCHAR(100) NOT NULL, 
    Rank INT, 
    IdParent VARCHAR(100) REFERENCES Menu(Id) 
);

CREATE TABLE Permission
(
	Id VARCHAR(100) NOT NULL PRIMARY KEY, 
    ApiUrl VARCHAR(500) NOT NULL, 
    Description VARCHAR(200) 
);

CREATE TABLE RolePermission
(
	Id VARCHAR(100) NOT NULL PRIMARY KEY, 
    IdRole VARCHAR(100) NOT NULL, 
    IdPermission VARCHAR(100) NOT NULL, 
    ApiUrl VARCHAR(500) NOT NULL
);

CREATE TABLE Historique
(
	Id VARCHAR(100) NOT NULL PRIMARY KEY, 
    IdUser VARCHAR(100) NOT NULL REFERENCES Utilisateur(Id), 
    RefObject VARCHAR(100) NOT NULL, 
    Action VARCHAR(500) NOT NULL, 
    DateAction DATE NOT NULL, 
    TimeAction VARCHAR(50) NOT NULL,
    Object VARCHAR(100) NOT NULL,
);

create sequence menu_seq start with 1 increment by 1;

create sequence permission_seq start with 1 increment by 1;

create sequence rolepermission_seq start with 1 increment by 1;

create sequence roles_seq start with 1 increment by 1;

create sequence utilisateur_seq start with 1 increment by 1;

create sequence historique_seq start with 1 increment by 1;