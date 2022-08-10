using Netphonebook.Modules.Records.ViewModels;
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

namespace Netphonebook.Modules.Records.Views
{
    /// <summary>
    /// Logika interakcji dla klasy ListEntryEditor.xaml
    /// </summary>
    public partial class ListEntryEditor : UserControl
    {
        public ListEntryEditorViewModel ViewModel;
        public ListEntryEditor(ListEntryEditorViewModel viewModel)
        {
            ViewModel = viewModel;
            this.DataContext = viewModel;
            InitializeComponent();
        }
    }
}
