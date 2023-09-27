using Scellecs.Morpeh;
using Scellecs.Morpeh.Systems;
using Scellecs.Morpeh.Utils;
using System.Collections.Generic;
using TriInspector;
using UnityEngine;

namespace Game.Scriptables.Installers
{
    public abstract class ScriptableInstaller : ScriptableObject
    {
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

        private List<SystemsGroup> groups = new List<SystemsGroup>();

        public void FillSystems(int order, World world = null)
        {
            if (world == null && World.Default != null)
                world = World.Default;

            var group = world.CreateSystemsGroup();

            groups.Add(group);

            for (int i = 0, length = initializers.Length; i < length; i++)
            {
                if(initializers[i] is ICopiableInitializer<Initializer> initializer)
                    group.AddInitializer(initializer.GetCopy());
            }

            AddSystems(updateSystems, group);
            AddSystems(fixedUpdateSystems, group);
            AddSystems(lateUpdateSystems, group);
            AddSystems(cleanupSystems, group);

            world.AddSystemsGroup(order, group);
        }

        public void RemoveSystems(World world = null)
        {
            if (world == null)
            {
                world = World.Default;
            }

            var group = world.CreateSystemsGroup();

            if (world != null)
            {
                RemoveSystems(updateSystems, group);
                RemoveSystems(fixedUpdateSystems, group);
                RemoveSystems(lateUpdateSystems, group);
                RemoveSystems(cleanupSystems, group);

                world.RemoveSystemsGroup(group);
            }

            groups.Remove(group);
        }

        private void AddSystems<T>(BasePair<T>[] pairs, SystemsGroup group) where T : class, ISystem
        {
            for (int i = 0, length = pairs.Length; i < length; i++)
            {
                var pair = pairs[i];
                var system = pair.System;
                if (system != null)
                {
                    group.AddSystem(system, pair.Enabled);
                }
                else
                {
                    SystemNullError();
                }
            }
        }

        private void SystemNullError()
        {
            Debug.LogError($"[MORPEH] System null in installer");
        }

        private void RemoveSystems<T>(BasePair<T>[] pairs, SystemsGroup group) where T : class, ISystem
        {
            for (int i = 0, length = pairs.Length; i < length; i++)
            {
                var system = pairs[i].System;
                if (system != null)
                {
                    group.RemoveSystem(system);
                }
            }
        }
    }

    public interface ICopiableInitializer<T> where T : class, IInitializer
    {
        public abstract T GetCopy();
    }

    public interface ICopiableSystem<T> where T : class, ISystem
    {
        public abstract T GetCopy();
    }
}