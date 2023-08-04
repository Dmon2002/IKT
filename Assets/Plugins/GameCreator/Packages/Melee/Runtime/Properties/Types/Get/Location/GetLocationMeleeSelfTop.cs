using System;
using GameCreator.Runtime.Common;
using UnityEngine;

namespace GameCreator.Runtime.Melee
{
    [Title("Self Top")]
    [Category("Melee/Self Top")]

    [Image(typeof(IconSelf), ColorTheme.Type.Green, typeof(OverlayArrowUp))]
    [Description("The position at the top of the Self offset by a certain amount")]

    [Serializable]
    public class GetLocationMeleeSelfTop : TGetLocationMelee
    {
        public override Location Get(Args args) => this.GetLocation(args.Target, args.Self, args, Vector3.up);

        public static PropertyGetLocation Create => new PropertyGetLocation(
            new GetLocationMeleeSelfTop()
        );

        public override string String => "Self Top";
    }
}