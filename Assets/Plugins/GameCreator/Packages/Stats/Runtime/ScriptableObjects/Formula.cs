using GameCreator.Runtime.Common;
using UnityEngine;

namespace GameCreator.Runtime.Stats
{
    [CreateAssetMenu(
        fileName = "Formula", 
        menuName = "Game Creator/Stats/Formula",
        order    = 50
    )]
    
    [Icon(EditorPaths.PACKAGES + "Stats/Editor/Gizmos/GizmoFormula.png")]
    
    public class Formula : ScriptableObject
    {
        [SerializeField] private string m_Formula;
        [SerializeField] private Table m_Table;
        
        // PROPERTIES: ----------------------------------------------------------------------------

        public bool Exists => !string.IsNullOrEmpty(this.m_Formula);
        
        // PUBLIC METHODS: ------------------------------------------------------------------------
        
        public double Calculate(Traits source, Traits target)
        {
            return Evaluation.Calculate(
                source, target, 
                this.m_Formula, this.m_Table
            );
        }
    }
}