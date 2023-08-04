using System;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;
using UnityEngine;

namespace GameCreator.Runtime.Melee
{
    [Serializable]
    public class ClipMeleeMotionWarping : Clip
    {
        [SerializeField] private RunConditionsList m_Conditions = new RunConditionsList();
        
        [SerializeField] private Easing.Type m_Easing = Common.Easing.Type.QuadInOut;
        
        [SerializeField] private PropertyGetLocation m_Self = Common.GetLocationTarget.Create;
        [SerializeField] private PropertyGetLocation m_Target = GetLocationNone.Create;

        // PROPERTIES: ----------------------------------------------------------------------------

        public Easing.Type Easing => this.m_Easing;

        // CONSTRUCTORS: --------------------------------------------------------------------------

        public ClipMeleeMotionWarping() : this(DEFAULT_TIME)
        { }

        public ClipMeleeMotionWarping(float time) : base(time, 0f)
        { }

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public bool CheckConditions(Args args) => this.m_Conditions.Check(args);
        
        public Location GetLocationSelf(Args args) => this.m_Self?.Get(args) ?? Location.NONE;
        public Location GetLocationTarget(Args args) => this.m_Target?.Get(args) ?? Location.NONE;
    }
}