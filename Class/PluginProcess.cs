using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Plugins_hierarchy.Class {
    public static class PluginProcess {
        public static List<Plugin> LoadPlugins() {
            if (!File.Exists("plugin/info.plg")) {
                Directory.CreateDirectory("plugin/");
                FileStream sw = File.Create("plugin/info.plg");
                sw.Close();
            }

            List<Plugin> pluginList = new List<Plugin>();
            string[] plugins_path = File.ReadAllText("plugin/info.plg").Split(new char[] { '\n', '\r' });

            foreach (string plugin_path in plugins_path) {
                if (plugin_path != "") { pluginList.Add(new Plugin(plugin_path)); }
            }
            return pluginList;
        }

        public static bool InstallPlugin(string pathToPlgFile) {
            string descriptionFile;
            using (StreamReader sr = new StreamReader(pathToPlgFile)) {
                descriptionFile = sr.ReadToEnd();
            }
            string[] parsedFile = descriptionFile.Split(' ');

            string className = parsedFile[0];
            string pluginName = parsedFile[2];
          
            Directory.CreateDirectory("plugin/" + className);
            File.Copy(pathToPlgFile, "plugin/" + className + "/plugin.plg");
            File.Copy(Path.GetDirectoryName(pathToPlgFile) + "/" + pluginName, "plugin/" + className + "/" + pluginName);

            using (StreamWriter sw = new StreamWriter("plugin/info.plg", true)) {
                sw.WriteLine("plugin/" + className + "/");
            }
            return true;
        }
    }
}
