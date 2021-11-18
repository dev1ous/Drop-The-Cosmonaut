using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public bool haveShield = false;

    private bool isAlive = true;
    public void TakeDamage()
    {
        if (haveShield)
            haveShield = false;
        else
            Death();
    }

    private void Death() => isAlive = false;
}
