using System;
using GameCreator.Runtime.Characters;
using GameCreator.Runtime.Common;

namespace GameCreator.Runtime.VisualScripting
{
    [Title("Activate Object on Focus")]
    [Image(typeof(IconCubeSolid), ColorTheme.Type.Yellow, typeof(OverlayDot))]
    
    [Category("Tooltips/Activate Object on Focus")]
    [Description(
        "Activates a game object scene instance when the Hotspot is enabled and " +
        "deactivates it when the Hotspot its unfocused"
    )]
    
    [Serializable]
    public class SpotTooltipObjectFocus : SpotTooltipObject
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [NonSerialized] private IInteractive m_Interactive;
        
        // PROPERTIES: ----------------------------------------------------------------------------

        public override string Title => string.Format(
            "Show {0} on Focus",
            this.m_GameObject != null ? this.m_GameObject.name : "(none)"
        );

        // OVERRIDE METHODS: ----------------------------------------------------------------------

        public override void OnAwake(Hotspot hotspot)
        {
            base.OnAwake(hotspot);
            this.m_Interactive = InteractionTracker.Require(hotspot.gameObject);
        }

        protected override bool EnableInstance(Hotspot hotspot)
        {
            bool isActive = base.EnableInstance(hotspot);
            
            Character character = hotspot.Target.Get<Character>();
            bool hasFocus = character != null && 
                            character.Interaction.Target?.Instance == hotspot.gameObject;
            
            return isActive && hasFocus && !this.m_Interactive.IsInteracting;
        }
    }
}