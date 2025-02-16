namespace SchoolCodeFirst
{
    // Таблица Students
    public class Student
    {
        public int StudentID { get; set; }
        public int ClassID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public SchoolClass SchoolClass { get; set; }
    }

    // Таблица Teachers
    public class Teacher
    {
        public int TeacherID { get; set; }
        public int CourseID { get; set; } 
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Course Course { get; set; }
    }

    // Модель таблицы Cabinets (Кабинеты)
    public class Cabinet
    {
        public int CabinetID { get; set; }
        public string RoomNumber { get; set; }
        public int Floor { get; set; }

        public List<Course> Courses { get; set; } = new List<Course>();
    }

    // Модель таблицы Courses (Предметы/Курсы)
    public class Course
    {
        public int CourseID { get; set; }
        public string CourseName { get; set; }
        public string Description { get; set; }
        public int CabinetID { get; set; }
        public Cabinet Cabinet { get; set; }

        public List<Teacher> Teachers { get; set; } = new List<Teacher>();
    }

    // Таблица SchoolClasses (Классы)
    public class SchoolClass
    {
        public int ClassID { get; set; }
        public string ClassName { get; set; }
        public int StudentCount { get; set; }

        public List<Student> Students { get; set; } = new List<Student>();
    }
}
