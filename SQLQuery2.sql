create database StudentDb2;
go

use StudentDb2;
go

create table Studentss (
	Id int primary key identity(1,1),
	Fullname nvarchar(100) not null,
	Age int not null,
	Course nvarchar(50) not null
);