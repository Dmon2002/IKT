using System;
using GameCreator.Runtime.Common;
using UnityEngine;

namespace GameCreator.Runtime.Melee
{
    [Title("Target Back")]
    [Category("Melee/Target Back")]

    [Image(typeof(IconTarget), ColorTheme.Type.Red, typeof(OverlayArrowDown))]
    [Description("The position at the back of the Target offset by a certain amount")]

    [Serializable]
    public class GetLocationMeleeTargetBack : TGetLocationMelee
    {
        public override Location Get(Args args) => this.GetLocation(args.Self, args.Target, args, Vector3.back);

        public static PropertyGetLocation Create => new PropertyGetLocation(
            new GetLocationMeleeTargetBack()
        );

        public override string String => "Target Back";
    }
}