using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace DndTracker
{
    /// <summary>
    /// Interaction logic for MonsterCreationWindow.xaml
    /// </summary>
    public partial class MonsterCreationWindow : Window
    {
        private CharacterBase character;

        public MonsterCreationWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Get data from UI controls
                string name = textBoxName.Text;
                string ac = textBoxAC.Text;
                string hp = textBoxHP.Text;
                string initiative = textBoxInitiative.Text;
                string str = textBoxSTR.Text;
                string dex = textBoxDEX.Text;
                string con = textBoxCON.Text;
                string intel = textBoxINT.Text;
                string wis = textBoxWIS.Text;
                string cha = textBoxCHA.Text;
                string hitDiceAmount = textBoxHitDiceAmount.Text;
                string hitDiceSize = textBoxHitDiceSize.Text;
                string hitDiceMod = textBoxHitDiceFlatModifier.Text;
                string notes = textBoxNotes.Text.Replace("\n", " ").Replace("\r", "");
                string imagePath = textBoxImagePath.Text;

                // Determine character type
                if (comboBox.SelectedValue.ToString() == "Player")
                {
                    character = new Player();
                }
                else
                {
                    character = new Enemy();
                }

                // Build output line
                string separator = "|";
                string line = string.Join(separator, new string[]
                {
                    name, ac, hp, initiative, str, dex, con, intel, wis, cha,
                    hitDiceAmount, hitDiceSize, hitDiceMod, notes, imagePath
                });

                // Path to AppData\DndTracker\name.txt
                string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                string dir = Path.Combine(appDataPath, "DndTracker");
                Directory.CreateDirectory(dir); // Ensure folder exists

                string filePath = Path.Combine(dir, $"{name}.txt");

                // Write to file
                using StreamWriter writer = new StreamWriter(filePath);
                writer.WriteLine(line);

                MessageBox.Show($"Monster '{name}' saved to {filePath}");

                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving monster: " + ex.Message);
            }
        }
    }
}
