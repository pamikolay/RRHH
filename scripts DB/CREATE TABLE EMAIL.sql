use RRHH

go

create table Emails(
ID int primary key IDENTITY(1,1) not null,
Date date not null,
ApplicantReference int not null,
Asunto varchar(100) not null,
Cuerpo varchar(MAX) not null
)

go



alter table Emails
add constraint fk_Emails_Applicants
foreign key(ApplicantReference) references Applicants(ID)

go