using System;
using System.Collections.Generic;
using System.IO;
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
using Microsoft.Win32;

namespace Module1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        
        private Network MyNetwork = new Network();
        
        private void ExitCommand_Executed(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void OpenCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void OpenCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.DefaultExt = ".net";
                dialog.Filter = "Network Files|*.net|All Files|*.*";

                // Display the dialog.
                bool? result = dialog.ShowDialog();
                if (result == true)
                {
                    MyNetwork = new Network(dialog.FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MyNetwork = new Network();
            }

            // Display the network.
            DrawNetwork();
        }

        private void DrawNetwork()
        {
            mainCanvas.Children.Clear();
            MyNetwork.Draw(mainCanvas);
        }

        private void SaveIntoFile(string fileName)
        {
            File.WriteAllText(fileName, MyNetwork.Serialization());
        }

        private void BtnSimple_OnClick(object sender, RoutedEventArgs e)
        {
            MyNetwork = new Network("simple.net");
            DrawNetwork();
        }

        private void BtnBasic_OnClick(object sender, RoutedEventArgs e)
        {
            MyNetwork = new Network("basic.net");
            DrawNetwork();
        }

        private void BtnComplex_OnClick(object sender, RoutedEventArgs e)
        {
            MyNetwork = new Network("complex.net");
            DrawNetwork();
        }

        private void BtnValidate_OnClick(object sender, RoutedEventArgs e)
        {
            var valid = MyNetwork.ValidateNetwork(MyNetwork, "pip.net");
            if (valid)
                tbValidate.Text = "Passed.";
            else
                tbValidate.Text = "Failed.";
        }
    }
}