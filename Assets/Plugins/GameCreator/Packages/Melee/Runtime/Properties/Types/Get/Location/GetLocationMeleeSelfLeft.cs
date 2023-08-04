using System;
using GameCreator.Runtime.Common;
using UnityEngine;

namespace GameCreator.Runtime.Melee
{
    [Title("Self Left")]
    [Category("Melee/Self Left")]

    [Image(typeof(IconSelf), ColorTheme.Type.Green, typeof(OverlayArrowLeft))]
    [Description("The position at the left of the Self offset by a certain amount")]

    [Serializable]
    public class GetLocationMeleeSelfLeft : TGetLocationMelee
    {
        public override Location Get(Args args) => this.GetLocation(args.Target, args.Self, args, Vector3.left);

        public static PropertyGetLocation Create => new PropertyGetLocation(
            new GetLocationMeleeSelfLeft()
        );

        public override string String => "Self Left";
    }
}
