using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairController : MonoBehaviour
{

    public Transform handGunPoint;
    public Transform shotGunPoint;
    public float distance = 4f;

    public WeaponManager weaponManager;

    SpriteRenderer spriteRenderer;


    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {

        if (weaponManager.weaponType == WeaponManager.WeaponType.Knife)
        {
            spriteRenderer.enabled = false;
            return;
        }
        else
        {
            spriteRenderer.enabled = true;

            if (weaponManager.weaponType == WeaponManager.WeaponType.Pistol)
            {
                transform.position = handGunPoint.position + handGunPoint.right * distance;
            }
            else if (weaponManager.weaponType == WeaponManager.WeaponType.Shotgun)
            {
                transform.position = shotGunPoint.position + shotGunPoint.right * distance;
            }
        }
    }
}
