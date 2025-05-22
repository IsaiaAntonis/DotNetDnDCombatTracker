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
    /// Interaction logic for AddEnemyWindow.xaml
    /// </summary>
    public partial class AddEnemyWindow : Window
    {
        public AddEnemyWindow()
        {
            InitializeComponent();
            closeButton.Visibility = Visibility.Hidden;
        }
        public AddEnemyWindow(string enemy)
        {
            InitializeComponent();
            ShowSelectedEnemy(enemy);
            SaveButton.Visibility = Visibility.Hidden;
            CancelButton.Visibility = Visibility.Hidden;
        }


        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if ((EnemyNameTextBox.Text == "Enemy Name") || EnemyNameTextBox.Text.Length <1) 
            {
                QuestionTextbox("name");
            }
           else if ((HpTextBox.Text == "0") || (HpTextBox.Text.Length < 1))
            {
                QuestionTextbox("hp");
            }
           else if ((AcTextBox.Text == "0") || (AcTextBox.Text.Length < 1))
            {
                QuestionTextbox("ac");
            }
            else
            {
                AddEnemyToLibrary();
                this.Close();
            }

        }

        private void AddEnemyToLibrary()
        {
            string folderPath = Environment.CurrentDirectory;
            string filePath = System.IO.Path.GetFullPath(System.IO.Path.Combine(folderPath, $@"..\..\..\EnemyTypes\{EnemyNameTextBox.Text}.txt"));

            try
            {
                using StreamWriter streamWriter = new StreamWriter(filePath);
                streamWriter.WriteLine($"Enemy name: {EnemyNameTextBox.Text}");
                streamWriter.WriteLine($"HP: {HpTextBox.Text}");
                streamWriter.WriteLine($"AC: {AcTextBox.Text}");
                streamWriter.WriteLine($"STR: {StrTextbox.Text}");
                streamWriter.WriteLine($"DEX: {DexTextbox.Text}");
                streamWriter.WriteLine($"CON: {ConTextbox.Text}");
                streamWriter.WriteLine($"INT: {IntTextbox.Text}");
                streamWriter.WriteLine($"WIS: {WisTextbox.Text}");
                streamWriter.WriteLine($"CHA: {ChaTextbox.Text}");
                streamWriter.WriteLine($"Additional notes: {noteTextBox.Text}");
                MessageBox.Show($"Enemy succesfully added to {filePath}");
            }
            catch(Exception ex) {

                MessageBox.Show(ex.Message.ToString());
            }
            


        }

        public void QuestionTextbox(string ForgottenItem)
        {
            MessageBox.Show($"Did you forget to set the enemy {ForgottenItem} ?", "Warning");
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public void ShowSelectedEnemy(string enemy)
        {
            string folderPath = Environment.CurrentDirectory;
            string filePath = System.IO.Path.GetFullPath(System.IO.Path.Combine(folderPath, $@"..\..\..\EnemyTypes\{enemy}.txt"));

            try
            {
                using StreamReader streamReader = new StreamReader(filePath);
                List<string> enemyElementList = new List<string>();
                while (!streamReader.EndOfStream) {
                    enemyElementList.Add(streamReader.ReadLine());
                }
                EnemyNameTextBox.IsEnabled = false;
                HpTextBox.IsEnabled = false;
                AcTextBox.IsEnabled = false;
                StrTextbox.IsEnabled = false;
                DexTextbox.IsEnabled = false;
                ConTextbox.IsEnabled = false;
                IntTextbox.IsEnabled = false;
                WisTextbox.IsEnabled = false;
                ChaTextbox.IsEnabled = false;
                noteTextBox.IsEnabled = false;

                EnemyNameTextBox.Text = enemyElementList[0].Split(':').Last();
                HpTextBox.Text = enemyElementList[1].Split(':').Last(); 
                AcTextBox.Text = enemyElementList[2].Split(':').Last();
                StrTextbox.Text = enemyElementList[3].Split(':').Last();
                DexTextbox.Text = enemyElementList[4].Split(':').Last();
                ConTextbox.Text = enemyElementList[5].Split(':').Last();
                IntTextbox.Text = enemyElementList[6].Split(':').Last();
                WisTextbox.Text = enemyElementList[7].Split(':').Last();
                ChaTextbox.Text = enemyElementList[8].Split(':').Last();
                noteTextBox.Text = enemyElementList[9].Split(':').Last();


            }
            catch (Exception ex) {
            
                MessageBox.Show(ex.Message.ToString());

            }

        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
