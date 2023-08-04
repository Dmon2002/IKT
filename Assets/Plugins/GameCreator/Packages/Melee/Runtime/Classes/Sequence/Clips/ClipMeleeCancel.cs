using System;
using GameCreator.Runtime.VisualScripting;

namespace GameCreator.Runtime.Melee
{
    [Serializable]
    public class ClipMeleeCancel : Clip
    {
        // CONSTRUCTORS: --------------------------------------------------------------------------

        public ClipMeleeCancel() : this(DEFAULT_TIME)
        { }

        public ClipMeleeCancel(float time) : base(time, 0f)
        { }
    }
}