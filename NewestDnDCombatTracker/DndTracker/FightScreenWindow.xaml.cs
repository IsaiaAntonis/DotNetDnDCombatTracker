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
        public FightScreenWindow(List<Enemy> listOfEnemies)
        {
            InitializeComponent();

            foreach (Enemy goblin in listOfEnemies)
                {
                    if (!string.IsNullOrWhiteSpace(goblin.ImagePath))
                    {
                        Image goblinImage = new Image
                        {
                            Width = 64,
                            Height = 64,
                            Source = new BitmapImage(new Uri(goblin.ImagePath, UriKind.Relative))
                        };

                        Canvas.SetLeft(goblinImage, 100);
                        Canvas.SetTop(goblinImage, 150);
                        paperCanvas.Children.Add(goblinImage);
                    }

                }
        }



    }
}
