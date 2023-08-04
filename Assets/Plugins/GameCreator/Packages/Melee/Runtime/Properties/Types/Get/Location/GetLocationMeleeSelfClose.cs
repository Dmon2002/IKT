using System;
using GameCreator.Runtime.Characters;
using GameCreator.Runtime.Common;
using UnityEngine;

namespace GameCreator.Runtime.Melee
{
    [Title("Self Close")]
    [Category("Melee/Self Close")]

    [Image(typeof(IconSelf), ColorTheme.Type.Green, typeof(OverlayDot))]
    [Description("The closest point from Self around a radius of Target")]

    [Serializable]
    public class GetLocationMeleeSelfClose : PropertyTypeGetLocation
    {
        private enum Towards
        {
            StartingPosition,
            HorizontalPosition,
            FollowLocation
        }
        
        // EXPOSED MEMBERS: -----------------------------------------------------------------------

        [SerializeField] private PropertyGetDecimal m_Radius = GetDecimalDecimal.Create(1f);
        [SerializeField] private Towards m_Towards = Towards.FollowLocation;

        // PUBLIC METHODS: ------------------------------------------------------------------------
        
        public override Location Get(Args args)
        {
            if (args.Self == null) return new Location();
            if (args.Target == null) return new Location();
            
            Vector3 direction = (args.Target.transform.position - args.Self.transform.position)
                .normalized;
            
            Vector3 planeDirection = Vector3.Scale(direction, Vector3Plane.NormalUp).normalized;

            Vector3 offsetWorldSpace = planeDirection != Vector3.zero
                ? planeDirection * (float) this.m_Radius.Get(args)
                : direction * (float) this.m_Radius.Get(args);

            Quaternion rotation = Quaternion.LookRotation(planeDirection != Vector3.zero
                ? -planeDirection
                : -direction
            );
            
            Character characterTarget = args.Target.Get<Character>();
            Character characterSelf = args.Self.Get<Character>();

            switch (this.m_Towards)
            {
                case Towards.StartingPosition:
                    return new Location(
                        characterSelf != null
                            ? characterSelf.Feet + offsetWorldSpace
                            : args.Self.transform.position + offsetWorldSpace,
                        rotation
                    );
                
                case Towards.HorizontalPosition:
                    Vector3 position = characterSelf != null
                        ? characterSelf.Feet + offsetWorldSpace
                        : args.Self.transform.position + offsetWorldSpace;
                    
                    return new Location(
                        new Vector3(
                            position.x, 
                            characterTarget != null
                                ? characterTarget.Feet.y
                                : args.Target.transform.position.y, 
                            position.z
                        ),
                        rotation
                    );
                
                case Towards.FollowLocation:
                    return new Location(
                        args.Self.transform,
                        Space.World,
                        offsetWorldSpace,
                        false,
                        rotation
                    );
                
                default: throw new ArgumentOutOfRangeException();
            }
        }

        public static PropertyGetLocation Create => new PropertyGetLocation(
            new GetLocationMeleeSelfClose()
        );

        public override string String => "Self Close";
    }
}
