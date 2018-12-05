using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public abstract class BaseSaveLoadComponent  : MonoBehaviour
{
    public abstract object Save();
    public abstract void Load(object @object);
}
