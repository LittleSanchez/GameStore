using AutoMapper;
using GameStore.Client.Models;
using GameStore.Client.Utils.Filter.Abstraction;
using GameStore.Client.Utils.Filter.Implemention;
using GameStore.Client.Utils.Helper;
using GameStore.DAL.Entities;
using GameStore.DLL.Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;

namespace GameStore.Client.Controllers
{
    public class GamesController : Controller
    {
        private readonly IGameService gameService;
        private readonly IMapper mapper;

        private ISortHelper<Game> gameSorter = new GamesSortHelper();

        //DI #3
        public GamesController(IGameService _gameService, IMapper _mapper)
        {
            gameService = _gameService;
            mapper = _mapper;
            
        }

        #region Index

        [HttpGet]
        public ActionResult Index(string filter)
        {
            var sortTypesList = new List<string>();
            foreach (var item in typeof(Game).GetProperties())
            {
                
                var instance = new Game();
                if (item.PropertyType.GetMethods().ToList().Exists(x => x.Name == "CompareTo"))
                {
                    sortTypesList.Add(item.Name.ToLower() + "-" + gameSorter.DESC);
                    sortTypesList.Add(item.Name.ToLower() + "-" + gameSorter.ASC);
                }
            }
            ViewBag.SortTypes = sortTypesList;
            ViewBag.Archive = mapper.Map<ICollection<ArchiveGameViewModel>>(gameService.GetArchiveGames());
            var games = gameService.GetAllGames();
            if (filter != null && !filter.Equals(""))
            {
                ViewBag.Filter = true;
                dynamic filters = System.Web.Helpers.Json.Decode(filter);
                var genres = ((object[])filters.genres)?.Select(x => x.ToString()).ToList();
                var developers = ((object[])filters.developers)?.Select(x => x.ToString()).ToList();
                var sortKeyWord = ((object)filters.sortKeyWord)?.ToString();
                games = games.Where(x => (developers.Count > 0 ? developers.Contains(x.Developer.Name) : true) && (genres.Count > 0 ? genres.Contains(x.Genre.Name) : true)).ToList();
                if (sortKeyWord != null && !sortKeyWord.Equals(""))
                    (games as List<Game>).Sort(gameSorter.GetComparison(sortKeyWord));
            }
            var model = mapper.Map<ICollection<GameViewModel>>(games);


            return View(model);
        }



        #endregion
        #region Create

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Developers = gameService.GetStringDevelopers();
            ViewBag.Genres = gameService.GetStringGenres();
            return View();
        }
        [HttpPost]
        public ActionResult Create(GameViewModel model, HttpPostedFileBase image)
        {

            if (ModelState.IsValid)
            {
                Image sourceImage = null;
                if (image != null)
                {
                    try
                    {
                        sourceImage = Bitmap.FromStream(image.InputStream);
                    }
                    catch(ArgumentException ex)
                    {
                        Debug.WriteLine(ex.Message);
                    }
                }
                else if (model.Image.StartsWith("data:image"))
                {
                    sourceImage = DataUrlConverter.ToImage(model.Image);
                }
                else 
                {
                    WebClient client = new WebClient();
                    try
                    {
                        using (var ms = new MemoryStream(client.DownloadData(model.Image)))
                        {
                            sourceImage = Bitmap.FromStream(ms);
                        }
                    }
                    catch(Exception ex)
                    {
                        Debug.WriteLine(ex.Message);
                    }
                }
                string fileName = Guid.NewGuid().ToString() + ".jpg";
                string fullPath = Server.MapPath("~/Images/Games/" + fileName);

                var converterPicture = CustomImageConverter.ConvertImage(sourceImage, 400, 700);

                if (converterPicture != null)
                {
                    converterPicture.Save(fullPath, ImageFormat.Jpeg);
                    model.Image = "/Images/Games/" + fileName;
                }
                //var game = new Game
                //{
                //    Id = model.Id,
                //    Name = model.Name,
                //    Price = model.Price,
                //    Year = model.Year,
                //    Description = model.Description,
                //    Image = model.Image,
                //    Developer = new Developer { Name = model.Developer },
                //    Genre = new Genre { Name = model.Genre }
                //};

                var game = mapper.Map<Game>(model);

                gameService.AddGame(game);

                return RedirectToAction("Index");
            }
            else return Create();
        }


        #endregion
        #region Edit

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = gameService.GetGame(id);
            var game = mapper.Map<GameViewModel>(model);
            ViewBag.Developers = gameService.GetStringDevelopers();
            ViewBag.Genres = gameService.GetStringGenres();
            return View(game);
        }
        [HttpPost]
        public ActionResult Edit(GameViewModel model, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                Image sourceImage = null;
                if (image != null)
                {
                    try
                    {
                        sourceImage = Bitmap.FromStream(image.InputStream);
                    }
                    catch (ArgumentException ex)
                    {
                        Debug.WriteLine(ex.Message);
                    }
                }
                else if (model.Image.StartsWith("data:image"))
                {
                    sourceImage = DataUrlConverter.ToImage(model.Image);
                }
                else
                {
                    WebClient client = new WebClient();
                    try
                    {
                        using (var ms = new MemoryStream(client.DownloadData(model.Image)))
                        {
                            sourceImage = Bitmap.FromStream(ms);
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.Message);
                    }
                }
                string fileName = Guid.NewGuid().ToString() + ".jpg";
                string fullPath = Server.MapPath("~/Images/Games/" + fileName);

                var converterPicture = CustomImageConverter.ConvertImage(sourceImage, 600, 800);

                if (converterPicture != null)
                {
                    converterPicture.Save(fullPath, ImageFormat.Jpeg);
                    model.Image = "/Images/Games/" + fileName;
                }

                var game = mapper.Map<Game>(model);
                gameService.UpdateGame(game);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Edit", model.Id);
        }

        #endregion
        #region Details

        [HttpGet]
        public ActionResult Details(int id)
        {
            var game = gameService.GetGame(id);
            var model = mapper.Map<GameViewModel>(game);

            return View(model);
        }

        #endregion
        #region Archive

        public ActionResult ArchiveList()
        {
            return PartialView(mapper.Map<ICollection<ArchiveGameViewModel>>(gameService.GetArchiveGames()));
        }

        public ActionResult Archive(int id)
        {
            gameService.ArchiveGame(id);
            return Content("ok");
        }
       
        public ActionResult Recover(int id)
        {
            gameService.RecoverGame(id);
            return Content("ok");
        }

        public ActionResult Delete(int id)
        {
            gameService.DeleteGame(id);
            return Content("ok");
        }

        #endregion
        #region Search


        public ActionResult Search(string data)
        {
            if (string.IsNullOrWhiteSpace(data))
            {
                return Content("n_found");
            }
            var games = gameService.GetAllGames().Where(x => x.Name.ToLower().Contains(data.ToLower()));
            if (games.Count() == 0)
            {
                return Content("n_found");
            }
            var model = mapper.Map<ICollection<GameViewModel>>(games);
            return PartialView(model);
        }


        #endregion

    }
}