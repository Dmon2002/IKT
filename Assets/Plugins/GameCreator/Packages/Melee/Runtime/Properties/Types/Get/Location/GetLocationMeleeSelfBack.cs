using System;
using GameCreator.Runtime.Common;
using UnityEngine;

namespace GameCreator.Runtime.Melee
{
    [Title("Self Back")]
    [Category("Melee/Self Back")]

    [Image(typeof(IconSelf), ColorTheme.Type.Green, typeof(OverlayArrowDown))]
    [Description("The position at the back of the Self offset by a certain amount")]

    [Serializable]
    public class GetLocationMeleeSelfBack : TGetLocationMelee
    {
        public override Location Get(Args args) => this.GetLocation(args.Target, args.Self, args, Vector3.back);

        public static PropertyGetLocation Create => new PropertyGetLocation(
            new GetLocationMeleeSelfBack()
        );

        public override string String => "Self Back";
    }
}
