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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Plugins_hierarchy.Class;
using Microsoft.Win32;

namespace Plugins_hierarchy {
    public partial class MainWindow : Window {

        public MainWindow() {
            InitializeComponent();
            Model.pluginList = PluginProcess.LoadPlugins();
            RefreshPluginList();
        }

        public void RefreshObjectList() {
            Items.ItemsSource = null;
            Items.ItemsSource = Model.objectList;
        }

        public void RefreshPluginList() {
            Types.ItemsSource = null;
            Types.ItemsSource = Model.pluginList;
            Model.typeList.Clear();
            foreach (var item in Model.pluginList) {
                Model.typeList.Add(item.CreateObject());
            }
        }

        private void BtnInstallPlugin_Click(object sender, RoutedEventArgs e) {
            try {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                if (openFileDialog.ShowDialog() == true) {
                    PluginProcess.InstallPlugin(openFileDialog.FileName);
                }

                MessageBox.Show("Plugin is installed");
                Model.pluginList = PluginProcess.LoadPlugins();
                RefreshPluginList();
            } catch {
                MessageBox.Show("Plugin is installed or invalid");
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e) {
        }

        private void Items_SelectionChanged(object sender, SelectionChangedEventArgs e) {
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e) {
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e) {
        }

        private void BtnSerialize_Click(object sender, RoutedEventArgs e) {
        }

        private void BtnDeserialize_Click(object sender, RoutedEventArgs e) {


        }
    }
}
