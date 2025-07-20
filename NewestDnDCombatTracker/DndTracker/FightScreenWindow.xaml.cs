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
    /// Interaction logic for FightScreenWindow.xaml
    /// </summary>
    public partial class FightScreenWindow : Window
    {


        private List<Enemy> turnOrderedList = new List<Enemy>();
        public FightScreenWindow(List<Enemy> listOfEnemies)
        {
            InitializeComponent();

            double startTop = 10;
            double verticalSpacing = 70;
            int index = 0;


            // need to orderhere list according to inititive and random 

            var turnOrderedList = listOfEnemies
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
        }




    }
}
