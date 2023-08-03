using System;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;
using UnityEngine;

namespace GameCreator.Runtime.Stats
{
    [CreateAssetMenu(
        fileName = "Status Effect", 
        menuName = "Game Creator/Stats/Status Effect",
        order    = 50
    )]
    
    [Icon(EditorPaths.PACKAGES + "Stats/Editor/Gizmos/GizmoStatusEffects.png")]
    
    public class StatusEffect : ScriptableObject
    {
        [NonSerialized] private CopyRunnerInstructionList TemplateOnStart;
        [NonSerialized] private CopyRunnerInstructionList TemplateOnEnd;
        [NonSerialized] private CopyRunnerInstructionList TemplateWhileActive;

        // MEMBERS: -------------------------------------------------------------------------------
        
        [SerializeField] private IdString m_ID = new IdString("status-effect-id");
        [SerializeField] private StatusEffectData m_Data = new StatusEffectData();
        [SerializeField] private StatusEffectInfo m_Info = new StatusEffectInfo();

        [SerializeField] private StatusEffectInstruction m_OnStart = new StatusEffectInstruction(
            new InstructionCommonDebugComment("Executed right after this Status Effect is applied")
        );
        
        [SerializeField] private StatusEffectInstruction m_OnEnd = new StatusEffectInstruction(
            new InstructionCommonDebugComment("Executed right after this Status Effect finishes")
        );
        
        [SerializeField] private StatusEffectInstruction m_WhileActive = new StatusEffectInstruction(
            new InstructionCommonDebugComment("Executed over and over again while this Status Effect lasts")
        );

        // PROPERTIES: ----------------------------------------------------------------------------

        public IdString ID => m_ID;

        public Sprite Icon => this.m_Info.icon;
        public Color Color => this.m_Info.color;

        public StatusEffectType Type => this.m_Data.Type;
        public bool Save => this.m_Data.Save;
        public bool HasDuration => this.m_Data.HasDuration;

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public string GetAcronym(Args args) => this.m_Info.acronym.Get(args);
        public string GetName(Args args) => this.m_Info.name.Get(args);
        public string GetDescription(Args args) => this.m_Info.description.Get(args);

        public float GetDuration(Args args) => (float) this.m_Data.GetDuration(args);
        public int GetMaxStack(Args args) => this.m_Data.GetMaxStack(args);

        // INSTRUCTIONS: --------------------------------------------------------------------------
        
        public CopyRunnerInstructionList CreateOnStart(Args args)
        {
            return MakeRunner(args, this.TemplateOnStart, this.m_OnStart.List);
        }
        
        public CopyRunnerInstructionList CreateOnEnd(Args args)
        {
            return MakeRunner(args, this.TemplateOnEnd, this.m_OnEnd.List);
        }
        
        public CopyRunnerInstructionList CreateWhileActive(Args args)
        {
            return MakeRunner(args, this.TemplateWhileActive, this.m_WhileActive.List);
        }
        
        // PRIVATE METHODS: -----------------------------------------------------------------------
        
        private static CopyRunnerInstructionList MakeRunner(Args args, 
            CopyRunnerInstructionList template, InstructionList instructions)
        {
            if (template == null)
            {
                template = CopyRunnerInstructionList
                    .CreateTemplate<CopyRunnerInstructionList>(instructions);
            }

            CopyRunnerInstructionList copy = template.CreateRunner<CopyRunnerInstructionList>(
                args.Self != null ? args.Self.transform.position : Vector3.zero, 
                args.Self != null ? args.Self.transform.rotation : Quaternion.identity, 
                null
            );
            
            return copy;
        }
    }
}