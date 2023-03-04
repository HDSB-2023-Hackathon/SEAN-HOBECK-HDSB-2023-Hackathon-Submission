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

namespace studio
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public lsn.ParsedFile pFile;

        public MainWindow()
        {
            pFile = new lsn.ParsedFile();
            InitializeComponent();
        }

        
        private void Note_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Question_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Answer_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Link_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Video_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
