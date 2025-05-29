using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDCombatTracker
{
    
    internal static class FileHandeler
    {
        public static readonly string programPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "DnDEncounterTracker"
        );
        public static void CheckIfDirectoryExists(string path) 
        {
            if (!System.IO.Directory.Exists(path))
            {
                CreateDirectory(path);
            }
        }
        private static void CreateDirectory(string path)
        {
            System.IO.Directory.CreateDirectory(path);
        }

    }
}
