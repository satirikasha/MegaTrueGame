using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponSocketType {
    Rifle,
    Stock,
    Scope
}

public class WeaponSocket : Socket {

    public WeaponSocketType SocketType;
}
