using UnityEngine;

public class DropData: ScriptableObject
{
    public string ItemName;

    public float Chance;
    
    public virtual GameObject Create(GameObject to)
    {
        return null;
    }
}