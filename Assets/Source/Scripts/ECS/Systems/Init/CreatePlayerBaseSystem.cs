using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Game.Start;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Initializers/" + nameof(CreatePlayerBaseSystem))]
public sealed class CreatePlayerBaseSystem : Initializer
{
    public override void OnAwake()
    {
        var prefab = Resources.Load<GameObject>("Prefabs/PlayerPrefab");
        var posTransform = GameStartup.Instance.RuntimeData.GetPosForCreatePlayerBase();
        var player = Instantiate(prefab);
        player.transform.position = posTransform.position;
        player.transform.eulerAngles = posTransform.eulerAngles;
    }

    public override void Dispose()
    {
    }
}