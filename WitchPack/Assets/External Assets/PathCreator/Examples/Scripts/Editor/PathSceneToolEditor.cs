﻿using UnityEditor;
using UnityEngine;

namespace External_Assets.PathCreator.Examples.Scripts.Editor
{
    [CustomEditor(typeof(PathSceneTool), true)]
    public class PathSceneToolEditor : UnityEditor.Editor
    {
        protected PathSceneTool pathTool;
        bool isSubscribed;

        public override void OnInspectorGUI()
        {
            using (var check = new EditorGUI.ChangeCheckScope())
            {
                DrawDefaultInspector();

                if (check.changed)
                {
                    if (!isSubscribed)
                    {
                        TryFindPathCreator();
                        Subscribe();
                    }

                    if (pathTool.autoUpdate)
                    {
                        TriggerUpdate();

                    }
                }
            }

            if (GUILayout.Button("Manual Update"))
            {
                if (TryFindPathCreator())
                {
                    TriggerUpdate();
                    SceneView.RepaintAll();
                }
            }

        }


        void TriggerUpdate() {
            if (pathTool.pathCreator != null) {
                pathTool.TriggerUpdate();
            }
        }


        protected virtual void OnPathModified()
        {
            if (pathTool.autoUpdate)
            {
                TriggerUpdate();
            }
        }

        protected virtual void OnEnable()
        {
            pathTool = (PathSceneTool)target;
            pathTool.onDestroyed += OnToolDestroyed;

            if (TryFindPathCreator())
            {
                Subscribe();
                TriggerUpdate();
            }
        }

        void OnToolDestroyed() {
            if (pathTool != null) {
                pathTool.pathCreator.pathUpdated -= OnPathModified;
            }
        }

 
        protected virtual void Subscribe()
        {
            if (pathTool.pathCreator != null)
            {
                isSubscribed = true;
                pathTool.pathCreator.pathUpdated -= OnPathModified;
                pathTool.pathCreator.pathUpdated += OnPathModified;
            }
        }

        bool TryFindPathCreator()
        {
            // Try find a path creator in the scene, if one is not already assigned
            if (pathTool.pathCreator == null)
            {
                if (pathTool.GetComponent<Core.Runtime.Objects.PathCreator>() != null)
                {
                    pathTool.pathCreator = pathTool.GetComponent<Core.Runtime.Objects.PathCreator>();
                }
                else if (FindObjectOfType<Core.Runtime.Objects.PathCreator>())
                {
                    pathTool.pathCreator = FindObjectOfType<Core.Runtime.Objects.PathCreator>();
                }
            }
            return pathTool.pathCreator != null;
        }
    }
}