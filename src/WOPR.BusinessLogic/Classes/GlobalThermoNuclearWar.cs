using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;

namespace WOPR.BusinessLogic.Classes
{
    public class GlobalThermoNuclearWar : GameBase
    {
        BackgroundWorker _backgroundWorker;

        public override event PropertyChangedEventHandler PropertyChanged;

        Action<string> UpdateConsole { get; }
        public override string Name => ToString();
        public override void Start()
        {
            GameState = GameState.Start();
            UpdateConsole(Environment.NewLine);
            UpdateConsole("You are about to embark on Global Thermo Nuclear War");
            UpdateConsole("Are you sure you want to do this?");
            Console.ReadKey();
        }
        IEnumerable<string> CountDownAsync
        {
            get
            {
                Thread.Sleep(10000);
                yield return "Defcon 5";
                Thread.Sleep(10000);
                yield return "Defcon 4";
                Thread.Sleep(10000);
                yield return "Defcon 3";
                Thread.Sleep(10000);
                yield return "Defcon 2";
                Thread.Sleep(10000);
                yield return "Defcon 1";
            }
        }
        public override void Play(ConsoleKeyInfo keyPressed)
        {
            _backgroundWorker.RunWorkerAsync();
            PropertyChanged(this, new PropertyChangedEventArgs("GTNW Initiated"));
            End();
        }
        public override void End()
        {
            GameState = GameState.End();
            UpdateConsole(Environment.NewLine);
            UpdateConsole("That's started, is there time for another game?");
        }
        public GlobalThermoNuclearWar(Action<string> onGameStateChanged, int noOfPlays) : base(onGameStateChanged, noOfPlays)
        {
            _backgroundWorker = new BackgroundWorker();
            _backgroundWorker.DoWork += _backgroundWorker_DoWork;
            _backgroundWorker.RunWorkerCompleted += _backgroundWorker_RunWorkerCompleted;
            UpdateConsole = onGameStateChanged;
        }

        private void _backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            foreach (var defcon in CountDownAsync)
            {
                Console.Title = defcon;
            }
        }
        private void _backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Console.BackgroundColor = ConsoleColor.Red;
        }
        public override string ToString()
        {
            return "Global Thermo Nuclear War";
        }
        protected override void Dispose(bool disposing)
        {
            _backgroundWorker.DoWork -= _backgroundWorker_DoWork;
            _backgroundWorker.RunWorkerCompleted -= _backgroundWorker_RunWorkerCompleted;
            base.Dispose(disposing);
        }
    
    }
}
