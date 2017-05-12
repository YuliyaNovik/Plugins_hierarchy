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
using System.Reflection;
using Plugins_hierarchy.Class;

namespace Plugins_hierarchy.Pages {
    public partial class Edit : Page {
        private Plugin plugin;
        private int index;
        private List<TextBox> inputField = new List<TextBox>();
        private List<TextBox> inputProperty = new List<TextBox>();
        private MainWindow super = new MainWindow();

        public Edit(int index, MainWindow super) {
            InitializeComponent();
            this.plugin = Model.pluginList[Model.objectTypeList[index]];
            this.super = super;
            this.index = index;
            CreateMarkup();
            DisplayItem();
        }

        private void CreateMarkup() {
            List<string> fieldsInput = new List<string>();
            List<string> propsInput = new List<string>();
            foreach (var item in this.plugin.GetFields()) {
                fieldsInput.Add(item.Name);
            }
            foreach (var item in this.plugin.GetProperties()) {
                propsInput.Add(item.Name);
            }
            Grid dynamicGrid = CreateGrid(fieldsInput, propsInput);
            CreateInput(dynamicGrid, fieldsInput, propsInput);
        }

        private Grid CreateGrid(List<string> fieldsInput, List<string> propsInput) {
            Grid dynamicGrid = new Grid();

            List<RowDefinition> rows = new List<RowDefinition>();
            foreach (var item in fieldsInput) {
                RowDefinition row = new RowDefinition();
                row.Height = new GridLength(30);
                rows.Add(row);
            }
            foreach (var item in propsInput) {
                RowDefinition row = new RowDefinition();
                row.Height = new GridLength(30);
                rows.Add(row);
            }
            RowDefinition rowBtn = new RowDefinition();
            rowBtn.Height = new GridLength(30);
            rows.Add(rowBtn);

            ColumnDefinition colInput = new ColumnDefinition();
            ColumnDefinition colDescription = new ColumnDefinition();
            colDescription.Width = new GridLength(100);

            dynamicGrid.ColumnDefinitions.Add(colDescription);
            dynamicGrid.ColumnDefinitions.Add(colInput);
            foreach (var item in rows) {
                dynamicGrid.RowDefinitions.Add(item);
            }

            return dynamicGrid;
        }

        private TextBox CreateInputRow(string stringInput, int pos, Grid dynamicGrid) {
            TextBlock label = new TextBlock();
            label.Text = stringInput;
            label.Margin = new Thickness(5, 5, 5, 5);
            Grid.SetRow(label, pos);
            Grid.SetColumn(label, 0);
            dynamicGrid.Children.Add(label);

            TextBox inputBox = new TextBox();
            inputBox.Margin = new Thickness(5, 5, 5, 5);
            Grid.SetRow(inputBox, pos);
            Grid.SetColumn(inputBox, 1);
            dynamicGrid.Children.Add(inputBox);
            return inputBox;
        }

        private void CreateInput(Grid dynamicGrid, List<string> fieldsInput, List<string> propsInput) {
            int index = 0;
            for (int i = 0; i < fieldsInput.Count; i++) {
                TextBox inputBox = CreateInputRow(fieldsInput[i], index, dynamicGrid);
                inputField.Add(inputBox);
                index++;
            }

            for (int i = 0; i < propsInput.Count; i++) {
                TextBox inputBox = CreateInputRow(propsInput[i], index, dynamicGrid);
                inputProperty.Add(inputBox);
                index++;
            }

            Button btnAdd = new Button();
            btnAdd.Content = "Edit";
            btnAdd.Margin = new Thickness(5, 5, 5, 5);

            btnAdd.Click += BtnEdit_Click;
            Grid.SetRow(btnAdd, fieldsInput.Count + propsInput.Count);
            Grid.SetColumn(btnAdd, 1);
            dynamicGrid.Children.Add(btnAdd);

            this.Content = dynamicGrid;
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e) {
            object obj = plugin.CreateObject();
            FieldInfo[] field = plugin.GetFields();
            PropertyInfo[] property = plugin.GetProperties();
            try {
                for (int i = 0; i < field.Length; i++) {
                    if (field[i].FieldType == typeof(String)) {
                        field[i].SetValue(obj, inputField[i].Text);
                    } else if (field[i].FieldType == typeof(Int32)) {
                        field[i].SetValue(obj, Int32.Parse(inputField[i].Text));
                    } else if (field[i].FieldType == typeof(Double)) {
                        field[i].SetValue(obj, Double.Parse(inputField[i].Text));
                    }
                }
                for (int i = 0; i < property.Length; i++) {
                    if (property[i].PropertyType == typeof(String)) {
                        property[i].SetValue(obj, inputProperty[i].Text);
                    } else if (property[i].PropertyType == typeof(Int32)) {
                        property[i].SetValue(obj, Int32.Parse(inputProperty[i].Text));
                    } else if (property[i].PropertyType == typeof(Double)) {
                        property[i].SetValue(obj, Double.Parse(inputProperty[i].Text));
                    }
                }
            } catch {
                MessageBox.Show("Invalid input");
                return;
            }

            Model.objectList[index] = obj;
            super.RefreshObjectList();
            MessageBox.Show("Successfully edit");
        }

        private void DisplayItem() {
            object obj = Model.objectList[index];
            FieldInfo[] field = plugin.GetFields();
            PropertyInfo[] property = plugin.GetProperties();

            for (int i = 0; i < field.Length; i++) {
                inputField[i].Text = field[i].GetValue(obj).ToString();
            }
            for (int i = 0; i < property.Length; i++) {
                inputProperty[i].Text = property[i].GetValue(obj).ToString();
            }
        }
    }
}
