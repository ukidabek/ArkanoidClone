using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

[CustomEditor(typeof(SaveLoadManager))]
public class SaveLoadManagerEditor : Editor
{

    private SaveLoadManager _manager = null;

    private void OnEnable()
    {
        _manager = target as SaveLoadManager;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Find save/load components on scene"))
        {
            GameObject[] gameObjects = _manager.gameObject.scene.GetRootGameObjects();
            for (int i = 0; i < gameObjects.Length; i++)
            {
                BaseSaveLoadComponent[] saveLoadComponents = gameObjects[i].GetComponentsInChildren<BaseSaveLoadComponent>();
                if(saveLoadComponents != null && saveLoadComponents.Length > 0)
                {
                    foreach (var item in saveLoadComponents)
                    {
                        _manager.AddSaveLoadComponent(item);
                    }
                    SetAsDirty();
                }
            }
        }

        if (GUILayout.Button("Clear save/load components on scene"))
        {
            _manager.CleaarList();
            SetAsDirty();
        }
        EditorGUI.BeginDisabledGroup(!Application.isPlaying);
        {
            if (GUILayout.Button("Save"))
            {
                _manager.Save();
            }

            if (GUILayout.Button("Load"))
            {
                _manager.Load();
            }
        }
        EditorGUI.EndDisabledGroup();
        EditorGUI.BeginDisabledGroup(!_manager.SaveExists);
        {

            if (GUILayout.Button("Clear save file"))
            {
                _manager.Clear();
            }
        }
        EditorGUI.EndDisabledGroup();

        if (GUILayout.Button("Open persistent data path"))
        {
            System.Diagnostics.Process.Start("explorer.exe", _manager.Path.Replace(@"/", @"\"));
        }
    }

    private void SetAsDirty()
    {
        EditorUtility.SetDirty(_manager);
        EditorSceneManager.MarkSceneDirty(_manager.gameObject.scene);
    }
}
