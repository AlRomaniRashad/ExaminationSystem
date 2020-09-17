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
    /// Interaction logic for UserControlBranch.xaml
    /// </summary>
    public partial class UserControlBranch : UserControl
    {
        Context context;
        //List<Branch> branches;
        public UserControlBranch()
        {
            context = new Context();

            InitializeComponent();

            allBranches.DisplayMemberPath = "Name";
            allBranches.SelectedValuePath = "ID";
            LoadBranch();
        }

        private void LoadBranch()
        {
            allBranches.ItemsSource = context.Branches.ToList();
        }

        private void AllBranches_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(allBranches.SelectedItems.Count > 0)
            {
                int id = int.Parse(allBranches.SelectedValue.ToString());
                txtId.Text = allBranches.SelectedValue.ToString();
                txtName.Text = context.Branches.Where(b => b.ID == id).Select(b => b.Name).FirstOrDefault();
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            if(txtName.Text != "")
            {
                //Context.AddBranch(txtName.Text);
                try
                {
                    context.Branches.Add(new Branch
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
                MessageBox.Show("Enter the name of the Branch", "Error");
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if(allBranches.SelectedItems.Count > 0)
            {
                //int id = int.Parse(allBranches.SelectedValue.ToString());
                var result = allBranches.SelectedItem as Branch;

                context.Branches.Remove(result);
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
