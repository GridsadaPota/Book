using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json.Serialization;
using WebBook.Models;

namespace WebBook.Controllers
{
    public class BookController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl;

        public BookController(HttpClient client, IConfiguration configuration)
        {
            _httpClient = client;
            _apiBaseUrl = configuration.GetValue<string>("WebApiUrl");
        }


        public IActionResult Index()
        {
            //BookModel bookModel = new BookModel();
            //bookModel.Book_Id = 1111;
            //bookModel.Book_Code = "C001";
            //bookModel.Book_Name = "Bank";

            //BookModel bookModel2 = new BookModel();
            //bookModel2.Book_Id = 2222;
            //bookModel2.Book_Code = "C002";
            //bookModel2.Book_Name = "Bank2";

            //List<BookModel>list = new List<BookModel>();          
            //list.Add(bookModel);
            //list.Add(bookModel2);

            //var jsonData = GetAll();
            //var list = JsonConvert.DeserializeObject<List<BookModel>>(jsonData.);
            //return View(list);
            List<BookViewModel> model = null;
            string url = _apiBaseUrl + "api/Book/GetAll";
            var task = _httpClient.GetAsync(url)
              .ContinueWith((taskwithresponse) =>
              {
                  var response = taskwithresponse.Result;
                  var jsonString = response.Content.ReadAsStringAsync();
                  jsonString.Wait();
                  model = JsonConvert.DeserializeObject<List<BookViewModel>>(jsonString.Result);

              });
            task.Wait();
            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(BookModel bookModel)
        {
            if (ModelState.IsValid)
            {
                bookModel.Publish_Date = DateTime.Now;
                string url = _apiBaseUrl + "api/Book/Add";
                StringContent content = new StringContent(JsonConvert.SerializeObject(bookModel), Encoding.UTF8, "application/json");
                var respone = _httpClient.PostAsync(url, content);
                return RedirectToAction("Index");
            }
            return View(bookModel);
        }


        public IActionResult Detail(string code)
        {
            string url = _apiBaseUrl + "api/Book/GetBook?code=" + code ;
            var response = _httpClient.GetAsync(url).Result;
            string jsonString = response.Content.ReadAsStringAsync().Result;
            BookViewModel bookViewModel = JsonConvert.DeserializeObject<BookViewModel>(jsonString);
            return View(bookViewModel);
        }

        public IActionResult Edit(string code)
        {
            string url = _apiBaseUrl + "api/Book/GetBook?code=" + code;
            var response = _httpClient.GetAsync(url).Result;
            string jsonString = response.Content.ReadAsStringAsync().Result;
            BookViewModel bookViewModel = JsonConvert.DeserializeObject<BookViewModel>(jsonString);
            BookModel bookModel = new BookModel()
            {
                Book_Id = bookViewModel.Book_Id,
                Book_Code = bookViewModel.Book_Code,
                Book_Name = bookViewModel.Book_Name,
                Book_Author = bookViewModel.Book_Author,
                Price = bookViewModel.Price,
                Publish_Date = bookViewModel.Publish_Date
                
            };

            return View(bookModel);
        }

      

        public IActionResult Delete(string code)
        {
            string url = _apiBaseUrl + "api/Book/Delete?code=" + code;
            var response = _httpClient.DeleteAsync(url);
            return RedirectToAction("Index");

        }

        public async Task<JsonResult> GetAll()
        {
            string url = _apiBaseUrl + "api/Book/GetAll";
            var response = await _httpClient.GetAsync(url);
            var content = await response.Content.ReadFromJsonAsync<List<BookViewModel>>();
            return Json(content);
        }



    }
}
