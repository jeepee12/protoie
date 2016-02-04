using UnityEngine;
using System.Collections;

public class SelectedWeapon : MonoBehaviour {
    
    private WeaponDisplay currentWeapon;

    public WeaponDisplay[] weaponList;

    public float Cooldown;

    private bool ready = true;
    private float currentCooldown;

    // Use this for initialization
    void Start ()
    {
        ready = false;
        currentCooldown = 0;
        currentWeapon = weaponList[0];

        for (int i = 0; i < weaponList.Length; i++)
            weaponList[i].Init();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetButtonDown("SwitchWeaponLeft"))
            switchWeaponLeft();

        if (Input.GetButtonDown("SwitchWeaponRight"))
            switchWeaponLeft();

        if (!ready)
        {
            currentCooldown += Time.deltaTime;

            currentWeapon.displayCooldown(currentCooldown / Cooldown);

            if (currentCooldown > Cooldown)
            {
                ready = true;
                currentWeapon.displayCooldown(0);
                currentWeapon.readyToFire();
            }
        }

	}

    void switchWeaponLeft()
    {
        int posCurrWeapon = 0;

        for (int i = 0; i < weaponList.Length; i++)
            if (weaponList[i] == currentWeapon)
            { 
                posCurrWeapon = i;
                break;
            }

        currentWeapon.stopDisplay();

        if (posCurrWeapon == 0)
            currentWeapon = weaponList[weaponList.Length - 1];
        else
            currentWeapon = weaponList[posCurrWeapon - 1];

        currentWeapon.startDisplay();

    }

    void switchWeaponRight()
    {
        int posCurrWeapon = 0;

        for (int i = 0; i < weaponList.Length; i++)
            if (weaponList[i] == currentWeapon)
            {
                posCurrWeapon = i;
                break;
            }

        currentWeapon.stopDisplay();

        if (posCurrWeapon == weaponList.Length - 1)
            currentWeapon = weaponList[0];
        else
            currentWeapon = weaponList[posCurrWeapon + 1];

        currentWeapon.startDisplay();

    }
}
