using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ModularCharacterSlot
{
    public string name;
    public Transform parent;
    public GameObject item;

    public ModularCharacterSlot(string name,Transform parent,GameObject item)
    {
        this.name = name;
        this.parent = parent;
        this.item = item;
    }
}
