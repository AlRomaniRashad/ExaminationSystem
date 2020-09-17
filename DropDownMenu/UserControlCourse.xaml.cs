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
    /// Interaction logic for UserControlCourse.xaml
    /// </summary>
    public partial class UserControlCourse : UserControl
    {
        Context context;
        List<Student> students;
        public UserControlCourse()
        {
            context = new Context();
            InitializeComponent();

            allCourses.Items.Clear();
            allCourses.SelectedValuePath = "ID";
            LoudCourses();
        }

        private void LoudCourses()
        {
            allCourses.ItemsSource = context.Courses.ToList();
        }

        private void AllCourses_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (allCourses.SelectedIndex != -1)
            {
                var id = Convert.ToInt32(allCourses.SelectedValue);
                var crs = context.Courses.FirstOrDefault(c => c.ID == id);

                if (crs != null)
                {
                    //var std = allStudent.SelectedItem as Student;
                    txtId.Text = crs.ID.ToString();
                    txtName.Text = crs.Name;
                    txtDesc.Text = crs.Description;
                    txtMinDeg.Text = crs.MinDegree.ToString();
                    txtMaxDeg.Text = crs.MaxDegree.ToString();
                }
                else
                {
                    MessageBox.Show("Can't display this item", "Error");
                }
            }
        }

        //private void CourseData(string name, string desc, short maxDeg, short minDeg)
        //{
        //    context.sp_AddCourse(name, desc, maxDeg, minDeg);
        //    context.SaveChanges();
        //}

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (txtName.Text != "" && txtDesc.Text != "" && txtMinDeg.Text != "" && txtMaxDeg.Text != "")
            {
                var crs = context.Courses.FirstOrDefault(c => c.Name.ToLower() == txtName.Text.ToLower());

                if (crs == null)
                {
                    //CourseData(txtName.Text, txtDesc.Text, Convert.ToInt16(txtMinDeg.Text), Convert.ToInt16(txtMaxDeg.Text));
                    //context.sp_AddCourse(txtName.Text, txtDesc.Text, Convert.ToInt16(txtMaxDeg.Text), Convert.ToInt16(txtMinDeg.Text));
                    //context.Database.ExecuteSqlCommand($"AddCourse {txtName.Text}, {txtDesc.Text}, {int.Parse(txtMaxDeg.Text)}, {int.Parse(txtMinDeg.Text)}");
                    context.Courses.Add(new Course
                    {
                        Name = txtName.Text,
                        Description = txtDesc.Text,
                        MinDegree = int.Parse(txtMinDeg.Text),
                        MaxDegree = int.Parse(txtMaxDeg.Text)
                    });
                    context.SaveChanges();
                    LoudCourses();
                    ClearInputs();
                }
                else
                {
                    MessageBox.Show("This Course already exists", "Error");
                }
            }
            else
            {
                MessageBox.Show("You Should fill All the fields", "Error");
            }
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if(allCourses.SelectedItems.Count > 0)
            {
                if (txtName.Text != "" && txtDesc.Text != "" && txtMinDeg.Text != "" && txtMaxDeg.Text != "")
                {
                    var crsID = int.Parse(allCourses.SelectedValue.ToString());
                    var result = context.Courses.SingleOrDefault(b => b.ID == crsID);

                    if (result != null)
                    {
                        result.Name = txtName.Text;
                        result.Description = txtDesc.Text;
                        result.MinDegree = int.Parse(txtMinDeg.Text);
                        result.MaxDegree = int.Parse(txtMaxDeg.Text);

                        context.SaveChanges();
                    }
                }
                else
                {
                    MessageBox.Show("You Should fill All the fields", "Error");
                }
            }
            else
            {
                MessageBox.Show("You have to Select One!", "Error");
            }
            //context.SaveChanges();
            LoudCourses();
            ClearInputs();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (allCourses.SelectedItems.Count > 0)
            {
                var id = int.Parse(allCourses.SelectedValue.ToString());
                
                if (id != 0)
                {
                    //context.deleteCourse(id);
                    var crs = context.Courses.SingleOrDefault(c => c.ID == id);
                    context.Courses.Remove(crs);
                    context.SaveChanges();
                    LoudCourses();
                    ClearInputs();
                }
                else
                {
                    MessageBox.Show("This Course not  exists", "Error");
                }
            }
            else
            {
                MessageBox.Show("You have to select one!", "Error");
            }
        }

        private void ClearInputs()
        {
            txtId.Text = "";
            txtName.Text = "";
            txtDesc.Text = "";
            txtMinDeg.Text = "";
            txtMaxDeg.Text = "";
        }
    }
}
