using System;
using GameCreator.Runtime.Common;
using UnityEngine;

namespace GameCreator.Runtime.Melee
{
    [Title("Self Bottom")]
    [Category("Melee/Self Bottom")]

    [Image(typeof(IconSelf), ColorTheme.Type.Green, typeof(OverlayArrowDown))]
    [Description("The position at the bottom of the Self offset by a certain amount")]

    [Serializable]
    public class GetLocationMeleeSelfBottom : TGetLocationMelee
    {
        public override Location Get(Args args) => this.GetLocation(args.Target, args.Self, args, Vector3.down);

        public static PropertyGetLocation Create => new PropertyGetLocation(
            new GetLocationMeleeSelfBottom()
        );

        public override string String => "Self Bottom";
    }
}