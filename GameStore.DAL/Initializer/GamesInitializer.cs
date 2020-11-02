using GameStore.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.DAL.Initializer
{
    internal class GamesInitializer :DropCreateDatabaseAlways<ApplicationContext>
    {
        protected override void Seed(ApplicationContext context)
        {
            var genres = new List<Genre>
            {
                new Genre{Name = "Action"},
                new Genre{Name = "RPG"},
                new Genre{Name = "Racing"},
                new Genre{Name = "Simulator"},
                new Genre{Name = "Strategy"},
            };
            var developers = new List<Developer>
            {
                new Developer{Name = "Rockstar"},
                new Developer{Name = "EA"},
                new Developer{Name = "Ubisoft"},
                new Developer{Name = "Bethesda"},
                new Developer{Name = "Valve"},
                new Developer{Name = "Activision"},
                new Developer{Name = "Ghost Games"},
                new Developer{Name = "Playrix"},
                new Developer{Name = "EpicGames"},
                new Developer{Name = "Blizzard"},
            };
            var games = new List<Game>
            {
                new Game{
                    Name="Far Cry",
                    Image = "https://upload.wikimedia.org/wikipedia/ru/4/42/Far_Cry_%D0%BE%D0%B1%D0%BB%D0%BE%D0%B6%D0%BA%D0%B0.png",
                    Price = 50,
                    Year = 2005,
                    Genre = genres.FirstOrDefault(x => x.Name == "Action"),
                    Developer = developers.FirstOrDefault(x => x.Name == "Ubisoft"),
                },
                new Game{
                    Name="Need for Speed",
                    Image = "/*find*/",
                    Price = 35,
                    Year = 2012,
                    Genre = genres.FirstOrDefault(x => x.Name == "Racing"),
                    Developer = developers.FirstOrDefault(x => x.Name == "EA"),
                },
                new Game{
                    Name="Warcraft III",
                    Image = "/*find*/",
                    Price = 999,
                    Year = 2003,
                    Genre = genres.FirstOrDefault(x => x.Name == "Strategy"),
                    Developer = developers.FirstOrDefault(x => x.Name == "Blizzard"),
                },
                new Game{
                    Name="Grand Theft Auto V",
                    Image = "https://i2.rozetka.ua/goods/14092663/grand_theft_auto_v_gta_5_premium_online_edition_pc_key_russkie_subtitri_elektronnij_klyuch_v_konverte_images_14092663448.jpg",
                    Price = 60,
                    Year = 2013,
                    Genre = genres.FirstOrDefault(x => x.Name == "RPG"),
                    Developer = developers.FirstOrDefault(x => x.Name == "Rockstar"),
                },
                new Game{
                    Name="FIFA20",
                    Image = "/*find*/",
                    Price = -20,
                    Year = 2020,
                    Genre = genres.FirstOrDefault(x => x.Name == "Simulator"),
                    Developer = developers.FirstOrDefault(x => x.Name == "EA"),
                },
            };
            context.Games.AddRange(games);
            context.Genres.AddRange(genres);
            context.Developers.AddRange(developers);
            context.ArchiveGames.AddRange(new List<ArchiveGame>());

            context.SaveChanges();

            base.Seed(context);
        }
    }
}
