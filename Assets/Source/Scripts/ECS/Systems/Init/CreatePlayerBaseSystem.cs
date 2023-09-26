using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Game.Start;
using Scellecs.Morpeh;
using Game.Components;
using System.Runtime.CompilerServices;
using UnityEngine.UIElements;

namespace Game.Systems.Init
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    [CreateAssetMenu(menuName = "ECS/Initializers/" + nameof(CreatePlayerBaseSystem))]

    public sealed class CreatePlayerBaseSystem : Initializer
    {
        [SerializeField]
        private Vector3[] _relativeSquadPositions; 

        public override void OnAwake()
        {
            var prefab = Resources.Load<GameObject>("Prefabs/PlayerPrefab");
            var posTransform = GameStartup.Instance.RuntimeData.GetPosForCreatePlayerBase();
            var player = Instantiate(prefab);
            player.transform.position = posTransform.position;
            player.transform.eulerAngles = posTransform.eulerAngles;
            
            var positionsEntity = this.World.CreateEntity();
            var value = new Vector3[_relativeSquadPositions.Length];
            for (int i = 0; i < value.Length; i++)
            {
                value[i] = _relativeSquadPositions[i] + player.transform.position;
            }
            positionsEntity.SetComponent( new SquadStartPositions()
            {
                Value = value,
            });

            Debug.Log(this.World.GetFriendlyName() + " " + positionsEntity.ID);
        }

        public override void Dispose()
        {
        }
    }
}