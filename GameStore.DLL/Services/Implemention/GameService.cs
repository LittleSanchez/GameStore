using AutoMapper;
using GameStore.DAL.Entities;
using GameStore.DAL.Repository.Abstraction;
using GameStore.DLL.Services.Abstraction;
using GameStore.DLL.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.DLL.Services.Implemention
{
    public class GameService : IGameService
    {
        private readonly IGenericRepository<Game> repo;
        private readonly IGenericRepository<Developer> repoDev;
        private readonly IGenericRepository<Genre> repoGenre;
        private readonly IGenericRepository<ArchiveGame> repoArchive;

        private readonly IMapper mapper;

        public GameService(IGenericRepository<Game> _repo, IGenericRepository<Developer> _repoDev, IGenericRepository<Genre> _repoGenre, IGenericRepository<ArchiveGame> _repoArchive, IMapper _mapper)
        {
            this.repo = _repo;
            this.repoDev = _repoDev;
            this.repoGenre = _repoGenre;
            this.repoArchive = _repoArchive;
            this.mapper = new MapperConfiguration(config => config.AddProfile(new AutomapperProfile())).CreateMapper();
        }

        public void AddGame(Game game)
        {
            var dev = repoDev.GetAll().FirstOrDefault(x => x.Name == game.Developer.Name);
            if (dev != null)
            {
                game.Developer = dev;
            }
            var genre = repoGenre.GetAll().FirstOrDefault(x => x.Name == game.Genre.Name);
            if (genre != null)
            {
                game.Genre = genre;
            }

            repo.Create(game);
        }

        public ICollection<Game> GetAllGames()
        {
            return repo.GetAll().ToList();
        }

        public void UpdateGame(Game game)
        {
            var dev = repoDev.GetAll().FirstOrDefault(x => x.Name == game.Developer.Name);
            if (dev != null)
            {
                game.DeveloperId = dev.Id;
            }
            var genre = repoGenre.GetAll().FirstOrDefault(x => x.Name == game.Genre.Name);
            if (genre != null)
            {
                game.GenreId = genre.Id;
            }
            repo.Update(game);
        }

        public void ArchiveGame(int id)
        {
            var game = repo.Find(id);
            if (game != null)
            {
                repoArchive.Create(mapper.Map<ArchiveGame>(game));
                repo.Delete(game);
            }
        }

        public void RecoverGame(int id)
        {
            var game = repoArchive.Find(id);
            if (game != null)
            {
                repo.Create(mapper.Map<Game>(game));
                repoArchive.Delete(game);
            }
        }

        public void DeleteGame(int id)
        {
            var game = repoArchive.Find(id);
            if (game != null)
            {
                repoArchive.Delete(game);
            }
        }

        public ICollection<string> GetStringDevelopers()
        {
            return repoDev.GetAll().Select(x => x.Name).ToList();
        }

        public Game GetGame(int id)
        {
            return repo.Find(id);
        }

        public ICollection<string> GetStringGenres()
        {
            return repoGenre.GetAll().Select(x => x.Name).ToList();
        }

        public ICollection<ArchiveGame> GetArchiveGames()
        {
            return repoArchive.GetAll().ToList();
        }

        public void AddGenre(string name)
        {
            repoGenre.Create(new Genre() { Name = name });
        }

        public void AddDeveloper(string name)
        {
            repoDev.Create(new Developer() { Name = name });
        }

        public void DeleteGenre(int id)
        {
            repoGenre.Delete(repoGenre.Find(id));
        }

        public void DeleteDeveloper(int id)
        {
            repoDev.Delete(repoDev.Find(id));
        }

        public IEnumerable<Developer> GetDevelopers()
        {
            return repoDev.GetAll();
        }

        public IEnumerable<Genre> GetGenres()
        {
            return repoGenre.GetAll();
        }
    }
}
