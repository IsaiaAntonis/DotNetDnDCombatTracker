using System;
using System.Collections.Generic;
using System.IO;
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

namespace DnDCombatTracker
{
    /// <summary>
    /// Interaction logic for EncounterWindow.xaml
    /// </summary>
    public partial class EncounterWindow : Window
    {
        public EncounterWindow(string encountername)
        {
            InitializeComponent();
            InitializeEnemyList(encountername);

        }

        private void InitializeEnemyList(string encountername)
        {
            string folderPath = Environment.CurrentDirectory;
            string filePath = System.IO.Path.GetFullPath(System.IO.Path.Combine(folderPath, $@"..\..\..\Encounters\{encountername}.txt"));

            try
            {
                using StreamReader streamReader = new StreamReader(filePath);
                List<string> encounterElementList = new List<string>();
                while (!streamReader.EndOfStream)
                {

                    encounterElementList.Add(streamReader.ReadLine());

                }
                this.Title = encounterElementList[0].Split(':').Last();
                foreach (string monster in encounterElementList) {
                    if (!(monster == encounterElementList[0])) {
                        encounterEnemyListbox.Items.Add(monster);
                    }
                   
                }
                amountOfEnemiesTextBox.Text = (encounterElementList.Count -1).ToString();
            }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
