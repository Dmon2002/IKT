using System;
using GameCreator.Runtime.Common;
using UnityEngine;

namespace GameCreator.Runtime.Melee
{
    [Title("Target Front")]
    [Category("Melee/Target Front")]

    [Image(typeof(IconTarget), ColorTheme.Type.Red, typeof(OverlayArrowUp))]
    [Description("The position in front of the Target offset by a certain amount")]

    [Serializable]
    public class GetLocationMeleeTargetFront : TGetLocationMelee
    {
        public override Location Get(Args args) => this.GetLocation(args.Self, args.Target, args, Vector3.forward);

        public static PropertyGetLocation Create => new PropertyGetLocation(
            new GetLocationMeleeTargetFront()
        );
        
        public override string String => "Target Front";
    }
}