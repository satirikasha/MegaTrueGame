using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugTemp : MonoBehaviour {

    public InventoryItemTemplate Template;

    void Awake() {
        var item = (WeaponChasisItem)Template.GenerateItem();
        Debug.Log(item.Data.WeaponType);  
    }
}
