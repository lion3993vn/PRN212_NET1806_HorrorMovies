using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Sheets.v4;
using Google.Apis.Util.Store;
using Repositories.Entities;
using Repositories.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class ExportCsvService
    {
        private readonly MoviesRepo _movieRepo;
        private readonly UserRepo _userRepo;
        private readonly CriticRepo _criticRepo;

        public ExportCsvService()
        {
            _movieRepo = new MoviesRepo();
            _userRepo = new UserRepo();
            _criticRepo = new CriticRepo();
        }

        public async Task WriteToGoogleSheet(List<IList<object>> data)
        {
            string[] Scopes = { SheetsService.Scope.Spreadsheets };
            string ApplicationName = "Google Sheets API .NET Quickstart";

            UserCredential credential;

            using (var stream = new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = "token.json";
                credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.FromStream(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true));
            }

            var service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            var spreadsheetId = "1gVMy4SoWmYTF7mhGYtlh04X2c9JtJjGDSVLs-lWY9Bg";
            var sheetName = "Sheet1";

            // Clear existing data in the sheet by specifying a wide range
            var clearRange = $"{sheetName}!A1:Z1000";  // Adjust the range as needed to cover all your data
            var clearRequest = service.Spreadsheets.Values.Clear(new ClearValuesRequest(), spreadsheetId, clearRange);
            var clearResponse = await clearRequest.ExecuteAsync();

            // Update with new data
            var range = $"{sheetName}!A1";
            var valueRange = new ValueRange
            {
                Values = data
            };

            var updateRequest = service.Spreadsheets.Values.Update(valueRange, spreadsheetId, range);
            updateRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.RAW;
            var updateResponse = await updateRequest.ExecuteAsync();
        }


        public static List<IList<object>> GetMovieData(List<Movie> m)
        {
            var data = new List<IList<object>>();
            data.Add(new List<object> { "MovieId", "MovieTitle", "MovieYear", "MovieUrl", "MovieRank" });
            foreach (var movie in m)
            {
                data.Add(new List<object> { movie.MovieId, movie.MovieTitle, movie.MovieYear, movie.MovieUrl, movie.MovieRank });
            }
            return data;
        }

        public async Task ExportMoviesAsync()
        {
            var list = _movieRepo.GetAll();
            await WriteToGoogleSheet(GetMovieData(list));
        }

        public static List<IList<object>> GetUserReviewData(List<UserReview> u)
        {
            var data = new List<IList<object>>();
            data.Add(new List<object> { "UserId", "Rating", "IsSuperReviewer", "HasSpoilers", "HasProfanity", "CreationDate", "MovieId" });
            foreach (var userReview in u)
            {
                data.Add(new List<object> { userReview.UserId, userReview.Rating, userReview.IsSuperReviewer, userReview.HasSpoilers, userReview.HasProfanity, userReview.CreationDate, userReview.MovieId });
            }
            return data;
        }

        public async Task ExportUserReviewsAsync()
        {
            var list = _userRepo.GetAll();
            await WriteToGoogleSheet(GetUserReviewData(list));
        }

        public static List<IList<object>> GetCriticData(List<CriticReview> u)
        {
            var data = new List<IList<object>>();
            data.Add(new List<object> { "ReviewId", "CreationDate", "CriticName", "IsTopCritic", "PublicationName", "ReviewUrl", "Quote", "scoreSentiment", "MovieId" });
            foreach (var critic in u)
            {
                data.Add(new List<object> { critic.ReviewId, critic.CreationDate, critic.CriticName, critic.IsTopCritic, critic.PublicationName, critic.ReviewUrl, critic.Quote, critic.ScoreSentiment, critic.MovieId });
            }
            return data;
        }

        public async Task ExportCriticReviewssAsync()
        {
            var list = _criticRepo.GetAll();
            await WriteToGoogleSheet(GetCriticData(list));
        }

        public async Task ExportDataAsync(string name)
        {
            if (name == "Movie")
            {
                await ExportMoviesAsync();
            }
            else if (name == "User Review")
            {
                await ExportUserReviewsAsync();
            }
            else
            {
                await ExportCriticReviewssAsync();
            }

        }
    }
}
