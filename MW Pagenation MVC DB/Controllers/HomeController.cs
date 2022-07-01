using MW_Pagenation_MVC_DB.Model;
using MW_Pagenation_MVC_DB.Models;
using MW_Pagenation_MVC_DB.ModelView;
using MW_Pagenation_MVC_DB.Repository;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MW_Pagenation_MVC_DB.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public PageView _pageView { get; set; }

        public HomeController(ILogger<HomeController> logger, PageView pageView)
        {
            _logger = logger;
            _pageView = pageView;
        }

        [HttpGet]
        public IActionResult Index(int size = 5, bool sortbyName = false, bool sortbySurname = false, bool sortbyAge = false, int page = 1)
        {
            _pageView.Size = size;
            _pageView.Page = page;
            using (SqlConnection connection = AllDataContext.GetConnection())
            {
                connection.Open();
                string query = "SELECT COUNT(ID) FROM Pagenation";
                //SqlCommand command = new SqlCommand(query, connection);
                _pageView.Pages = (int)Math.Round((decimal)connection.QueryFirst<int>(query) / _pageView.Size, MidpointRounding.ToPositiveInfinity);
                query = $"SELECT * FROM Pagenation ORDER BY ID OFFSET {(_pageView.Page - 1) * _pageView.Size} ROWS FETCH NEXT {_pageView.Size} ROWS ONLY; ";
                _pageView.Users = connection.Query<User>(query);
                //string items = "";
                //foreach (User item in users)
                //{
                //    items += item.Age.ToString() + "\n";
                //}
                if (sortbyName)
                {
                    _pageView.Users = (_pageView.Users as IEnumerable<User>).OrderBy(u => u.Name);
                }
                else if (sortbySurname)
                {
                    _pageView.Users = (_pageView.Users as IEnumerable<User>).OrderBy(u => u.SurName);
                }
                else if (sortbyAge)
                {
                    _pageView.Users = (_pageView.Users as IEnumerable<User>).OrderBy(u => u.Age);
                }
                return View(_pageView);
            }
        }

        [HttpPost]
        public IActionResult ChangeSize(int size)
        {
            _pageView.Size = size;
            return RedirectToAction("Index");
            //return Index();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
