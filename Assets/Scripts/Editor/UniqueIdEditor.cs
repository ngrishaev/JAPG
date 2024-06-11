using System;
using System.Linq;
using Game;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(UniqueId))]
    public class UniqueIdEditor : UnityEditor.Editor
    {
        private void OnEnable()
        {
            var uniqueId = (UniqueId) target;
            
            if(IsPrefab(uniqueId))
                return;
            
            if (string.IsNullOrEmpty(uniqueId.Id))
            {
                SetNewId(uniqueId);
            }
            else
            {
                var ids = FindObjectsOfType<UniqueId>();
                if (ids.Any(other => other != uniqueId && other.Id == uniqueId.Id))
                    SetNewId(uniqueId);
            }
        }

        private bool IsPrefab(UniqueId uniqueId)
        {
            return uniqueId.gameObject.scene.rootCount == 0;
        }

        private void SetNewId(UniqueId uniqueId)
        {
            uniqueId.Id = $"{uniqueId.gameObject.scene.name}_{Guid.NewGuid().ToString()}";
            if (Application.isPlaying)
                return;
            
            EditorUtility.SetDirty(uniqueId);
            EditorSceneManager.MarkSceneDirty(uniqueId.gameObject.scene);
        }
    }
}