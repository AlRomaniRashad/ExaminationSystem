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
    /// Interaction logic for UserControlDashboard.xaml
    /// </summary>
    public partial class UserControlInstructor : UserControl
    {
        Context context;
        List<Instructor> instructors;
        public UserControlInstructor()
        {
            context = new Context();
            InitializeComponent();

            AllInstructor.Items.Clear();
            AllInstructor.SelectedValuePath = "ID";
            LoudInstructors();

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
        private void LoudInstructors()
        {
            //allStudent.Items.Clear();
            instructors = context.Instructors.ToList();
            AllInstructor.ItemsSource = instructors.Select(s => new {
                s.ID,
                s.Name,
                s.Login.UserName,
                s.Login.Password,
                s.IsManager,
                BranchName = s.BranchTrackIntake.Branch.Name,
                TrackName = s.BranchTrackIntake.Track.Name,
                IntakeName = s.BranchTrackIntake.Intake.Name
            }).ToList();
        }

        private void AllInstructor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (AllInstructor.SelectedIndex != -1)
            {
                var instructor = instructors[AllInstructor.SelectedIndex] as Instructor;

                if (instructor != null)
                {
                    txtId.Text = instructor.ID.ToString();
                    txtName.Text = instructor.Name;
                    txtUserName.Text = instructor.Login.UserName;
                    txtPassword.Text = instructor.Login.Password.ToString();
                    branches.SelectedItem = instructor.BranchTrackIntake.Branch;
                    trackes.SelectedItem = instructor.BranchTrackIntake.Track;
                    intakes.SelectedItem = instructor.BranchTrackIntake.Intake;
                }
                else
                {
                    MessageBox.Show("Can't display this item", "Error");
                }
            }
        }

        private void InstructorData(string name, string userName, string password, bool isManager, int branchID, int trackID, int intakeID)
        {
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

            //context.Logins.Add(new Login
            //{
            //    UserName = userName,
            //    Password = password,
            //    Type = "Instructor"
            //});
            //context.SaveChanges();

            //var userLoginID = context.Logins.Where(log => log.UserName == userName).Select(user => user.ID).FirstOrDefault();

            //context.addInstructor(name, userLoginID, isManager, bTIID);
            //context.Database.ExecuteSqlCommand($"AddInstructor {name} {userLoginID} {isManager} {bTIID}");
            try
            {
                context.Instructors.Add(new Instructor
                {
                    Name = name,
                    IsManager = isManager,
                    BranchTrackIntakeID = bTIID,
                    Login = new Login
                    {
                        UserName = userName,
                        Password = password,
                        Type = "Instructor"
                    }
                });
                context.SaveChanges();
                LoudInstructors();
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (txtName.Text != "" && txtUserName.Text != "" && txtPassword.Text != "")
            {
                var id = context.Logins.Where(log => log.UserName == txtUserName.Text).FirstOrDefault();
                if (id == null)
                {
                    bool isManagers = false;
                    if (((ComboBoxItem)isManager.SelectedItem).Content.ToString() == "True")
                        isManagers = true;

                    InstructorData(txtName.Text, txtUserName.Text, txtPassword.Text, isManagers,
                                int.Parse(branches.SelectedValue.ToString()), int.Parse(trackes.SelectedValue.ToString()),
                                int.Parse(intakes.SelectedValue.ToString()));
                }
                else
                {
                    MessageBox.Show("This Instructor or The User Name already exists", "Error");
                }
            }
            else
            {
                MessageBox.Show("You Should fill All the fields", "Error");
            }
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (txtName.Text != "" && txtUserName.Text != "" && txtPassword.Text != "" && AllInstructor.SelectedItems.Count > 0)
            {
                var instructorID = int.Parse(AllInstructor.SelectedValue.ToString());
                var result = context.Instructors.SingleOrDefault(b => b.ID == instructorID);

                int branchID = int.Parse(branches.SelectedValue.ToString());
                int trackID = int.Parse(trackes.SelectedValue.ToString());
                int intakeID = int.Parse(intakes.SelectedValue.ToString());

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
                        result.IsManager = ((ComboBoxItem)isManager.SelectedItem).Content.ToString() == "True" ? true : false;
                        //result.BranchTrackIntake.Branch = branches.SelectedItem as Branch;
                        //result.BranchTrackIntake.Track = trackes.SelectedItem as Track;
                        //result.BranchTrackIntake.Intake = intakes.SelectedItem as Intake;
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
                    catch (Exception ex)
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
            LoudInstructors();
            ClearTextBoxes();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (AllInstructor.SelectedItems.Count > 0)
            {
                var id = context.Logins.Where(log => log.UserName == txtUserName.Text).FirstOrDefault();
                if (id != null)
                {
                    //context.deleteInstructor(int.Parse(txtId.Text));
                    context.Logins.Remove(id);
                    context.SaveChanges();
                    LoudInstructors();
                    ClearTextBoxes();
                }
                else
                {
                    MessageBox.Show("This Instructor not exists", "Error");
                }
            }
            else
            {
                MessageBox.Show("You have to select one!", "Error");
            }
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

