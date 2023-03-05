using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
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

namespace safari.Model.Views
{
    /// <summary>
    /// Interaction logic for LessonView.xaml
    /// </summary>
    public partial class LessonView : UserControl
    {
        public LessonView()
        {
            InitializeComponent();
        }

        private void LessonButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fDialog = new OpenFileDialog();
            fDialog.DefaultExt = ".lsn";
            fDialog.Filter = "Lesson Files (*.lsn)|*.lsn";
            Nullable<bool> nResult = fDialog.ShowDialog();

            if (nResult == false)
            {
                MessageBox.Show("Please select a Lesson File to import.");
                return;
            }

            /// Getting the parsed file by decryption.
            lsn.ParsedFile pLesson = new lsn.ParsedFile();
            lsn.ParsedFile.DecryptFile(ref pLesson, fDialog.FileName);

            /// Clearing the Screen and removing the load lesson button.
            RadioButton LessonButton = FindName("LessonButton1") as RadioButton;
            Grid pGridParent = FindName("ParentGrid1") as Grid;

            /// Removing the button.
            if (LessonButton != null && pGridParent != null)
                pGridParent.Children.Remove(LessonButton);

            /// Static
            bool _default = false;
            int x = 0, y = 0, w = 0, h = 0;

            /// Looping through the tokens and rendering them.
            for (int i = 0; i < pLesson.l_Tokens.Count; i++) 
            {
                lsn.Token_t token = pLesson.l_Tokens[i];

                /// Setting some of the static attributes.
                if (token._data_T == (sbyte)lsn.EToken.ATTR)
                {
                    switch (token._data[0]) 
                    {
                        case ("posi_x"):
                            x = int.Parse(token._data[1]);
                            break;
                        case ("posi_y"):
                            y = int.Parse(token._data[1]);
                            break;
                        case ("width"):
                            w = int.Parse(token._data[1]);
                            break;
                        case ("height"):
                            h = int.Parse(token._data[1]);
                            break;
                    }
                }

                /// Writing a note to screen.
                else if (token._data_T == (sbyte)lsn.EToken.NOTE)
                {
                    TextBlock tBlock = new TextBlock();
                    tBlock.Text = token._data[1];
                    tBlock.VerticalAlignment = VerticalAlignment.Center;
                    tBlock.HorizontalAlignment = HorizontalAlignment.Center;
                    tBlock.Margin = new Thickness(x, y, 0, 0);
                    tBlock.FontFamily = new FontFamily("Reem Kufi");
                    _default = true;
                    pGridParent.Children.Add(tBlock);
                }

                /// Clearing for next token.
                if (_default) 
                {
                    _default = false;
                    x = 0; y = 0; w = 0; h = 0;
                }
            }
        }
    }
}
