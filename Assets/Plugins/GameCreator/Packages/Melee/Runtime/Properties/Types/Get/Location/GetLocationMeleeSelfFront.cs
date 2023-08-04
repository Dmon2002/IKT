using System;
using GameCreator.Runtime.Common;
using UnityEngine;

namespace GameCreator.Runtime.Melee
{
    [Title("Self Front")]
    [Category("Melee/Self Front")]

    [Image(typeof(IconSelf), ColorTheme.Type.Green, typeof(OverlayArrowUp))]
    [Description("The position in front of the Self offset by a certain amount")]

    [Serializable]
    public class GetLocationMeleeSelfFront : TGetLocationMelee
    {
        public override Location Get(Args args) => this.GetLocation(args.Target, args.Self, args, Vector3.forward);

        public static PropertyGetLocation Create => new PropertyGetLocation(
            new GetLocationMeleeSelfFront()
        );

        public override string String => "Self Front";
    }
}
