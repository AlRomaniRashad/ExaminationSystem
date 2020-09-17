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

namespace ExaminationProject
{
    /// <summary>
    /// Interaction logic for UserControlAssignCousreToStudent.xaml
    /// </summary>
    public partial class UserControlAssignCousreToStudent : UserControl
    {
        Context context;
        public List<Person> OutCourse { get; set; }
        public List<Person> InCourse { get; set; }
        //public List<Student> Students { get; set; }
        public UserControlAssignCousreToStudent()
        {
            context = new Context();

            InitializeComponent();

            cmbCourse.ItemsSource = context.Courses.ToList();
            //cmbBranch.ItemsSource = context.Branches.ToList();
            //cmbTrack.ItemsSource = context.Tracks.ToList();

            cmbCourse.SelectedValuePath = "ID";
            cmbCourse.DisplayMemberPath = "Name";
            cmbCourse.SelectedIndex = 0;

            //cmbBranch.SelectedValuePath = "Id";
            //cmbBranch.DisplayMemberPath = "Name";
            //cmbBranch.SelectedIndex = 0;

            //cmbTrack.SelectedValuePath = "Id";
            //cmbTrack.DisplayMemberPath = "Name";
            //cmbTrack.SelectedIndex = 0;

            LoadCourses();
        }

        private void LoadCourses()
        {
            if(cmbCourse.SelectedValue != null)
            {
                int crsID = int.Parse(cmbCourse.SelectedValue.ToString());

                var stdIDInCrs = context.CourseStudents.Where(c => c.CourseID == crsID).Select(c => c.StudentID).ToList();
                lstINCourse.ItemsSource = context.Students
                    .Select(std => new Person
                    {
                        Id = std.ID,
                        Name = std.Name,
                        BranchName = std.BranchTrackIntake.Branch.Name,
                        TrackName = std.BranchTrackIntake.Track.Name
                    }).Where(p => stdIDInCrs.Contains(p.Id)).ToList();

                lstOutCourse.ItemsSource = context.Students
                    .Select(std => new Person
                    {
                        Id = std.ID,
                        Name = std.Name,
                        BranchName = std.BranchTrackIntake.Branch.Name,
                        TrackName = std.BranchTrackIntake.Track.Name
                    }).Where(p => !stdIDInCrs.Contains(p.Id)).ToList();
            }
            
            //var query = (from std in context.Students
            //                            from stdCrs in context.courseStudents
            //                            where std.Id == stdCrs.StudentId && stdCrs.courseId == crsID
            //                           select new Person {
            //                                Id = std.Id,
            //                                Name = std.Name,
            //                                BranchName = std.BranchTrackIntake.Branch.Name,
            //                                TrackName = std.BranchTrackIntake.Track.Name
            //                            });
            //lstINCourse.ItemsSource = query.ToList();

            //lstOutCourse.ItemsSource = context.Students
            //    .Select(std => new Person
            //    {
            //        Id = std.Id,
            //        Name = std.Name,
            //        BranchName = std.BranchTrackIntake.Branch.Name,
            //        TrackName = std.BranchTrackIntake.Track.Name
            //    }).Except(query).ToList();
        }

        private void BtnRemove_Click(object sender, RoutedEventArgs e)
        {
            //var selectedOnes = OutCourse.Where(x => x.Include == true).ToList();
            short crsID = Convert.ToInt16(cmbCourse.SelectedValue.ToString());

            if (lstINCourse.SelectedItems.Count > 0)
            {
                foreach (Person person in lstINCourse.SelectedItems)
                {
                    var stdCrs = context.CourseStudents.SingleOrDefault(s => s.CourseID == crsID && s.StudentID == person.Id);
                    context.CourseStudents.Remove(stdCrs);
                }
                context.SaveChanges();
                LoadCourses();
            }
            else
            {
                MessageBox.Show("You have to select at least one!", "Error");
            }
        }

        private void BtnAssign_Click(object sender, RoutedEventArgs e)
        {
            short crsID = Convert.ToInt16(cmbCourse.SelectedValue.ToString());

            if (lstOutCourse.SelectedItems.Count > 0)
            {
                foreach (Person person in lstOutCourse.SelectedItems)
                {
                    context.CourseStudents.Add(new CourseStudent
                    {
                        CourseID = crsID,
                        StudentID = person.Id
                    });
                }
                context.SaveChanges();
                LoadCourses();
            }
            else
            {
                MessageBox.Show("You have to select at least one!", "Error");
            }
        }

        private void CmbCourse_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadCourses();
        }

        //private void CmbBranch_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    if(cmbTrack.SelectedIndex < 0)
        //    {

        //    }
        //}

        //private void CmbTrack_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{

        //}
    }
}
