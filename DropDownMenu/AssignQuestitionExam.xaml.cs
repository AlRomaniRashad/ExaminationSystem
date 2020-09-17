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
    /// Interaction logic for AssignQuestitionExam.xaml
    /// </summary>
    public partial class AssignQuestitionExam : UserControl
    {
        Context context = new Context();

        public int instructorId { get; set; }
        public AssignQuestitionExam()
        {
            InitializeComponent();
            instructorId = 1;
            dgAllQues.Items.Clear();
           
            LoadCourses();
            LoadExam();
            cmbExam.SelectedIndex = 0;
            loadDataGridExamQes();
            //loaddatagrid();
            dgAllQues.SelectedValuePath = "Id";
            cmbExam.SelectedValuePath = "Id";
        }
        void LoadCourses()
        {
            cmbCourse.SelectedValuePath = "Id";
            cmbCourse.DisplayMemberPath = "Name";
            cmbCourse.ItemsSource = (from c in context.Courses
                                     join ci in context.CourseInstructors
                                     on c.ID equals ci.CourseID
                                     select new { c, ci })
                                    .Where(co => co.ci.InstructorID == instructorId)
                                    .Select(co => co.c).ToList();
                    cmbCourse.SelectedIndex = 0;
        }
        void LoadExam()
        {
            try
            {
                int cid = Convert.ToInt16(cmbCourse.SelectedValue.ToString());
                cmbExam.ItemsSource = context.Exams.Where(ex => ex.CourseId == cid).ToList();
                cmbExam.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
            }
        }

        //private void loaddatagrid()
        //{
        //    var query = context.Exams.Select(x => new { x.Id, x.CourseId, x.Course.Name, x.ExamType, x.StartTime, x.EndTime, x.TotalTime });
        //}
        private void loadDataGridExamQes()
        {
            try
            {
                int idExam = Convert.ToInt16(cmbExam.SelectedValue.ToString());
                int idcourse = Convert.ToInt16(cmbCourse.SelectedValue.ToString());
                dgQuesEx.ItemsSource = context.ExamQuestions.Where(ee => ee.ExamId == idExam).Select(ee => ee.Question).ToList();
            }
            catch (Exception ex)
            {
            }

        }
        private void loadDgAllQestion()
        {
            try
            {
                if (cmbExam.SelectedItem != null)
                {
                    int idExam = Convert.ToInt16(cmbExam.SelectedValue.ToString());
                    var qq1 = context.ExamQuestions.Where(ee => ee.ExamId == idExam).Select(e2 => e2.Question).ToList();
                    int idcourse = Convert.ToInt16(cmbCourse.SelectedValue.ToString());
                    var qq2 = context.Questions.Where(q => q.CourseId == idcourse).ToList();

                    var query = (qq2.Except(qq1)).ToList();
                    dgAllQues.ItemsSource = query;
                }
                else
                {
                    int idcourse = Convert.ToInt16(cmbCourse.SelectedValue.ToString());
                    var query = context.Questions.Where(q => q.CourseId == idcourse).ToList();
                    dgAllQues.ItemsSource = query;
                }

            }
            catch (Exception ex)
            {

            }

        }

        private void CmbCourse_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadExam();
            loadDgAllQestion();
            loadDataGridExamQes();
        }

        private void CmbExam_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            loadDataGridExamQes();
            loadDgAllQestion();
        }

        private void DataGridQuesEx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void DataGridAllQues_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var xx = Convert.ToInt16(dgQuesEx.SelectedValue);
                var exam = context.ExamQuestions.Where(X => X.QuestionId == xx).SingleOrDefault();
                context.ExamQuestions.Remove(exam);
                context.SaveChanges();
                loadDataGridExamQes();
                loadDgAllQestion();
            }catch(Exception m)
            {

            }
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (dgAllQues.SelectedItem != null && cmbExam.SelectedItem != null)
            {
                var id = Convert.ToInt16(dgAllQues.SelectedValue);
                int idExam = Convert.ToInt16(cmbExam.SelectedValue.ToString());
               // context.addQuestionExam(id, idExam, 2);
                loadDataGridExamQes();
                loadDgAllQestion();
            }
        }
    }
}
