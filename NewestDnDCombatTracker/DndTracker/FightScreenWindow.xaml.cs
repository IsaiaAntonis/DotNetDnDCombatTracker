using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
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
    /// Interaction logic for FightScreenWindow.xaml
    /// </summary>
    public partial class FightScreenWindow : Window
    {

        private Enemy _currentEnemy = new Enemy();
        private List<Enemy> turnOrderedList;
        private int currentEnemyIndex = 0;
        private Ellipse _lastEllipse = new Ellipse();
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
                listBox.Items.Add(enemy);
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
            _currentEnemy = turnOrderedList.FirstOrDefault();
            DataContext = _currentEnemy;


        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (turnOrderedList == null || !turnOrderedList.Any())
                return;

            currentEnemyIndex++;

            // Loop back to the beginning if we reach the end
            if (currentEnemyIndex >= turnOrderedList.Count)
                currentEnemyIndex = 0;

            _currentEnemy = turnOrderedList[currentEnemyIndex];
            DataContext = _currentEnemy;

            enemyCircleIndicator(currentEnemyIndex);
        }

        private void buttonAttack_Click(object sender, RoutedEventArgs e)
        {
            int selectedTarget = listBox.SelectedIndex;

            turnOrderedList[selectedTarget].HP = 1; // 1 needs to replaced by damage calculation and randomness


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
