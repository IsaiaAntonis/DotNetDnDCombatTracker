using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
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

namespace DndTracker
{
    /// <summary>
    /// Interaction logic for FightScreenWindow.xaml
    /// </summary>
    public partial class FightScreenWindow : Window
    {

        private Enemy _currentTurnEntity = new Enemy();
        private List<Enemy> turnOrderedList;
        private int currentEnemyIndex = 0;
        private Ellipse _lastEllipse = new Ellipse();
        private Random _random = new Random();


        // gpt generated all dnd 5e weapons
        private readonly Dictionary<string, (int DiceCount, int DiceSides, bool IsFinesse)> _weaponDamageMap = new()
        {
            // Simple Melee Weapons
            ["Club"] = (1, 4, false),
            ["Dagger"] = (1, 4, true),
            ["Greatclub"] = (1, 8, false),
            ["Handaxe"] = (1, 6, false),
            ["Javelin"] = (1, 6, false),
            ["Light Hammer"] = (1, 4, false),
            ["Mace"] = (1, 6, false),
            ["Quarterstaff"] = (1, 6, false),
            ["Sickle"] = (1, 4, false),
            ["Spear"] = (1, 6, false),

            // Simple Ranged Weapons
            ["Light Crossbow"] = (1, 8, false),
            ["Dart"] = (1, 4, true),
            ["Shortbow"] = (1, 6, false),
            ["Sling"] = (1, 4, false),

            // Martial Melee Weapons
            ["Battleaxe"] = (1, 8, false),
            ["Flail"] = (1, 8, false),
            ["Glaive"] = (1, 10, false),
            ["Greataxe"] = (1, 12, false),
            ["Greatsword"] = (2, 6, false),
            ["Halberd"] = (1, 10, false),
            ["Lance"] = (1, 12, false),
            ["Longsword"] = (1, 8, false),
            ["Maul"] = (2, 6, false),
            ["Morningstar"] = (1, 8, false),
            ["Pike"] = (1, 10, false),
            ["Rapier"] = (1, 8, true),
            ["Scimitar"] = (1, 6, true),
            ["Shortsword"] = (1, 6, true),
            ["Trident"] = (1, 6, false),
            ["War Pick"] = (1, 8, false),
            ["Warhammer"] = (1, 8, false),
            ["Whip"] = (1, 4, true),

            // Martial Ranged Weapons
            ["Blowgun"] = (1, 1, false),
            ["Hand Crossbow"] = (1, 6, false),
            ["Heavy Crossbow"] = (1, 10, false),
            ["Longbow"] = (1, 8, false),
            ["Net"] = (0, 0, false), // No damage, restraining tool
        };

        public FightScreenWindow(List<Enemy> listOfEnemies)
        {
            InitializeComponent();

            double startTop = 10;
            double verticalSpacing = 70;
            int index = 0;
           

            // need to orderhere list according to inititive and random 

            turnOrderedList = listOfEnemies
                .OrderByDescending(e => e.Initiative)
                .ThenBy(_ => Guid.NewGuid()) // to break ties randomly
                .ToList();

   
            listBox.Items.Clear();

            foreach (Enemy enemy in turnOrderedList)
            {

                ListBoxItem item = new ListBoxItem();
                item.Content = enemy.Name;
                item.Tag = enemy.Name;                      // for attack method
                listBox.Items.Add(item);
            }



            foreach (Enemy enemy in turnOrderedList)
                {
                    if (!string.IsNullOrWhiteSpace(enemy.ImagePath))
                    {
                        Image entityImage = new Image
                        {
                            Width = 64,
                            Height = 64,
                            Source = new BitmapImage(new Uri(enemy.ImagePath, UriKind.Relative))
                        };

                        Canvas.SetLeft(entityImage, 10);
                        Canvas.SetTop(entityImage, startTop + index * verticalSpacing);

                        paperCanvas.Children.Add(entityImage);


                        index++;    
                    }

            }

            enemyCircleIndicator(currentEnemyIndex);


            _currentTurnEntity = turnOrderedList.FirstOrDefault();
            DataContext = _currentTurnEntity; // DataContext here fills the stats in xaml 
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (turnOrderedList == null || !turnOrderedList.Any())
                return;

            do
            {
                currentEnemyIndex++;

                if (currentEnemyIndex >= turnOrderedList.Count)
                    currentEnemyIndex = 0;

                // Keep looping until we find an enemy that's not dead
            } while (turnOrderedList[currentEnemyIndex].IsDead);

            _currentTurnEntity = turnOrderedList[currentEnemyIndex];
            DataContext = _currentTurnEntity;

            enemyCircleIndicator(currentEnemyIndex);

            buttonAttack.IsEnabled = true;
        }

        private void buttonAttack_Click(object sender, RoutedEventArgs e)
        {


            buttonAttack.IsEnabled = false;
            int dexMod = _currentTurnEntity.DEX / 2 - 5;
            int strMod = _currentTurnEntity.STR / 2 - 5;
            string equippedWeapon = _currentTurnEntity.EquippedWeapons;
            int selectedTarget = listBox.SelectedIndex;
            Enemy attackTarget = turnOrderedList[selectedTarget];


            int damage;
            // if custom damage is empty then we just use the default calculation which is the weapon and str or dex depending on finesse

            // DOES TARGET GET HIT OR NOT LOGIC AC CALCULATIONS


            // === AC CHECK LOGIC ===
            int attackRoll = _random.Next(1, 21); // 1d20

            // Check if weapon is finesse-capable
          
            bool isFinesseWeapon = _weaponDamageMap.TryGetValue(equippedWeapon, out var finesseCheckStats) && finesseCheckStats.IsFinesse;

            // Apply DEX mod if finesse weapon + finesse used, otherwise STR mod
            int attackBonus = (isFinesseWeapon && _currentTurnEntity.Finesse) ? dexMod : strMod;

            // Total attack = d20 roll + mod (no proficiency bonus yet)
            int totalAttack = attackRoll + attackBonus;

            if (totalAttack < attackTarget.AC)
            {
                labelDamage.Text = $"{_currentTurnEntity.Name} attacks {attackTarget.Name} but misses! (Rolled {totalAttack} vs AC {attackTarget.AC})";
                return; // Skip damage if attack misses
            }


            // TARGET DAMAGE LOGIC 


            if (!string.IsNullOrWhiteSpace(textBoxCustomAttack.Text) &&
                int.TryParse(textBoxCustomAttack.Text, out int customDamage))
            {
                damage = customDamage;
            }
            else if (_currentTurnEntity.Name.Split(",").First().Trim().Split(" ").Last() == "Dragon") // special  entity
            {
                damage = _random.Next(1, 7) + _random.Next(1, 7); // 2d6
                strMod = _currentTurnEntity.STR / 2 - 5;
                damage += strMod;

                labelDamage.Text = $"{attackTarget.Name} took {damage} slashing damage from Dragon Claw!";
                attackTarget.HP -= damage;
            }

            else if (_weaponDamageMap.TryGetValue(equippedWeapon, out var weaponStats))
            {
                int totalRoll = 0;
                for (int i = 0; i < weaponStats.DiceCount; i++)
                {
                    totalRoll += _random.Next(1, weaponStats.DiceSides + 1);
                }

                // als finesse bool van de attack current entity true is (dus hij heeft finesse weapon) dan gebruikt dex modifier in plaats van str modifier
                int abilityMod = weaponStats.IsFinesse && _currentTurnEntity.Finesse ? dexMod : strMod;
                damage = totalRoll + abilityMod;
            }
            else
            {
                damage = _random.Next(1, 4) + _random.Next(1, 4) + strMod; // fallback
            }


            labelDamage.Text = attackTarget.Name + " took " + damage +  " Damage!";

            attackTarget.HP -= damage;


            // TARGET DEAD LOGIC 
            if (attackTarget.HP <= 0)
            {
                attackTarget.IsDead = true;
                foreach (ListBoxItem item in listBox.Items)                 // this is used to compare which target is eliminated and grey them out 
                {
                    if (item.Content?.ToString() == attackTarget.Name) // this string comparison is fragile becuase duplicate names will break it good thing we generate each of our targets with numbering #
                    {
                        item.IsEnabled = false;
                        item.Opacity = 0.5; // Optional: gray it out
                        break;
                    }
                }

            }


        }

        private void buttonHeal_Click(object sender, RoutedEventArgs e)
        {

        }

        private void enemyCircleIndicator(int turnCounter)
        {
            paperCanvas.Children.Remove(_lastEllipse);

            Ellipse ellipse = new Ellipse();
            ellipse.Stroke = new SolidColorBrush(Colors.Red);

            ellipse.Width = 80;
            ellipse.Height = 80;
            ellipse.Margin = new Thickness(0, 0 + turnCounter * 70, 0,0);

            _lastEllipse = ellipse;
            paperCanvas.Children.Add(ellipse);
        }

    }
}
