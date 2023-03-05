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
using System.Windows.Shapes;

namespace safari.UI.Dialog
{
    /// <summary>
    /// Interaction logic for QuestionDialog.xaml
    /// </summary>
    public partial class QuestionDialog : Window
    {
        /// <summary>
        /// Question Label.
        /// </summary>
        public Label lQuestion;
        /// <summary>
        /// Parent grid.
        /// </summary>
        public Grid gParent;
        /// <summary>
        /// If they answered the correct question.
        /// </summary>
        public bool bCorrect = false;
        /// <summary>
        /// The answer.
        /// </summary>
        public string szAnswer = "";

        /// <summary>
        /// Custom Constructor for the QuestionDialog.
        /// </summary>
        /// <param name="szQuestion">Question string.</param>
        /// <param name="_szAnswer">Answer string./param>
        public QuestionDialog(string szQuestion, string _szAnswer)
        {
            InitializeComponent();

            /// Setting the answer.
            szAnswer = _szAnswer;

            /// Getting the parent grid and getting all of the labels and such.
            gParent = FindName("ParentGrid1") as Grid;
            lQuestion = new Label();
            lQuestion.Content = szQuestion;
            lQuestion.HorizontalAlignment = HorizontalAlignment.Center;
            lQuestion.VerticalAlignment = VerticalAlignment.Top;
            lQuestion.Margin = new Thickness(0,10,0,0);
            lQuestion.FontFamily = new FontFamily("Reem Kufi");

            /// Add them to the window.
            gParent.Children.Add(lQuestion);
        }

        /// <summary>
        /// When the "Submit" Button is clicked.
        /// </summary>
        /// <param name="sender">Submit Button Object</param>
        /// <param name="e">Event Arguments</param>
        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            /// Checking if we are correct.
            bCorrect = string.Equals((FindName("InputField1") as TextBox).Text, szAnswer, StringComparison.OrdinalIgnoreCase);

            /// Congratulating the student!
            Label lAnswer = FindName("LabelResult") as Label;
            lAnswer.Content = (bCorrect ? "Your Correct!" : "Thats not quite it..");
        }
    }
}
