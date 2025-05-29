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

namespace DnDCombatTracker
{
    /// <summary>
    /// Interaction logic for EncounterSelectWindow.xaml
    /// </summary>
    public partial class EncounterSelectWindow : Window
    {
        public EncounterSelectWindow()
        {
            InitializeComponent();
            this.Title = "Encounter Select";
            InitializeEncounterListbox();
        }

        private void InitializeEncounterListbox()
        {
            string folderPath = FileHandeler.programPath; //Environment.CurrentDirectory;

            string filePath = System.IO.Path.Combine(folderPath, "Encounters");
            string[] Files = System.IO.Directory.GetFiles(filePath);

            foreach (string File in Files)
            {
                encounterListBox.Items.Add(System.IO.Path.GetFileNameWithoutExtension(File));
            }
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void encounterListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(encounterListBox.SelectedItem != null)
            {
                EncounterWindow encounterWindow = new EncounterWindow(encounterListBox.SelectedItem.ToString());
                encounterWindow.ShowDialog();
            }
        }
    }
}
