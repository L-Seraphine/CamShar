using CamShar.Common;
using CamShar.DataBaseCon;
using CamShar.Model;
using CamShar.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace CamShar.View
{
    /// <summary>
    /// MyFile.xaml 的交互逻辑
    /// </summary>
    public partial class MyFile : Page
    {
        private FileDetailModel fileDetailModel;
        public MyFile()
        {
            InitializeComponent();
            Refresh();
        }

        private void GetFilePath(object sender, DragEventArgs e)
        {
            //获取拖动文件的信息
            string fullPath = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
            FileInfo fileInfo = new FileInfo(fullPath);
            //获取文件路径
            m_FilePath.Text = fullPath;
            //获取文件名称
            //m_FileName.Text = Path.GetFileNameWithoutExtension(fullPath);
            m_FileName.Text = Path.GetFileName(fullPath);
            //获取文件大小
            m_FileSize.Text = Convert.ToString(System.Math.Ceiling(fileInfo.Length / 1024.0))+" KB";


            fileDetailModel.UpLoadUser = LoginModel.GetUserName();
            //fileDetailModel.UpLoadTime = DateTime.Now.ToLocalTime();
            fileDetailModel.FileName = Path.GetFileNameWithoutExtension(fullPath);
            fileDetailModel.FileSize = Convert.ToString(System.Math.Ceiling(fileInfo.Length / 1021.0)) + " KB";
            fileDetailModel.FileType = Path.GetExtension(fullPath);
            fileDetailModel.DownLoadURL = "https://wordpress-serverless-code-ap-beijing-1303234197.cos.ap-beijing.myqcloud.com/COS/" + new UrlEncode().UrlEncodeTurn(Path.GetFileNameWithoutExtension(fullPath))+ Path.GetExtension(fullPath);
        }
        //上传文件到对象存储
        private void UpLoadFile(object sender, RoutedEventArgs e)
        {
            //实例化COS对象
            QCloundCos qCloundCos = new QCloundCos();
            //主线程上传动画
            UpLoadingAnimation.Visibility = Visibility.Visible;
            //开始子线程上传
            _ = qCloundCos.UpLoadFileAsync(m_FilePath.Text, m_FileName.Text, UpLoadingCallBack, UpLoadingAnimation,fileDetailModel);


        }

        //刷新
        public void Refresh()
        {
            myFileListView.Visibility = Visibility.Hidden;
            loadingAnimation.Visibility = Visibility.Visible;
            //新线程查询文件
            Thread thread = new Thread(() =>
            {
                MyFileViewModel myFileViewModel = new MyFileViewModel();
	//为了演示动画，暂时线程休眠2秒
                Thread.Sleep(2000);
                Dispatcher.Invoke(() =>
                {
                    this.DataContext = myFileViewModel;
                    loadingAnimation.Visibility = Visibility.Hidden;
                    myFileListView.Visibility = Visibility.Visible;
                });
            });
            thread.Start();

            //this.DataContext = new MyFileViewModel();
            fileDetailModel = new FileDetailModel();
        }
    }
}
