using Repositories.Entities;
using Services.Model;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PRN212_NET1806_HorrorMovies
{
    /// <summary>
    /// Interaction logic for MoviesDetails.xaml
    /// </summary>
    public partial class MoviesDetails : Window
    {
        private readonly string _Id;
        private readonly MovieService _movieService;

        public MoviesDetails(string movieId)
        {
            InitializeComponent();
            _Id = movieId;
            _movieService = new MovieService();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MovieCheckModel model = new MovieCheckModel()
                {
                    MovieTitle = txtTitle.Text,
                    MovieRank = txtRank.Text,
                    MovieUrl = txtUrl.Text,
                    MovieYear = txtYear.Text,
                };

                var check = _movieService.ValidateInputMovie(model);

                if (check)
                {
                    Movie data = new Movie()
                    {
                        MovieTitle = model.MovieTitle,
                        MovieRank = int.Parse(model.MovieRank),
                        MovieUrl = model.MovieUrl,
                        MovieYear = int.Parse(model.MovieYear),
                    };

                    if (_Id == "create")
                    {
                        data.MovieId = _movieService.checkIdGenerate();
                        _movieService.AddMovie(data);
                        System.Windows.MessageBox.Show("Add Successfull", "Notification", MessageBoxButton.OK);
                    }
                    else
                    {
                        data.MovieId = txtMovieId.Text;
                        _movieService.UpdateMovie(data);
                        System.Windows.MessageBox.Show("Update Successfull", "Notification", MessageBoxButton.OK);
                    }

                    MainWindow mainWindow = new MainWindow();
                    this.Close();
                    mainWindow.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult result = (DialogResult) System.Windows.MessageBox.Show("Do you want to stop your Action", "Confirm Cancel", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == System.Windows.Forms.DialogResult.No)
            {
                return;
            }
            else
            {
                MainWindow mainWindow = new MainWindow();
                this.Close();
                mainWindow.ShowDialog();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtMovieId.IsEnabled = false;

            if (_Id != "create")
            { 
                LoadedMovie();
            }
            else
            {
                txtMovieId.Text = "Id auto generate";
            }

        }

        private void LoadedMovie()
        {
            var item = _movieService.GetById(_Id);

            txtMovieId.Text = item.MovieId;
            txtTitle.Text = item.MovieTitle;
            txtUrl.Text = item.MovieUrl;
            txtYear.Text = item.MovieYear.ToString();
            txtRank.Text = item.MovieRank.ToString();
        }
    }

}
