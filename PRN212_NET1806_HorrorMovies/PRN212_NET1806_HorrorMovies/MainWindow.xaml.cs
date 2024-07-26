using Repositories.Entities;
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

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            MoviesDetails detailWindow = new MoviesDetails("create");
            this.Close();
            detailWindow.ShowDialog();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgvMovie.SelectedItem == null)
                {
                    System.Windows.MessageBox.Show("Please Selected 1 column", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    var item = dgvMovie.SelectedItem as Movie;
                    MoviesDetails detailWindow = new MoviesDetails(item.MovieId);
                    this.Close();
                    detailWindow.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnImport_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string filename = "";
                // Create OpenFileDialog
                Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

                // Set filter for file extension and default file extension
                dlg.DefaultExt = ".csv";
                dlg.Filter = "Text documents (.csv)|*.csv";

                // Display OpenFileDialog by calling ShowDialog method
                Nullable<bool> result = dlg.ShowDialog();

                // Get the selected file name and display in a TextBox
                if (result == true)
                {
                    // Open document
                    filename = dlg.FileName;
                }

                _addCsvService.AddCSV(filename);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }
    }
}