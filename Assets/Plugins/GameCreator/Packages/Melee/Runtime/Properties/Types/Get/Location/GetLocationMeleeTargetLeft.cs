using System;
using GameCreator.Runtime.Common;
using UnityEngine;

namespace GameCreator.Runtime.Melee
{
    [Title("Target Left")]
    [Category("Melee/Target Left")]

    [Image(typeof(IconTarget), ColorTheme.Type.Red, typeof(OverlayArrowLeft))]
    [Description("The position at the left of the Target offset by a certain amount")]

    [Serializable]
    public class GetLocationMeleeTargetLeft : TGetLocationMelee
    {
        public override Location Get(Args args) => this.GetLocation(args.Self, args.Target, args, Vector3.left);

        public static PropertyGetLocation Create => new PropertyGetLocation(
            new GetLocationMeleeTargetLeft()
        );

        public override string String => "Target Left";
    }
}