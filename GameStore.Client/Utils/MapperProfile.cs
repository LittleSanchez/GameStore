using AutoMapper;
using GameStore.Client.Models;
using GameStore.DAL.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameStore.Client.Utils
{
    public class MapperProfile: Profile
    {
        public MapperProfile()
        {
            // GameViewModel <= Game
            CreateMap<Game, GameViewModel>()
                .ForMember(x => x.Developer, opt => opt.MapFrom(x => x.Developer.Name))
                .ForMember(x => x.Genre, opt => opt.MapFrom(x => x.Genre.Name));
            CreateMap<GameViewModel, Game>()
                .ForMember(x => x.Developer, opt => opt.MapFrom(x => new Developer { Name = x.Developer }))
                .ForMember(x => x.Genre, opt => opt.MapFrom(x => new Genre { Name = x.Genre }));
            CreateMap<ArchiveGame, ArchiveGameViewModel>()
               .ForMember(x => x.Developer, opt => opt.MapFrom(x => x.Developer.Name))
               .ForMember(x => x.Genre, opt => opt.MapFrom(x => x.Genre.Name));
            CreateMap<ArchiveGameViewModel, ArchiveGame>()
                .ForMember(x => x.Developer, opt => opt.MapFrom(x => new Developer { Name = x.Developer }))
                .ForMember(x => x.Genre, opt => opt.MapFrom(x => new Genre { Name = x.Genre }));
            CreateMap<IdentityUser, UserViewModel>();
        }
        
    }
}