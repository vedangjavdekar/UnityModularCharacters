using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class ModularCharacterBase : MonoBehaviour
{
    public float gizmoSize;

    public List<ModularCharacterSlot> characterBaseSlots = new List<ModularCharacterSlot>();
    public List<ModularCharacterSlot> additionalSlots = new List<ModularCharacterSlot>();
    //public Dictionary<string, ModularCharacterSlot> characterBaseSlots = new Dictionary<string, ModularCharacterSlot>();
    //public Dictionary<string,ModularCharacterSlot> additionalSlots = new Dictionary<string, ModularCharacterSlot>();

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        foreach (var slot in characterBaseSlots)
        {
            Gizmos.DrawWireSphere(slot.item.transform.position, gizmoSize);
        }

        Gizmos.color = Color.green;
        foreach (var slot in additionalSlots)
        {
            if (slot.item != null)
            {
                Gizmos.DrawWireSphere(slot.item.transform.position, 1.2f*gizmoSize);
            }
        }
    }
}
