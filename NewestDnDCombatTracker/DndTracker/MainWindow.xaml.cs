using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace DndTracker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public Enemy  generateEnemy()
        {
            Random random = new Random();

            Enemy goblin = new Enemy();
            goblin.Name = "goblin";
            goblin.CON = 10;
            goblin.DEX = 14;
            goblin.INT = 10;
            goblin.CHA = 8;
            goblin.STR = 8;
            goblin.WIS = 8;

            goblin.AC = 15;

            goblin.Initiative = (goblin.DEX % 2) - 5; // this should give back 2 
            goblin.HitDiceFlatModifier = (goblin.CON/2) - 5 ; // if constituion is 10 then it should equal 5 -5 = 0 if less gives a negative modifier and if more a positive 

            goblin.HitDiceAmount = 2;
            goblin.HitDiceSize = 6;

            int generatedhp = 0;
            for (int i = 0; i < goblin.HitDiceAmount; i++) {

                generatedhp += random.Next(1, goblin.HitDiceSize + 1);
       
            }

            goblin.HP = goblin.HitDiceFlatModifier +  generatedhp ;



            goblin.ImagePath = @"Images\goblin.jpg";


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


            return goblin;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {

            List<Enemy> listOfEnemies = new List<Enemy>();

            for (int i = 0; i < Convert.ToInt64(AmountOfEnemiesTextBox.Text); i++)
            {
                listOfEnemies.Add(generateEnemy());
            }
            foreach (Enemy enemy in listOfEnemies)
            {
                listBox.Items.Add(enemy.Name);

            }




           
        }
    }
}

