using System;
using GameCreator.Runtime.Common;
using UnityEngine;

namespace GameCreator.Runtime.Melee
{
    [Title("Look at Self")]
    [Category("Melee/Look at Self")]

    [Image(typeof(IconEye), ColorTheme.Type.Green)]
    [Description("The rotation from Target to look at Self without any translation")]

    [Serializable]
    public class GetLocationMeleeLookSelf : PropertyTypeGetLocation
    {
        // PUBLIC METHODS: ------------------------------------------------------------------------

        public override Location Get(Args args)
        {
            if (args.Target == null) return new Location();
            if (args.Self == null) return new Location();

            Vector3 direction = Vector3.Scale(
                (args.Self.transform.position - args.Target.transform.position).normalized,
                Vector3Plane.NormalUp
            );

            return new Location(Quaternion.LookRotation(direction));
        }

        public static PropertyGetLocation Create => new PropertyGetLocation(
            new GetLocationMeleeLookSelf()
        );

        public override string String => "Look at Self";
    }
}
