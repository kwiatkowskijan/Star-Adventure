using System.Collections.Generic;
using StarAdventure.Interface;

namespace StarAdventure.Managers
{
    public class GameManager
    {
        private static GameManager _instance;
        public static GameManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new GameManager();

                return _instance;
            }
        }

        private int _points;
        public int Points
        {
            get { return _points; }
            set { _points = value; }
        }

        private float _distanceTravelled;
        public float DistanceTravelled
        {
            get { return _distanceTravelled; }
            set { _distanceTravelled = value; }
        }

        private int _playerHealth;

        public int PlayerHealth
        {
            get { return _playerHealth; }
            set { 
                _playerHealth = value;
            
                if (_playerHealth <= 0)
                    EndGame();
            }
        }

        private List<IGameEndListener> _endListeners = new List<IGameEndListener>();

        private GameManager()
        {
            _points = 0;
            _distanceTravelled = 0;
            _playerHealth = 1;
        }

        public void RegisterListener(IGameEndListener listener)
        {
            _endListeners.Add(listener);
        }

        public void UnregisterListener(IGameEndListener listener)
        {
            _endListeners.Remove(listener);
        }

        void EndGame()
        {
            foreach (var listener in _endListeners)
            {
                listener.OnGameEnd();
            }
        }
    }
}
