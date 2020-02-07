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

namespace ExpertQuestionnaire.GUI.View
{
    /// <summary>
    /// Interaction logic for QuestionnaireEditView.xaml
    /// </summary>
    public partial class QuestionnaireEditView : Window
    {
        public QuestionnaireEditView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SelectCurrentItem(object sender, MouseButtonEventArgs e)
        {
            {
                ListViewItem item = (ListViewItem)sender;
                item.IsSelected = true;
            }

            for (int i = 0; i < ListItems.Items.Count; i++)
            {
                var control = ListItems.ItemContainerGenerator.ContainerFromIndex(i);

                if (control != sender)
                {
                    var item = control as ListViewItem;

                    item.IsSelected = false;
                }
            }
        }
    }
}
