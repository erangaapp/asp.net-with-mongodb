using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApp.Models;

using BE = BusinessEntities;
using SVC = MongoDB.Service;

namespace WebApp.Controllers
{

    [Authorize]
    public class DemandController : Controller
    {
        private readonly SVC.IBookService bookService;
        private readonly SVC.IUserService userService;
        private readonly SVC.IDemandService demandService;
        private readonly Models.IApiClient<BE.Book> apiClient;

        public DemandController(SVC.IBookService bookService,
            SVC.IUserService userService,
            SVC.IDemandService demandService,
            Models.IApiClient<BE.Book> apiClient)
        {
            this.bookService = bookService;
            this.userService = userService;
            this.demandService = demandService;
            this.apiClient = apiClient;
        }

        public async Task<ActionResult> MyDemands(bool IsAfterDelete = false)
        {
            //Show deleted alert for user
            if (IsAfterDelete)
                ViewBag.Status = "DeleteSuccess";

            //ViewBag.Message = "My Demands";
            var demands = await demandService.GetByUser(User.Identity.Name);

            return View(demands.ToList());
        }

        public ActionResult BooksDemand()
        {
            ViewBag.Message = "Books Demand";

            return View();
        }

        public async Task<ActionResult> Delete(string id)
        {
            ViewBag.Message = "Delete Demand";

            var demand = await demandService.Get(id);

            return View(demand);
        }

        [HttpPost]
        public async Task<ActionResult> DeleteDemand(string id)
        {
            var demand = await demandService.Delete(id);

            //IsAfterDelete = true will notify to user about the success
            return RedirectToAction("MyDemands", new { IsAfterDelete = true });
        }

        #region Partial Views

        /// <summary>
        /// Get books from api
        /// </summary>
        /// <param name="query">filter query</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> SearchBooks(string query)
        {
            var books = await apiClient.Search("api/Books" + (string.IsNullOrEmpty(query) ? "/Get" : "/Get?filter=" + query));
            return PartialView("_BooksRearchResult", books);
        }

        /// <summary>
        /// Get book demands by Ids
        /// </summary>
        /// <param name="bookIds">Demand Ids separated by comma(',')</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> BooksDemand(string bookIds)
        {

            //Avoid empty string and To the list
            var IdList = bookIds.Split(',').Select(s => s).Where(w => !string.IsNullOrEmpty(w)).ToList();

            if (IdList.Count > 0)
            {
                var books = await bookService.GetByIds(IdList);

                if (books.Count() > 0)
                {
                    //User Object For Book Demand
                    var user = await userService.GetByUserName(User.Identity.Name);

                    foreach (var book in books)
                    {
                        //Insert books for DB
                        await demandService.Insert(new BE.Demand() { Book = book, User = user });
                    }

                    //Success msg for user alert
                    ViewBag.Status = "Success";
                    return PartialView("_BookDemand", books.ToList());
                }
                else
                {
                    //NotSelected msg for user alert
                    ViewBag.Status = "NotSelected";
                    return PartialView("_BookDemand", null);
                }

            }
            else
            {
                //NotSelected msg for user alert
                ViewBag.Status = "NotSelected";
                return PartialView("_BookDemand", null);
            }

        }

        #endregion

    }
}