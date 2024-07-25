using Repositories.Entities;
using Services.Services;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
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
        private readonly MovieService _movieService;
        private Movie _selected = null;

        public MainWindow()
        {
            InitializeComponent();
            _addCsvService = new AddCSVService();
            _exportService = new ExportCsvService();
            _movieService = new MovieService();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cbbTable.ItemsSource = Table();
            FillDataGridView();
        }

        public void FillDataGridView()
        {
            dgvMovie.ItemsSource = null;
            dgvMovie.ItemsSource = _movieService.GetAll();
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
                    System.Windows.MessageBox.Show("the data has been successfully written to google sheet!");
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

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFromYear.Text) && string.IsNullOrEmpty(txtToYear.Text))
            {
                FillDataGridView();
                return;
            }

            if (!int.TryParse(txtFromYear.Text, out int fromYear))
            {
                System.Windows.MessageBox.Show("Please enter a valid number for 'From Year' field.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                dgvMovie.ItemsSource = null;
                return;
            }

            if (!int.TryParse(txtToYear.Text, out int toYear))
            {
                System.Windows.MessageBox.Show("Please enter a valid number for 'To Year' field.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                dgvMovie.ItemsSource = null;
                return;
            }

            if (toYear < fromYear)
            {
                System.Windows.MessageBox.Show("'To Year' must be greater than 'From Year'.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                dgvMovie.ItemsSource = null;
                return;
            }

            dgvMovie.ItemsSource = null;
            dgvMovie.ItemsSource = _movieService.GetSearchYear(fromYear, toYear);
        }

        private void dgvMovie_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgvMovie.SelectedItems.Count > 0)
            {
                Movie _selected = dgvMovie.SelectedItems[0] as Movie;
            }
            else
            {
                _selected = null;
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            _selected = dgvMovie.SelectedItem as Movie;
            if (_selected == null)
            {
                System.Windows.MessageBox.Show("Please select a certain movie to delete!", "Select one movie", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            DialogResult answer = (DialogResult)System.Windows.MessageBox.Show("Do you really want to delete this movie", "Confirm delete?", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (answer == System.Windows.Forms.DialogResult.No)
                return;

            try
            {
                _movieService.DeleteMovie(_selected);
                FillDataGridView();
                _selected = null;
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("This movie cannot be deleted...!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}