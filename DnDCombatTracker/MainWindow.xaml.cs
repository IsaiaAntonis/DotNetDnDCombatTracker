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

namespace DnDCombatTracker
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

        private void AddEnemyButton_Click(object sender, RoutedEventArgs e)
        {
            AddEnemyWindow addEnemyWindow = new AddEnemyWindow();
            addEnemyWindow.ShowDialog();
        }

        private void StartEncounterButton_Click(object sender, RoutedEventArgs e)
        {
            EncounterSetupWindow encounterSetupWindow = new EncounterSetupWindow();
            encounterSetupWindow.ShowDialog();
        }

        private void loadEncounterButton_Click(object sender, RoutedEventArgs e)
        {
            EncounterSelectWindow encounterSelectWindow = new EncounterSelectWindow();
            encounterSelectWindow.ShowDialog();
        }
    }
}