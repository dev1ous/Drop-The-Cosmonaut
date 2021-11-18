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
        Death();
    }

    private bool Death()
    {
        if (haveShield)
            haveShield = false;
        else
        {
            isAlive = false;
            gameObject.SetActive(false);
            return true;
        }

        return false;
    }
}
