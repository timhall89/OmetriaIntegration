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
using OmIntLib;
using IOPath = System.IO.Path;

namespace UIConroller
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private Settings settings { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            foreach (string s in RootMethods.SendMethodNames) SendMethodComboBox.Items.Add(s);
            foreach (string s in RootMethods.GetMethodNames) GetMethodComboBox.Items.Add(s);

        }

        private void GetFileNameBtn_Click(object sender, RoutedEventArgs e)
        {
            string path = string.Empty;

            try {
                path = GetFileName("JPEG Files (*.json)|*.json");
                FileNameTB.Text = path;

                SQLNameComboBox.Items.Clear();

                settings = new Settings(File.ReadAllText(path));
                
                foreach (string s in Directory.GetFiles(settings["SQLFolder"]).Where(s => IOPath.GetExtension(s) == ".sql").Select(s => IOPath.GetFileName(s))) SQLNameComboBox.Items.Add(s);
                
            } catch { }
            
        }

        private string GetFileName(string filter)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog
            {
                DefaultExt = ".json",
                Filter = "JSON Files (*.json)|*.json"
            };

            bool? result = dlg.ShowDialog();

            if (result == true) return dlg.FileName;
            else throw new Exception("No File Selected");
        }

        private void RunBtn_Click(object sender, RoutedEventArgs e)
        {
            if (settings == null)
            {
                MessageBox.Show("Please select a settings file");
                return;
            }
            Action<string> Log = s => File.AppendAllText(settings["LogFilePath"], $"{DateTime.Now:yyyy/MM/dd HH:mm:ss} || {s}{Environment.NewLine}");
            string SendMethodName = SendMethodComboBox.Text;
            string GetMethodName = GetMethodComboBox.Text;
            string SQLName = SQLNameComboBox.Text;

            try {
                new RootMethods(s => Log(s), settings).RunFlow(SendMethodName, GetMethodName, SQLName);
                MessageBox.Show("Complete");
            } catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}
