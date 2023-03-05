using Microsoft.Win32;
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
    /// Interaction logic for AddVideoDialog.xaml
    /// </summary>
    public partial class AddVideoDialog : Window
    {
        /// <summary>
        /// Possible video path.
        /// </summary>
        public string szVideo = "";
        public int attrX, attrY, attrW, attrH;

        public AddVideoDialog()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Clearing the dialog before usage.
        /// </summary>
        public void ClearDialog()
        {
            szVideo = "";
            attrX = 0;
            attrY = 0;
            attrW = 0;
            attrH = 0;
        }


        /// <summary>
        /// When the "Select Video" Button is clicked.
        /// </summary>
        /// <param name="sender">Sending Object</param>
        /// <param name="e">Event Args</param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofDialog = new OpenFileDialog();
            ofDialog.Filter = "Video Files|*.mp4;*.mov";
            ofDialog.DefaultExt = ".mp4";
            ofDialog.ShowDialog();
            Nullable<bool> nResult = ofDialog.ShowDialog();

            if (nResult == false)
            {
                MessageBox.Show("Please select a Video to import.");
                return;
            }

            szVideo = ofDialog.FileName;
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
        /// When the "Apply" Button is clicked.
        /// </summary>
        /// <param name="sender">Sending Object</param>
        /// <param name="e">Event Args</param>
        private void Apply_Click(object sender, RoutedEventArgs e)
        {
            attrX = Try((FindName("XAttr") as TextBox).Text);
            attrY = Try((FindName("YAttr") as TextBox).Text);
            attrW = Try((FindName("WAttr") as TextBox).Text);
            attrH = Try((FindName("HAttr") as TextBox).Text);

            this.Close();
        }
    }
}
