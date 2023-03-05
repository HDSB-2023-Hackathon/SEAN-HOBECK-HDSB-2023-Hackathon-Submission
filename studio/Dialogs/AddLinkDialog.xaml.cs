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
    /// Interaction logic for AddLinkDialog.xaml
    /// </summary>
    public partial class AddLinkDialog : Window
    {
        /// <summary>
        /// The link and its attributes.
        /// </summary>
        public string szLink = "";
        public int attrX, attrY, attrW, attrH;

        public AddLinkDialog()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Clearing the dialog before usage.
        /// </summary>
        public void ClearDialog()
        {
            szLink = "";
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
            szLink = (FindName("LinkAttr") as TextBox).Text;
            attrX = Try((FindName("XAttr") as TextBox).Text);
            attrY = Try((FindName("YAttr") as TextBox).Text);
            attrW = Try((FindName("WAttr") as TextBox).Text);
            attrH = Try((FindName("HAttr") as TextBox).Text);

            this.Close();
        }
    }
}
