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
using System.Windows.Shapes;

namespace CamShar.View
{
    /// <summary>
    /// Setting.xaml 的交互逻辑
    /// </summary>
    public partial class Setting : Window
    {
        public Setting()
        {
            InitializeComponent();
            
        }
        //关闭设置页面
        private void CloseSettingWin(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }
        //拖动设置窗口
        private void DrapSettingWin(object sender, MouseButtonEventArgs e)
        {
            if (e.MouseDevice.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void ChangeDownloadPath(object sender, RoutedEventArgs e)
        {
            //打开文件夹，选择保存路径
            string path = string.Empty;
            System.Windows.Forms.FolderBrowserDialog fbd = new System.Windows.Forms.FolderBrowserDialog();
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                path = fbd.SelectedPath;
            }
            downloadFilePath.Text = path + @"\";
        }

        //保存设置
        private void SaveSetting(object sender, RoutedEventArgs e)
        {
            ConfigurationManager.AppSettings.Set("SavePath", downloadFilePath.Text);
            ConfigurationManager.AppSettings.Set("SearchNum", numSerach.Text);
            MessageBox.Show("保存成功");
        }

        //修改下载位置
        private void SettingLoad(object sender, RoutedEventArgs e)
        {
            //修改配置文件，保存设置的默认路径
            downloadFilePath.Text = ConfigurationManager.AppSettings["SavePath"];

        }
    }
}
