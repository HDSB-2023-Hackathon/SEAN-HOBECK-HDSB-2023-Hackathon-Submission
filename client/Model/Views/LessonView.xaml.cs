using lsn;
using Microsoft.Win32;
using safari.UI.Dialog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        /// <summary>
        /// Parsed file class.
        /// </summary>
        public ParsedFile pLesson;

        /// <summary>
        /// List of Videos
        /// </summary>
        public List<MediaElement> lVideos = new List<MediaElement>();

        public LessonView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// When you open the lesson.
        /// </summary>
        /// <param name="sender">Calling object.</param>
        /// <param name="e">Event Arguments.</param>
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
            pLesson = new lsn.ParsedFile();
            lsn.ParsedFile.DecryptFile(ref pLesson, fDialog.FileName);

            /// Clearing the Screen and removing the load lesson button.
            RadioButton LessonButton = FindName("LessonButton1") as RadioButton;
            Grid pGridParent = FindName("ParentGrid1") as Grid;

            /// Removing the button.
            if (LessonButton != null && pGridParent != null)
                pGridParent.Children.Remove(LessonButton);

            /// If we need to clear the attributes
            bool bClear = false;
            int attrX = 0, attrY = 0, attrW = 0, attrH = 0;

            /// Count of questions & videos.
            int iQuestionCount = 1, iVideoCount = 0;

            /// Looping through the tokens and rendering them.
            for (int i = 0; i < pLesson.l_Tokens.Count; i++) 
            {
                /// Getting the current token.
                lsn.Token_t token = pLesson.l_Tokens[i];

                /// Setting some of the attributes.
                if (token._data_T == (sbyte)lsn.EToken.ATTR)
                {
                    switch (token._data[0])
                    {
                        case ("x"):
                            attrX = int.Parse(token._data[1]);
                            break;
                        case ("y"):
                            attrY = int.Parse(token._data[1]);
                            break;
                        case ("w"):
                            attrW = int.Parse(token._data[1]);
                            break;
                        case ("h"):
                            attrH = int.Parse(token._data[1]);
                            break;
                    }
                }

                /// Writing a note to screen.
                else if (token._data_T == (sbyte)lsn.EToken.NOTE)
                {
                    TextBlock tBlock = new TextBlock();
                    tBlock.Text = token._data[1];
                    tBlock.VerticalAlignment = VerticalAlignment.Top;
                    tBlock.HorizontalAlignment = HorizontalAlignment.Left;
                    tBlock.Margin = new Thickness(attrX, attrY, 0, 0);
                    tBlock.FontFamily = new FontFamily("Reem Kufi");
                    tBlock.TextWrapping = TextWrapping.Wrap;

                    /// Clear Attributes for next element.
                    bClear = true;
                    pGridParent.Children.Add(tBlock);
                }

                /// Writing a link to screen.
                else if (token._data_T == (sbyte)lsn.EToken.LINK)
                {
                    TextBlock tLink = new TextBlock();
                    tLink.FontFamily = new FontFamily("Reem Kufi");
                    tLink.VerticalAlignment = VerticalAlignment.Top;
                    tLink.HorizontalAlignment = HorizontalAlignment.Left;
                    tLink.Margin = new Thickness(attrX, attrY, 0, 0);
                    Hyperlink hLink = new Hyperlink(new Run(token._data[1]));
                    hLink.NavigateUri = new Uri(token._data[1]);
                    hLink.RequestNavigate += RequestNavigate;
                    tLink.Inlines.Add(hLink);

                    /// Clear Attributes for next element.
                    bClear = true;
                    pGridParent.Children.Add(tLink);
                }

                /// Writing a video to the screen
                else if (token._data_T == (sbyte)lsn.EToken.VIDEO)
                {
                    MediaElement tVideo = new MediaElement();
                    tVideo.VerticalAlignment = VerticalAlignment.Top;
                    tVideo.HorizontalAlignment = HorizontalAlignment.Left;
                    tVideo.Margin = new Thickness(attrX, attrY, 0, 0);
                    string szName = "Video" + iVideoCount;
                    tVideo.Name = szName;
                    tVideo.Width = attrW;
                    tVideo.Height = attrH;
                    tVideo.Source = new Uri(token._data[1]);

                    Button cButton = new Button();
                    cButton.Content = "Mute Video";
                    cButton.Uid = "index-" + iVideoCount;
                    cButton.FontFamily = new FontFamily("Reem Kufi");
                    cButton.VerticalAlignment = VerticalAlignment.Top;
                    cButton.HorizontalAlignment = HorizontalAlignment.Left;
                    cButton.Margin = new Thickness(attrX + attrW / 2 - 30, attrY + attrH - 50, 0, 0);
                    cButton.Click += MuteClick;

                    pGridParent.Children.Add(tVideo);
                    pGridParent.Children.Add(cButton);
                    lVideos.Add(tVideo);
                    iVideoCount++;
                }

                /// Writing a question to the screen
                else if (token._data_T == (sbyte)lsn.EToken.QUES)
                {
                    Button cButton = new Button();
                    cButton.Content = "Question" + iQuestionCount;
                    cButton.Uid = "index-" + i;
                    cButton.Width = attrW;
                    cButton.FontFamily = new FontFamily("Reem Kufi");
                    cButton.VerticalAlignment = VerticalAlignment.Top;
                    cButton.HorizontalAlignment = HorizontalAlignment.Left;
                    cButton.Margin = new Thickness(attrX, attrY, 0, 0);
                    cButton.Click += QuestionClick;

                    pGridParent.Children.Add(cButton);
                    iQuestionCount++;
                }

                /// Clearing for next token.
                if (bClear) 
                {
                    bClear = false;
                    attrX = 0; attrY = 0; attrW = 0; attrH = 0;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void MuteClick(object sender, RoutedEventArgs e)
        {
            /// Getting the button and its name.
            Button cButton = sender as Button;
            string szFind = "Video" + cButton.Uid.Split('-')[1];
            MediaElement eVideo = lVideos[int.Parse(cButton.Uid.Split('-')[1])];
            eVideo.IsMuted = !eVideo.IsMuted;
        }
        /// <summary>
        /// For every question button on the screen.
        /// </summary>
        /// <param name="sender">Sending Button.</param>
        /// <param name="e">Event Arguments.</param>
        public void QuestionClick(object sender, RoutedEventArgs e)
        {
            /// Getting the button and its name.
            Button cButton = sender as Button;
            int iIndex = int.Parse(cButton.Uid.Split('-')[1]);
            Token_t sToken = pLesson.l_Tokens[iIndex], sAnswer = pLesson.l_Tokens[iIndex + 1];

            /// Opening our question dialog.
            QuestionDialog quDialog = new QuestionDialog(sToken._data[1], sAnswer._data[1]);
            quDialog.ShowDialog();
        }
        /// <summary>
        /// Opening a Hyperlink
        /// </summary>
        /// <param name="oSender">Hyperlink object caller.</param>
        /// <param name="e">Event Arguments</param>
        public void RequestNavigate(object oSender, RequestNavigateEventArgs e) 
        {
            /// Open the senders URI.
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }
    }
}
