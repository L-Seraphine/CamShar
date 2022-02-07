using CamShar.Common;
using CamShar.Model;
using CamShar.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CamShar.View
{
    /// <summary>
    /// FindFile.xaml 的交互逻辑
    /// </summary>
    public partial class FindFile : Page
    {
        public FindFile()
        {
            InitializeComponent();
            Refresh();
        }

        //下载文件
        private void DownLoadFile(object sender, RoutedEventArgs e)
        {
            (sender as Button).Content = "下载中";
            (sender as Button).Foreground = new SolidColorBrush(Colors.Red);
            var c = ((sender as Button).DataContext as FileDetailModel);
            Thread thread = new Thread(()=> {
                QCloundCos qCloundCos = new QCloundCos();
                qCloundCos.DownLoadFileAsync(c.DownLoadURL, ConfigurationManager.AppSettings["SavePath"]+ c.FileName+c.FileType);
                Dispatcher.Invoke(() => {
                    MessageBox.Show("下载完成");
                    (sender as Button).Content = "下载";
                    (sender as Button).Foreground = new SolidColorBrush(Colors.Black);
                });
            });
            thread.Start();
            
        }

        //刷新页面
        public void Refresh()
        {
            datalistView.Visibility = Visibility.Hidden;
            loadingAni.Visibility = Visibility.Visible;
            loadingAni.Play();
            Thread thread = new Thread(() =>
            {
                FindFileViewModel findFileViewModel = new FindFileViewModel();
                Dispatcher.Invoke(() =>
                {
                    this.DataContext = findFileViewModel;
                    loadingAni.Visibility = Visibility.Hidden;
                    loadingAni.Pause();
                    datalistView.Visibility = Visibility.Visible;
                });

            });
            thread.Start();
        }

        //重载刷新页面
        public void Refresh(string serachcon)
        {
            datalistView.Visibility = Visibility.Hidden;
            loadingAni.Visibility = Visibility.Visible;
            loadingAni.Play();
            Thread thread = new Thread(() =>
            {
                FindFileViewModel findFileViewModel = new FindFileViewModel(serachcon);
                Dispatcher.Invoke(() =>
                {
                    this.DataContext = findFileViewModel;
                    loadingAni.Visibility = Visibility.Hidden;
                    loadingAni.Pause();
                    datalistView.Visibility = Visibility.Visible;
                });

            });
            thread.Start();
        }
    }
}
