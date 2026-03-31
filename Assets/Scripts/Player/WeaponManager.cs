using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    AnimationController animationController;
    SoundController soundController;

    [Header("Audio Clip change weapon")]
    [SerializeField]
    AudioClip switchHandGun;
    [SerializeField]
    AudioClip switchKnife;
    [SerializeField]
    AudioClip switchShotGun;

    [Header("Audio Clip attack")]
    [SerializeField]
    AudioClip attackHandGun;
    [SerializeField]
    AudioClip attackKnife;
    [SerializeField]
    AudioClip attackShotGun;

    [SerializeField]
    GameObject bulletPrefab;
    [SerializeField]
    GameObject gunPoint;
    [SerializeField]
    GameObject shotGunPoint;
    [SerializeField]
    GameObject muzzlePrefab;

    public float attackCooldown = 0.4f;
    public float knifeHitTime = 0.2f;

    public enum WeaponType
    {
        Knife,
        Pistol,
        Shotgun
    }

    public WeaponType weaponType;
    private WeaponType checkWeaponType;

    bool canAttack = true;

    [Header("Ammo")]
    public int currentAmmo;
    int pistoAmmo;
    int shotgunAmmo;
    public int maxAmmo = 6;
    int totalAmmoUsed = 0;

    [SerializeField]
    AmmoUI ammoUI;
    [SerializeField]
    GameObject bulletManager;

    void Awake()
    {
        weaponType = WeaponType.Pistol;
        animationController = GetComponent<AnimationController>();
        soundController = GetComponent<SoundController>();
    }
    void Start()
    {
        pistoAmmo = maxAmmo;
        shotgunAmmo = maxAmmo;
        totalAmmoUsed = 0;
        ammoUI.UpdateAmmo(GetCurrrentAmmo());
    }

    int GetCurrrentAmmo()
    {
        if (weaponType == WeaponType.Pistol)
            return pistoAmmo;
        if (weaponType == WeaponType.Shotgun)
            return shotgunAmmo;
        return 0;
    }

    void useAmmo()
    {
        if (weaponType == WeaponType.Pistol)
        {
            pistoAmmo--;
        }
        else if (weaponType == WeaponType.Shotgun)
        {
            shotgunAmmo--;
        }
        totalAmmoUsed++;
        CheckResetAmmo();
    }

    void CheckResetAmmo()
    {
        if (totalAmmoUsed >= 12)
        {
            totalAmmoUsed = 0;
            if (weaponType == WeaponType.Pistol)
            {
                shotgunAmmo = maxAmmo;
            }
            else if (weaponType == WeaponType.Shotgun)
            {
                pistoAmmo = maxAmmo;
            }
        }
    }

    public void changeKnife()
    {
        if (canAttack)
        {
            if (weaponType != WeaponType.Knife)
            {
                checkWeaponType = weaponType;
                soundController.PlayAudio(switchKnife);
            }
            bulletManager.SetActive(false);
            weaponType = WeaponType.Knife;
            animationController.Move(0.5f);
            animationController.Idle(1);
        }
    }
    public void changeHandGun()
    {
        bulletManager.SetActive(true);

        if (!canAttack) return;
        if (shotgunAmmo == 0 && pistoAmmo == 0 && checkWeaponType != WeaponType.Pistol)
        {
            pistoAmmo = 6;
        }

        if (weaponType != WeaponType.Pistol)
        {
            soundController.PlayAudio(switchHandGun);
        }

        weaponType = WeaponType.Pistol;
        ammoUI.UpdateAmmo(GetCurrrentAmmo());

        animationController.Move(0.5f);
        animationController.Idle(0);

    }
    public void changeShotGun()
    {
        bulletManager.SetActive(true);

        if (!canAttack) return;

        if (weaponType != WeaponType.Shotgun)
        {
            soundController.PlayAudio(switchShotGun);
        }

        weaponType = WeaponType.Shotgun;
        if (pistoAmmo == 0 && shotgunAmmo == 0 && checkWeaponType != WeaponType.Shotgun)
        {
            shotgunAmmo = 6;
        }
        ammoUI.UpdateAmmo(GetCurrrentAmmo());

        animationController.Move(0.5f);
        animationController.Idle(3);
    }

    public void playerAttack()
    {
        if (!canAttack) return;
        if (weaponType != WeaponType.Knife && GetCurrrentAmmo() <= 0)
        {
            return;
        }
        canAttack = false;
        animationController.PlayerAttack();
        Attack();
        if (weaponType != WeaponType.Knife)
        {
            useAmmo();
            ammoUI.UpdateAmmo(GetCurrrentAmmo());
        }
        Invoke("EndAttack", attackCooldown);
    }

    public void Attack()
    {
        switch (weaponType)
        {
            case WeaponType.Pistol:
                HandGunAttack();
                break;
            case WeaponType.Knife:
                KnifeAttack();
                break;
            case WeaponType.Shotgun:
                ShotGunAttack();
                break;
        }
    }
    public void HandGunAttack()
    {
        soundController.PlayAudio(attackHandGun);
        GameObject vfx = Instantiate(muzzlePrefab, gunPoint.transform.position, gunPoint.transform.rotation);
        vfx.layer = gameObject.layer;
        GameObject bullet = Instantiate(bulletPrefab, gunPoint.transform.position, gunPoint.transform.rotation);
        ManagerBullet bulletScript = bullet.GetComponent<ManagerBullet>();
        AnimatedTexture vfxScript = vfx.GetComponent<AnimatedTexture>();
        bullet.layer = gameObject.layer;
        SpriteRenderer playerSR = GetComponent<SpriteRenderer>();
        bulletScript.weaponType = weaponType;
        bulletScript.SetLayer(playerSR);
        vfxScript.SetLayer(playerSR);
    }
    public void ShotGunAttack()
    {
        soundController.PlayAudio(attackShotGun);
        GameObject vfx = Instantiate(muzzlePrefab, shotGunPoint.transform.position, shotGunPoint.transform.rotation);
        vfx.layer = gameObject.layer;
        GameObject bullet = Instantiate(bulletPrefab, shotGunPoint.transform.position, shotGunPoint.transform.rotation);
        ManagerBullet bulletScript = bullet.GetComponent<ManagerBullet>();
        AnimatedTexture vfxScript = vfx.GetComponent<AnimatedTexture>();
        bullet.layer = gameObject.layer;
        SpriteRenderer playerSR = GetComponent<SpriteRenderer>();
        bulletScript.weaponType = weaponType;
        bulletScript.SetLayer(playerSR);
        vfxScript.SetLayer(playerSR);
    }
    public void KnifeAttack()
    {
        soundController.PlayAudio(attackKnife);
    }
    public void EndAttack()
    {
        canAttack = true;
    }

}
