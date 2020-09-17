using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CodeFirstProject;

namespace ExaminationProject
{
    /// <summary>
    /// Interaction logic for CorrectExam.xaml
    /// </summary>
    public partial class CorrectExam : UserControl
    {
        public CorrectExam()
        {
            InitializeComponent();
        }
        Context context = new Context();


        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        List<Question> questions = new List<Question>();
        private void loudQuestions()
        {
            var query = (from stdex in context.StudentExamQuestions
                         from ex in context.ExamQuestions
                         where stdex.ExamQuestionId == ex.ID
                         where ex.Question.TypeQuestion == "Text"
                         select ex.Question).ToList();
            questions = query;

        }
        private void loudCourse()
        {
            List<Course> courses = new List<Course>();
            foreach (var item in questions)
            {
                courses.Add(item.Course);
            }
            //unice
            cmbCourse.ItemsSource = courses;
            cmbCourse.SelectedIndex = 0;

        }

        
    }
}
