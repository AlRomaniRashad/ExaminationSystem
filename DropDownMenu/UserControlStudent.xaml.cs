using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
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
    /// Interaction logic for UserControlProviders.xaml
    /// </summary>
    public partial class UserControlStudent : UserControl
    {
        Context context;
        List<Student> students;
        //short new_Id;
        public UserControlStudent()
        {
            context = new Context();
            InitializeComponent();

            //new_Id = 24;

            allStudent.Items.Clear();
            allStudent.SelectedValuePath = "ID";
            LoudStudents();

            branches.DisplayMemberPath = "Name";
            intakes.DisplayMemberPath = "Name";
            trackes.DisplayMemberPath = "Name";

            branches.SelectedValuePath = "ID";
            intakes.SelectedValuePath = "ID";
            trackes.SelectedValuePath = "ID";

            branches.ItemsSource = context.Branches.ToList();
            trackes.ItemsSource = context.Tracks.ToList();
            intakes.ItemsSource = context.Intakes.ToList();

            branches.SelectedIndex = 0;
            trackes.SelectedIndex = 0;
            intakes.SelectedIndex = 0;
        }

        private void LoudStudents()
        {
            //allStudent.Items.Clear();
            students = context.Students.ToList();
            allStudent.ItemsSource = students.Select(s => new {
                s.ID,
                s.Name,
                s.Login.UserName,
                s.Login.Password,
                BranchName = s.BranchTrackIntake.Branch.Name,
                TrackName = s.BranchTrackIntake.Track.Name,
                IntakeName = s.BranchTrackIntake.Intake.Name
            }).ToList();
        }

        private void AllStudent_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(allStudent.SelectedIndex != -1)
            {
                var std = students[allStudent.SelectedIndex] as Student;
                //var std = context.Students[allStudent.SelectedIndex];

                if (std != null)
                {
                    //var std = allStudent.SelectedItem as Student;
                    txtId.Text = std.ID.ToString();
                    txtName.Text = std.Name;
                    txtUserName.Text = std.Login.UserName;
                    txtPassword.Text = std.Login.Password.ToString();
                    branches.SelectedItem = std.BranchTrackIntake.Branch;
                    trackes.SelectedItem = std.BranchTrackIntake.Track;
                    intakes.SelectedItem = std.BranchTrackIntake.Intake;
                }
                else
                {
                    MessageBox.Show("Can't display this item", "Error");
                }
            }
            //student.DataContext = std;
        }

        private void StudentData(string name, string userName, string password, int branchID, int trackID, int intakeID)
        {
            var bTIID = context.BranchTrackIntakes
                        .Where(b => b.BranchID == branchID && b.TrackID == trackID && b.IntakeID == intakeID)
                        .Select(b => b.ID).FirstOrDefault();

            if (bTIID == 0)
            {
                //context.sp_AddStudent(name, userName, bTIID);
                //context.addBranchTrackIntack(branchID, trackID, intakeID);
                //context.Database.ExecuteSqlCommand($"AddBranchTrackIntack {branchID} {trackID} {intakeID}");
                context.BranchTrackIntakes.Add(new BranchTrackIntake
                {
                    BranchID = branchID,
                    TrackID = trackID,
                    IntakeID = intakeID
                });

                context.SaveChanges();
                bTIID = context.BranchTrackIntakes
                        .Where(b => b.BranchID == branchID && b.TrackID == trackID && b.IntakeID == intakeID)
                        .Select(b => b.ID).FirstOrDefault();
            }

            //context.Logins.Add(new Login
            //{
            //    UserName = userName,
            //    Password = password,
            //    Type = "Student"
            //});
            //context.SaveChanges();

            var userLoginID = context.Logins.Where(log => log.UserName == userName).Select(user => user.ID).FirstOrDefault();

            //context.sp_AddStudent(name, userLoginID, bTIID);
            //context.Database.ExecuteSqlCommand($"AddStudent {name} {userLoginID} {bTIID}");
            try
            {
                context.Students.Add(new Student
                {
                    Name = name,
                    LoginID = userLoginID,
                    BranchTrackIntakeID = bTIID,
                    Login = new Login
                    {
                        UserName = userName,
                        Password = password,
                        Type = "Student"
                    }
                });
                context.SaveChanges();
                LoudStudents();
                ClearTextBoxes();
            }
            catch (DbEntityValidationException ex)
            {
                StringBuilder sb = new StringBuilder();

                foreach (var failure in ex.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }

                MessageBox.Show(sb.ToString(), "Error");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            if(txtName.Text != "" && txtUserName.Text != "" && txtPassword.Text != "")
            {
                var id = context.Logins.Where(log => log.UserName == txtUserName.Text).FirstOrDefault();
                if (id == null)
                {
                    StudentData(txtName.Text, txtUserName.Text, txtPassword.Text,
                                int.Parse(branches.SelectedValue.ToString()), int.Parse(trackes.SelectedValue.ToString()),
                                int.Parse(intakes.SelectedValue.ToString()));
                    
                }
                else
                {
                    MessageBox.Show("This Student or The User Name already exists", "Error");
                }
            }
            else
            {
                MessageBox.Show("You Should fill All the fields", "Error");
            }
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (txtName.Text != "" && txtUserName.Text != "" && txtPassword.Text != "" && allStudent.SelectedItems.Count > 0)
            {
                var stdID = int.Parse(allStudent.SelectedValue.ToString());
                var result = context.Students.SingleOrDefault(b => b.ID == stdID);

                short branchID = Convert.ToInt16(branches.SelectedValue.ToString());
                short trackID = Convert.ToInt16(trackes.SelectedValue.ToString());
                short intakeID = Convert.ToInt16(intakes.SelectedValue.ToString());

                var bTIID = context.BranchTrackIntakes
                        .Where(b => b.BranchID == branchID && b.TrackID == trackID && b.IntakeID == intakeID)
                        .Select(b => b.ID).FirstOrDefault();

                if (bTIID == 0)
                {
                    //context.addBranchTrackIntack(branchID, trackID, intakeID);
                    //context.Database.ExecuteSqlCommand($"AddBranchTrackIntack {branchID} {trackID} {intakeID}");
                    context.BranchTrackIntakes.Add(new BranchTrackIntake
                    {
                        BranchID = branchID,
                        TrackID = trackID,
                        IntakeID = intakeID
                    });
                    context.SaveChanges();

                    bTIID = context.BranchTrackIntakes
                            .Where(b => b.BranchID == branchID && b.TrackID == trackID && b.IntakeID == intakeID)
                            .Select(b => b.ID).FirstOrDefault();
                }
                if (result != null)
                {
                    try
                    {
                        result.Name = txtName.Text;
                        result.Login.UserName = txtUserName.Text;
                        result.Login.Password = txtPassword.Text;
                        //result.BranchTrackIntake1.Branch = branches.SelectedItem as Branch;
                        //result.BranchTrackIntake1.Track = trackes.SelectedItem as Track;
                        //result.BranchTrackIntake1.Intake = intakes.SelectedItem as Intake;

                        result.BranchTrackIntake = context.BranchTrackIntakes.FirstOrDefault(b => b.ID == bTIID);
                        context.SaveChanges();
                    }
                    catch (DbEntityValidationException ex)
                    {
                        StringBuilder sb = new StringBuilder();

                        foreach (var failure in ex.EntityValidationErrors)
                        {
                            sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                            foreach (var error in failure.ValidationErrors)
                            {
                                sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                                sb.AppendLine();
                            }
                        }

                        MessageBox.Show(sb.ToString(), "Error");
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error");
                    }
                }
            }
            else
            {
                MessageBox.Show("You Should fill All the fields", "Error");
            }
            //context.SaveChanges();
            LoudStudents();
            ClearTextBoxes();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if(allStudent.SelectedItems.Count > 0)
            {
                var id = context.Logins.Where(log => log.UserName == txtUserName.Text).FirstOrDefault();
                if (id != null)
                {
                    //context.deleteStudent(int.Parse(txtId.Text));
                    //var std = context.Students.SingleOrDefault()
                    context.Logins.Remove(id);
                    context.SaveChanges();
                    LoudStudents();
                    ClearTextBoxes();
                }
            }
            else
            {
                MessageBox.Show("You have to select one!", "Error");
            }
        }

        private void Branches_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //var branchID = int.Parse(branches.SelectedValue.ToString());
            //trackes.ItemsSource = context.BranchTrackIntakes
            //                        .Where(b => b.BranchId == branchID)
            //                        .Select(b=>b.Track)
            //                        .ToList();

            //intakes.ItemsSource = context.BranchTrackIntakes
            //                        .Where(b => b.BranchId == branchID)
            //                        .Select(b => b.Intake)
            //                        .ToList().Take(1);

            //if (trackes.ItemsSource != null)
            //    trackes.SelectedIndex = 0;

            //if (intakes.ItemsSource != null)
            //    intakes.SelectedIndex = 0;
        }

        private void ClearTextBoxes()
        {
            txtId.Text = "";
            txtName.Text = "";
            txtUserName.Text = "";
            txtPassword.Text = "";
        }
    }
}
