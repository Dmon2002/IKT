using System;
using GameCreator.Runtime.Characters;
using GameCreator.Runtime.Common;
using UnityEngine;

namespace GameCreator.Runtime.Melee
{
    [Serializable]
    public abstract class TGetLocationMelee : PropertyTypeGetLocation
    {
        private enum Towards
        {
            StartingPosition,
            HorizontalPosition,
            FollowLocation
        }
        
        // EXPOSED MEMBERS: -----------------------------------------------------------------------

        [SerializeField] private PropertyGetDecimal m_Distance = GetDecimalDecimal.Create(1f);
        [SerializeField] private Towards m_Towards = Towards.FollowLocation;

        // PROTECTED METHODS: ---------------------------------------------------------------------
        
        protected Location GetLocation(GameObject from, GameObject to, Args args, Vector3 direction)
        {
            if (from == null) return new Location();
            if (to == null) return new Location();

            float distance = (float) this.m_Distance.Get(args);
            
            Quaternion rotation = Quaternion.LookRotation(
                to.transform.TransformDirection(-direction)
            );
            
            Character characterFrom = from.Get<Character>();
            Character characterTo = to.Get<Character>();

            switch (this.m_Towards)
            {
                case Towards.StartingPosition:
                    return new Location(
                        (characterTo != null
                            ? characterTo.Feet
                            : args.Target.transform.position
                        ) + to.transform.TransformDirection(direction * distance),
                        rotation
                    );

                case Towards.HorizontalPosition:
                    Vector3 position = (characterTo != null
                        ? characterTo.Feet
                        : args.Target.transform.position
                    ) + to.transform.TransformDirection(direction * distance);
                    
                    return new Location(
                        new Vector3(
                            position.x,
                            characterFrom != null
                                ? characterFrom.Feet.y
                                : from.transform.position.y, 
                            position.z
                        ),
                        rotation
                    );
                
                case Towards.FollowLocation:
                    return new Location(
                        to.transform, 
                        Space.Self,
                        direction * distance,
                        false, 
                        rotation
                    );
                
                default: throw new ArgumentOutOfRangeException();
            }
        }
    }
}