--Use SISDB
USE SISDB;

--Insert student details into the student table
INSERT INTO Students(first_name,last_name,date_of_birth,e_mail,phone_number)
VALUES('John','Doe','1995-08-15','john.doe@example.com',1234567890);

--Display Student Table after adding a record
SELECT *FROM Students;

--Enroll a student in a course
INSERT INTO Enrollments(Students,Courses,enrollment_date)
VALUES(11,206,'2012-10-11');

--Display Enrollment Table after enrolling a student in a course
SELECT *FROM Enrollments;

--Modifying the email address of a teacher - Modifying the email address of the first teacher from janetstephen@gmail.com to janet.stephen@gmail.com
UPDATE Teacher SET e_mail = 'janet.stephen@gmail.com' WHERE teacher_id = 100;

--Display the teacher table after modifying the email address
SELECT *FROM Teacher;

--Delete a record from the enrollment table
DELETE FROM Enrollments WHERE Students = 10 AND Courses = 200;

--Display enrollment table after deleting a record
SELECT *FROM Enrollments;

--Update the course table to assign a specific teacher to a course
UPDATE Courses SET Teacher = 108 WHERE course_id = 201;

--Display the course table after updating
SELECT *FROM Courses;