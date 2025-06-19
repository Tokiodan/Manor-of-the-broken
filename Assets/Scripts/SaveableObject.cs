using UnityEngine;

public abstract class SaveableObject : MonoBehaviour
{
    public abstract void Save();
    public abstract void Load();
}
