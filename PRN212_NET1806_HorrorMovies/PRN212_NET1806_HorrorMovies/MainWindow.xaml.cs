using Services.Services;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PRN212_NET1806_HorrorMovies
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly AddCSVService _addCsvService;
        private readonly ExportCsvService _exportService;

        public MainWindow()
        {
            InitializeComponent();
            _addCsvService = new AddCSVService();
            _exportService = new ExportCsvService();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cbbTable.ItemsSource = Table();
        }

        private async void btnExport_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbbTable.SelectedItem == null)
                {
                    System.Windows.MessageBox.Show("Please choose value to export");
                }
                else
                {
                    var name = cbbTable.SelectedItem.ToString();

                    var item = _exportService.ExportDataAsync(name);
                    await item;
                    System.Windows.MessageBox.Show("the data has been successfully written to google sheet.");
                }

            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }

        private List<string> Table()
        {
            var list = new List<string> { "Movie", "User Review", "Critic Review" };
            return list;
        }

        private void dgvMovie_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}