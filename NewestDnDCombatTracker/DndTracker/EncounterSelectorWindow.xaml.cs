using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Numerics;
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
using System.Xml.Linq;

namespace DndTracker
{
    /// <summary>
    /// Interaction logic for EncounterSelectorWindow.xaml
    /// </summary>
    public partial class EncounterSelectorWindow : Window
    {
        public EncounterSelectorWindow()
        {
            InitializeComponent();
            listBox.DisplayMemberPath = "Name";
            listBox.MouseDoubleClick += listBoxItem_DoubleClick;


            int totalEntities = 0;
                // Path to AppData\DndTracker\name.txt
            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string dir = System.IO.Path.Combine(appDataPath, "DndTracker");

            try
            {


                if (!Directory.Exists(dir))
                {
                    MessageBox.Show("No entities found.");
                    return;
                }

                totalEntities = Directory.GetFiles(dir, "*.txt").Length;

            }
            catch
            {
                MessageBox.Show("You didn't create any entities");
            }

            foreach (string text in Directory.GetFiles(dir, "*.txt"))
            {
                ComboBoxItem comboBoxItem = new ComboBoxItem();

               
                comboBoxItem.Content = text.Split('\\').Last().Split(".").First();
                combox.Items.Add(comboBoxItem);
            }
 

        }

        private List<Enemy> listOfEnemies = new List<Enemy>();// will private give issue 
        private int entityIndex = 0;    
        public Enemy generateEnemy(int enemyindex)  
        {
            Random random = new Random();

            // this should be the select entity
            string selectedType = combox.SelectionBoxItem.ToString();

            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string dir = System.IO.Path.Combine(appDataPath, "DndTracker");
     
            string filePath = System.IO.Path.Combine(dir, $"{selectedType}.txt");


            string[] data = File.ReadAllText(filePath).Split('|');

            Enemy entity = new Enemy();

            // Fill properties from file
            entity.Name = data[0];
            entity.AC = int.Parse(data[1]);
            entity.HP = int.Parse(data[2]);
            entity.Initiative = int.Parse(data[3]) + random.Next(1,21);

            entity.STR = int.Parse(data[4]);
            entity.DEX = int.Parse(data[5]);
            entity.CON = int.Parse(data[6]);
            entity.INT = int.Parse(data[7]);
            entity.WIS = int.Parse(data[8]);
            entity.CHA = int.Parse(data[9]);

            entity.HitDiceAmount = int.Parse(data[10]);
            entity.HitDiceSize = int.Parse(data[11]);
            entity.HitDiceFlatModifier = int.Parse(data[12]);

            entity.Notes = data[13];


            //dit is om het meer robust te maken als je bvb een pad copy dan heeft die quotes en @ terwijl als je manual schijft moet het ook werken
            string cleanedPath = data[14]
                .Replace("\"", "")           // remove quotes
                .Replace("@", "")            // remove @
                .Trim();                     // remove whitespace and newlines
            entity.ImagePath =cleanedPath;

 
            int hpFromDice = 0;
            for (int i = 0; i < entity.HitDiceAmount; i++)
            {
                hpFromDice += random.Next(1, entity.HitDiceSize + 1);
            }
            entity.HP = hpFromDice + entity.HitDiceFlatModifier;

            entity.Name = $"#{enemyindex + 1} {data[0]} , Initiative: {entity.Initiative}";

            entity.EquippedWeapons = data[15];

            if (data[16].Equals("true")){
                entity.Finesse = true;
            }
            else
            {
                entity.Finesse = false;

            };



            return entity;



            /*
            if (selectedType.Equals("Goblin")) // the static data inside here will eventually be replaced with a streamreader out of a txt 
            {
                Enemy goblin = new Enemy();

                goblin.CON = 10;
                goblin.DEX = 14;
                goblin.INT = 10;
                goblin.CHA = 8;
                goblin.STR = 8;
                goblin.WIS = 8;

                goblin.AC = 15;

                goblin.Initiative = (goblin.DEX / 2) - 5 + random.Next(1, 21); // this should give back 2 + random
                goblin.HitDiceFlatModifier = (goblin.CON / 2) - 5; // if constituion is 10 then it should equal 5 -5 = 0 if less gives a negative modifier and if more a positive 

                goblin.Name = $"#{enemyindex + 1} Goblin , Initiative: {goblin.Initiative}";

                goblin.HitDiceAmount = 2;
                goblin.HitDiceSize = 6;
                goblin.EquippedWeapons = "daggers";

                int generatedhp = 0;
                for (int i = 0; i < goblin.HitDiceAmount; i++)
                {

                    generatedhp += random.Next(1, goblin.HitDiceSize + 1);

                }

                goblin.HP = goblin.HitDiceFlatModifier + generatedhp;

                goblin.ImagePath = @"Images\goblin.jpg";

                return goblin;

            }else if (selectedType.Equals("Bandit"))
            {
                Enemy bandit = new Enemy();

                bandit.CON = 12;
                bandit.DEX = 11;
                bandit.INT = 10;
                bandit.CHA = 10;
                bandit.STR = 11;
                bandit.WIS = 10;

                bandit.AC = 12;

                bandit.Initiative = (bandit.DEX / 2) - 5 + random.Next(1, 21); // e.g., 0 + d20
                bandit.HitDiceFlatModifier = (bandit.CON / 2) - 5;

                bandit.Name = $"#{enemyindex + 1} Bandit , Initiative: {bandit.Initiative}";

                bandit.HitDiceAmount = 2;
                bandit.HitDiceSize = 8;
                bandit.EquippedWeapons = "longsword";

                int generatedhp = 0;
                for (int i = 0; i < bandit.HitDiceAmount; i++)
                {
                    generatedhp += random.Next(1, bandit.HitDiceSize + 1);
                }

                bandit.HP = bandit.HitDiceFlatModifier + generatedhp;
                bandit.ImagePath = @"Images\bandit.jpg";

                return bandit;
            }
            else
            {
                return null;
            }
            */

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            listBox.Items.Clear();


            for (int i = 0; i < Convert.ToInt64(AmountOfEnemiesTextBox.Text); i++)
            {
                listOfEnemies.Add(generateEnemy(entityIndex));
                entityIndex++;
            }

            foreach (Enemy enemy in listOfEnemies)
            {
                listBox.Items.Add(enemy);

            }

        }

        private void buttonStartTurnBased_Click(object sender, RoutedEventArgs e)
        {
            Window fightWindow = new FightScreenWindow(listOfEnemies);
            fightWindow.Show();
        }

        private void buttonStartInteractive_Click(object sender, RoutedEventArgs e)
        {

        }
        private void listBoxItem_DoubleClick(object sender, RoutedEventArgs e)
        {
            ListBox listBox = (ListBox)sender;
    
            if (listBox.SelectedItem is Enemy selectedEnemy)
            {
                Window enemyInformationWindow = new EnemyInformationWindow(selectedEnemy);
                enemyInformationWindow.Show();
            }

        }

    }
}
