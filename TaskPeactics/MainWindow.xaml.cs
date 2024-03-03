using System.Collections.ObjectModel;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TaskPeactics
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Comment> Comments { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            Comments = new ObservableCollection<Comment>();
            DataContext = this;
        }

        private async void getclick(object sender, RoutedEventArgs e)
        {
            List<Comment> comments = await DeserializeJsonApi();
            Comments.Clear(); 
            foreach (var comment in comments)
            {
                Comments.Add(comment);
            }
        }

        public async Task<String> GetDataFromJsonApi()
        {
            HttpClient client = new HttpClient();

            string data = await client.GetStringAsync(urltxt.Text);
            return data;
        }

      
      public async Task<List<Comment>> DeserializeJsonApi()
        {
            string filepath = "../../../comments.json";
            List<Comment> comments;
            await Task.Delay(3000);

            if (!File.Exists(filepath))
            {
                string jsonstr = await GetDataFromJsonApi(); 
                comments = JsonSerializer.Deserialize<List<Comment>>(jsonstr);

              
                await File.WriteAllTextAsync(filepath, jsonstr);
            }
            else
            {
                string jsonstr = await File.ReadAllTextAsync(filepath);
                comments = JsonSerializer.Deserialize<List<Comment>>(jsonstr);
            }

            return comments;
        }


    }
}