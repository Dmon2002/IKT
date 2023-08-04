using System;
using GameCreator.Runtime.Common;
using UnityEngine;

namespace GameCreator.Runtime.Melee
{
    [Title("Target Floor")]
    [Category("Melee/Target Floor")]

    [Image(typeof(IconTarget), ColorTheme.Type.Red, typeof(OverlayBar))]
    [Description("The first collision ray-casting downwards from the Target position")]

    [Serializable]
    public class GetLocationMeleeTargetFloor : PropertyTypeGetLocation
    {
        // EXPOSED MEMBERS: -----------------------------------------------------------------------

        [SerializeField] private LayerMask m_LayerMask = -1;
        [SerializeField] private PropertyGetDecimal m_Distance = GetDecimalDecimal.Create(5f);

        public override Location Get(Args args)
        {
            if (args.Self == null) return new Location();
            if (args.Target == null) return new Location();

            bool isHit = Physics.Raycast(
                args.Target.transform.position, Vector3.down,
                out RaycastHit hit,
                (float) this.m_Distance.Get(args), this.m_LayerMask,
                QueryTriggerInteraction.Ignore
            );

            return isHit ? new Location(hit.point, Quaternion.identity) : new Location();
        }

        public static PropertyGetLocation Create => new PropertyGetLocation(
            new GetLocationMeleeTargetFloor()
        );

        public override string String => "Target Floor";
    }
}