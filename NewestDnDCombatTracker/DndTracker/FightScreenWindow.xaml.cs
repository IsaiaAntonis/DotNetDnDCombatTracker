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
        }

        private void buttonAttack_Click(object sender, RoutedEventArgs e)
        {
            int selectedTarget = listBox.SelectedIndex;
            Enemy attackTarget = turnOrderedList[selectedTarget];
            int damage;
            // if custom damage is empty then we just use the default calculation which is the weapon and str or dex depending on finesse

            if (!string.IsNullOrWhiteSpace(textBoxCustomAttack.Text) &&
                int.TryParse(textBoxCustomAttack.Text, out int customDamage))
            {
                // Use the custom input
                damage = customDamage;
            }
            else
            {
                string equippedWeapon = _currentTurnEntity.EquippedWeapons;
                int strengthMod = _currentTurnEntity.STR;

                // THIS LOGIC IS NOT FINISHED 
                if (equippedWeapon.Equals("longsword"))
                {
                    damage = _random.Next(1, 8) + _random.Next(1, 8) + strengthMod;
                }else
                {

                    damage = _random.Next(1, 4) + _random.Next(1, 4)  + strengthMod;
                }
            }

            labelDamage.Content = attackTarget.Name + " took " + damage +  " Damage!";
            
            attackTarget.HP = attackTarget.HP - damage ; // 1 needs to replaced by damage calculation and randomness
            
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
