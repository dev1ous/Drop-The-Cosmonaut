using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCamera : MonoBehaviour
{
    public enum Lookat { MAINMENU, OPTION, CREDIT }

    [SerializeField] private Camera usedCamera = null;
    private Lookat lookat = Lookat.MAINMENU;

    [SerializeField] private GameObject[] obstacles = null;
    private int usedIndex = -1;
    private float lerpTimer;
    private float randomY = 0f;
    private float randomDelay = 0f;

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
        else if (lookat == Lookat.CREDIT)
        {
            usedCamera.transform.rotation = Quaternion.Euler(0f, Mathf.MoveTowardsAngle(usedCamera.transform.rotation.eulerAngles.y, -90, 180f * Time.deltaTime), 0f);
        }

        if (usedIndex == -1 && obstacles.Length != 0)
        {
            randomY = Random.Range(-300f, 300f);
            usedIndex = Random.Range(0, obstacles.Length);
            lerpTimer = 0f;
            randomDelay = Random.Range(0f, 0.5f);
        }
        else
        {
            lerpTimer += Time.deltaTime / 4.5f;
            obstacles[usedIndex].transform.localPosition = Vector3.Lerp(new Vector3(275, randomY, 550), new Vector3(-275, randomY, 550), lerpTimer);
            obstacles[usedIndex].transform.Rotate(new Vector3(30f, 30f, 30f) * Time.deltaTime);

            if (lerpTimer >= 1f + randomDelay)
            {
                obstacles[usedIndex].transform.localPosition = new Vector3(0f, 0f, -100f);
                usedIndex = -1;
            }
        }
    }

    public void LookMainMenu()
    {
        lookat = Lookat.MAINMENU;
    }

    public void LookOptionToMainMenu()
    {
        lookat = Lookat.MAINMENU;
        OptionMenu.Save();
    }

    public void LookOptionMenu()
    {
        lookat = Lookat.OPTION;
    }

    public void LookCredits()
    {
        lookat = Lookat.CREDIT;
    }
}
