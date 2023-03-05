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
    /// Interaction logic for ExportDialog.xaml
    /// </summary>
    public partial class ExportDialog : Window
    {
        public ExportDialog()
        {
            InitializeComponent();
        }

        /// <summary>
        /// The output file to be exported to.
        /// </summary>
        public string szOutput = "";

        /// <summary>
        /// Set the text and close the dialog.
        /// </summary>
        /// <param name="sender">Sending Object</param>
        /// <param name="e">Event arguments</param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            szOutput = (FindName("Filename") as TextBox).Text;
            this.Close();
        }
    }
}
