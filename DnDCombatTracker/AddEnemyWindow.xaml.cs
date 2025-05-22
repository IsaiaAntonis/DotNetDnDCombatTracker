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
    }
}
