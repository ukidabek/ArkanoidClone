using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    public string Path { get { return string.Format("{0}/{1}", Application.persistentDataPath, "Save"); } }
    public string FileName = "AC.save";
    public bool SaveExists { get { return File.Exists(FullPath); } }
    public string FullPath { get { return string.Format("{0}/{1}", Path, FileName); } }
    
    [SerializeField] private List<BaseSaveLoadComponent> _objectToSaveLoadList = new List<BaseSaveLoadComponent>();

    public void CleaarList()
    {
        _objectToSaveLoadList.Clear();
    }

    public void AddSaveLoadComponent(BaseSaveLoadComponent saveLoadComponent)
    {
        if (saveLoadComponent != null && !_objectToSaveLoadList.Contains(saveLoadComponent))
            _objectToSaveLoadList.Add(saveLoadComponent);
    }

    public void Save()
    {
        if(!File.Exists(this.Path))
            Directory.CreateDirectory(Path);

        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream file = File.Open(FullPath, FileMode.Create);

        foreach (var item in _objectToSaveLoadList)
            binaryFormatter.Serialize(file, item.Save());

        file.Close();
    }

    public void Load()
    {
        if(File.Exists(FullPath))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream file = File.Open(FullPath, FileMode.Open);
            foreach (var item in _objectToSaveLoadList)
                item.Load(binaryFormatter.Deserialize(file));
            file.Close();
        }
    }

    public void Clear()
    {
        if(SaveExists)
            File.Delete(FullPath);
    }

    public void Load(int index)
    {
        if (File.Exists(FullPath))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream file = File.Open(FullPath, FileMode.Open);

            for (int i = 0; i < _objectToSaveLoadList.Count; i++)
            {
                object @object = binaryFormatter.Deserialize(file);
                if(i == index)
                {
                    _objectToSaveLoadList[i].Load(@object);
                    break;
                }
            }

            file.Close();
        }
    }
}
