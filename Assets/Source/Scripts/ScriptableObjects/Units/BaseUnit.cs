using Scellecs.Morpeh;
using UnityEngine;

namespace Game.Data.Squads
{
    public abstract class BaseUnit : ScriptableObject
    {
        [SerializeField]
        private GameObject _unitPrefab;

        public abstract Unit GetUnit();
    }

    public abstract class Unit
    {
        public Vector3 UnitSize => _unitPrefab.transform.localScale;

        private GameObject _unitPrefab;

        public abstract GameObject CreateUnit(Entity entity);

        public abstract Unit FillComponents(Entity entity);
    }
}