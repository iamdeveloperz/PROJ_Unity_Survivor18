using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="DefaultItemData", menuName ="New Item/Default", order= 0)]
public class ItemData : ScriptableObject
{
    public string displayName;
    public string description;
    public Sprite icon;
}
