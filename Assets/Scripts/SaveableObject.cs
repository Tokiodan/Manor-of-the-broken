using UnityEngine;

public abstract class SaveableObject : MonoBehaviour
{
    // base class for anything that can be saved/loaded
    public abstract void Save();
    public abstract void Load();
}
