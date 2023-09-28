using Game.Data.Units;
using Scellecs.Morpeh;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Data.Squads
{
    public abstract class BaseSquadData : ScriptableObject
    {
        [SerializeField]
        protected Vector3 _squadSize;
        [SerializeField]
        protected int _unitCount;
        [SerializeField]
        protected BaseUnitData _unit;

        public abstract Squad GetSquad(World world);
    }

    public abstract class Squad
    {
        private int _unitCount;
        private Unit _unit;
        private Dictionary<Entity, GameObject> _units;
        private Vector3 _squadSize;
        private World _world;

        public Squad(int count, Unit unit, World world, Vector3 squadSize)
        {
            _units = new Dictionary<Entity, GameObject>();
            _unitCount = count;
            _unit = unit;
            _squadSize = squadSize;
            _world = world;
            for (int i = 0; i < _unitCount; i++)
            {
                var entity = world.CreateEntity();
                var unitGO = _unit.CreateUnit(entity);

                _units.Add(entity, unitGO);

                unitGO.SetActive(false);
            }
        }

        public virtual Squad ActivateUnitsWithFormation(Vector3 position)
        {
            //TODO Add formation for units

            var positions = GetPositionsForUnits(_unit.UnitSize);
            var counter = 0;

            foreach (var item in _units.Values)
            {
                item.transform.position = positions[counter];
                counter++;
                item.SetActive(true);
            }

            return this;
        }

        public virtual Squad RestoreUnits()
        {
            foreach (var item in _units)
            {
                var newEntity = _world.CreateEntity();
                var unitGO = item.Value;
                _units.Remove(item.Key);
                _world.RemoveEntity(item.Key);

                _unit.FillComponents(newEntity, unitGO);

                _units.Add(newEntity, unitGO);
            }

            return this;
        }

        private Vector3[] GetPositionsForUnits(Vector3 unitSize)
        {
            var result = new Vector3[_unitCount];

            var unitsInLine = _squadSize.x / (unitSize.x * 5);
            var lines = _unitCount / unitsInLine;

            var indentX = unitSize.x * 3;
            var indentZ = unitSize.z * 3;

            var posX = -(unitsInLine / 2 * indentX) + unitSize.x / 2;
            var posZ = (lines * indentZ) + unitSize.z / 2;

            for (int z = 0; z < lines; z++)
            {
                for (int x = 0; x < unitsInLine; x++)
                {
                    result[(int)unitsInLine * z + x] = new Vector3(posX, 0.5f, posZ);

                    posX += indentZ;
                }

                posX = -(unitsInLine / 2 * indentX) + unitSize.x / 2;
                posZ -= indentZ;
            }

            return result;
        }
    }
}