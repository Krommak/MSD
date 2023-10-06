using Game.Data.Units;
using Scellecs.Morpeh;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Data.Squads
{
    [CreateAssetMenu(fileName = "InfanterySquadData", menuName = "Game/Data/Units/InfanterySquadData")]
    public class InfanterySquadData : BaseSquadData
    {
        public override Squad GetSquad(World world)
        {
            return new InfanterySquad(_unitCount, _unit.GetUnit(), world, _squadSize, _squadIcon);
        }
    }

    public class InfanterySquad : Squad
    {
        public InfanterySquad(int count, Unit unit, World world, Vector3 squadSize, Sprite squadIcon) : base(count, unit, world, squadSize, squadIcon)
        {
        }
    }
}
