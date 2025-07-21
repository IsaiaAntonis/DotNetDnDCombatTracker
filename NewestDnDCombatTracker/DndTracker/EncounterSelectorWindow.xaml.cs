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

        }

        private List<Enemy> listOfEnemies = new List<Enemy>();// will private give issue 
        private int entityIndex = 0;    
        public Enemy generateEnemy(int enemyindex) // static enemy generation method 
        {
            Random random = new Random();

            string selectedType = combox.SelectionBoxItem.ToString();

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
