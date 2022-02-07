using CamShar.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
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

namespace CamShar.View
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        //获取页面单例
        MainPage mainPage;
        MyFile myFile;
        FindFile findFile;
        Setting setting;
        DownloadPageView downloadPageView;


        //初始窗体
        public MainWindow(string userName)
        {
            LoginModel.SetUerName(userName);
            InitializeComponent();
            SerachPart.Visibility = Visibility.Hidden;

            //初始化页面
            mainPage = SingletonPage.GetInstanc1();
            MainF.Content = mainPage;
            UserName.Text = "你好! "+LoginModel.GetUserName();
            //写入配置文件默认下载路径
            ConfigurationManager.AppSettings.Set("SearchNum","10");
            ConfigurationManager.AppSettings.Set("SavePath", Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\Downloads\");
        }

        //拖动窗口
        private void MoveWindows(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        //关闭窗口
        private void CloseMainWindow(object sender, RoutedEventArgs e)
        {
            if(setting != null)
            {
                if (setting.IsLoaded)
                {
                    setting.Close();
                }
            }
            this.Close();
            Environment.Exit(0);
        }

        //最小化窗口
        private void MinMainWindow(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        //最大化窗口
        private void MaxMainWindow(object sender, RoutedEventArgs e)
        {
            if (this.WindowState != WindowState.Maximized)
            {
                this.WindowState = WindowState.Maximized;
                this.MaxHeight = SystemParameters.WorkArea.Height;
                this.MaxWidth = SystemParameters.WorkArea.Width;
            }
            else
            {
                this.WindowState = WindowState.Normal;

            }
        }

        //我的文件页面
        private void ChangeMyFilePage(object sender, RoutedEventArgs e)
        {
            
            myFile = SingletonPage.GetInstanc2();
            myFile.Refresh();
            MainF.Content = myFile;
            SerachPart.Visibility = Visibility.Hidden;
        }

        //首页
        private void MainPageChanged(object sender, RoutedEventArgs e)
        {
            if (mainPage == null)
            {
                SerachPart.Visibility = Visibility.Hidden;
            }
            else
            {
                MainF.Content = mainPage;
                SerachPart.Visibility = Visibility.Hidden;
            }
            
        }

        //查找资源页面
        private void FindFileChanged(object sender, RoutedEventArgs e)
        {
            findFile = SingletonPage.GetInstanc3();
            findFile.Refresh();
            MainF.Content = findFile;
            SerachPart.Visibility = Visibility.Visible;
        }

        //打开设置页面
        private void SettingWindow(object sender, RoutedEventArgs e)
        {
            setting = SingletonPage.GetInstanc4();
            setting.Show();
        }

        //显示下载记录页
        private void DownLoadPage(object sender, RoutedEventArgs e)
        {
            downloadPageView = SingletonPage.GetInstanc5();
            MainF.Content= downloadPageView;
        }

        //搜索指定文件
        private void SerachFile(object sender, RoutedEventArgs e)
        {
            if (fileContent.Text == "")
            {
                MessageBox.Show("请输入搜索内容");
            }
            else
            {
                //模糊搜索
                findFile.Refresh(fileContent.Text);
            }
        }
    }
}
