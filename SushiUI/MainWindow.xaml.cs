using BLL.Models;
using BLL.Services;
using ChatClientWPF.dto;
using DAL.Models;
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
        string base64Image1;
        string base64Image2;
        string base64Image3;
        private IList<SushiDTO> _sushis;

        public MainWindow()
        {
            InitializeComponent();
            _sushiService = new SushiService();
            _sushis = _sushiService.GetAll();

            foreach(var item in _sushis)
            {
                CreateGrid(item);
            }
        }

        private void CreateGrid(SushiDTO sushi)
        {

            Grid container = new Grid() { Background = Brushes.LightPink, Width=1230};
            container.ColumnDefinitions.Add(new ColumnDefinition());
            container.ColumnDefinitions.Add(new ColumnDefinition());
            container.ColumnDefinitions.Add(new ColumnDefinition());
            container.ColumnDefinitions.Add(new ColumnDefinition());
            container.ColumnDefinitions.Add(new ColumnDefinition());
            container.ColumnDefinitions.Add(new ColumnDefinition());
            container.ColumnDefinitions.Add(new ColumnDefinition());
            container.ColumnDefinitions.Add(new ColumnDefinition());
            container.ColumnDefinitions.Add(new ColumnDefinition());
            container.RowDefinitions.Add(new RowDefinition());
            container.RowDefinitions.Add(new RowDefinition());
            container.RowDefinitions.Add(new RowDefinition());
            container.RowDefinitions.Add(new RowDefinition());




            BitmapImage bmp = new BitmapImage();
            string someUrl = $"https://bv012.novakvova.com{sushi.MainPhoto}";
            using (var webClient = new WebClient())
            {
                byte[] imageBytes = webClient.DownloadData(someUrl);
                bmp = ToBitmapImage(imageBytes);
                
            }
            var image = new Image() { Source = bmp,Width = 250, Height = 220, Margin = new Thickness(0) };
            Grid.SetRow(image, 1);
            Grid.SetRowSpan(image, 2);
            Grid.SetColumn(image, 1);
            Grid.SetColumnSpan(image, 2);

            BitmapImage bmp2 = new BitmapImage();
            string someUrl2 = $"https://bv012.novakvova.com{sushi.Photo2}";
            using (var webClient = new WebClient())
            {
                byte[] imageBytes2 = webClient.DownloadData(someUrl2);
                bmp2 = ToBitmapImage(imageBytes2);

            }
            var image2 = new Image() { Source = bmp2, Width = 120, Height = 120, Margin = new Thickness(0) };
            Grid.SetRow(image2, 3);
            Grid.SetColumn(image2, 1);

            BitmapImage bmp3 = new BitmapImage();
            string someUrl3 = $"https://bv012.novakvova.com{sushi.Photo3}";
            using (var webClient = new WebClient())
            {
                byte[] imageBytes3 = webClient.DownloadData(someUrl3);
                bmp3 = ToBitmapImage(imageBytes3);

            }
            var image3 = new Image() { Source = bmp3, Width = 120, Height = 120, Margin = new Thickness(0) };
            Grid.SetRow(image3, 3);
            Grid.SetColumn(image3, 2);

            var textName = new TextBlock() { Text = sushi.Name, VerticalAlignment = VerticalAlignment.Center, FontSize=18,
                Margin = new Thickness(5, 0, 0, 0), FontWeight= FontWeights.Bold };
            Grid.SetRow(textName, 1);
            Grid.SetColumn(textName, 3);
            Grid.SetColumnSpan(textName, 5);
      

            var textDescr = new TextBlock() { Text = sushi.Description, VerticalAlignment = VerticalAlignment.Center, Margin = new Thickness(5, 0, 0, 0) };
            Grid.SetRow(textDescr, 2);
            Grid.SetColumn(textDescr, 3);
            Grid.SetColumnSpan(textDescr, 6);

            var textPrice = new TextBlock() { Text = sushi.Price.ToString() + " грн", VerticalAlignment = VerticalAlignment.Center, FontSize = 20,
                Margin = new Thickness(5, 0, 0, 0) }; ;
            Grid.SetRow(textPrice, 1);
            Grid.SetColumn(textPrice, 7);


            container.Children.Add(image);
            container.Children.Add(image2);
            container.Children.Add(image3);
            container.Children.Add(textName);
            container.Children.Add(textDescr);
            container.Children.Add(textPrice);

            listbox.Items.Add(container);
        }



        public static BitmapImage ToBitmapImage(byte[] data)
        {
            using (MemoryStream ms = new MemoryStream(data))
            {

                BitmapImage img = new BitmapImage();
                img.BeginInit();
                img.CacheOption = BitmapCacheOption.OnLoad;
                img.StreamSource = ms;
                img.EndInit();

                if (img.CanFreeze)
                {
                    img.Freeze();
                }
                return img;
            }
        }


        private string UploadImage(string base64)
        {
            string server = "https://bv012.novakvova.com";
            UploadImagesDTO upload = new UploadImagesDTO();
            upload.Photo = base64;
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

        private void addImg1_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.ShowDialog();
            string filePath = dialog.FileName;
            byte[] imageBytes = File.ReadAllBytes(filePath);
            base64Image1 = Convert.ToBase64String(imageBytes);
            base64Image1 = UploadImage(base64Image1);
            addImg1.IsEnabled = false;
        }
        private void addImg2_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.ShowDialog();
            string filePath = dialog.FileName;
            byte[] imageBytes = File.ReadAllBytes(filePath);
            base64Image2 = Convert.ToBase64String(imageBytes);
            base64Image2 = UploadImage(base64Image2);
            addImg2.IsEnabled = false;
        }

        private void addImg3_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.ShowDialog();
            string filePath = dialog.FileName;
            byte[] imageBytes = File.ReadAllBytes(filePath);
            base64Image3 = Convert.ToBase64String(imageBytes);
            base64Image3 = UploadImage(base64Image3);
            addImg3.IsEnabled = false;
        }

        private void addItemBtn_Click(object sender, RoutedEventArgs e)
        {
            _sushiService.Create(new SushiDTO()
            {
                Name = name_tb.Text,
                Description = descr_tb.Text,
                MainPhoto = base64Image1,
                Photo2 = base64Image2,
                Photo3 = base64Image3,
                Price = Int32.Parse(price_tb.Text)
            });

            name_tb.Text = "";
            descr_tb.Text = "";
            price_tb.Text = "";

            addImg1.IsEnabled = true;
            addImg2.IsEnabled = true;
            addImg3.IsEnabled = true;

            listbox.Items.Clear();
            foreach (var item in _sushis)
            {
                CreateGrid(item);
            }
        }
    }
}
