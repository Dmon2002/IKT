using System;
using GameCreator.Runtime.Characters;
using GameCreator.Runtime.Common;
using UnityEngine;

namespace GameCreator.Runtime.Melee
{
    [Title("Target Close")]
    [Category("Melee/Target Close")]

    [Image(typeof(IconTarget), ColorTheme.Type.Red, typeof(OverlayDot))]
    [Description("The closest point from Target around a radius of Self")]

    [Serializable]
    public class GetLocationMeleeTargetClose : PropertyTypeGetLocation
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
            
            Vector3 direction = (args.Self.transform.position - args.Target.transform.position)
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
                        characterTarget != null
                            ? characterTarget.Feet + offsetWorldSpace
                            : args.Target.transform.position + offsetWorldSpace,
                        rotation
                    );
                
                case Towards.HorizontalPosition:
                    Vector3 position = characterTarget != null
                        ? characterTarget.Feet + offsetWorldSpace
                        : args.Target.transform.position + offsetWorldSpace;

                    return new Location(
                        new Vector3(
                            position.x, 
                            characterSelf != null
                                ? characterSelf.Feet.y
                                : args.Self.transform.position.y, 
                            position.z
                        ),
                        rotation
                    );
                
                case Towards.FollowLocation:
                    return new Location(
                        args.Target.transform,
                        Space.World,
                        offsetWorldSpace,
                        false,
                        rotation
                    );
                
                default: throw new ArgumentOutOfRangeException();
            }
        }

        public static PropertyGetLocation Create => new PropertyGetLocation(
            new GetLocationMeleeTargetClose()
        );

        public override string String => "Target Close";
    }
}