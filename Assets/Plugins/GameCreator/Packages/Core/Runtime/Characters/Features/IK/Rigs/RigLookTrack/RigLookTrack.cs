using System;
using System.Collections.Generic;
using GameCreator.Runtime.Common;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace GameCreator.Runtime.Characters.IK
{
    [Title("Track with Body")]
    [Category("Track with Body")]
    [Image(typeof(IconEye), ColorTheme.Type.Green)]
    
    [Description(
        "IK system that allows the Character to naturally look at points of interest using the " +
        "whole upper-body chain of bones. Requires a humanoid character"
    )]
    
    [Serializable]
    public class RigLookTrack : TRigAnimationRigging
    {
        private class LookTargets : List<ILookTrack>
        {
            public ILookTrack Get(Vector3 target)
            {
                float minDistance = Mathf.Infinity;
                ILookTrack minLookTrack = null;

                foreach (ILookTrack lookTrack in this)
                {
                    if (lookTrack is not { Exists: true }) continue;

                    float distance = Vector3.Distance(target, lookTrack.Position);
                    if (!(distance < minDistance)) continue;
                    
                    minLookTrack = lookTrack;
                    minDistance = distance;
                }

                return minLookTrack;
            }
        }
        
        private class LookLayers : SortedDictionary<int, LookTargets>
        { }
        
        // CONSTANTS: -----------------------------------------------------------------------------

        public const string RIG_NAME = "RigLookTrack";

        private const float HORIZON = 10f;

        // EXPOSED MEMBERS: -----------------------------------------------------------------------

        [SerializeField] private float m_TrackSpeed = 270f;
        [SerializeField] private float m_MaxAngle = 120f;
        
        [SerializeField, Range(0f, 1f)] private float m_HeadWeight = 1f;
        [SerializeField, Range(0f, 1f)] private float m_NeckWeight = 0.2f;
        [SerializeField, Range(0f, 1f)] private float m_ChestWeight = 0.5f;
        [SerializeField, Range(0f, 1f)] private float m_SpineWeight = 0.2f;
        
        // MEMBERS: -------------------------------------------------------------------------------
        
        [NonSerialized] private float m_WeightTarget;

        [NonSerialized] private Transform m_LookHandle;
        [NonSerialized] private Transform m_LookPoint;

        [NonSerialized] private ILookTrack m_LookTrackTarget;
        [NonSerialized] private readonly LookLayers m_Layers = new LookLayers();

        // PROPERTIES: ----------------------------------------------------------------------------
        
        public override string Title => "Look at Target";
        public override string Name => RIG_NAME;

        public override bool RequiresHuman => true;
        public override bool DisableOnBusy => true;

        protected override float WeightTarget => this.m_WeightTarget;
        protected override float WeightSmoothTime => 0.35f;
        
        [field: NonSerialized] private MultiAimConstraint ConstraintHead { get; set; }
        [field: NonSerialized] private MultiAimConstraint ConstraintNeck { get; set; }
        [field: NonSerialized] private MultiAimConstraint ConstraintChest { get; set; }
        [field: NonSerialized] private MultiAimConstraint ConstraintSpine { get; set; }

        public ILookTrack LookTrackTarget => this.m_LookTrackTarget;

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public void SetTarget<T>(T look) where T : ILookTrack
        {
            if (look == null) return;
            if (!this.m_Layers.ContainsKey(look.Layer))
            {
                this.m_Layers[look.Layer] = new LookTargets();
            }

            if (this.m_Layers[look.Layer].Contains(look)) return;
            
            this.m_Layers[look.Layer].Add(look);
        }

        public void RemoveTarget<T>(T look) where T : ILookTrack
        {
            if (look == null) return;
            if (this.m_Layers.TryGetValue(look.Layer, out LookTargets targets))
            {
                targets.Remove(look);
            }
        }

        public void ClearTargets()
        {
            foreach (KeyValuePair<int, LookTargets> entry in this.m_Layers)
            {
                if (entry.Key == 0) continue;
                entry.Value.Clear();
            }
        }
        
        // IMPLEMENT METHODS: ---------------------------------------------------------------------

        protected override bool DoUpdate(Character character)
        {
            bool rebuildGraph = base.DoUpdate(character);
            if (this.Animator == null || this.Animator.avatar == null) return rebuildGraph;
            
            bool hasNeck = this.Animator.GetBoneTransform(HumanBodyBones.Neck) != null;
            bool hasChest = this.Animator.GetBoneTransform(HumanBodyBones.Chest) != null;

            if (hasNeck)
            {
                this.ConstraintHead.data.sourceObjects.SetWeight(0, this.m_HeadWeight);
                this.ConstraintNeck.data.sourceObjects.SetWeight(0, this.m_NeckWeight);
            }
            else
            {
                float weight = this.m_HeadWeight + this.m_NeckWeight;
                this.ConstraintHead.data.sourceObjects.SetWeight(0, weight);
            }

            if (hasChest)
            {
                this.ConstraintChest.data.sourceObjects.SetWeight(0, this.m_ChestWeight);
                this.ConstraintSpine.data.sourceObjects.SetWeight(0, this.m_SpineWeight);
            }
            else
            {
                float weight = this.m_ChestWeight + this.m_SpineWeight;
                this.ConstraintSpine.data.sourceObjects.SetWeight(0, weight);
            }
            
            this.m_LookTrackTarget = this.GetLookTrackTarget(character);

            Vector3 targetDirection;

            this.m_LookHandle.position = character.Eyes;
            
            if (this.m_LookTrackTarget is { Exists: true })
            {
                this.m_WeightTarget = 1f;
                Vector3 targetPosition = this.m_LookTrackTarget.Position;
                
                Vector3 characterDirection = character.transform.forward;
                targetDirection = targetPosition - character.Eyes;
                
                float angle = Vector3.Angle(characterDirection, targetDirection);
                float distance = Vector2.Distance(
                    character.transform.position.XZ(),
                    targetPosition.XZ()
                );
                
                if (angle > this.m_MaxAngle || distance < character.Motion.Radius)
                {
                    this.m_WeightTarget = 0f;
                    targetDirection = character.transform.forward;
                }
            }
            else
            {
                this.m_WeightTarget = 0f;
                targetDirection = character.transform.forward;
            }

            this.m_LookHandle.rotation = Quaternion.RotateTowards(
                this.m_LookHandle.rotation,
                Quaternion.LookRotation(targetDirection, Vector3.up),
                character.Time.DeltaTime * this.m_TrackSpeed
            );

            return rebuildGraph;
        }

        protected override void OnBuildRigLayer(Character character)
        {
            if (this.m_LookHandle == null || this.m_LookPoint == null)
            {
                if (this.m_LookHandle != null) UnityEngine.Object.Destroy(this.m_LookHandle.gameObject);
                if (this.m_LookPoint != null) UnityEngine.Object.Destroy(this.m_LookPoint.gameObject);
                
                GameObject handle = new GameObject(RIG_NAME + "Handle");
                GameObject point = new GameObject(RIG_NAME + "Point");
                
                handle.hideFlags = HideFlags.HideAndDontSave;
                point.hideFlags = HideFlags.HideAndDontSave;

                this.m_LookHandle = handle.transform;
                this.m_LookHandle.position = character.Eyes;

                this.m_LookPoint = point.transform;
                this.m_LookPoint.SetParent(this.m_LookHandle);
                this.m_LookPoint.localPosition = Vector3.forward * HORIZON;
            }
            
            this.ConstraintSpine = this.CreateConstraint(
                RIG_NAME + "Spine", character, 
                HumanBodyBones.Spine, this.m_SpineWeight,
                new Vector3Int(0,1,0)
            );
            
            this.ConstraintChest = this.CreateConstraint(
                RIG_NAME + "Chest",  character, 
                HumanBodyBones.Chest, this.m_ChestWeight,
                new Vector3Int(0,1,0)
            );
            
            this.ConstraintNeck = this.CreateConstraint(
                RIG_NAME + "Neck", character, 
                HumanBodyBones.Neck, this.m_NeckWeight,
                new Vector3Int(0,1,0)
            );

            this.ConstraintHead = this.CreateConstraint(
                RIG_NAME + "Head", character, 
                HumanBodyBones.Head, this.m_HeadWeight,
                new Vector3Int(1,1,1)
            );
        }
        
        // PRIVATE METHODS: -----------------------------------------------------------------------

        private ILookTrack GetLookTrackTarget(Character character)
        {
            foreach (KeyValuePair<int,LookTargets> entryLayer in this.m_Layers)
            {
                ILookTrack target = entryLayer.Value.Get(character.Eyes);
                if (target is { Exists: true }) return target;
            }
            
            return null;
        }
        
        private MultiAimConstraint CreateConstraint(string name, Character character, 
            HumanBodyBones bone, float weight, Vector3Int constraintAxis)
        {
            Transform boneTransform = character.Animim.Animator.GetBoneTransform(bone);
            if (boneTransform == null) return null;
            
            GameObject container = new GameObject(name);
            container.transform.SetParent(this.RigLayer.rig.transform);
            container.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
            
            MultiAimConstraint constraint = container.AddComponent<MultiAimConstraint>();
            constraint.data.constrainedObject = boneTransform;
            constraint.data.aimAxis = MultiAimConstraintData.Axis.Z;
            constraint.data.upAxis = MultiAimConstraintData.Axis.Y;
            
            constraint.data.constrainedXAxis = constraintAxis.x != 0;
            constraint.data.constrainedYAxis = constraintAxis.y != 0;
            constraint.data.constrainedZAxis = constraintAxis.z != 0;
            
            constraint.data.limits = new Vector2(
                -this.m_MaxAngle / 2f,
                this.m_MaxAngle / 2f
            );

            WeightedTransformArray sourceObjects = constraint.data.sourceObjects;
            sourceObjects.Insert(0, new WeightedTransform(this.m_LookPoint, weight));

            constraint.data.sourceObjects = sourceObjects;
            return constraint;
        }

        // GIZMOS: --------------------------------------------------------------------------------

        protected override void DoDrawGizmos(Character character)
        {
            base.DoDrawGizmos(character);
            Gizmos.color = Color.cyan;
            
            if (this.m_LookPoint == null) return;
            
            Gizmos.DrawWireCube(
                this.m_LookPoint.position,
                Vector3.one * 0.1f
            );
            
            if (this.ConstraintHead == null) return;
            if (this.ConstraintHead.data.constrainedObject == null) return;
            
            Gizmos.DrawLine(
                this.ConstraintHead.data.constrainedObject.position,
                this.m_LookPoint.position
            );
        }
    }
}