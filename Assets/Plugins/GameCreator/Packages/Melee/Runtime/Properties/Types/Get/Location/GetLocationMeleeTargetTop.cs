using System;
using GameCreator.Runtime.Common;
using UnityEngine;

namespace GameCreator.Runtime.Melee
{
    [Title("Target Top")]
    [Category("Melee/Target Top")]

    [Image(typeof(IconTarget), ColorTheme.Type.Red, typeof(OverlayArrowUp))]
    [Description("The position at the top of the Target offset by a certain amount")]

    [Serializable]
    public class GetLocationMeleeTargetTop : TGetLocationMelee
    {
        public override Location Get(Args args) => this.GetLocation(args.Self, args.Target, args, Vector3.up);

        public static PropertyGetLocation Create => new PropertyGetLocation(
            new GetLocationMeleeTargetTop()
        );
        
        public override string String => "Target Top";
    }
}