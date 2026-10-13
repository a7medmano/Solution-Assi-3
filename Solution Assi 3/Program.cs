
using System;
using System.Collections.Generic;
using System.Linq;

// ==================== Student Class ====================
class Student
{
    public int StudentId { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public List<Course> Courses { get; set; }

    public Student(int studentId, string name, int age)
    {
        StudentId = studentId;
        Name = name;
        Age = age;

        // Every new student starts with an empty course list
        Courses = new List<Course>();
    }

    public bool Enroll(Course course)
    {
        // Check if the student is already enrolled
        if (Courses.Any(c => c.CourseId == course.CourseId))
        {
            return false;
        }

        Courses.Add(course);
        return true;
    }

    public string PrintDetails()
    {
        string courseNames = Courses.Count == 0
            ? "No courses"
            : string.Join(", ", Courses.Select(c => c.Title));

        return $"ID: {StudentId}, Name: {Name}, Age: {Age}, Courses: {courseNames}";
    }
}

// ==================== Instructor Class ====================
class Instructor
{
    public int InstructorId { get; set; }
    public string Name { get; set; }
    public string Specialization { get; set; }

    public Instructor(int instructorId, string name, string specialization)
    {
        InstructorId = instructorId;
        Name = name;
        Specialization = specialization;
    }

    public string PrintDetails()
    {
        return $"ID: {InstructorId}, Name: {Name}, Specialization: {Specialization}";
    }
}

// ==================== Course Class ====================
class Course
{
    public int CourseId { get; set; }
    public string Title { get; set; }
    public Instructor Instructor { get; set; }

    public Course(int courseId, string title, Instructor instructor)
    {
        CourseId = courseId;
        Title = title;
        Instructor = instructor;
    }

    public string PrintDetails()
    {
        return $"ID: {CourseId}, Title: {Title}, Instructor: {Instructor.Name}";
    }
}

// ==================== StudentManager Class ====================
class StudentManager
{
    public List<Student> Students { get; set; }
    public List<Course> Courses { get; set; }
    public List<Instructor> Instructors { get; set; }

    public StudentManager()
    {
        Students = new List<Student>();
        Courses = new List<Course>();
        Instructors = new List<Instructor>();
    }

    public bool AddStudent(Student student)
    {
        if (FindStudent(student.StudentId) != null)
            return false;

        Students.Add(student);
        return true;
    }

    public bool AddCourse(Course course)
    {
        if (FindCourse(course.CourseId) != null)
            return false;

        Courses.Add(course);
        return true;
    }

    public bool AddInstructor(Instructor instructor)
    {
        if (FindInstructor(instructor.InstructorId) != null)
            return false;

        Instructors.Add(instructor);
        return true;
    }

    public Student FindStudent(int studentId)
    {
        return Students.FirstOrDefault(s => s.StudentId == studentId);
    }

    public Course FindCourse(int courseId)
    {
        return Courses.FirstOrDefault(c => c.CourseId == courseId);
    }

    public Instructor FindInstructor(int instructorId)
    {
        return Instructors.FirstOrDefault(i => i.InstructorId == instructorId);
    }

    public bool EnrollStudentInCourse(int studentId, int courseId)
    {
        Student student = FindStudent(studentId);
        Course course = FindCourse(courseId);

        if (student == null || course == null)
            return false;

        return student.Enroll(course);
    }

    // Update student information
    public bool UpdateStudent(int studentId, string name, int age)
    {
        Student student = FindStudent(studentId);

        if (student == null)
            return false;

        student.Name = name;
        student.Age = age;
        return true;
    }

    // Delete a student
    public bool DeleteStudent(int studentId)
    {
        Student student = FindStudent(studentId);

        if (student == null)
            return false;

        Students.Remove(student);
        return true;
    }

    // Search for a student by name
    public Student FindStudentByName(string name)
    {
        return Students.FirstOrDefault(
            s => s.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
    }

    // Search for a course by name
    public Course FindCourseByName(string title)
    {
        return Courses.FirstOrDefault(
            c => c.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
    }

    // Bonus 11: Check if a student is enrolled in a course
    public bool IsStudentEnrolled(int studentId, int courseId)
    {
        Student student = FindStudent(studentId);

        if (student == null)
            return false;

        return student.Courses.Any(c => c.CourseId == courseId);
    }

    // Bonus 12: Get instructor name using course name
    public string GetInstructorNameByCourse(string courseName)
    {
        Course course = FindCourseByName(courseName);

        if (course == null)
            return "Course not found";

        return course.Instructor.Name;
    }
}

// ==================== Main Program ====================
class Program
{
    static void Main()
    {
        StudentManager school = new StudentManager();
        bool running = true;

        while (running)
        {
            Console.WriteLine("\n===== Student Management System =====");
            Console.WriteLine("1. Add Student");
            Console.WriteLine("2. Add Instructor");
            Console.WriteLine("3. Add Course");
            Console.WriteLine("4. Enroll Student in Course");
            Console.WriteLine("5. Show All Students");
            Console.WriteLine("6. Show All Courses");
            Console.WriteLine("7. Show All Instructors");
            Console.WriteLine("8. Find Student by ID or Name");
            Console.WriteLine("9. Find Course by ID or Name");
            Console.WriteLine("10. Exit");
            Console.WriteLine("11. Check Student Enrollment (Bonus)");
            Console.WriteLine("12. Get Instructor Name by Course (Bonus)");
            Console.WriteLine("13. Update Student");
            Console.WriteLine("14. Delete Student");
            Console.Write("Choose an option: ");

            if (!int.TryParse(Console.ReadLine(), out int choice))
            {
                Console.WriteLine("Please enter a valid number.");
                continue;
            }

            switch (choice)
            {
                case 1:
                    Console.Write("Enter student ID: ");
                    int studentId = int.Parse(Console.ReadLine());

                    Console.Write("Enter student name: ");
                    string studentName = Console.ReadLine();

                    Console.Write("Enter student age: ");
                    int age = int.Parse(Console.ReadLine());

                    Student student = new Student(studentId, studentName, age);

                    if (school.AddStudent(student))
                        Console.WriteLine("Student added successfully.");
                    else
                        Console.WriteLine("Student ID already exists.");

                    break;

                case 2:
                    Console.Write("Enter instructor ID: ");
                    int instructorId = int.Parse(Console.ReadLine());

                    Console.Write("Enter instructor name: ");
                    string instructorName = Console.ReadLine();

                    Console.Write("Enter specialization: ");
                    string specialization = Console.ReadLine();

                    Instructor instructor = new Instructor(
                        instructorId, instructorName, specialization);

                    if (school.AddInstructor(instructor))
                        Console.WriteLine("Instructor added successfully.");
                    else
                        Console.WriteLine("Instructor ID already exists.");

                    break;

                case 3:
                    Console.Write("Enter course ID: ");
                    int courseId = int.Parse(Console.ReadLine());

                    Console.Write("Enter course title: ");
                    string title = Console.ReadLine();

                    Console.Write("Enter instructor ID: ");
                    int courseInstructorId = int.Parse(Console.ReadLine());

                    Instructor courseInstructor =
                        school.FindInstructor(courseInstructorId);

                    if (courseInstructor == null)
                    {
                        Console.WriteLine("Instructor not found.");
                        break;
                    }

                    Course course = new Course(
                        courseId, title, courseInstructor);

                    if (school.AddCourse(course))
                        Console.WriteLine("Course added successfully.");
                    else
                        Console.WriteLine("Course ID already exists.");

                    break;

                case 4:
                    Console.Write("Enter student ID: ");
                    int enrollStudentId = int.Parse(Console.ReadLine());

                    Console.Write("Enter course ID: ");
                    int enrollCourseId = int.Parse(Console.ReadLine());

                    if (school.EnrollStudentInCourse(
                        enrollStudentId, enrollCourseId))
                        Console.WriteLine("Enrollment successful.");
                    else
                        Console.WriteLine(
                            "Student/course not found or already enrolled.");

                    break;

                case 5:
                    if (school.Students.Count == 0)
                        Console.WriteLine("No students found.");
                    else
                        foreach (Student s in school.Students)
                            Console.WriteLine(s.PrintDetails());

                    break;

                case 6:
                    if (school.Courses.Count == 0)
                        Console.WriteLine("No courses found.");
                    else
                        foreach (Course c in school.Courses)
                            Console.WriteLine(c.PrintDetails());

                    break;

                case 7:
                    if (school.Instructors.Count == 0)
                        Console.WriteLine("No instructors found.");
                    else
                        foreach (Instructor i in school.Instructors)
                            Console.WriteLine(i.PrintDetails());

                    break;

                case 8:
                    Console.Write("Enter student ID or name: ");
                    string studentSearch = Console.ReadLine();

                    Student foundStudent;

                    if (int.TryParse(studentSearch, out int searchStudentId))
                        foundStudent = school.FindStudent(searchStudentId);
                    else
                        foundStudent = school.FindStudentByName(studentSearch);

                    if (foundStudent != null)
                        Console.WriteLine(foundStudent.PrintDetails());
                    else
                        Console.WriteLine("Student not found.");

                    break;

                case 9:
                    Console.Write("Enter course ID or name: ");
                    string courseSearch = Console.ReadLine();

                    Course foundCourse;

                    if (int.TryParse(courseSearch, out int searchCourseId))
                        foundCourse = school.FindCourse(searchCourseId);
                    else
                        foundCourse = school.FindCourseByName(courseSearch);

                    if (foundCourse != null)
                        Console.WriteLine(foundCourse.PrintDetails());
                    else
                        Console.WriteLine("Course not found.");

                    break;

                case 10:
                    running = false;
                    Console.WriteLine("Goodbye!");
                    break;

                case 11:
                    Console.Write("Enter student ID: ");
                    int checkStudentId = int.Parse(Console.ReadLine());

                    Console.Write("Enter course ID: ");
                    int checkCourseId = int.Parse(Console.ReadLine());

                    if (school.IsStudentEnrolled(
                        checkStudentId, checkCourseId))
                        Console.WriteLine("Student is enrolled.");
                    else
                        Console.WriteLine("Student is not enrolled, or IDs are invalid.");

                    break;

                case 12:
                    Console.Write("Enter course name: ");
                    string instructorCourseName = Console.ReadLine();

                    Console.WriteLine(
                        school.GetInstructorNameByCourse(instructorCourseName));

                    break;

                case 13:
                    Console.Write("Enter student ID: ");
                    int updateId = int.Parse(Console.ReadLine());

                    Console.Write("Enter new name: ");
                    string newName = Console.ReadLine();

                    Console.Write("Enter new age: ");
                    int newAge = int.Parse(Console.ReadLine());

                    if (school.UpdateStudent(updateId, newName, newAge))
                        Console.WriteLine("Student updated successfully.");
                    else
                        Console.WriteLine("Student not found.");

                    break;

                case 14:
                    Console.Write("Enter student ID: ");
                    int deleteId = int.Parse(Console.ReadLine());

                    if (school.DeleteStudent(deleteId))
                        Console.WriteLine("Student deleted successfully.");
                    else
                        Console.WriteLine("Student not found.");

                    break;

                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
        }
    }
}
