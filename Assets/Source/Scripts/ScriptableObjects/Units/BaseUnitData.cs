using Scellecs.Morpeh;
using UnityEngine;

namespace Game.Data.Units
{
    public abstract class BaseUnitData : ScriptableObject
    {
        [SerializeField]
        protected GameObject UnitPrefab;
        [SerializeField]
        protected int Attack;
        [SerializeField]
        protected int Armor;
        [SerializeField]
        protected float MovementSpeed;

        public abstract Unit GetUnit();
    }

    public abstract class Unit
    {
        protected int Attack;
        protected int Armor;
        protected float MovementSpeed;

        public Vector3 UnitSize => UnitPrefab.transform.localScale;

        protected GameObject UnitPrefab;

        public Unit(GameObject unitPrefab, int attack, int armor, float movementSpeed)
        {
            Attack = attack;
            Armor = armor;
            MovementSpeed = movementSpeed;
            UnitPrefab = unitPrefab;
        }

        public abstract GameObject CreateUnit(Entity entity);

        public abstract Unit FillComponents(Entity entity, GameObject unit);
    }
}