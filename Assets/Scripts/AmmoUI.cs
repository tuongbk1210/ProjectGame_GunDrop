using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoUI : MonoBehaviour
{
    public GameObject[] bullets;

    public void UpdateAmmo(int currentAmmo)
    {
        for (int i = 0; i < bullets.Length; i++)
        {
            if (i < currentAmmo)
            {
                bullets[i].SetActive(true);
            }
            else
            {
                bullets[i].SetActive(false);
            }
        }
    }
}
