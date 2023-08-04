using System;
using GameCreator.Runtime.Characters;
using GameCreator.Runtime.Common;
using UnityEngine;

namespace GameCreator.Runtime.Melee
{
    [Title("Target Right")]
    [Category("Melee/Target Right")]

    [Image(typeof(IconTarget), ColorTheme.Type.Red, typeof(OverlayArrowRight))]
    [Description("The position at the right of the Target offset by a certain amount")]

    [Serializable]
    public class GetLocationMeleeTargetRight : TGetLocationMelee
    {
        public override Location Get(Args args) => this.GetLocation(args.Self, args.Target, args, Vector3.right);

        public static PropertyGetLocation Create => new PropertyGetLocation(
            new GetLocationMeleeTargetRight()
        );

        public override string String => "Target Right";
    }
}