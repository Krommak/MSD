using Game.Scriptables.Installers;
using Scellecs.Morpeh;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Start
{
    public class GameStartup : MonoBehaviour
    {
        public static GameStartup Instance { get; private set; }
        [SerializeField]
        private Installer _installerGO;
        [SerializeField]
        private ScriptableInstaller _playerInstaller;
        [SerializeField]
        private ScriptableInstaller _battleInstaller;

        [SerializeField]
        private List<Transform> _playerPositions;

        private Dictionary<int, Installer> _gameInstallers;

        private int _playerCount = 2;
        private RuntimeData _runtimeData;

        public RuntimeData RuntimeData
        {
            get
            {
                if (_runtimeData == null)
                    _runtimeData = new RuntimeData(_playerPositions);

                return _runtimeData;
            }
            private set
            {
            }
        }

        private void Start()
        {
            Instance = this;

            _gameInstallers = new Dictionary<int, Installer>();
            var battleInstaller = GameObject.Instantiate(_installerGO).GetComponent<Installer>();
            _gameInstallers.Add(0, battleInstaller);
            battleInstaller.World = World.Create("BattleWorld");
            _battleInstaller.FillSystems(0, battleInstaller.World);
            battleInstaller.order = 0;

            for (int i = 1; i <= _playerCount; i++)
            {
                var installer = GameObject.Instantiate(_installerGO).GetComponent<Installer>();
                installer.order = i;
                installer.World = World.Create($"Player {i + 1}");
                _playerInstaller.FillSystems(i, installer.World);
                _gameInstallers.Add(i + 1, installer);
                installer.enabled = true;
            }
        }

        private void Update()
        {
            foreach (var item in _gameInstallers.Values)
            {
                item.World.Update(Time.deltaTime);
            }
        }

        private void FixedUpdate()
        {
            foreach (var item in _gameInstallers.Values)
            {
                item.World.FixedUpdate(Time.fixedDeltaTime);
            }
        }

        private void LateUpdate()
        {
            foreach (var item in _gameInstallers.Values)
            {
                item.World.LateUpdate(Time.deltaTime);
            }
            foreach (var item in _gameInstallers.Values)
            {
                item.World.CleanupUpdate(Time.deltaTime);
            }
        }

        private void OnDisable()
        {
            foreach (var item in _gameInstallers.Values)
            {
                _playerInstaller.RemoveSystems(item.World);
                _battleInstaller.RemoveSystems(item.World);
            }
        }
    }

    public class RuntimeData
    {
        private List<Transform> _playerPositions;
        private int actualPosition;

        public RuntimeData(List<Transform> positions)
        {
            _playerPositions = positions;
        }

        public Transform GetPosForCreatePlayerBase()
        {
            var result = _playerPositions[actualPosition];
            actualPosition++;
            return result;
        }
    }
}

