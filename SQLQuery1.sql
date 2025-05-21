CREATE DATABASE	 StudentDB;
GO

USE StudentDB;
GO

Create TABLE Students (
	Id int Primary KEY IDENTITY(1,1),
	FullName NVARCHAR(100),
	Age int,
	course nvarchar(50)
);
GO

insert into Students (FullName,Age,course) Values
('Ali Karimov', 20, 'Backend'),
('Dilnoza Mamatova', 22, 'Frontend'),
('Jasur Rahimov', 19, 'Fullstack');
Go