using Scellecs.Morpeh;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Start
{
    public class GameStartup : MonoBehaviour
    {
        public static GameStartup Instance { get; private set; }
        [SerializeField]
        private Installer _battleInstaller;
        [SerializeField]
        private Installer _playerInstaller;

        private Dictionary<int, Installer> _gameInstallers;

        private int _playerCount = 2;

        private void Start()
        {
            Instance = this;

            _gameInstallers = new Dictionary<int, Installer>();
            var battleInstaller = GameObject.Instantiate(_battleInstaller).GetComponent<Installer>();
            _gameInstallers.Add(0, battleInstaller);
            battleInstaller.order = 0;

            for (int i = 1; i <= _playerCount; i++)
            {
                var installer = GameObject.Instantiate(_playerInstaller).GetComponent<Installer>();
                installer.order = i;
                _gameInstallers.Add(i, installer);
            }

            foreach (var item in _gameInstallers.Values)
            {
                item.enabled = true;
            }
        }
    }
}

