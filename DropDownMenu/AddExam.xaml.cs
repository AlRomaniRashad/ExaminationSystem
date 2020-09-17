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
//using CodeFirstProject;

namespace ExaminationProject
{
    /// <summary>
    /// Interaction logic for AddExam.xaml
    /// </summary>
    public partial class AddExam : UserControl
    {
        Context context = new Context();
        public int instructorId { get; set; }
        public AddExam()
        {
            InitializeComponent();
            
            loaddatagrid();
            //  coursess.DataContext = context.Courses;
            dataGrid1.SelectedIndex = 0;

            LoadCourses();

            loadstarttime();
            loadendtime();
            LoadIntake();
            LoadTrack();
            LoadBranch();
            cmbTypeExam.SelectedIndex = 0;

        }


        void LoadCourses()
        {
            //cmbCourse.SelectedValuePath=""
            cmbCourse.ItemsSource = context.Courses.ToList();
            cmbCourse.SelectedIndex = 0;
        }
        void LoadIntake()
        {
            //cmbCourse.SelectedValuePath=""
            cmbIntake.ItemsSource = context.Intakes.ToList();
            cmbIntake.SelectedIndex = 0;
        }
        void LoadBranch()
        {
            //cmbCourse.SelectedValuePath=""
            cmbBranch.ItemsSource = context.Branches.ToList();
            cmbBranch.SelectedIndex = 0;
        }

        void LoadTrack()
        {
            //cmbCourse.SelectedValuePath=""
            cmbTrack.ItemsSource = context.Tracks.ToList();
            cmbTrack.SelectedIndex = 0;
        }


        void loadstarttime()
        {
            
            int id = Convert.ToInt32(dataGrid1.SelectedValue);
            Exam exam = context.Exams.FirstOrDefault(x => x.ID == id);
            if(exam!=null)
            startPicker.Text = exam.StartTime.ToString();
        }

        void loadendtime()
        {

            int id = Convert.ToInt32(dataGrid1.SelectedValue);
            Exam exam = context.Exams.FirstOrDefault(x => x.ID == id);
            if (exam != null)
                endPicker.Text = exam.EndTime.ToString();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {

            Exam test = new Exam();

            var CourseId = Convert.ToInt16(cmbCourse.SelectedValue);

            var brancheid = Convert.ToInt16(cmbBranch.SelectedValue);
            var trakeeid = Convert.ToInt16(cmbTrack.SelectedValue);
            var intakeeid = Convert.ToInt16(cmbIntake.SelectedValue);

            // MessageBox.Show(CourseId.ToString());
            var courseName = context.Courses.Where(ee => ee.ID == CourseId).Select(ee => ee.Name).FirstOrDefault();

            var endTime = TimeSpan.Parse(endPicker.Text.ToString());

            var startTime = TimeSpan.Parse(startPicker.Text.ToString());


            var examType = ((ComboBoxItem)cmbTypeExam.SelectedItem).Content.ToString();

            var IntakeTrackBranchID = context.BranchTrackIntakes.Where(ee => ee.BranchID == brancheid && ee.TrackID == trakeeid && ee.IntakeID == intakeeid).Select(ee => ee.ID).FirstOrDefault();

            var id = context.Exams.Where(ee => ee.CourseId == CourseId && ee.ExamType == examType/* && ee.Course == IntakeTrackBranchID*/).FirstOrDefault();

            if (id != null)
            {
                MessageBox.Show("Exists");

            }
            else
            {
               // context.Database.ExecuteSqlCommand($"exec AddExam {StartTime} ,{EndTime}, {courseName}, {ExamType}, {IntakeTrackBranchID}") ;
                context.Exams.Add(new Exam {StartTime=DateTime.Parse(startTime.ToString()),
                    EndTime = DateTime.Parse(endTime.ToString()),ExamType=examType,
                    BranchTrackIntakeId = IntakeTrackBranchID,
                    ExpirationDate=DateTime.Now.AddDays(1),
                   // ExamDegree=
                    
                });
              //  context.addExam2(StartTime, EndTime, courseName, ExamType, IntakeTrackBranchID);
                loaddatagrid();
                context.SaveChanges();
            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            // var courseid = Convert.ToInt32(cmbCourse.SelectedValue);
            var Examid = Convert.ToInt32(dataGrid1.SelectedValue);
            // var Examid = Convert.ToInt32(cmbexam.SelectedValue);

            //MessageBox.Show(courseid.ToString());

            // var Examtype = ((ComboBoxItem)cmbTypeExam.SelectedItem).Content.ToString();
            // MessageBox.Show(Examtype.ToString());
            var exam = context.Exams.Where(X => X.ID == Examid).SingleOrDefault();

            //  MessageBox.Show(exam.Id.ToString());

            context.Exams.Remove(exam);
            context.SaveChanges();
            loaddatagrid();

            //var selectedItem = dataGrid1.SelectedItem;
            //if (selectedItem != null)
            //{
            //    dataGrid1.Items.Remove(selectedItem);
            //}

        }
        private void loaddatagrid()
        {


            var query = context.Exams.Select(x => new { x.ID, x.CourseId, x.Course.Name, x.ExamType, x.StartTime, x.EndTime });

            dataGrid1.SelectedValuePath = "ID";
            

            dataGrid1.ItemsSource = query.ToList();


        }



        private void UpBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DownBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

        }

        private void dataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int id = Convert.ToInt32(dataGrid1.SelectedValue);
            Exam addexam = context.Exams.FirstOrDefault(x => x.ID == id);
            if (addexam != null)
            {
                cmbCourse.Text = addexam.Course.Name.ToString();
                cmbTypeExam.Text = addexam.ExamType.ToString();
                startPicker.Text = addexam.StartTime.ToString();
                endPicker.Text = addexam.EndTime.ToString();

            }


        }

    }
}
