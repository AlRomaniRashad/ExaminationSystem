using BeautySolutions.View.ViewModel;
using MaterialDesignThemes.Wpf;
using BeautySolutions.View.ViewModel;
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
    public partial class InstructorWindow : Window
    {

        public InstructorWindow()
        {
            InitializeComponent();

            var menuRegister = new List<SubItem>();
            menuRegister.Add(new SubItem("Add Exam", new AddExam()));
           var item6 = new ItemMenu("Exam", menuRegister, PackIconKind.BookLockOpen);

            var menuSchedule = new List<SubItem>();
            menuSchedule.Add(new SubItem("Add Question",new AddQuestion()));
            var item1 = new ItemMenu("Question", menuSchedule, PackIconKind.Schedule);

            var menuReports = new List<SubItem>();//CorrectExam
            menuReports.Add(new SubItem("Assign Questition to Exam", new AssignQuestitionExam()));
            menuReports.Add(new SubItem("Correct Exam", new CorrectExam()));
            var item2 = new ItemMenu("Assign", menuReports, PackIconKind.FileReport);


            Menu.Children.Add(new UserControlMenuItem(item6, this));
            Menu.Children.Add(new UserControlMenuItem(item1, this));
            Menu.Children.Add(new UserControlMenuItem(item2, this));
            
        }

        internal void SwitchScreen(object sender)
        {
            var screen = ((UserControl)sender);

            if(screen!=null)
            {
                StackPanelMain.Children.Clear();
                StackPanelMain.Children.Add(screen);
            }
        }
    }
}
