--Use SISDB
USE SISDB;

--Calculate total payments made by a student
SELECT s.first_name, s.last_name, SUM(p.amount) AS Total_Payment 
FROM Students as s JOIN Payments as p ON s.student_id = p.Students 
WHERE s.student_id = 1 GROUP BY s.first_name,s.last_name,s.student_id;

--Count of students enrolled in each course
SELECT c.course_name, COUNT(e.Students) AS Total_Students_Enrolled 
FROM Courses as c LEFT JOIN Enrollments as e ON c.course_id = e.Courses
GROUP BY c.course_name,c.course_id;

--Students with no enrollments
SELECT s.first_name as First_Name, s.last_name as Last_Name FROM Students as s 
LEFT JOIN Enrollments as e ON s.student_id = e.Students
WHERE e.enrollment_id IS NULL;

--Students and the course they are enrolled in
SELECT s.first_name, s.last_name, c.course_name
FROM Students as s JOIN Enrollments as e ON s.student_id = e.Students
JOIN Courses as c ON e.Courses = c.course_id;

--Teachers and the course they are assigned to
SELECT t.first_name as Teacher_First_Name, t.last_name as Teacher_Last_Name, c.course_name as Course_Name
FROM Teacher as T JOIN Courses as c ON t.teacher_id = c.Teacher;

--Students and their enrollment dates for a specific course 
SELECT s.first_name as First_Name, s.last_name as Last_Name, e.enrollment_date as Enrollment_Date, c.course_name as Course_Name
FROM Students as S JOIN Enrollments as e ON s.student_id = e.Students
JOIN Courses as c ON e.Courses = c.course_id
WHERE course_name = 'Information Technology';

--Students with no payment
SELECT s.first_name as First_Name, s.last_name as Last_Name
FROM Students as s LEFT JOIN Payments as p on s.student_id = p.Students
WHERE payment_id is NULL;

--Courses with no enrollment
SELECT c.course_name as Course_Name
FROM Courses as c LEFT JOIN Enrollments as e on c.course_id = e.Courses
WHERE enrollment_id is NULL;

--Students enrolled in multiple courses
SELECT e1.Students, s.first_name as First_Name, s.last_name as Last_Name
FROM Enrollments e1 JOIN Students s ON e1.Students = s.student_id 
GROUP BY e1.Students,s.first_name,s.last_name HAVING COUNT(e1.Courses) > 1;

--Teachers with no assigned course
SELECT t.first_name as Teacher_First_Name, t.last_name as Teacher_Last_Name
FROM Teacher as t LEFT Join Courses as c ON t.teacher_id = c.Teacher
WHERE c.course_id IS NULL;