using System.ComponentModel.Composition;
using PluginContracts;
using System;
using System.Reflection;
using System.Security.Policy;
using System.IO;
using System.Security;

namespace SecondPlugin
{
    [Export(typeof(IPlugin))]
    public class SecondPlugin : IPlugin
    {
        private int Click { get; set; }

        public SecondPlugin()
        {
            Click = 0;
        }

        #region IPlugin Members

        public string Name
        {
            get
            {
                return "Second Plugin";
            }
        }

        public void Do()
        {
           
            System.Windows.MessageBox.Show($"Do Something in Second Plugin 2 - {Click++}x");
        }

        #endregion
    }
}
