-- Создание базы --

create database RkkInfo_db
go

-- Конец --

-- Использовать для базы --

use RkkInfo_db
go

create table RkkInfo_Users
(
RkkInfo_Users_id int primary key identity(1,1),
RkkInfo_Users_Login nvarchar(50),
RkkInfo_Users_Password nvarchar(50),
)
create table RkkInfo_Main
(
RkkInfo_Main_id int primary key identity(1,1),
RkkInfo_Employees_id int,
RkkInfo_Branch_id int,
RkkInfo_Files_id int,
RkkInfo_Jobs_Opening_id int,
RkkInfo_Jobs_Vacancy_id int,
RkkInfo_Vacation_id int,
RkkInfo_Dismissal_id int,
)
create table RkkInfo_Employees
(
RkkInfo_Employees_id int primary key identity(1,1),
RkkInfo_Employees_First_Name nvarchar(50),
RkkInfo_Employees_Last_Name nvarchar(50),
RkkInfo_Employees_Position nvarchar(50),
RkkInfo_Employees_Department nvarchar(50),
RkkInfo_Employees_Start_Date nvarchar(50),
RkkInfo_Employees_Is_Active nvarchar(50),
)
create table RkkInfo_Branch
(
RkkInfo_Branch_id int primary key identity(1,1),
RkkInfo_Branch_Name nvarchar(50),
)
create table RkkInfo_Files
(
RkkInfo_Files_id int primary key identity(1,1),
RkkInfo_Files_Name nvarchar(50),
RkkInfo_Files_Data nvarchar(50),
RkkInfo_Files_Files varbinary(max),
)
create table RkkInfo_Jobs_Opening
(
RkkInfo_Jobs_Opening_id int primary key identity(1,1),
RkkInfo_Jobs_Opening_Name nvarchar(50),
RkkInfo_Jobs_Opening_Date nvarchar(50),
RkkInfo_Jobs_Opening_Files varbinary(max),
RkkInfo_Jobs_Opening_Status nvarchar(50),
)
create table RkkInfo_Jobs_Vacancy
(
RkkInfo_Jobs_Vacancy_id int primary key identity(1,1),
RkkInfo_Jobs_Vacancy_Name nvarchar(50),
RkkInfo_Jobs_Vacancy_First_Name nvarchar(50),
RkkInfo_Jobs_Vacancy_Last_Name nvarchar(50),
RkkInfo_Jobs_Vacancy_Position nvarchar(50),
RkkInfo_Jobs_Vacancy_Date nvarchar(50),
RkkInfo_Jobs_Vacancy_Files varbinary(max),
RkkInfo_Jobs_Vacancy_Status nvarchar(50),
)
create table RkkInfo_Vacation
(
RkkInfo_Vacation_id int primary key identity(1,1),
RkkInfo_Vacation_Name nvarchar(50),
RkkInfo_Vacation_First_Name nvarchar(50),
RkkInfo_Vacation_Last_Name nvarchar(50),
RkkInfo_Vacation_Position nvarchar(50),
RkkInfo_Vacation_Start_Date nvarchar(50),
RkkInfo_Vacation_End_Date nvarchar(50),
RkkInfo_Vacation_Files varbinary(max),
RkkInfo_Vacation_Status nvarchar(50),
)
create table RkkInfo_Dismissal
(
RkkInfo_Dismissal_id int primary key identity(1,1),
RkkInfo_Dismissal_Name nvarchar(50),
RkkInfo_Dismissal_First_Name nvarchar(50),
RkkInfo_Dismissal_Last_Name nvarchar(50),
RkkInfo_Dismissal_Position nvarchar(50),
RkkInfo_Dismissal_Date nvarchar(50),
RkkInfo_Dismissal_Files varbinary(max),
RkkInfo_Dismissal_Status nvarchar(50),
)