using BLL.Models;
using BLL.Services;
using ChatClientWPF.dto;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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

namespace SushiUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SushiService _sushiService;
        string base64Image;

        public MainWindow()
        {
            InitializeComponent();
            _sushiService = new SushiService();
        }

        private void Add()
        {
            _sushiService.Create(new SushiDTO()
            {
                Name = "SushiTest",
                Description = "descriptipn test",
                MainPhoto = base64Image,
                Price = 300
            });
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.ShowDialog();
            string filePath = dialog.FileName;
            byte[] imageBytes = File.ReadAllBytes(filePath);

            base64Image = Convert.ToBase64String(imageBytes);

            base64Image = UploadImage(base64Image);

            Add();
        }

        private string UploadImage(string base64)
        {
            string server = "https://bv012.novakvova.com";
            UploadImagesDTO upload = new UploadImagesDTO();
            upload.Photo = base64Image;
            string json = JsonConvert.SerializeObject(upload);
            byte[] bytes = Encoding.UTF8.GetBytes(json);

            WebRequest request = WebRequest.Create($"{server}/api/account/upload");
            request.Method = "POST";
            request.ContentType = "application/json";
            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(bytes, 0, bytes.Length);

            }

            try
            {
                var response = request.GetResponse();
                using (var stream = new StreamReader(response.GetResponseStream()))
                {
                    string data = stream.ReadToEnd();
                    var resp = JsonConvert.DeserializeObject<UploadImgResponseDTO>(data);
                    return resp.Image;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error");
            }

            return null;
        }

    }
}
