/*==============================================================*/
/* DBMS name:      Microsoft SQL Server 2008                    */
/* Created on:     2018/4/20 11:52:01                           */
/*==============================================================*/


if exists (select 1
            from  sysobjects
           where  id = object_id('Sys_Menu')
            and   type = 'U')
   drop table Sys_Menu
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Sys_Role')
            and   type = 'U')
   drop table Sys_Role
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Sys_User')
            and   type = 'U')
   drop table Sys_User
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Sys_User_Role')
            and   type = 'U')
   drop table Sys_User_Role
go

/*==============================================================*/
/* Table: Sys_Menu                                              */
/*==============================================================*/
create table Sys_Menu (
   SM_ID                varchar(32)          not null,
   SM_Name              nvarchar(50)         null,
   SM_ParentID          varchar(32)          null,
   SM_Order             int                  null,
   SM_Icon              varchar(50)          null,
   SM_Remark            nvarchar(1000)       null,
   constraint PK_SYS_MENU primary key (SM_ID)
)
go

/*==============================================================*/
/* Table: Sys_Role                                              */
/*==============================================================*/
create table Sys_Role (
   SR_ID                varchar(32)          not null,
   SR_Name              nvarchar(50)         null,
   SR_CreateTime        datetime             null,
   SR_Remark            nvarchar(1000)       null,
   constraint PK_SYS_ROLE primary key (SR_ID)
)
go

/*==============================================================*/
/* Table: Sys_User                                              */
/*==============================================================*/
create table Sys_User (
   SU_ID                varchar(32)          not null,
   SU_Account           varchar(50)          not null,
   SU_Password          varchar(100)         not null,
   SU_RealName          nvarchar(50)         null,
   SU_NickName          nvarchar(50)         null,
   SU_HeadIcon          varchar(50)          null,
   SU_Gender            int                  null,
   SU_Birthday          datetime             null,
   SU_Phone             varchar(20)          null,
   SU_Email             varchar(50)          null,
   SU_WeChat            varchar(50)          null,
   SU_SortCode          int                  null,
   SU_Remark            varchar(500)         null,
   SU_IsDelete          int                  null,
   SU_IsEnable          int                  null,
   SU_LastLoginTime     datetime             null,
   SU_LoginTimes        int                  null,
   SU_IP                varchar(20)          null,
   constraint PK_SYS_USER primary key (SU_ID)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('Sys_User') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'Sys_User' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   'É¾³ý±êÖ¾(SU_IsDelete)
   0£ºÎ´É¾³ý
   1£ºÉ¾³ý', 
   'user', @CurrentUser, 'table', 'Sys_User'
go

/*==============================================================*/
/* Table: Sys_User_Role                                         */
/*==============================================================*/
create table Sys_User_Role (
   SU_ID                varchar(32)          null,
   SR_ID                varchar(32)          null
)
go

