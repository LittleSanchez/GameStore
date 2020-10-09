using AutoMapper;
using GameStore.DAL.Entities;
using GameStore.DLL.Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GameStore.Client.Controllers
{
    public class DevelopersController : Controller
    {

        private readonly IGameService gameService;
        private readonly IMapper mapper;

        //DI #3
        public DevelopersController(IGameService _gameService, IMapper _mapper)
        {
            gameService = _gameService;
            mapper = _mapper;

        }

        // GET: Developers
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(Developer developer)
        {
            gameService.AddDeveloper(developer.Name);
            return Redirect("~/Editor/Index");
        }

        public ActionResult Delete(string id)
        {
            gameService.DeleteDeveloper(gameService.GetDevelopers().Where(x => x.Name == id).Select(x => x.Id).First());
            return Redirect("~/Editor/Index");
        }
    }
}