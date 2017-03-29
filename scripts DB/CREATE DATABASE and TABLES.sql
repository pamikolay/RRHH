CREATE DATABASE RRHH

go

use RRHH

go

--create table Cv(
--CvID int primary key not null,
--CvStatus varchar (20) not null
--)

create table UserTypes(
ID int primary key not null,
Name varchar (20) not null
)

create table Provinces(
ID int primary key not null,
Name varchar (50) not null
)

create table Citys(
ID int primary key not null,
Province int,
Name varchar (50) not null
)

create table Users(
ID int primary key IDENTITY(1,1) not null,
FirstName varchar(50) not null,
LastName varchar(50) not null,
Email varchar(50) not null,
Address varchar(50) not null,
Phone varchar(50) not null,
Password varchar(15) not null,
Genre varchar(15) not null,
CvStatus varchar(15),
City int,
Province int,
--CvID int,
UserType int
)

create table Companys(
ID int primary key IDENTITY(1,1) not null,
Name varchar(50) not null,
Address varchar(50),
Phone varchar(50)
)

create table JobStatuses(
ID int primary key not null,
Details varchar(15) not null
)

create table Jobs(
ID int primary key IDENTITY(1,1) not null,
Name varchar(100) not null,
Date datetime not null,
Description varchar(MAX) not null,
Company int,
Status int
)

create table JobApplications(
ID int primary key not null,
Details varchar(50) not null
)

create table Interviews(
ID int primary key not null,
Status varchar(50) not null

)

create table Applicants(
ID int primary key IDENTITY(1,1) not null,
Date datetime not null,
Postulant int,
Job int,
ApplicationStatus int,
InterviewStatus int
)


go



alter table Citys
add constraint fk_Citys_Provinces
foreign key(Province) references Provinces(ID)

go



alter table Users
add constraint fk_Users_Citys
foreign key(City) references Citys(ID)

go



alter table Users
add constraint fk_Users_Provinces
foreign key(Province) references Provinces(ID)

go



--alter table UserTable
--add constraint fk_usertablecv
--foreign key(CvID) references Cv(CvID)

--go



alter table Users
add constraint fk_Users_Usertypes
foreign key(UserType) references UserTypes(ID)

go



alter table Jobs
add constraint fk_Jobs_Companys
foreign key(Company) references Companys(ID)

go



alter table Jobs
add constraint fk_Jobs_Jobstatuses
foreign key(Status) references JobStatuses(ID)

go



alter table Applicants
add constraint fk_Applicants_Users
foreign key(Postulant) references Users(ID)

go



alter table Applicants
add constraint fk_Applicants_Jobs
foreign key(Job) references Jobs(ID)

go



alter table Applicants
add constraint fk_Applicants_Jobapplications
foreign key(ApplicationStatus) references JobApplications(ID)

go



alter table Applicants
add constraint fk_Applicants_Interviews
foreign key(InterviewStatus) references Interviews(ID)

go



alter table Users
add unique (Email)

go