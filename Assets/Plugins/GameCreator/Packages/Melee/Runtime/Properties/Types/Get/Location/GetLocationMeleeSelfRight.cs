using System;
using GameCreator.Runtime.Common;
using UnityEngine;

namespace GameCreator.Runtime.Melee
{
    [Title("Self Right")]
    [Category("Melee/Self Right")]

    [Image(typeof(IconSelf), ColorTheme.Type.Green, typeof(OverlayArrowRight))]
    [Description("The position at the right of the Self offset by a certain amount")]

    [Serializable]
    public class GetLocationMeleeSelfRight : TGetLocationMelee
    {
        public override Location Get(Args args) => this.GetLocation(args.Target, args.Self, args, Vector3.right);

        public static PropertyGetLocation Create => new PropertyGetLocation(
            new GetLocationMeleeSelfRight()
        );

        public override string String => "Self Right";
    }
}
