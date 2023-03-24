using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemDataObject : ScriptableObject
{
    public string id;
    public Sprite icon;

    public abstract void OnDrop(int amount);
}
