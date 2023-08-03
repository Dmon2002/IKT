using System;
using System.Collections.Generic;

namespace GameCreator.Runtime.Stats
{
    internal class ModifierList
    {
        private readonly int m_StatID;
        private readonly List<Modifier> m_List;

        // PROPERTIES: ----------------------------------------------------------------------------
        
        public double Value { get; private set; }

        // CONSTRUCTORS: --------------------------------------------------------------------------

        public ModifierList(int statID)
        {
            this.m_StatID = statID;
            this.m_List = new List<Modifier>();
            
            this.Value = 0f;
        }

        public ModifierList(int statID, List<Modifier> list) : this(statID)
        {
            foreach (Modifier element in list)
            {
                this.Value += element.Value;
                this.m_List.Add(element.Clone);
            }
        }
        
        // PUBLIC METHODS: ------------------------------------------------------------------------

        public void Add(double value)
        {
            this.Value += value;
            this.m_List.Add(new Modifier(this.m_StatID, value));
        }

        public bool Remove(double value)
        {
            for (int i = this.m_List.Count - 1; i >= 0; --i)
            {
                if (Math.Abs(this.m_List[i].Value - value) < float.Epsilon)
                {
                    this.Value -= this.m_List[i].Value;
                    this.m_List.RemoveAt(i);
                    
                    return true;
                }
            }

            return false;
        }
    }
}