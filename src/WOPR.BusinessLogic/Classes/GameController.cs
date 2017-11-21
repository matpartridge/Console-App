using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WOPR.BusinessLogic.GameState;
using WOPR.BusinessLogic.RepositoryState;
using WOPR.DataLayer;
using WOPR.Infrastructure.Classes;
using WOPR.Infrastructure.Interfaces;

namespace WOPR.BusinessLogic.Classes
{
    public class GameController
    {
        DataStore _ds = DataStore.Instance;

        IRepositoryState _repo = new Unknown();

        IGame selectedGame;

        static GameController _instance = new GameController();
        public static GameController Instance => _instance;

        GameController()
        {
            Action<string> OnGameStateChanged;
            OnGameStateChanged = UpdateConsole;
            _ds.GameList.Add(new GlobalThermoNuclearWar(OnGameStateChanged, 1));
            _ds.GameList.Add(new PlayYourPlayCountRight(OnGameStateChanged, 5));
            _ds.GameList.Add(new WheelOfFortune(OnGameStateChanged, 4));

            foreach (var game in _ds.GameList)
            {
                game.PropertyChanged += SelectedGame_PropertyChanged;
            }           
        }

        private void SelectedGame_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            _ds.GameList.Remove((IGame)sender);
            ((IGame)sender).PropertyChanged -= SelectedGame_PropertyChanged;
        }

        void UpdateConsole(string message)
        {
            Console.WriteLine(message);
        }
        string GetGameList()
        {
            var sb = new StringBuilder();
            _ds.GameList.ForEach(g => sb.AppendLine($"{g.ToString()} [{g.ToString().ToCharArray()[0].ToString()}]"));
            return sb.ToString();
        }
        public bool StartGame(ConsoleKeyInfo selection)
        {
            selectedGame = _ds.GameList.FirstOrDefault(g => g.Name.StartsWith(selection.KeyChar.ToString(), StringComparison.CurrentCultureIgnoreCase));

            if (selectedGame == null)
                return false;
            
            selectedGame.Start();

            return true;
        }
        public bool Play(ConsoleKeyInfo keyPressed)
        {
            selectedGame.Play(keyPressed);

            if (selectedGame.GameState is Lost)
                selectedGame.End();

            return selectedGame.GameState.Underway;
        }
        public bool? PlayAgain(ConsoleKeyInfo keyPressed)
        {
            if (keyPressed.Key == ConsoleKey.Y)
                return true;
            else if (keyPressed.Key == ConsoleKey.N)
                return false;
            else
                return null;
        }

        public void SelectDataSource()
        {
            OperationResult operationResult = new OperationResult(false, "Do you want to play on-line [O] or locally [L]?");
            Console.WriteLine(operationResult.Message);

            while (_repo is Unknown)
            {
                EvaluateRepository(Console.ReadKey().Key);
                Console.WriteLine($"{Environment.NewLine}{_repo.ToString()}");
            }
            Console.Clear();

            Console.WriteLine(_repo.ToString());
            Console.WriteLine("Type the name of an Artist - if offline the choices are [Bob Dylan; Maddona; The Beatles]");

            operationResult = GetTopAlbumsByArtistAsync(_repo.Repo, Console.ReadLine().ToString()).Result;

            Console.WriteLine(operationResult.Message);
            Console.WriteLine("Do you want to see the available games?");
        }
        public ConsoleKeyInfo? SelectGame()
        {
            Console.Clear();
            Console.WriteLine("Which game do you want to play?");
            Console.WriteLine();
            Console.WriteLine(GetGameList());

            var keyPressed = Console.ReadKey();
            if ((keyPressed.Key == ConsoleKey.G && _ds.GameList.Count == 3) || keyPressed.Key == ConsoleKey.W || keyPressed.Key == ConsoleKey.P)
                return keyPressed;
            return null;
        }
        void EvaluateRepository(ConsoleKey keyPressed)
        {
            if (keyPressed == ConsoleKey.O)
                _repo = _repo.Online();
            else if (keyPressed == ConsoleKey.L)
                _repo = _repo.Offline();
            else
                _repo = _repo.NotSet();
        }
        async Task<OperationResult> GetTopAlbumsByArtistAsync(IRepository repo, string artist)
        {
            AlbumRetrievalService prs = new AlbumRetrievalService(repo);
            var operationResult = await prs.GetTopAlbumsByArtistAsync(artist);
            return operationResult;
        }
    }
}
