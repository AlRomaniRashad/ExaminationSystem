using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExaminationProject
{
    public class Context : DbContext
    {
        public Context() : base($@"Data Source=DESKTOP-73Q7MGM\ALROMANI;Initial Catalog=ExaminationSystem;Integrated Security = true")
        {
        }

        public virtual DbSet<Login> Logins { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Instructor> Instructors { get; set; }
        public virtual DbSet<Branch> Branches { get; set; }
        public virtual DbSet<Track> Tracks { get; set; }
        public virtual DbSet<Intake> Intakes { get; set; }
        public virtual DbSet<BranchTrackIntake> BranchTrackIntakes { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<CourseStudent> CourseStudents { get; set; }
        public virtual DbSet<CourseInstructor> CourseInstructors { get; set; }
        public virtual DbSet<Exam> Exams { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<TextQuestion> TextQuestions { get; set; }
        public virtual DbSet<ChoiceQuestion> ChoiceQuestions { get; set; }
        public virtual DbSet<TrueFalseQuestion> TrueFalseQuestions { get; set; }
        public virtual DbSet<StudentExam> StudentExams { get; set; }
        public virtual DbSet<ExamQuestion> ExamQuestions { get; set; }
        public virtual DbSet<StudentExamQuestion> StudentExamQuestions { get; set; }
        public virtual DbSet<StudentTextAnswer> StudentTextAnswers { get; set; }
        public virtual DbSet<StudentChoiceAnswer> StudentChoiceAnswers { get; set; }
        public virtual DbSet<StudentTrueFalseAnswer> StudentTrueFalseAnswers { get; set; }
    }
}
