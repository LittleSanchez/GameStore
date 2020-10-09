using AutoMapper;
using GameStore.DLL.Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GameStore.Client.Controllers
{
    public class EditorController : Controller
    {

        private readonly IGameService gameService;
        private readonly IMapper mapper;

        //DI #3
        public EditorController(IGameService _gameService, IMapper _mapper)
        {
            gameService = _gameService;
            mapper = _mapper;

        }


        // GET: Editor



        public ActionResult Index()
        {
            ViewBag.Genres = gameService.GetStringGenres();
            ViewBag.Developers = gameService.GetStringDevelopers();
            return View();
        }
    }
}