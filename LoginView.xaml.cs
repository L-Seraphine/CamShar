using CamShar.DataBaseCon;
using CamShar.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CamShar.View
{
    /// <summary>
    /// LoginView.xaml 的交互逻辑
    /// </summary>
    public partial class LoginView : Window
    {
        public LoginView()
        {
            InitializeComponent();
        }

        //拖动窗体
        private void DropWin(object sender, MouseButtonEventArgs e)
        {
            if(e.MouseDevice.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        //关闭窗体
        private void WinClose(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //登录软件
        private void LoginCheck(object sender, RoutedEventArgs e)
        {
            errorFeedback.Text = "";
            string userName1 = userName.Text;
            string password1 = password.Password;
            //加载动画
            Storyboard storyboardIn = (Storyboard)this.FindResource("moveInLogin");
            Storyboard storyboardOut = (Storyboard)this.FindResource("moveOutLogin");
            LoginViewModel loginViewModel = new LoginViewModel();
            //loginViewModel.UserLogin(userName, password, this, errorFeedback);
            if(userName.Text==""||password.Password == "")
            {
                errorFeedback.Text = "请输入账户或者密码";

            }
            else
            {
                this.BeginStoryboard(storyboardIn);
                Task task = new Task(() =>
                {
                    loginViewModel.UserLogin(userName1, password1, this, errorFeedback, storyboardOut);
                });
                task.Start();
            }

        }

        //新用户注册动画
        private void Register(object sender, RoutedEventArgs e)
        {
            Storyboard register = (Storyboard)this.FindResource("register");
            this.BeginStoryboard(register);
        }
        //新用户注册逻辑
        private void RegisterCheck(object sender, RoutedEventArgs e)
        {
            r_ErrorCallBack.Text = "";
            if (r_UserName.Text == "" || r_Password.Password == "" || r_TwicePassword.Password == "")
            {
                r_ErrorCallBack.Text = "请输入完整的内容";
            }
            else
            {
                if (r_Password.Password == r_TwicePassword.Password)
                {
                    if ((bool)r_XieYi.IsChecked)
                    {
                        registerLoading.Visibility = Visibility.Visible;
                        RegisterViewModel registerViewModel = new RegisterViewModel();
                        Thread register = new Thread(() =>
                        {
                                int isRegistered = 0;
                                isRegistered = registerViewModel.RegisterNewUser(
                                    Dispatcher.Invoke(() => {
                                        return r_UserName.Text;
                                    })
                                ,
                                    Dispatcher.Invoke(() => {
                                        return r_Password.Password;
                                    })
                                    );
                                if (isRegistered == 1)
                                {
                                    MessageBox.Show("注册成功");
                                    //重置注册信息
                                    Dispatcher.Invoke(() =>
                                    {
                                        r_Password.Password = "";
                                        r_UserName.Text = "";
                                        r_TwicePassword.Password = "";
                                        r_XieYi.IsChecked = false;
                                        OutRegister(this, e);
                                        registerLoading.Visibility = Visibility.Hidden;
                                    });
                                }
                                else
                                {
                                    Dispatcher.Invoke(() => {
                                        r_ErrorCallBack.Text = "该用户名已存在";
                                        registerLoading.Visibility = Visibility.Hidden;
                                    });
                                }

                        });
                        register.Start();
                    }
                    else
                    {
                        r_ErrorCallBack.Text = "请先阅读并同意用户协议";
                    }
                    
                }
                else
                {
                    r_ErrorCallBack.Text = "两次密码不一致";
                }
            }
        }
        //退出注册
        private void OutRegister(object sender, RoutedEventArgs e)
        {
            Storyboard register = (Storyboard)this.FindResource("registerOut");
            this.BeginStoryboard(register);
        }

        //窗体加载的时候
        private void Loading(object sender, RoutedEventArgs e)
        {
            //errorFeedback.Text = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)+ @"\Downloads"; 
            //errorFeedback.Text = System.IO.Path.GetFileName("https://wordpress-serverless-code-ap-beijing-1303234197.cos.ap-beijing.myqcloud.com/COS/1110615.png");
        }
    }
}
