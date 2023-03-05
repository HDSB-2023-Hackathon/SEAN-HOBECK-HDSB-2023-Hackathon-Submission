using Microsoft.Win32;
using studio.Dialogs;
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
using System.Windows.Navigation;

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
