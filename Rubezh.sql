-- Создание базы --

create database Rubezh_db
go

-- Конец --

-- Использовать для базы --

use Rubezh_db
go

create table Rubezh_Users
(
Rubezh_Users_id int primary key identity(1,1),
Rubezh_Users_Login nvarchar(50),
Rubezh_Users_Password nvarchar(50),
Rubezh_Users_Level nvarchar(50),
)
create table Rubezh_Main
(
Rubezh_Main_id int primary key identity(1,1),
Rubezh_Company_id int,
Rubezh_Catalog_id int,
Rubezh_News_id int,
Rubezh_Price_List_id int,
)
create table Rubezh_Company
(
Rubezh_Company_id int primary key identity(1,1),
Rubezh_Company_Name nvarchar(50),
)


create table Rubezh_Catalog
(
Rubezh_Catalog_id int primary key identity(1,1),
Rubezh_Catalog_Name_Company nvarchar(50),
Rubezh_Catalog_Name nvarchar(50),
Rubezh_Catalog_Image varbinary(max) not null,
Rubezh_Catalog_Description nvarchar(max),
Rubezh_Catalog_Feature nvarchar(max),
Rubezh_Catalog_Link nvarchar(max),
)

create table Rubezh_News
(
Rubezh_News_id int primary key identity(1,1),
Rubezh_News_Name nvarchar(max),
Rubezh_News_Date nvarchar(50),
Rubezh_News_Text nvarchar(max),
)
create table Rubezh_Price_List
(
Rubezh_Price_List_id int primary key identity(1,1),
Rubezh_Price_List_Name nvarchar(50),
Rubezh_Price_List_File varbinary(max),
)

