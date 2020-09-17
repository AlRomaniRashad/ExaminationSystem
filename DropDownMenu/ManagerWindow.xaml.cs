using BeautySolutions.View.ViewModel;
using MaterialDesignThemes.Wpf;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //var menuStudent = new List<SubItem>
            //{
            //    new SubItem("Add/Update/Delete Student", new UserControlStudent()),
            //    new SubItem("Add/Update/Delete Instructor", new UserControlInstructor())
            //};
            //var item1 = new ItemMenu("Student", menuStudent, PackIconKind.PersonAdd);

            var menuPerson = new List<SubItem>
            {
                new SubItem("Add/Update/Delete Student", new UserControlStudent()),
                new SubItem("Add/Update/Delete Instructor", new UserControlInstructor())
            };
            var item1 = new ItemMenu("Add Member", menuPerson, PackIconKind.PersonAdd);

            //var menuInstructor = new List<SubItem>
            //{
            //    new SubItem("Add/Update/Delete", new UserControlInstructor())
            //};
            //var item2 = new ItemMenu("Instructor", menuInstructor, PackIconKind.PersonAdd);

            var menuBranchTrackIntake = new List<SubItem>
            {
                new SubItem("Add/Delete Branch", new UserControlBranch()),
                new SubItem("Add/Delete Trake ", new UserControlTrake()),
                new SubItem("Add/Delete Intake", new UserControlIntake())
            };
            var item3 = new ItemMenu("ITI", menuBranchTrackIntake, PackIconKind.SourceBranch);

            var menuCourse = new List<SubItem>
            {
                new SubItem("Add/Update/Delete Cousre", new UserControlCourse()),
                new SubItem("Assign Cousre to Student", new UserControlAssignCousreToStudent()),
                new SubItem("Assign Cousre to Instructor", new UserControlAssignCourseToInstructor())
            };
            var item4 = new ItemMenu("Courses", menuCourse, PackIconKind.FileDocumentBoxes);

            //var menuReports = new List<SubItem>();
            //menuReports.Add(new SubItem("Customers"));
            //menuReports.Add(new SubItem("Providers"));
            //menuReports.Add(new SubItem("Products"));
            //menuReports.Add(new SubItem("Stock"));
            //menuReports.Add(new SubItem("Sales"));
            //var item5 = new ItemMenu("Reports", menuReports, PackIconKind.FileReport);


            //Menu.Children.Add(new UserControlMenuItem(item6, this));
            Menu.Children.Add(new UserControlMenuItem(item1, this));
            //Menu.Children.Add(new UserControlMenuItem(item2, this));
            Menu.Children.Add(new UserControlMenuItem(item3, this));
            Menu.Children.Add(new UserControlMenuItem(item4, this));
           // Menu.Children.Add(new UserControlMenuItem(item5, this));
        }

        //internal void SwitchScreen(object sender)
        //{
        //    var screen = ((UserControl)sender);

        //    if(screen!=null)
        //    {
        //        StackPanelMain.Children.Clear();
        //        StackPanelMain.Children.Add(screen);
        //    }
        //}
        internal void SwitchScreen(UserControl sender)
        {
            if (sender != null)
            {
                StackPanelMain.Children.Clear();
                StackPanelMain.Children.Add(sender);
            }
        }
    }
}
