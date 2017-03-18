CREATE DATABASE RRHH

go

use RRHH

go

create table Cv(
CvID int primary key not null,
CvStatus varchar (20) not null
)

create table UserType(
UserTypeID int primary key not null,
UserTypeName varchar (20) not null
)

create table Province(
ProvinceID int primary key not null,
ProvinceName varchar (50) not null
)

create table City(
CityID int primary key not null,
ProvinceID int,
CityName varchar (50) not null
)

create table UserTable(
UserTableID int primary key IDENTITY(1,1) not null,
UserTableFirstName varchar(50) not null,
UserTableLastName varchar(50) not null,
UserTableEmail varchar(50) not null,
UserTableAddress varchar(50) not null,
UserTablePhone varchar(50) not null,
UserTablePassword varchar(15) not null,
UserTableGenre varchar(15) not null,
CityID int,
ProvinceID int,
CvID int,
UserTypeID int
)

create table Company(
CompanyID int primary key IDENTITY(1,1) not null,
CompanyName varchar(50) not null,
CompanyAddress varchar(50),
CompanyPhone varchar(50)
)

create table JobStatus(
JobStatusID int primary key not null,
JobStatusDetails varchar(15) not null
)

create table Job(
JobID int primary key IDENTITY(1,1) not null,
JobName varchar(100) not null,
JobDate date not null,
JobDescription varchar(MAX) not null,
CompanyID int,
JobStatusID int
)

create table JobApplication(
JobApplicationID int primary key not null,
JobApplicationDetails varchar(50) not null
)

create table Interview(
InterviewID int primary key not null,
InterviewStatus varchar(50) not null

)

create table Applicant(
ApplicantID int primary key IDENTITY(1,1) not null,
ApplicantDate date not null,
UserTableID int,
JobID int,
JobApplicationID int,
InterviewID int
)


go



alter table City
add constraint fk_cityprovince
foreign key(ProvinceID) references Province(ProvinceID)

go



alter table UserTable
add constraint fk_usertablecity
foreign key(CityID) references City(CityID)

go



alter table UserTable
add constraint fk_usertableprovince
foreign key(ProvinceID) references Province(ProvinceID)

go



alter table UserTable
add constraint fk_usertablecv
foreign key(CvID) references Cv(CvID)

go



alter table UserTable
add constraint fk_usertableusertype
foreign key(UserTypeID) references UserType(UserTypeID)

go



alter table Job
add constraint fk_jobcompany
foreign key(CompanyID) references Company(CompanyID)

go



alter table Job
add constraint fk_jobjobstatus
foreign key(JobStatusID) references JobStatus(JobStatusID)

go



alter table Applicant
add constraint fk_applicantusertable
foreign key(UserTableID) references UserTable(UserTableID)

go



alter table Applicant
add constraint fk_applicantjob
foreign key(JobID) references Job(JobID)

go



alter table Applicant
add constraint fk_applicantjobapplication
foreign key(JobApplicationID) references JobApplication(JobApplicationID)

go



alter table Applicant
add constraint fk_applicantinterview
foreign key(InterviewID) references Interview(InterviewID)

go



alter table UserTable
add unique (UserTableEmail)

go