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
    /// Interaction logic for EncounterSetupWindow.xaml
    /// </summary>
    public partial class EncounterSetupWindow : Window
    {
        public EncounterSetupWindow()
        {
            InitializeComponent();
            LoadEnemiesIntoList();
            
        }

        private void LoadEnemiesIntoList()
        {
            string folderPath = Environment.CurrentDirectory;
            string filePath = System.IO.Path.GetFullPath(System.IO.Path.Combine(folderPath, $@"..\..\..\EnemyTypes"));
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
            this.Close();
            EncounterWindow encounterWindow = new EncounterWindow();
            encounterWindow.ShowDialog();
        }
    }
}
