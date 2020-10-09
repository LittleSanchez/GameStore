using AutoMapper;
using GameStore.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.DLL.Utils
{
    public class AutomapperProfile:Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Game, ArchiveGame>();
            CreateMap<ArchiveGame, Game>();
        }
    }
}
