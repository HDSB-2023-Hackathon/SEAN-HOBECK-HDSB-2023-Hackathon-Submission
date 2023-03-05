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

namespace studio.Dialogs
{
    /// <summary>
    /// Interaction logic for AddQuestionDialog.xaml
    /// </summary>
    public partial class AddQuestionDialog : Window
    {
        /// <summary>
        /// The Question & Answer along with its attributes.
        /// </summary>
        public string szQuestion = "", szAnswer = "";
        public int attrX, attrY, attrW, attrH;

        public AddQuestionDialog()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Clearing the dialog before usage.
        /// </summary>
        public void ClearDialog()
        {
            szQuestion = "";
            szAnswer = "";
            attrX = 0;
            attrY = 0;
            attrW = 0;
            attrH = 0;
        }

        /// <summary>
        /// Trying to parse to a integer.
        /// </summary>
        /// <param name="szString">String to tryparsed</param>
        /// <returns></returns>
        public int Try(string szString)
        {
            int result = 0;
            int.TryParse(szString, out result);
            return result;
        }

        /// <summary>
        /// When the "apply" button is clicked.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Event arguments</param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            szQuestion = (FindName("QuestionAttr") as TextBox).Text;
            szAnswer = (FindName("AnswerAttr") as TextBox).Text;
            attrX = Try((FindName("XAttr") as TextBox).Text);
            attrY = Try((FindName("YAttr") as TextBox).Text);
            attrW = Try((FindName("WAttr") as TextBox).Text);
            attrH = Try((FindName("HAttr") as TextBox).Text);

            this.Close();
        }
    }
}
