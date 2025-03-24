--Use the DB
USE SISDB;

--Average number of students enrolled in each course
SELECT AVG(Student_count) AS Avg_Student_Count_Per_Course FROM 
(Select Courses, COUNT(Students) AS Student_count FROM Enrollments GROUP BY Courses) AS t;

--Student(s) who made the highest payment
SELECT s.first_name AS First_Name, s.last_name AS Last_Name, p.amount AS Amount
FROM Students as s
JOIN Payments as p ON p.Students = s.student_id
WHERE p.amount = (SELECT MAX(amount) FROM Payments);

--List of courses with the highest number of enrollments
SELECT c.course_name AS Course_Name FROM Courses as c WHERE c.course_id IN (SELECT Courses FROM Enrollments GROUP BY Courses HAVING COUNT(Courses)>1);

--Total payments made to courses taught by each teacher
SELECT t.teacher_id AS Teacher_ID, t.first_name AS First_Name, t.last_name AS Last_Name, 
       (SELECT SUM(p.amount) FROM Payments AS p
        WHERE p.Students IN 
            (SELECT e.Students FROM Enrollments AS e 
             WHERE e.Courses IN 
                 (SELECT c.course_id FROM Courses AS c 
                  WHERE c.Teacher = t.teacher_id))) AS Total_Payment
FROM Teacher AS t;

--Students enrolled in all available courses
SELECT s.student_id AS Student_ID, s.first_name AS First_Name, s.last_name AS Last_Name
FROM Students AS s
WHERE (SELECT COUNT(DISTINCT Courses) FROM Enrollments WHERE student_id = s.student_id) = 
      (SELECT COUNT(course_id) FROM Courses);

--Names of teachers not assigned to any course
SELECT first_name AS First_Name, last_name AS Last_Name FROM Teacher 
WHERE teacher_id NOT IN (SELECT DISTINCT teacher_id FROM Courses WHERE teacher_id IS NOT NULL);

--Average age of all students
SELECT AVG(YEAR(GETDATE()) - YEAR(date_of_birth)) AS Average_age FROM Students;

--Courses with no enrollments
SELECT course_name AS Course_Name FROM Courses 
WHERE course_id NOT IN (SELECT DISTINCT course_id FROM Enrollments);

--Total payments made by each student for the course they are enrolled in
SELECT s.first_name AS First_Name, s.last_name AS Last_Name, c.course_name AS Course_Name, 
       (SELECT SUM(p.amount) FROM Payments AS p 
        WHERE p.Students = s.student_id) AS Total_Amount_Paid
FROM Students AS s
JOIN Enrollments e ON s.student_id = e.Students
JOIN Courses c ON e.Courses = c.course_id;

--Students who have made more than one payment
SELECT s.first_name AS First_Name, s.last_name AS Last_Name, COUNT(p.payment_id) AS Payment_Count
FROM Students AS s
JOIN Payments AS p ON s.student_id = p.Students
GROUP BY s.first_name, s.last_name
HAVING COUNT(p.payment_id) > 1;

--Total Payment made by each student
SELECT s.first_name AS First_Name, s.last_name AS Last_Name, SUM(p.amount) AS Total_Payment
FROM Students AS s
LEFT JOIN Payments AS p ON s.student_id = p.Students
GROUP BY s.first_name, s.last_name;

--Course name along with the count of students enrolled in each course
SELECT c.course_name AS Course_Name, COUNT(e.Students) AS Total_Students
FROM Courses AS c
LEFT JOIN Enrollments AS e ON c.course_id = e.Courses
GROUP BY c.course_name;

 --Average payment amount made by students
SELECT AVG(student_avg) AS Total_Average_Payment
FROM (
    SELECT s.student_id, AVG(p.amount) AS student_avg
    FROM Students s
    JOIN Payments p ON s.student_id = p.Students
    GROUP BY s.student_id
) AS t;

