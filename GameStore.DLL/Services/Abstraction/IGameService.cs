using GameStore.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.DLL.Services.Abstraction
{
    public interface IGameService
    {
        ICollection<Game> GetAllGames();
        void AddGame(Game game);
        Game GetGame(int id);

        ICollection<string> GetStringDevelopers();
        ICollection<string> GetStringGenres();

        IEnumerable<Developer> GetDevelopers();
        IEnumerable<Genre> GetGenres();

        ICollection<ArchiveGame> GetArchiveGames();
        void UpdateGame(Game game);
        void ArchiveGame(int id);
        void RecoverGame(int id);
        void DeleteGame(int id);

        void AddGenre(string name);
        void AddDeveloper(string name);
        void DeleteGenre(int id);
        void DeleteDeveloper(int id);

    }
}
