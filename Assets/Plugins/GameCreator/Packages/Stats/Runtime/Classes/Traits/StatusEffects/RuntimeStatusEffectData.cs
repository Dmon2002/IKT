using System.Threading.Tasks;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;
using UnityEngine;

namespace GameCreator.Runtime.Stats
{
    internal class RuntimeStatusEffectData
    {
        // MEMBERS: -------------------------------------------------------------------------------
        
        private readonly float m_StartTime;
        private readonly float m_Duration;

        private readonly StatusEffect m_StatusEffect;

        private CopyRunnerInstructionList m_OnStart;
        private CopyRunnerInstructionList m_OnEnd;
        private CopyRunnerInstructionList m_WhileActive;

        // PROPERTIES: ----------------------------------------------------------------------------
        
        private Args Args { get; }

        // CONSTRUCTOR: ---------------------------------------------------------------------------

        public RuntimeStatusEffectData(Traits traits, StatusEffect statusEffect, float elapsedTime = 0f)
        {
            this.Args = new Args(traits.gameObject);
            this.m_StartTime = Time.time - elapsedTime;
            this.m_Duration = statusEffect.GetDuration(this.Args);

            this.m_StatusEffect = statusEffect;

            _ = this.RunOnStart();
            _ = this.RunWhileActive();
        }

        // INTERNAL METHODS: ----------------------------------------------------------------------

        public bool Update()
        {
            if (!this.m_StatusEffect.HasDuration) return false;
            return Time.time > this.m_StartTime + this.m_Duration;
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private async Task RunOnStart()
        {
            this.m_OnStart = this.m_StatusEffect.CreateOnStart(this.Args);
            CopyRunnerInstructionList reference = this.m_OnStart;
            
            await reference.GetRunner<InstructionList>().Run(this.Args.Clone);
            if (reference != null) Object.Destroy(reference.gameObject);
        }
        
        private async Task RunOnEnd()
        {
            this.m_OnEnd = this.m_StatusEffect.CreateOnEnd(this.Args);
            CopyRunnerInstructionList reference = this.m_OnEnd;
            
            await reference.GetRunner<InstructionList>().Run(this.Args.Clone);
            if (reference != null) Object.Destroy(reference.gameObject);
        }

        private async Task RunWhileActive()
        {
            this.m_WhileActive = this.m_StatusEffect.CreateWhileActive(this.Args);
            CopyRunnerInstructionList reference = this.m_WhileActive;

            while (this.m_WhileActive != null)
            {
                int frame = Time.frameCount;
                await this.m_WhileActive.GetRunner<InstructionList>().Run(this.Args.Clone);

                if (frame == Time.frameCount) await Task.Yield();
            }
            
            if (reference != null) Object.Destroy(reference.gameObject);
        }

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public void Stop()
        {
            if (this.m_WhileActive != null)
            {
                this.m_WhileActive.GetRunner<InstructionList>()?.Cancel();
                Object.Destroy(this.m_WhileActive.gameObject);
            }

            _ = this.RunOnEnd();
        }

        public RuntimeStatusEffectValue GetValue()
        {
            float timeElapsed = Time.time - this.m_StartTime;

            if (this.m_StatusEffect.HasDuration)
            {
                return new RuntimeStatusEffectValue(
                    this.m_StatusEffect.ID.String,
                    timeElapsed, this.m_StatusEffect.GetDuration(this.Args) - timeElapsed
                );
            }
            
            return new RuntimeStatusEffectValue(
                this.m_StatusEffect.ID.String,
                timeElapsed, -1f
            );
        }
    }
}