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






            foreach (Enemy goblin in turnOrderedList)
                {
                    if (!string.IsNullOrWhiteSpace(goblin.ImagePath))
                    {
                        Image goblinImage = new Image
                        {
                            Width = 64,
                            Height = 64,
                            Source = new BitmapImage(new Uri(goblin.ImagePath, UriKind.Relative))
                        };

                        Canvas.SetLeft(goblinImage, 10);
                        Canvas.SetTop(goblinImage, startTop + index * verticalSpacing);

                        paperCanvas.Children.Add(goblinImage);


                        index++;    
                    }

                }
        }



    }
}
