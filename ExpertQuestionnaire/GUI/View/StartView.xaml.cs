using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
    /// Interaction logic for StartView.xaml
    /// </summary>
    public partial class StartView : Window
    {
        public StartView()
        {
            InitializeComponent();
        }

        private void BtClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private static MethodInfo GetMethod(Type type, string methodName)
        { return type.GetMethod(methodName, BindingFlags.Instance | BindingFlags.NonPublic); }

        private MethodInfo GetMethod(string methodName)
        {
            if (DataContext == null)
            { return null; }
            else
            { return GetMethod(DataContext.GetType(), methodName); }
        }

        private void SetPassword(string password, string passwordMethod)
        {
            var setMethod = GetMethod(passwordMethod);

            if (setMethod != null && setMethod.ReturnType == typeof(void) && setMethod.GetParameters().Length == 1 && setMethod.GetParameters()[0].ParameterType == typeof(string))
            { setMethod.Invoke(DataContext, new object[] { password }); }
        }

        private string GetPassword(string passwordMethod)
        {
            var setMethod = GetMethod(passwordMethod);

            if (setMethod != null && setMethod.ReturnType == typeof(string) && setMethod.GetParameters().Length == 0)
            {
                return (string)setMethod.Invoke(DataContext, null);
            }

            return "";
        }

        private string GetUserPassword()
        { return GetPassword("GetUserPassword"); }

        private void SetUserPassword(string password)
        { SetPassword(password, "SetUserPassword"); }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            pbPassword.Password = GetUserPassword();
        }

        private void pbUserPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            SetUserPassword(pbPassword.Password);
        }

        private void CbLogin_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
