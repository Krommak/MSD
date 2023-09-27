using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Scellecs.Morpeh;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(ECS__System))]
public sealed class ECS__System : UpdateSystem
{
    public override void OnAwake()
    {
    }

    public override void OnUpdate(float deltaTime)
    {
        Debug.Log(this.World.GetFriendlyName());
    }
}