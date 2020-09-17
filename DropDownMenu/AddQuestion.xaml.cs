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
using CodeFirstProject;

namespace ExaminationProject
{
    /// <summary>
    /// Interaction logic for AddQuestion.xaml
    /// </summary>
    public partial class AddQuestion : UserControl
    {
        public int instructorId { get; set; }
        List<string> chlist = new List<string>();
        Context context = new Context();

        public AddQuestion()
        {
            InitializeComponent();
            instructorId = 3;
            
            CmbCourse.Items.Clear();
            chooselist.Items.Clear();
            Clear();
            loud();
            chooselist.DisplayMemberPath = "Choice";
            //grdQiestion.selc

        }
       
       
       
        private void loud()
        {
            
            CmbCourse.DisplayMemberPath = "Name";
            CmbCourse.SelectedValuePath = "ID";
            var x = context.CourseInstructors.Where(c => c.InstructorID == instructorId).Select(c => c.Course).ToList();

            CmbCourse.ItemsSource = x;
            CmbCourse.SelectedIndex = 0;
            CmbType.SelectedIndex = 0;
            if(CmbCourse.SelectedItem !=null)
            {
                int CId = int.Parse(CmbCourse.SelectedValue.ToString());
                string type = CmbType.Text;

                grdQiestion.ItemsSource = context.Questions.Where(q => q.CourseId == CId).Where(q => q.TypeQuestion == type).ToList();
            }


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var qes = (Question)grdQiestion.SelectedItem;
            qes.Content = txtcontent.Text;
            if (qes.TypeQuestion == "True and False")
            {
                var tr = context.TrueFalseQuestions.Where(q => q.QuestionId == qes.ID).FirstOrDefault();
                if (redtrue.IsChecked.Value)
                {
                    tr.Answer = true;
                }
                else
                {
                    tr.Answer = false;
                }
            }
            else if (qes.TypeQuestion == "Text")
            {
                var trtext = context.TextQuestions.Where(q => q.QuestionId == qes.ID).FirstOrDefault();
                trtext.Answer = txtanswer.Text;
            }
            else if (qes.TypeQuestion == "choose")
            {
                
            }
            context.SaveChanges();
            Clear();
            loud();
        }

        private void Delet_Click(object sender, RoutedEventArgs e)
        {
            var qes = (Question)grdQiestion.SelectedItem;
            context.Questions.Remove(qes);
            context.SaveChanges();
            Clear();
            loud();

        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            int cid = int.Parse(CmbCourse.SelectedValue.ToString());
            string cName = context.Courses.FirstOrDefault(c => c.ID == cid).Name;
            string type = ((ComboBoxItem)CmbType.SelectedItem).Content.ToString();
            string cont = txtcontent.Text;
            bool answerTF = false;
            Question question1 = new Question();
            question1.Content = cont;
            question1.TypeQuestion = type;
            question1.CourseId = cid;
            question1.Content = cont;
            context.Questions.Add(question1);
            context.SaveChanges();


            if (type == "True and False")
            {
                if (redtrue.IsChecked.Value)
                {
                    answerTF = true;
                }
                context.TrueFalseQuestions.Add(new TrueFalseQuestion { Answer= answerTF,QuestionId= question1.ID });                
            }
            else if (type == "Text")
            {
                 //context.addTextQuestion(type, cont, txtanswer.Text, cName);
                
                  context.TextQuestions.Add(new TextQuestion { Answer= txtanswer.Text, QuestionId=question1.ID });
            }
            else if (type == "choose")
            {
               if(chooselist.Items.Count>0)
                {
                    foreach (var item in chooselist.Items)
                    {
                        var ch = (ChoiceQuestion)item;
                        ch.QuestionId = question1.ID;
                        context.ChoiceQuestions.Add(ch);
                    }
                    chooselist.ItemsSource = null;

                }

            }
            context.SaveChanges();
            Clear();
           loud();
            // MessageBox.Show(cName + "    type    "+ type +"       content   "+ cont+"           answer    "+ answerTF);
        }

        private void Clear()
        {
            try
            {
                txtcontent.Text = "";
                txtchoose.Text = "";
                txtanswer.Text = "";
            }
            catch (Exception e)
            {
            }
            
        }

        private void CmbCourse_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //MessageBox.Show(CmbCourse.SelectedValue.ToString());
            Clear();
            try
            {
                string type = ((ComboBoxItem)CmbType.SelectedItem).Content.ToString();

                if (type == "True and False")
                {
                    redfalse.IsChecked = true;
                    tabchoos.IsEnabled = false;
                    tabtext.IsEnabled = false;
                    tabtruefalse.IsEnabled = true;
                    tabtruefalse.IsSelected = true;

                }
                else if (type == "choose")
                {
                    tabchoos.IsEnabled = true;
                    tabtext.IsEnabled = false;
                    tabtruefalse.IsEnabled = false;
                    tabchoos.IsSelected = true;
                }
                else if (type == "Text")
                {

                    tabtext.IsEnabled = true;
                    tabtext.IsSelected = true;
                    tabchoos.IsEnabled = false;
                    tabtruefalse.IsEnabled = false;
                }

                int CId = int.Parse(CmbCourse.SelectedValue.ToString());
                grdQiestion.ItemsSource = context.Questions.Where(q => q.CourseId == CId).Where(q => q.TypeQuestion == type).ToList();
                grdQiestion.SelectedIndex = 0;
            }
            catch (Exception)
            {


            }


        }

        private void CmbType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {

            }
            catch (Exception)
            {


            }

        }

        private void GrdQiestion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var qes = (Question)grdQiestion.SelectedItem;
                txtcontent.Text = qes.Content;
                int id = qes.ID;
                if (qes.TypeQuestion == "True and False")
                {
                    bool answerTF = context.TrueFalseQuestions.FirstOrDefault(q => q.QuestionId == id).Answer;
                    if (answerTF)
                        redtrue.IsChecked = true;
                    else
                        redfalse.IsChecked = true;
                }
                else if (qes.TypeQuestion == "Text")
                {
                    txtanswer.Text = context.TextQuestions.FirstOrDefault(q => q.QuestionId == id).Answer;
                }
                else if (qes.TypeQuestion == "choose")
                {
                    chooselist.ItemsSource = context.ChoiceQuestions.Where(q => q.QuestionId == id).ToList();
                }

            }
            catch (Exception ex)
            {

            }
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ChoiceQuestion choice1 = new ChoiceQuestion();
            choice1.Choice = txtchoose.Text;
            choice1.IsTrue = IsTrue.IsChecked.Value;
            List<ChoiceQuestion> choices = new List<ChoiceQuestion>();
            foreach (var item in chooselist.Items)
            {
                choices.Add((ChoiceQuestion)item);
            }
            chooselist.ItemsSource = null;
            choices.Add(choice1);
            chooselist.ItemsSource = choices;

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var sel = (ChoiceQuestion)chooselist.SelectedItem;
            context.ChoiceQuestions.Remove(sel);
            context.SaveChanges();
            var x = grdQiestion.SelectedItem as Question;
            var chooses = context.ChoiceQuestions.Where(q => q.QuestionId ==x.ID).ToList();
            chooselist.ItemsSource = chooses;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            string cont = txtcontent.Text;
           //int cid = int.Parse(CmbCourse.SelectedValue.ToString());
            var x = grdQiestion.SelectedItem as Question;
            x.Content = cont;
            
            context.SaveChanges();
            Clear();

            loud();

        }

        private void Chooselist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(chooselist.SelectedItem !=null)
            {
                var cho = (ChoiceQuestion)chooselist.SelectedItem;
                IsTrue.IsChecked= cho.IsTrue;
            }
        }

        private void Txtcontent_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            Clear();
        }
    }
}
