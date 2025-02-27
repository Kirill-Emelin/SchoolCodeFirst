using Microsoft.EntityFrameworkCore;

namespace SchoolCodeFirst
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Student> Students { get; set; } // Таблица студентов
        public DbSet<Teacher> Teachers { get; set; } // Таблица учителей
        public DbSet<Cabinet> Cabinets { get; set; } // Таблица кабинетов
        public DbSet<Course> Courses { get; set; } // Таблица предметов
        public DbSet<SchoolClass> SchoolClasses { get; set; } // Таблица классов
        public DbSet<StudentCourse> StudentCourses { get; set; } // Таблица связи студентов и курсов

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Строка подключения к SQL Server
            optionsBuilder.UseSqlServer("Server=.\\MYSQLEXPRESS;Database=SchoolDB;Trusted_Connection=True;TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Конфигурация таблицы Students
            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(e => e.StudentID); // первичный ключ

                entity.Property(e => e.FirstName)
                      .HasMaxLength(50)
                      .IsRequired(); 

                entity.Property(e => e.LastName)
                      .HasMaxLength(50)
                      .IsRequired();

                entity.Property(e => e.DateOfBirth)
                      .HasColumnType("date")
                      .IsRequired();

                entity.Property(e => e.Gender)
                      .HasMaxLength(10)
                      .IsRequired();

                entity.HasOne(e => e.SchoolClass)
                      .WithMany(sc => sc.Students)
                      .HasForeignKey(e => e.ClassID);
            });

            // Конфигурация таблицы Teachers
            modelBuilder.Entity<Teacher>(entity =>
            {
                entity.HasKey(e => e.TeacherID); // первичный ключ

                entity.Property(e => e.FirstName)
                      .HasMaxLength(50)
                      .IsRequired(); 

                entity.Property(e => e.LastName)
                      .HasMaxLength(50)
                      .IsRequired();

                entity.Property(e => e.Email)
                      .HasMaxLength(100)
                      .IsRequired();

                entity.HasOne(e => e.Course)
                      .WithMany(s => s.Teachers)
                      .HasForeignKey(e => e.CourseID);
            });

            // Конфигурация таблицы Cabinets
            modelBuilder.Entity<Cabinet>(entity =>
            {
                entity.HasKey(e => e.CabinetID); // первичный ключ

                entity.Property(e => e.RoomNumber)
                      .HasMaxLength(20)
                      .IsRequired(); 

                entity.Property(e => e.Floor)
                      .IsRequired();
            });

            // Конфигурация таблицы Courses (Предметы)
            modelBuilder.Entity<Course>(entity =>
            {
                entity.HasKey(e => e.CourseID); // первичный ключ

                entity.Property(e => e.CourseName)
                      .HasMaxLength(100)
                      .IsRequired(); 

                entity.Property(e => e.Description)
                      .HasMaxLength(500); 

                entity.Property(e => e.CabinetID)
                      .IsRequired(); 

                entity.HasOne(e => e.Cabinet)
                      .WithMany(c => c.Courses)
                      .HasForeignKey(e => e.CabinetID);
            });

            // Конфигурация таблицы SchoolClasses (Классы)
            modelBuilder.Entity<SchoolClass>(entity =>
            {
                entity.HasKey(e => e.ClassID); // первичный ключ

                entity.Property(e => e.ClassName)
                      .HasMaxLength(20)
                      .IsRequired(); 

                entity.Property(e => e.StudentCount)
                      .IsRequired();
            });

            modelBuilder.Entity<StudentCourse>(entity =>
            {
                entity.HasKey(sc => new { sc.StudentID, sc.CourseID });

                entity.HasOne(sc => sc.Student)
                      .WithMany(s => s.StudentCourses)
                      .HasForeignKey(sc => sc.StudentID);

                entity.HasOne(sc => sc.Course)
                      .WithMany(c => c.StudentCourses)
                      .HasForeignKey(sc => sc.CourseID);
            });
        }
    }
}
