using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Game.Scriptables.Installers;

namespace Game.Systems.Init
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    [CreateAssetMenu(menuName = "ECS/Initializers/" + nameof(InitUISystem))]
    public sealed class InitUISystem : Initializer, ICopiableInitializer<Initializer> 
    {
        public override void OnAwake() 
        {
        }

        public override void Dispose() 
        {
        }

        public Initializer GetCopy()
        {
            return new InitUISystem()
            {
            };
        }
    }
}