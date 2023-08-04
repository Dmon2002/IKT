using System;
using GameCreator.Runtime.Common;
using UnityEngine;

namespace GameCreator.Runtime.Melee
{
    [Title("Look at Target")]
    [Category("Melee/Look at Target")]

    [Image(typeof(IconEye), ColorTheme.Type.Red)]
    [Description("The rotation from Self to look at Target without any translation")]

    [Serializable]
    public class GetLocationMeleeLookTarget : PropertyTypeGetLocation
    {
        // PUBLIC METHODS: ------------------------------------------------------------------------

        public override Location Get(Args args)
        {
            if (args.Self == null) return new Location();
            if (args.Target == null) return new Location();

            Vector3 direction = Vector3.Scale(
                (args.Target.transform.position - args.Self.transform.position).normalized,
                Vector3Plane.NormalUp
            );

            return new Location(Quaternion.LookRotation(direction));
        }

        public static PropertyGetLocation Create => new PropertyGetLocation(
            new GetLocationMeleeLookTarget()
        );

        public override string String => "Look at Target";
    }
}
