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
    /// Interaction logic for UserControlTrake.xaml
    /// </summary>
    public partial class UserControlTrake : UserControl
    {
        Context context;
        
        public UserControlTrake()
        {
           
            context = new Context();

            InitializeComponent();

            allTrakes.DisplayMemberPath = "Name";
            allTrakes.SelectedValuePath = "ID";
            LoadBranch();
        }
        private void LoadBranch()
        {
            allTrakes.ItemsSource = context.Tracks.ToList();
        }

        private void AllTrake_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (allTrakes.SelectedItems.Count > 0)
            {
                int id = int.Parse(allTrakes.SelectedValue.ToString());
                txtId.Text = allTrakes.SelectedValue.ToString();
                txtName.Text = context.Tracks.Where(b => b.ID == id).Select(b => b.Name).FirstOrDefault();
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {

            if (txtName.Text != "")
            {
                //context.addTrack(txtName.Text);
                //context.Database.ExecuteSqlCommand($"exec AddTrack {txtName.Text}");
                try
                {
                    context.Tracks.Add(new Track
                    {
                        Name = txtName.Text
                    });
                    context.SaveChanges();
                    LoadBranch();
                    ClearInputs();
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
            }
            else
            {
                MessageBox.Show("Enter the name of the Track", "Error");
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (allTrakes.SelectedItems.Count > 0)
            {
                //int id = int.Parse(allBranches.SelectedValue.ToString());

                var result = allTrakes.SelectedItem as Track;
                context.Tracks.Remove(result);
                context.SaveChanges();
                LoadBranch();
                ClearInputs();
            }
            else
            {
                MessageBox.Show("Select One!", "Error");
            }

        }
        private void ClearInputs()
        {
            txtId.Text = "";
            txtName.Text = "";
        }
    }
}
