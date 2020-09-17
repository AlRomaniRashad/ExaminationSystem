using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
    /// Interaction logic for UserControlAssignCourseToInstructor.xaml
    /// </summary>
    public partial class UserControlAssignCourseToInstructor : UserControl
    {
        Context context;
        public List<Person> OutCourse { get; set; }
        public List<Person> InCourse { get; set; }

        public UserControlAssignCourseToInstructor()
        {
            context = new Context();

            InitializeComponent();

            cmbCourse.ItemsSource = context.Courses.ToList();
            cmbBranch.ItemsSource = context.Branches.ToList();
            cmbTrack.ItemsSource = context.Tracks.ToList();

            //MessageBox.Show(cmbTrack.Items.Count.ToString());

            cmbCourse.SelectedValuePath = "ID";
            cmbCourse.DisplayMemberPath = "Name";
            cmbCourse.SelectedIndex = 0;

            cmbBranch.SelectedValuePath = "ID";
            cmbBranch.DisplayMemberPath = "Name";
            cmbBranch.SelectedIndex = 0;

            cmbTrack.SelectedValuePath = "ID";
            cmbTrack.DisplayMemberPath = "Name";
            cmbTrack.SelectedIndex = 0;

            LoadCourses();
        }

        private void LoadCourses()
        {
            if(cmbCourse.SelectedValue != null && cmbBranch.SelectedValue != null && cmbTrack.SelectedValue != null)
            {
                int crsID = Convert.ToInt16(cmbCourse.SelectedValue.ToString());
                int trackID = Convert.ToInt16(cmbTrack.SelectedValue.ToString());
                int branchID = Convert.ToInt16(cmbBranch.SelectedValue.ToString());

                var stdIDInCrs = context.CourseInstructors
                    .Where(c => c.CourseID == crsID && c.TrackID == trackID && c.BranchID == branchID)
                    .Select(c => c.InstructorID).ToList();

                lstINCourse.ItemsSource = context.Instructors
                    .Select(ins => new Person
                    {
                        Id = ins.ID,
                        Name = ins.Name,
                        BranchName = ins.BranchTrackIntake.Branch.Name,
                        TrackName = ins.BranchTrackIntake.Track.Name
                    }).Where(p => stdIDInCrs.Contains(p.Id)).ToList();

                lstOutCourse.ItemsSource = context.Instructors
                    .Select(std => new Person
                    {
                        Id = std.ID,
                        Name = std.Name,
                        BranchName = std.BranchTrackIntake.Branch.Name,
                        TrackName = std.BranchTrackIntake.Track.Name
                    }).Where(p => !stdIDInCrs.Contains(p.Id)).ToList();

                btnAssign.IsEnabled = lstINCourse.Items.Count > 0 ? false : true;
            }
        }

        private void BtnRemove_Click(object sender, RoutedEventArgs e)
        {
            //var selectedOnes = OutCourse.Where(x => x.Include == true).ToList();
            short crsID = Convert.ToInt16(cmbCourse.SelectedValue.ToString());
            int trackID = Convert.ToInt16(cmbTrack.SelectedValue.ToString());
            int branchID = Convert.ToInt16(cmbBranch.SelectedValue.ToString());

            if (lstINCourse.SelectedItems.Count > 0)
            {
                foreach (Person person in lstINCourse.SelectedItems)
                {
                    var stdCrs = context.CourseInstructors
                        .SingleOrDefault(s => s.CourseID == crsID && s.InstructorID == person.Id && s.TrackID == trackID && s.BranchID == branchID);

                    context.CourseInstructors.Remove(stdCrs);
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
            short trackID = Convert.ToInt16(cmbTrack.SelectedValue.ToString());
            short branchID = Convert.ToInt16(cmbBranch.SelectedValue.ToString());

            if (lstOutCourse.SelectedItems.Count > 0)
            {
                foreach (Person person in lstOutCourse.SelectedItems)
                {
                    context.CourseInstructors.Add(new CourseInstructor
                    {
                        CourseID = crsID,
                        InstructorID = person.Id,
                        BranchID = branchID,
                        TrackID = trackID
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

        private void Cmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadCourses();
        }
    }
}
