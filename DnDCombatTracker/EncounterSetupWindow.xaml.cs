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
using System.Windows.Shapes;

namespace DnDCombatTracker
{
    /// <summary>
    /// Interaction logic for EncounterSetupWindow.xaml
    /// </summary>
    public partial class EncounterSetupWindow : Window
    {
        public EncounterSetupWindow()
        {
            InitializeComponent();
            this.Title = "Enemy encounter setup";
            LoadEnemiesIntoList();
            
        }

        private void LoadEnemiesIntoList()
        {
            string folderPath = FileHandeler.programPath; //Environment.CurrentDirectory;
            string filePath = System.IO.Path.Combine(folderPath, "EnemyTypes");
            string[] Files = System.IO.Directory.GetFiles(filePath);
            
            foreach (string File in Files)
            {
                enemyTypeListBox.Items.Add(System.IO.Path.GetFileNameWithoutExtension(File));
            }

       


        }

        private void enemyTypeListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (enemyTypeListBox.SelectedItem != null)
            {
                string selected_enemy = enemyTypeListBox.SelectedItem.ToString();

                AddEnemyWindow showSelectedEnemyWindow = new AddEnemyWindow(selected_enemy);
                showSelectedEnemyWindow.ShowDialog();

            }
        }

        private void applyButton_Click(object sender, RoutedEventArgs e)
        {
            if (enemyTypeListBox.SelectedItem != null)
            {
                int amountOfEnemies = Int32.Parse(enemyAmountTextBox.Text);
                for (int i = 0; i < amountOfEnemies; i++)
                {
                    enemyAmountListBox.Items.Add(enemyTypeListBox.SelectedItem);
                }
                
            }
        }

        private void startEncounterButton_Click(object sender, RoutedEventArgs e)
        {
            string folderPath = FileHandeler.programPath; //Environment.CurrentDirectory;
            if (encounterName.Text.Length > 0 && enemyAmountListBox.Items.Count >0)
            {
                string filePath = System.IO.Path.Combine(folderPath, $@"Encounters\{encounterName.Text}.txt");
                try
                {
                    using StreamWriter encounterWriter = new StreamWriter(filePath);
                    encounterWriter.WriteLine($"encounter name: {encounterName.Text}");
                    foreach (string enemy in enemyAmountListBox.Items)
                    {
                        encounterWriter.WriteLine($"{enemy}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                this.Close();
                EncounterWindow encounterWindow = new EncounterWindow(encounterName.Text);
                encounterWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show("Your encounter needs a name and enemies!");
            }
        }
    }
}
