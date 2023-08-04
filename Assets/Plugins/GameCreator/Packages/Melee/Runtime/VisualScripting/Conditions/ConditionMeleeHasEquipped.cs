using System;
using GameCreator.Runtime.Characters;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;
using UnityEngine;

namespace GameCreator.Runtime.Melee
{
    [Title("Has Equipped Melee")]
    [Description("Returns true if the Character has a specific Melee Weapon equipped")]

    [Category("Melee/Has Equipped Melee")]
    
    [Parameter("Character", "The targeted Character")]
    [Parameter("Weapon", "The Melee Weapon to check if it is equipped")]

    [Keywords("Combat", "Melee")]
    
    [Image(typeof(IconMeleeSword), ColorTheme.Type.Blue)]
    [Serializable]
    public class ConditionMeleeHasEquipped : Condition
    {
        // MEMBERS: -------------------------------------------------------------------------------
        
        [SerializeField] private PropertyGetGameObject m_Character = GetGameObjectPlayer.Create();
        [SerializeField] private MeleeWeapon m_Weapon;

        // PROPERTIES: ----------------------------------------------------------------------------
        
        protected override string Summary => string.Format(
            "has {0} Equipped {1}",
            this.m_Character, 
            this.m_Weapon != null ? this.m_Weapon.name : "(none)"
        );
        
        // RUN METHOD: ----------------------------------------------------------------------------

        protected override bool Run(Args args)
        {
            Character character = this.m_Character.Get<Character>(args);
            return character != null && 
                   this.m_Weapon != null &&
                   character.Combat.IsEquipped(this.m_Weapon);
        }
    }
}
