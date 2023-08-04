using System;
using GameCreator.Runtime.Common;
using UnityEngine;

namespace GameCreator.Runtime.Melee
{
    [Title("Target Bottom")]
    [Category("Melee/Target Bottom")]

    [Image(typeof(IconTarget), ColorTheme.Type.Red, typeof(OverlayArrowUp))]
    [Description("The position at the bottom of the Target offset by a certain amount")]

    [Serializable]
    public class GetLocationMeleeTargetBottom : TGetLocationMelee
    {
        public override Location Get(Args args) => this.GetLocation(args.Self, args.Target, args, Vector3.down);

        public static PropertyGetLocation Create => new PropertyGetLocation(
            new GetLocationMeleeTargetBottom()
        );
        
        public override string String => "Target Bottom";
    }
}