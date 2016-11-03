using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using PluginContracts;
using System;

namespace MEFPlugin
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		Dictionary<string, IPlugin> _Plugins;

		public MainWindow()
        {
            InitializeComponent();

            IEnumerable<IPlugin> plugins = Compose();
           // foreach (var item in plugins)
            //{
              //  _Plugins.Add(item.Name, item);

                //Button b = new Button();
                //b.Content = item.Name;
                //b.Click += b_Click;
                //PluginGrid.Children.Add(b);
            //}

        }

        private IEnumerable<IPlugin> Compose()
        {
            //MEFPluginLoader loader = new MEFPluginLoader("Plugins");
            GenericMEFPluginLoader<IPlugin> loader = new GenericMEFPluginLoader<IPlugin>(@"Plugins");
            _Plugins = new Dictionary<string, IPlugin>();
            IEnumerable<IPlugin> plugins = loader.Plugins;
            _Plugins.Clear();
            foreach (var item in plugins)
            {
                _Plugins.Add(item.Name, item);
            }
                return plugins;
        }

        private void b_Click(object sender, RoutedEventArgs e)
		{
			Button b = sender as Button;
			if(b != null)
			{
				string key = b.Content.ToString();
				if(_Plugins.ContainsKey(key))
				{
					IPlugin plugin = _Plugins[key];
					plugin.Do();
				}
			}
		}

        private void butRecompose_Click(object sender, RoutedEventArgs e)
        {
            var pls = Compose();
            foreach (var item in pls)
            {
                item.Do();
            }
        }

        private void butCompose_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in _Plugins)
            {
                item.Value.Do();
            }

            
        }
    }
}
