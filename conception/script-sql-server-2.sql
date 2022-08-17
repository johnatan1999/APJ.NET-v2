/*==============================================================*/
/* Nom de SGBD :  Microsoft SQL Server 2008                     */
/* Date de création :  15/09/2021 16:22:17                      */
/*==============================================================*/


if exists (select 1
            from  sysindexes
           where  id    = object_id('BET')
            and   name  = 'RELATION_5_FK'
            and   indid > 0
            and   indid < 255)
   drop index BET.RELATION_5_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('BET')
            and   name  = 'RELATION_4_FK'
            and   indid > 0
            and   indid < 255)
   drop index BET.RELATION_4_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('BET')
            and   type = 'U')
   drop table BET
go

if exists (select 1
            from  sysobjects
           where  id = object_id('CATEGORY')
            and   type = 'U')
   drop table CATEGORY
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('MATCHS')
            and   name  = 'RELATION_7_FK'
            and   indid > 0
            and   indid < 255)
   drop index MATCHS.RELATION_7_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('MATCHS')
            and   name  = 'RELATION_3_FK'
            and   indid > 0
            and   indid < 255)
   drop index MATCHS.RELATION_3_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('MATCHS')
            and   name  = 'RELATION_2_FK'
            and   indid > 0
            and   indid < 255)
   drop index MATCHS.RELATION_2_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('MATCHS')
            and   type = 'U')
   drop table MATCHS
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('TEAM')
            and   name  = 'RELATION_1_FK'
            and   indid > 0
            and   indid < 255)
   drop index TEAM.RELATION_1_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('TEAM')
            and   type = 'U')
   drop table TEAM
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('TRANSACTIONS')
            and   name  = 'RELATION_6_FK'
            and   indid > 0
            and   indid < 255)
   drop index TRANSACTIONS.RELATION_6_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('TRANSACTIONS')
            and   type = 'U')
   drop table TRANSACTIONS
go

if exists (select 1
            from  sysobjects
           where  id = object_id('UTILISATEUR')
            and   type = 'U')
   drop table UTILISATEUR
go

/*==============================================================*/
/* Table : BET                                                  */
/*==============================================================*/
create table BET (
   IDBET                varchar(50)          not null,
   IDMATCH              varchar(50)          null,
   IDUSER               varchar(50)          null,
   BETVALUE             int                  not null,
   ODDS                 decimal              not null,
   BETDATE              timestamp            not null,
   STATE                int                  null default 0,
   constraint PK_BET primary key nonclustered (IDBET)
)
go

/*==============================================================*/
/* Index : RELATION_4_FK                                        */
/*==============================================================*/
create index RELATION_4_FK on BET (
IDUSER ASC
)
go

/*==============================================================*/
/* Index : RELATION_5_FK                                        */
/*==============================================================*/
create index RELATION_5_FK on BET (
IDMATCH ASC
)
go

/*==============================================================*/
/* Table : CATEGORY                                             */
/*==============================================================*/
create table CATEGORY (
   IDCATEGORY           varchar(50)          not null,
   NAME                 varchar(50)          not null,
   constraint PK_CATEGORY primary key nonclustered (IDCATEGORY)
)
go

/*==============================================================*/
/* Table : MATCHS                                               */
/*==============================================================*/
create table MATCHS (
   IDMATCH              varchar(50)          not null,
   IDCATEGORY           varchar(50)          null,
   IDTEAMA              varchar(50)          null,
   IDTEAMB              varchar(50)          null,
   SCOREA               int                  null default 0,
   SCOREB               int                  null default 0,
   ODDSA                decimal              not null,
   ODDSB                decimal              not null,
   ODDSNULL             decimal              not null,
   MATCHDATE            timestamp            not null,
   STATE                int                  null default 0,
   constraint PK_MATCHS primary key nonclustered (IDMATCH)
)
go

/*==============================================================*/
/* Index : RELATION_2_FK                                        */
/*==============================================================*/
create index RELATION_2_FK on MATCHS (
IDTEAMA ASC
)
go

/*==============================================================*/
/* Index : RELATION_3_FK                                        */
/*==============================================================*/
create index RELATION_3_FK on MATCHS (
IDTEAMB ASC
)
go

/*==============================================================*/
/* Index : RELATION_7_FK                                        */
/*==============================================================*/
create index RELATION_7_FK on MATCHS (
IDCATEGORY ASC
)
go

/*==============================================================*/
/* Table : TEAM                                                 */
/*==============================================================*/
create table TEAM (
   IDTEAM               varchar(50)          not null,
   IDCATEGORY           varchar(50)          null,
   NAME                 varchar(50)          not null,
   STATE                int                  null default 0,
   constraint PK_TEAM primary key nonclustered (IDTEAM)
)
go

/*==============================================================*/
/* Index : RELATION_1_FK                                        */
/*==============================================================*/
create index RELATION_1_FK on TEAM (
IDCATEGORY ASC
)
go

/*==============================================================*/
/* Table : TRANSACTIONS                                         */
/*==============================================================*/
create table TRANSACTIONS (
   IDTRANSACTION        varchar(50)          not null,
   IDUSER               varchar(50)          null,
   JETON                int                  not null,
   TYPETRANSACTION      varchar(25)          not null,
   DATETRANSACTION      timestamp            not null,
   constraint PK_TRANSACTIONS primary key nonclustered (IDTRANSACTION)
)
go

/*==============================================================*/
/* Index : RELATION_6_FK                                        */
/*==============================================================*/
create index RELATION_6_FK on TRANSACTIONS (
IDUSER ASC
)
go

/*==============================================================*/
/* Table : UTILISATEUR                                          */
/*==============================================================*/
create table UTILISATEUR (
   ID              varchar(50)          not null,
   LOGIN                varchar(200)         not null,
   PASSWORD             varchar(600)         not null,
   USERNAME            varchar(100)         not null,
   JETONS               int                  null,
   STATE                int                  null default 0,
   [IdRole] VARCHAR(100) NOT NULL,
	CONSTRAINT [PK_UTILISATEUR] PRIMARY KEY NONCLUSTERED ([ID] ASC),
	FOREIGN KEY ([IdRole]) REFERENCES [dbo].[Roles] ([Id])
)
go

create sequence menu_seq start with 1 increment by 1;
create sequence utilisateur_seq start with 1 increment by 1;
create sequence bet_seq start with 1 increment by 1;
create sequence team_seq start with 1 increment by 1;
create sequence Roles_Seq start with 1 increment by 1;
create sequence transactions_seq start with 1 increment by 1;
create sequence matchs_seq start with 1 increment by 1;
create sequence historique_seq start with 1 increment by 1;
create sequence category_seq start with 1 increment by 1;


