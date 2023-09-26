using Scellecs.Morpeh;
using Scellecs.Morpeh.Systems;
using Scellecs.Morpeh.Utils;
using TriInspector;
using UnityEngine;

namespace Game.Scriptables.Installers
{
    public abstract class ScriptableInstaller : ScriptableObject
    {
        [HideInInspector]
        public int order;
        
        [Space]
#if UNITY_EDITOR
        [PropertyOrder(-6)]
        [Required]
#endif
        public Initializer[] initializers;
#if UNITY_EDITOR
        [PropertyOrder(-5)]
#endif
        public UpdateSystemPair[] updateSystems;
#if UNITY_EDITOR
        [PropertyOrder(-4)]
#endif
        public FixedSystemPair[] fixedUpdateSystems;
#if UNITY_EDITOR
        [PropertyOrder(-3)]
#endif
        public LateSystemPair[] lateUpdateSystems;

#if UNITY_EDITOR
        [PropertyOrder(-2)]
#endif
        public CleanupSystemPair[] cleanupSystems;

        private SystemsGroup group;

        public void FillSystems(World world = null)
        {
            if (world == null && World.Default != null)
                world = World.Default;

            group = world.CreateSystemsGroup();

            for (int i = 0, length = this.initializers.Length; i < length; i++)
            {
                var initializer = this.initializers[i];
                this.group.AddInitializer(initializer);
            }

            this.AddSystems(this.updateSystems);
            this.AddSystems(this.fixedUpdateSystems);
            this.AddSystems(this.lateUpdateSystems);
            this.AddSystems(this.cleanupSystems);

            world.AddSystemsGroup(this.order, this.group);
        }

        public void RemoveSystems(World world = null)
        {
            if (world != null)
            {
                this.RemoveSystems(this.updateSystems);
                this.RemoveSystems(this.fixedUpdateSystems);
                this.RemoveSystems(this.lateUpdateSystems);
                this.RemoveSystems(this.cleanupSystems);

                world.RemoveSystemsGroup(this.group);
            }
            this.group = null;
        }

        private void AddSystems<T>(BasePair<T>[] pairs) where T : class, ISystem
        {
            for (int i = 0, length = pairs.Length; i < length; i++)
            {
                var pair = pairs[i];
                var system = pair.System;
                if (system != null)
                {
                    this.group.AddSystem(system, pair.Enabled);
                }
                else
                {
                    this.SystemNullError();
                }
            }
        }

        private void SystemNullError()
        {
            Debug.LogError($"[MORPEH] System null in installer");
        }

        private void RemoveSystems<T>(BasePair<T>[] pairs) where T : class, ISystem
        {
            for (int i = 0, length = pairs.Length; i < length; i++)
            {
                var system = pairs[i].System;
                if (system != null)
                {
                    this.group.RemoveSystem(system);
                }
            }
        }
    }
}
