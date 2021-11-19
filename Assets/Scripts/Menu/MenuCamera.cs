using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCamera : MonoBehaviour
{
    public enum Lookat { MAINMENU, OPTION }
    
    [SerializeField] private Camera usedCamera = null;
    private Lookat lookat = Lookat.MAINMENU;

    void Update()
    {
        if (lookat == Lookat.MAINMENU)
        {
            usedCamera.transform.rotation = Quaternion.Euler(0f, Mathf.MoveTowardsAngle(usedCamera.transform.rotation.eulerAngles.y, 0f, 180f * Time.deltaTime), 0f);
        }
        else if (lookat == Lookat.OPTION)
        {
            usedCamera.transform.rotation = Quaternion.Euler(0f, Mathf.MoveTowardsAngle(usedCamera.transform.rotation.eulerAngles.y, 90, 180f * Time.deltaTime), 0f);
        }
    }

    public void LookMainMenu()
    {
        lookat = Lookat.MAINMENU;
    }

    public void LookOptionMenu()
    {
        lookat = Lookat.OPTION;
    }
}
