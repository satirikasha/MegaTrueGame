using UnityEngine;
using System.Collections;
using System;
using System.Linq;

public enum AmplifierType {
    Impact,
    Electric,
    Fire
}

[CreateAssetMenu(fileName = "WeaponAmplifier.asset", menuName = "Inventory System/Weapons/Amplifier", order = 0)]
public class WeaponAmplifierItemTemplate : WeaponPartItemTemplate {
    [Header("Weapon Amplifier")]
    public AmplifierType AmplifierType;
    public float AmplifierPower;
}

[Serializable]
public class WeaponAmplifierItem : WeaponPartItem<WeaponAmplifierItemTemplate> { }

