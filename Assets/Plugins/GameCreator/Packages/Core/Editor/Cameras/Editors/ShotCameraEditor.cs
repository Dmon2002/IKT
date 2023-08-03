using System;
using GameCreator.Editor.Common;
using GameCreator.Runtime.Cameras;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameCreator.Editor.Cameras
{
    [CustomEditor(typeof(ShotCamera), true)]
    public class ShotCameraEditor : UnityEditor.Editor
    {
        private const string PROP_IS_MAIN = "m_IsMainShot";
        private const string PROP_TIME_MODE = "m_TimeMode";
        private const string PROP_CLIPPING = "m_Clipping";
        private const string PROP_SHOT_TYPE = "m_ShotType";

        private const int PREVIEW_CAMERA_DEPTH = -999;
        private static readonly GUIContent PREVIEW_TITLE = new GUIContent("Camera Shot Preview");
        
        // MEMBERS: -------------------------------------------------------------------------------
        
        [NonSerialized] private Camera m_PreviewCamera;
        [NonSerialized] private float m_PreviewAspectRatio = 1.0f;
        [NonSerialized] private RenderTexture m_RenderTexture;

        // EVENTS: --------------------------------------------------------------------------------
        
        public static event Action<ShotCamera, SerializedObject> EventSceneGUI;
        
        // CREATE INSPECTOR: ----------------------------------------------------------------------

        public override VisualElement CreateInspectorGUI()
        {
            VisualElement root = new VisualElement();

            SerializedProperty isMainShot = this.serializedObject.FindProperty(PROP_IS_MAIN);
            SerializedProperty timeMode = this.serializedObject.FindProperty(PROP_TIME_MODE);
            SerializedProperty clipping = this.serializedObject.FindProperty(PROP_CLIPPING);
            SerializedProperty shotType = this.serializedObject.FindProperty(PROP_SHOT_TYPE);

            PropertyField fieldIsMainShot = new PropertyField(isMainShot);
            PropertyField fieldTimeMode = new PropertyField(timeMode);
            PropertyField fieldClipping = new PropertyField(clipping);
            PropertyField fieldShotType = new PropertyField(shotType);

            root.Add(fieldIsMainShot);
            root.Add(fieldTimeMode);
            root.Add(fieldClipping);
            root.Add(fieldShotType);

            return root;
        }

        private void OnEnable()
        {
            EventSceneGUI = null;
            
            this.m_PreviewCamera = EditorUtility.CreateGameObjectWithHideFlags(
                "Preview Camera",
                HideFlags.HideAndDontSave,
                typeof(Camera)
            ).GetComponent<Camera>();

            this.m_PreviewCamera.cameraType = CameraType.Preview;
            this.m_PreviewCamera.depth = PREVIEW_CAMERA_DEPTH;
        }
        
        private void OnDisable()
        {
            EventSceneGUI = null;
            
            if (this.m_PreviewCamera != null) DestroyImmediate(this.m_PreviewCamera);
            if (this.m_RenderTexture != null) RenderTexture.ReleaseTemporary(this.m_RenderTexture);
        }

        // PREVIEW WINDOW: ------------------------------------------------------------------------

        public override bool RequiresConstantRepaint() => true;
        public override bool HasPreviewGUI() => Camera.main != null;

        public override GUIContent GetPreviewTitle() => PREVIEW_TITLE;

        public override void OnPreviewGUI(Rect r, GUIStyle background)
        {
            if (Event.current.type != EventType.Repaint) return;
            
            Camera camera = Camera.main;
            if (camera == null) return;
            
            ShotCamera shotCamera = this.target as ShotCamera;
            if (shotCamera == null) return;

            Vector2 size1 = new Vector2(r.width, r.width / this.m_PreviewAspectRatio);
            Vector2 size2 = new Vector2(r.height * this.m_PreviewAspectRatio, r.height);

            float scale1 = Mathf.Min(r.width / size1.x, r.height / size1.y);
            float scale2 = Mathf.Min(r.width / size2.x, r.height / size2.y);

            Vector2 size = scale1 < scale2 
                ? size1 * scale1 
                : size2 * scale2;
            
            this.m_RenderTexture = RenderTexture.GetTemporary(
                (int) size.x * Mathf.CeilToInt(EditorGUIUtility.pixelsPerPoint),
                (int) size.y * Mathf.CeilToInt(EditorGUIUtility.pixelsPerPoint),
                24, 
                RenderTextureFormat.ARGB32
            );
            
            this.m_PreviewCamera.targetTexture = this.m_RenderTexture;
            
            Transform shotTransform = shotCamera.transform;
            this.m_PreviewCamera.transform.SetPositionAndRotation(
                shotTransform.position,
                shotTransform.rotation
            );

            this.m_PreviewCamera.orthographic = camera.orthographic;
            this.m_PreviewCamera.orthographicSize = camera.orthographicSize;
            this.m_PreviewCamera.fieldOfView = camera.fieldOfView;
                
            this.m_PreviewCamera.Render();
            this.m_PreviewCamera.targetTexture = null;

            GUI.DrawTexture(r, this.m_RenderTexture, ScaleMode.ScaleToFit, false);
            RenderTexture.ReleaseTemporary(this.m_RenderTexture);
        }

        // CREATION MENU: -------------------------------------------------------------------------
        
        [MenuItem("GameObject/Game Creator/Cameras/Camera Shot", false, 0)]
        public static void CreateElement(MenuCommand menuCommand)
        {
            GameObject instance = new GameObject("Camera Shot");
            ShotCamera shotCamera = instance.AddComponent<ShotCamera>();

            GameObjectUtility.SetParentAndAlign(instance, menuCommand?.context as GameObject);

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
            
            TCamera camera = FindObjectOfType<TCamera>();
            if (camera != null)
            {
                if (camera.Transition.CurrentShotCamera != null) return;
            }
            else
            {
                Camera mainCamera = Camera.main;
                if (mainCamera == null) mainCamera = FindObjectOfType<Camera>();

                if (mainCamera == null) return;
                camera = mainCamera.gameObject.AddComponent<MainCamera>();
            }

            camera.Transition.CurrentShotCamera = shotCamera;
        }
        
        // GUI: -----------------------------------------------------------------------------------
        
        private void OnSceneGUI()
        {
            ShotCamera shotCamera = this.target as ShotCamera;
            EventSceneGUI?.Invoke(shotCamera, this.serializedObject);
            if (Camera.current != null) this.m_PreviewAspectRatio = Camera.current.aspect;
        }
    }
}