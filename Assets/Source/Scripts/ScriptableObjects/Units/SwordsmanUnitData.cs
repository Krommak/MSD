using Game.Components;
using Scellecs.Morpeh;
using UnityEngine;

namespace Game.Data.Units
{
    public class SwordsmanUnitData : BaseUnitData
    {
        public override Unit GetUnit()
        {
            return new SwordsmanUnit(UnitPrefab, Attack, Armor, MovementSpeed);
        }
    }

    public class SwordsmanUnit : Unit
    {
        public SwordsmanUnit(GameObject unitPrefab, int attack, int armor, float movementSpeed) : base(unitPrefab, attack, armor, movementSpeed)
        {
        }

        public override GameObject CreateUnit(Entity entity)
        {
            var result = GameObject.Instantiate(UnitPrefab);
            FillComponents(entity, result);

            return result;
        }

        public override Unit FillComponents(Entity entity, GameObject unit)
        {
            entity.SetComponent(new UnitStats()
            {
                Attack = Attack,
                Armor = Armor,
                MovementSpeed = MovementSpeed,
            });
            entity.SetComponent(new UnitTransform()
            {
                Transform = unit.transform
            });

            return this;
        }
    }
}
