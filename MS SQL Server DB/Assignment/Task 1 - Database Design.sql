--Task 1:Database Design

--Create Database
CREATE DATABASE SISDB;

--Use database SISDB
USE SISDB;

--Create Student Table
CREATE TABLE Students(
student_id int IDENTITY(01,1) PRIMARY KEY NOT NULL,
first_name varchar(50),
last_name varchar(50),
date_of_birth date,
e_mail varchar(50),
phone_number bigint
);

--Create Teacher Table
CREATE TABLE Teacher(
teacher_id int IDENTITY(100,1) PRIMARY KEY NOT NULL,
first_name varchar(50),
last_name varchar(50),
e_mail varchar(50)
);

--Create Course Table
CREATE TABLE Courses(
course_id int IDENTITY(200,1) PRIMARY KEY NOT NULL,
course_name varchar(50),
credits int,
Teacher int NOT NULL,
CONSTRAINT FK_Teacher FOREIGN KEY(Teacher) REFERENCES Teacher(teacher_id)
);

--Create Enrollment Table
CREATE TABLE Enrollments(
enrollment_id int IDENTITY(1000,1) PRIMARY KEY NOT NULL,
enrollment_date date,
Students int NOT NULL,
CONSTRAINT FK_Students FOREIGN KEY(Students) REFERENCES Students(student_id),
Courses int NOT NULL,
CONSTRAINT FK_Courses FOREIGN KEY(Courses) REFERENCES Courses(course_id),
);

--Create Payment Table
CREATE TABLE Payments(
payment_id bigint PRIMARY KEY NOT NULL,
Students int NOT NULL,
CONSTRAINT FK_Student FOREIGN KEY(Students) REFERENCES Students(student_id),
amount decimal,
payment_date date
);

--Inserting 10 sample records into Student Table
INSERT INTO Students(first_name,last_name,date_of_birth,e_mail,phone_number)
VALUES ('Vishal','Jonathan','2004-01-01','vishaljonathan1001@gmail.com','9751963858');
INSERT INTO Students(first_name,last_name,date_of_birth,e_mail,phone_number)
VALUES ('Santhosh','Kumar','2003-08-19','santhoshkumar@gmail.com','9843565733');
INSERT INTO Students(first_name,last_name,date_of_birth,e_mail,phone_number)
VALUES ('Nithin','Narayanan','2003-02-14','nithinnarayanan@gmail.com','9453256677');
INSERT INTO Students(first_name,last_name,date_of_birth,e_mail,phone_number)
VALUES ('Arwin','Parasu','2003-09-07','arwinparasu@gmail.com','9854732345');
INSERT INTO Students(first_name,last_name,date_of_birth,e_mail,phone_number)
VALUES ('Praveen','Kumar','2003-02-07','praveenkumar@gmail.com','6369487995');
INSERT INTO Students(first_name,last_name,date_of_birth,e_mail,phone_number)
VALUES ('Jaga','Prathaban','2003-02-18','jagaprathaban@gmail.com','8148987730');
INSERT INTO Students(first_name,last_name,date_of_birth,e_mail,phone_number)
VALUES ('Mohamed','Arshad','2003-10-14','mohamedarshad@gmail.com','9456256478');
INSERT INTO Students(first_name,last_name,date_of_birth,e_mail,phone_number)
VALUES ('Jerlin','Trixie','2003-10-28','jerlintrixie@gmail.com','8249997831');
INSERT INTO Students(first_name,last_name,date_of_birth,e_mail,phone_number)
VALUES ('Shivani','Suvatheka','2003-10-12','shivanisuvatheka@gmail.com','8350007932');
INSERT INTO Students(first_name,last_name,date_of_birth,e_mail,phone_number)
VALUES ('Sarah','Martin','2004-09-12','sarah.martin@gmail.com','9852973959');

--Display Student Table after inserting values
SELECT *FROM Students;

--Inserting sample records into Teachers Table
INSERT INTO Teacher(first_name,last_name,e_mail)
VALUES ('Janet','Stephen','janetstephen@gmail.com');
INSERT INTO Teacher(first_name,last_name,e_mail)
VALUES ('Pradeep','Kumar','pradeepkumar@gmail.com');
INSERT INTO Teacher(first_name,last_name,e_mail)
VALUES ('Robert','Steve','robert.steve@gmail.com');
INSERT INTO Teacher(first_name,last_name,e_mail)
VALUES ('Laurra','Haris','laura.harris@gmail.com');
INSERT INTO Teacher(first_name,last_name,e_mail)
VALUES ('Daniel','Clark','daniel.clark@gmail.com');
INSERT INTO Teacher(first_name,last_name,e_mail)
VALUES ('Karthick','Kumar','karthickkumar@gmail.com');
INSERT INTO Teacher(first_name,last_name,e_mail)
VALUES ('Suveetha','Dhanaselvam','suveethadhanaselvam@gmail.com');
INSERT INTO Teacher(first_name,last_name,e_mail)
VALUES ('Uma','Maheswari','umamahes@gmail.com');
INSERT INTO Teacher(first_name,last_name,e_mail)
VALUES ('Kartika','Mahendran','karthika.mahendran@gmail.com');

--Display Teacher Table after inserting values
SELECT *FROM Teacher;

--Inserting sample values into Courses Table
INSERT INTO Courses(course_name,credits,Teacher)
VALUES ('Electronics & Communication','4',100);
INSERT INTO Courses(course_name,credits,Teacher)
VALUES ('Computer Science','5',101);
INSERT INTO Courses(course_name,credits,Teacher)
VALUES ('Information Technology','4',103);
INSERT INTO Courses(course_name,credits,Teacher)
VALUES ('Electrical & Electronics','3',102);
INSERT INTO Courses(course_name,credits,Teacher)
VALUES ('Civil','3',104);
INSERT INTO Courses(course_name,credits,Teacher)
VALUES ('Mechanical','2',105);
INSERT INTO Courses(course_name,credits,Teacher)
VALUES ('Artificial Intelligence','3',106);
INSERT INTO Courses(course_name,credits,Teacher)
VALUES ('Data Science','3',107);
INSERT INTO Courses(course_name,credits,Teacher)
VALUES ('Management Studies','4',108);

--Display Course table after inserting sample values
SELECT *FROM Courses;

--Inserting sample data into enrollments table
INSERT INTO Enrollments(Students,Courses,enrollment_date)
VALUES(1,200,'2021-11-15');
INSERT INTO Enrollments(Students,Courses,enrollment_date)
VALUES(2,201,'2021-11-02');
INSERT INTO Enrollments(Students,Courses,enrollment_date)
VALUES(3,202,'2021-10-04');
INSERT INTO Enrollments(Students,Courses,enrollment_date)
VALUES(4,203,'2021-11-16');
INSERT INTO Enrollments(Students,Courses,enrollment_date)
VALUES(5,204,'2021-11-14');
INSERT INTO Enrollments(Students,Courses,enrollment_date)
VALUES(6,205,'2021-10-29');
INSERT INTO Enrollments(Students,Courses,enrollment_date)
VALUES(7,206,'2021-11-28');
INSERT INTO Enrollments(Students,Courses,enrollment_date)
VALUES(8,207,'2021-11-01');
INSERT INTO Enrollments(Students,Courses,enrollment_date)
VALUES(9,208,'2021-10-10');
INSERT INTO Enrollments(Students,Courses,enrollment_date)
VALUES(10,200,'2021-11-11');

--Display Enrollment Table after inserting sample values
SELECT *FROM Enrollments;

--Inserting sample values into Payment Table
INSERT INTO Payments(payment_id,Students,amount,payment_date)
VALUES (6705452502,1, 80000.00, '2021-11-15'),
(3937576888,2, 100000.00, '2021-11-03'),
(2976206059,3, 90000.00, '2021-10-05'),
(3410156173,4, 70000.00, '2021-11-16'),
(7587149070,5, 65000.00, '2021-11-14'),
(8377705954,6, 50000.00, '2021-10-29'),
(7644992430,7, 50000.00, '2021-11-29'),
(7999470324,8, 95000.00, '2021-11-02'),
(5170095376,9, 95000.00, '2021-10-10'),
(1476758770,10, 80000.00, '2021-11-12');

--Display Payment Table after inserting sample values
SELECT *FROM Payments;

--Display all tables together
SELECT *FROM Students;
SELECT *FROM Courses;
SELECT *FROM Teacher;
SELECT *FROM Enrollments;
SELECT *FROM Payments;