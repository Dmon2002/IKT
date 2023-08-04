using System;
using GameCreator.Runtime.Common;
using UnityEngine;

namespace GameCreator.Runtime.Melee
{
    [Title("Self Floor")]
    [Category("Melee/Self Floor")]

    [Image(typeof(IconSelf), ColorTheme.Type.Green, typeof(OverlayBar))]
    [Description("The first collision ray-casting downwards from the Self position")]

    [Serializable]
    public class GetLocationMeleeSelfFloor : PropertyTypeGetLocation
    {
        // EXPOSED MEMBERS: -----------------------------------------------------------------------

        [SerializeField] private LayerMask m_LayerMask = -1;
        [SerializeField] private PropertyGetDecimal m_Distance = GetDecimalDecimal.Create(5f);

        public override Location Get(Args args)
        {
            if (args.Self == null) return new Location();
            if (args.Target == null) return new Location();

            bool isHit = Physics.Raycast(
                args.Self.transform.position, Vector3.down,
                out RaycastHit hit,
                (float) this.m_Distance.Get(args), this.m_LayerMask,
                QueryTriggerInteraction.Ignore
            );

            return isHit ? new Location(hit.point) : new Location();
        }

        public static PropertyGetLocation Create => new PropertyGetLocation(
            new GetLocationMeleeSelfFloor()
        );

        public override string String => "Self Floor";
    }
}