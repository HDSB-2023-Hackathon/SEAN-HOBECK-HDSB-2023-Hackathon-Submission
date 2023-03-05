using Microsoft.Win32;
using studio.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace studio
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// ParsedFile Object to be written to.
        /// </summary>
        public lsn.ParsedFile pFile;
        /// <summary>
        /// Reference to the XAML Canvas
        /// </summary>
        public Canvas uiCanvas;
        /// <summary>
        /// Basic Constructor
        /// </summary>
        public MainWindow()
        {
            /// Creating our parsed output file.
            pFile = new lsn.ParsedFile();
            InitializeComponent();

            /// Finding the UI Canvas
            uiCanvas = FindName("UICanvas") as Canvas;
        }
        /// <summary>
        /// Adding a Rectangle to the canvas.
        /// </summary>
        /// <param name="uRect">x, y, w, h of the Rectangle</param>
        public void Create_Rectangle(Rect uRect) 
        {
            Rectangle cRect = new Rectangle();
            cRect.VerticalAlignment = VerticalAlignment.Top;
            cRect.HorizontalAlignment = HorizontalAlignment.Left;
            cRect.Margin = new Thickness(uRect.X, uRect.Y, 0, 0);
            cRect.Height = uRect.Height;
            cRect.Width = uRect.Width;
            cRect.Fill = Brushes.Transparent;
            cRect.Stroke = Brushes.Black;

            uiCanvas.Children.Add(cRect);
        }
        /// <summary>
        /// Adding a Textbox to the canvas.
        /// </summary>
        /// <param name="uRect">x, y, w, h of the textbox</param>
        /// <param name="szText">The text to be displayed.</param>
        public void Create_TextBox(Rect uRect, string szText)
        {
            Label cText = new Label();
            cText.VerticalAlignment = VerticalAlignment.Top;
            cText.HorizontalAlignment = HorizontalAlignment.Left;
            cText.Margin = new Thickness(uRect.X, uRect.Y, 0, 0);
            cText.Height = uRect.Height;
            cText.Width = uRect.Width;
            cText.Content = szText;
            cText.FontFamily = new FontFamily("Reem Kufi");

            uiCanvas.Children.Add(cText);
        }

        /// <summary>
        /// When the "Add Note" button is clicked.
        /// </summary>
        /// <param name="sender">Object that called this.</param>
        /// <param name="e">Event parameters</param>
        private void Note_Click(object sender, RoutedEventArgs e)
        {
            AddNoteDialog anDialog = new AddNoteDialog();
            anDialog.ClearDialog();
            anDialog.ShowDialog();

            if (anDialog.szNote != "") 
            {
                /// Adding x, y, w, h attributes then the video.
                pFile.l_Tokens.Add(
                    new lsn.Token_t(new string[] { "x", Convert.ToString(anDialog.attrX) }, (sbyte)lsn.EToken.ATTR));
                pFile.l_Tokens.Add(
                    new lsn.Token_t(new string[] { "y", Convert.ToString(anDialog.attrY) }, (sbyte)lsn.EToken.ATTR));
                pFile.l_Tokens.Add(
                    new lsn.Token_t(new string[] { "w", Convert.ToString(anDialog.attrW) }, (sbyte)lsn.EToken.ATTR));
                pFile.l_Tokens.Add(
                    new lsn.Token_t(new string[] { "h", Convert.ToString(anDialog.attrH) }, (sbyte)lsn.EToken.ATTR));
                pFile.l_Tokens.Add(
                    new lsn.Token_t(new string[] { "note", anDialog.szNote }, (sbyte)lsn.EToken.NOTE));

                /// Adding a rectangle to the canvas, then adding a textbox
                Rect rPos = new Rect(anDialog.attrX, anDialog.attrY, anDialog.attrW, anDialog.attrH);
                Create_Rectangle(rPos);
                Create_TextBox(rPos, anDialog.szNote);
            }
        }
        /// <summary>
        /// When the "Add Question" button is clicked.
        /// </summary>
        /// <param name="sender">Object that called this.</param>
        /// <param name="e">Event parameters</param>
        private void Question_Click(object sender, RoutedEventArgs e)
        {
            AddQuestionDialog aqDialog = new AddQuestionDialog();
            aqDialog.ShowDialog();

            if (aqDialog.szQuestion != "" && aqDialog.szAnswer != "") 
            {
                /// Adding x, y, w, h attributes then the question and answer.
                pFile.l_Tokens.Add(
                    new lsn.Token_t(new string[] { "x", Convert.ToString(aqDialog.attrX) }, (sbyte)lsn.EToken.ATTR));
                pFile.l_Tokens.Add(
                    new lsn.Token_t(new string[] { "y", Convert.ToString(aqDialog.attrY) }, (sbyte)lsn.EToken.ATTR));
                pFile.l_Tokens.Add(
                    new lsn.Token_t(new string[] { "w", Convert.ToString(aqDialog.attrW) }, (sbyte)lsn.EToken.ATTR));
                pFile.l_Tokens.Add(
                    new lsn.Token_t(new string[] { "h", Convert.ToString(aqDialog.attrH) }, (sbyte)lsn.EToken.ATTR));
                pFile.l_Tokens.Add(
                    new lsn.Token_t(new string[] { "question", aqDialog.szQuestion }, (sbyte)lsn.EToken.QUES));
                pFile.l_Tokens.Add(
                    new lsn.Token_t(new string[] { "answer", aqDialog.szAnswer }, (sbyte)lsn.EToken.ANSWER));

                /// Adding a rectangle to the canvas, then adding a textbox
                Rect rPos = new Rect(aqDialog.attrX, aqDialog.attrY, aqDialog.attrW, aqDialog.attrH);
                Create_Rectangle(rPos);
                Create_TextBox(rPos, aqDialog.szQuestion);
                Rect rNew = new Rect(aqDialog.attrX, aqDialog.attrY + aqDialog.attrH - 50, aqDialog.attrW, 50);
                Create_TextBox(rNew, aqDialog.szAnswer);
            }
        }
        /// <summary>
        /// When the "Add Link" button is clicked.
        /// </summary>
        /// <param name="sender">Object that called this.</param>
        /// <param name="e">Event parameters</param>
        private void Link_Click(object sender, RoutedEventArgs e)
        {
            AddLinkDialog alDialog = new AddLinkDialog();
            alDialog.ShowDialog();

            if (alDialog.szLink != "")
            {
                /// Adding x, y, w, h attributes then the video.
                pFile.l_Tokens.Add(
                    new lsn.Token_t(new string[] { "x", Convert.ToString(alDialog.attrX) }, (sbyte)lsn.EToken.ATTR)); ;
                pFile.l_Tokens.Add(
                    new lsn.Token_t(new string[] { "y", Convert.ToString(alDialog.attrY) }, (sbyte)lsn.EToken.ATTR));
                pFile.l_Tokens.Add(
                    new lsn.Token_t(new string[] { "w", Convert.ToString(alDialog.attrW) }, (sbyte)lsn.EToken.ATTR));
                pFile.l_Tokens.Add(
                    new lsn.Token_t(new string[] { "h", Convert.ToString(alDialog.attrH) }, (sbyte)lsn.EToken.ATTR));
                pFile.l_Tokens.Add(
                    new lsn.Token_t(new string[] { "link", alDialog.szLink }, (sbyte)lsn.EToken.LINK));
            }
        }
        /// <summary>
        /// When the "Add Video" button is clicked.
        /// </summary>
        /// <param name="sender">Object that called this.</param>
        /// <param name="e">Event parameters</param>
        private void Video_Click(object sender, RoutedEventArgs e)
        {
            AddVideoDialog avDialog = new AddVideoDialog();
            avDialog.ClearDialog();
            avDialog.ShowDialog();

            if (avDialog.szVideo != "")
            {
                /// Adding x, y, w, h attributes then the video.
                pFile.l_Tokens.Add(
                    new lsn.Token_t(new string[] { "x", Convert.ToString(avDialog.attrX) }, (sbyte)lsn.EToken.ATTR));
                pFile.l_Tokens.Add(
                    new lsn.Token_t(new string[] { "y", Convert.ToString(avDialog.attrY) }, (sbyte)lsn.EToken.ATTR));
                pFile.l_Tokens.Add(
                    new lsn.Token_t(new string[] { "w", Convert.ToString(avDialog.attrW) }, (sbyte)lsn.EToken.ATTR));
                pFile.l_Tokens.Add(
                    new lsn.Token_t(new string[] { "h", Convert.ToString(avDialog.attrH) }, (sbyte)lsn.EToken.ATTR));
                pFile.l_Tokens.Add(
                    new lsn.Token_t(new string[] { "video", avDialog.szVideo }, (sbyte)lsn.EToken.VIDEO));


                /// Adding a rectangle to the canvas, then adding a textbox
                Rect rPos = new Rect(avDialog.attrX, avDialog.attrY, avDialog.attrW, avDialog.attrH);
                Create_Rectangle(rPos);
                Create_TextBox(rPos, avDialog.szVideo);
            }
        }
        /// <summary>
        /// We now need to export to a file.
        /// </summary>
        /// <param name="sender">Object that called this.</param>
        /// <param name="e">Event parameters</param>
        private void Export_Click(object sender, RoutedEventArgs e)
        {
            ExportDialog expDialog = new ExportDialog();
            expDialog.ShowDialog();

            /// We don't want to throw a error because of a user incorrect prompt.
            if (expDialog.szOutput == "" || expDialog.szOutput == null)
            {
                MessageBox.Show("Please choose a output file.");
                return;
            }

            /// Encrypting the file.
            lsn.ParsedFile.EncryptFile(pFile, expDialog.szOutput);
            MessageBox.Show("File successfully exported!");
        }
    }
}
