using UnityEngine;
using System.Collections;

public class Cyborg : EstadisticasBase
{

    public override void Awake()
    {
        base.Awake();
        Vida += 100;
        Fuerza = 10;
        Speed = 20;
        SpeedJump = 10;
        SaltaAlDragonPunch = false;
    }

}
