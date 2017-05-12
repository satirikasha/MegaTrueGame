using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[Serializable]
public class WeaponData {
    public WeaponType WeaponType;
    public BarrelType BarrelType;
    public BulletType BulletType;
    public AmplifierType AmplifierType;
    public float Damage;
    public float PushForce;
    public float Precision;
    public float MoveSpeed;
    public float BulletSpeed;
    public float RechargeTime;
    public float CooldownTime;
    public float AimOffset;
    public float AimAssistance;
    public float AmplifierPower;
    public int ClipSize;
}

