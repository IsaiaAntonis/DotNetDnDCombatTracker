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
    /// Interaction logic for EncounterWindow.xaml
    /// </summary>
    public partial class EncounterWindow : Window
    {
        public EncounterWindow(string encountername)
        {
            InitializeComponent();
            InitializeEnemyList(encountername);

        }

        private void InitializeEnemyList(string encountername)
        {
            string folderPath = FileHandeler.programPath; //Environment.CurrentDirectory;
            string filePath = System.IO.Path.Combine(folderPath, $@"Encounters\{encountername}.txt");
  

            try
            {
                using StreamReader streamReader = new StreamReader(filePath);
                List<string> encounterElementList = new List<string>();
                while (!streamReader.EndOfStream)
                {

                    encounterElementList.Add(streamReader.ReadLine());

                }
                this.Title = encounterElementList[0].Split(':').Last();
                int monsterNumber = 1;
                foreach (string monster in encounterElementList) {
                    if (!(monster == encounterElementList[0])) {

                        if (monster.Contains("#")) {

                            encounterEnemyListbox.Items.Add($"{monster}");

                        }
                        else
                        {
                            encounterEnemyListbox.Items.Add($"#{monsterNumber} {monster}");
                            monsterNumber++;
                        }

                    }
                   
                }
                amountOfEnemiesTextBox.Text = (encounterElementList.Count -1).ToString();
            }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
            }
        }

        private void encounterEnemyListbox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            if (encounterEnemyListbox.SelectedItem != null)
            {
                int index = encounterEnemyListbox.SelectedIndex;


                if (encounterEnemyListbox.SelectedItem.ToString().Contains("hp"))
                {
                    string selectedItem = encounterEnemyListbox.SelectedItem.ToString();
                    int currentHp = Int32.Parse(selectedItem.Split(' ').First());
                    int newHp = currentHp + Int32.Parse(healOrDamageTextBox.Text);

                    if (newHp < 0)
                    {
                        encounterEnemyListbox.Items.RemoveAt(index);
                        amountOfEnemiesTextBox.Text = (Int32.Parse(amountOfEnemiesTextBox.Text) - 1).ToString();
                        return;
                    }

                    string updatedItem = selectedItem.Substring(selectedItem.IndexOf("hp"));
                    encounterEnemyListbox.Items[index] = $"{newHp} {updatedItem}";
                }
                else
                {
                    string selected_enemy = encounterEnemyListbox.SelectedItem.ToString().Split(" ").Last();

                    encounterEnemyListbox.Items[index] = $"{GetEnemyHP(selected_enemy)} hp {encounterEnemyListbox.SelectedItem.ToString()}";

                }

                

            }
        }

        public int GetEnemyHP(string enemy)
        {
            int hp = 0;
            string folderPath = FileHandeler.programPath; //Environment.CurrentDirectory;
            string filePath = System.IO.Path.Combine(folderPath, $@"EnemyTypes\{enemy}.txt");

            try
            {
                using StreamReader streamReader = new StreamReader(filePath);
                List<string> enemyElementList = new List<string>();
                while (!streamReader.EndOfStream)
                {
                    enemyElementList.Add(streamReader.ReadLine());
                }



                string[] hitDiceElements = enemyElementList[10].Split(new Char[] { ':', 'D', '-', '+', ' ' }, StringSplitOptions.RemoveEmptyEntries);


                List<string> numericParts = new List<string>();

                foreach (string part in hitDiceElements)
                {
                    if (int.TryParse(part, out _))
                    {
                        numericParts.Add(part);
                    }
                }

               int hitDiceBoxAmount = Int32.Parse( numericParts[0]);
               int hitDiceBoxSizeMax = Int32.Parse(numericParts[1]);
               int hitDiceModifier = Int32.Parse(numericParts[2]);


               Random randomHP = new Random();
               for (int i = 0; i < hitDiceBoxAmount; i++)
                {

                  hp +=  randomHP.Next(1, hitDiceBoxSizeMax);
                    
                }
                hp += hitDiceBoxSizeMax;

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message.ToString());

            }
            return hp;
        }

        private void saveEncounterButton_Click(object sender, RoutedEventArgs e)
        {
            string folderPath = FileHandeler.programPath; //Environment.CurrentDirectory;
            string filePath = System.IO.Path.Combine(folderPath, $@"Encounters\{this.Title.Trim()}.txt");

            try
            {
                using StreamWriter encounterWriter = new StreamWriter(filePath);
                encounterWriter.WriteLine($"encounter name: {this.Title}");
                foreach (string enemy in encounterEnemyListbox.Items)
                {
                    encounterWriter.WriteLine($"{enemy}");
                }
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            
        }
    }
}
