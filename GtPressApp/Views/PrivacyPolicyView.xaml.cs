using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238
using GtPress.StoreApp.Common;

namespace GtPress.StoreApp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PrivacyPolicyView : LayoutAwarePage
    {
        public PrivacyPolicyView()
        {
            this.InitializeComponent();
            using (var sr = new StreamReader(this.GetType().GetTypeInfo().Assembly.GetManifestResourceStream("GtPress.StoreApp.Content.PrivacyPolicy.txt")))
            {
                PrivacyPolicyTextBlock.Text = sr.ReadToEnd();
            }
        }
    }
}
