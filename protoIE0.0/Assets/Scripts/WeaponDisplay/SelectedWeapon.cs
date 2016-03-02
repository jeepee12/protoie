using UnityEngine;
using System.Collections;

public class SelectedWeapon : MonoBehaviour {

	[System.Serializable]
	public class Weapons
	{
		public string name;
		public GameObject projectile;
		public Transform[] cannonHole;
		public GameObject cannonFireEffect;
		public float fireRate;
        [System.NonSerialized]
        public float cooldown = 0;
	}

	public Weapons[] weapons;

	private WeaponDisplay currentWeapon;

	public WeaponDisplay[] weaponList;

	public float Cooldown;

	private bool ready;
	private float currentCooldown;
    private int posCurrWeapon;

    // Use this for initialization
    void Start ()
	{
		ready = false;
		currentCooldown = 0;
        posCurrWeapon = 0;
        currentWeapon = weaponList[posCurrWeapon];

        for (int i = 0; i < weaponList.Length; i++)
			weaponList[i].Init();

		currentWeapon.Show();
	}
	
	// Update is called once per frame
	void Update ()
	{
        if ((Input.GetButton("Fire1") || Input.GetAxis("Fire1") > 0) && Time.time > weapons[posCurrWeapon].cooldown)
        {
            for (int i = 0; i < weapons[posCurrWeapon].cannonHole.Length; ++i)
            {
                //Offset for test the oil will be thrown
                Vector3 offset = new Vector3(0, 0, 0);
                if (weapons[posCurrWeapon].name.Contains("Oil"))
                {
                    offset.y = 10;
                }
                GameObject projectile = 
               (GameObject)Instantiate(weapons[posCurrWeapon].projectile, 
                                       weapons[posCurrWeapon].cannonHole[i].position + offset, 
                                       weapons[posCurrWeapon].cannonHole[i].rotation); //as GameObject;

                if (projectile.name.Contains("Fire"))
                {
                    projectile.transform.parent = gameObject.transform;
                }

                if (weapons[posCurrWeapon].cannonFireEffect)
                {
                    Instantiate(weapons[posCurrWeapon].cannonFireEffect, weapons[posCurrWeapon].cannonHole[i].position, weapons[posCurrWeapon].cannonHole[i].rotation);
                }
            }

            weapons[posCurrWeapon].cooldown = Time.time + weapons[posCurrWeapon].fireRate;
        }

        //Left
        if (Input.GetAxis("WeaponSelectDpadH") < 0)
        {
            ShowCurrentWeapon(1);
        }
        //Right
		if (Input.GetAxis("WeaponSelectDpadH") > 0)
        {
            ShowCurrentWeapon(0);
        }
        //Up
        if (Input.GetAxis("WeaponSelectDpadV") > 0)
        {
            ShowCurrentWeapon(2);
        }
        //Down
        if (Input.GetAxis("WeaponSelectDpadV") < 0)
        {
            ShowCurrentWeapon(3);
        }

        if (Input.GetButtonDown("SwitchWeaponLeft"))
            switchWeaponLeft();

        if (Input.GetButtonDown("SwitchWeaponRight"))
            switchWeaponRight();



        currentWeapon.move();

		if (!ready)
		{
			currentCooldown += Time.deltaTime;

			currentWeapon.displayCooldown(currentCooldown / Cooldown);

			if (currentCooldown > Cooldown)
			{
				ready = true;
				currentWeapon.displayCooldown(0);
			}
		}

	}

	void switchWeaponLeft()
	{
		if (posCurrWeapon == 0)
        {
            posCurrWeapon = weaponList.Length - 1;
        }
		else
        {
            posCurrWeapon = posCurrWeapon - 1;
        }

        ShowCurrentWeapon(posCurrWeapon);
    }

	void switchWeaponRight()
    { 
        if (posCurrWeapon == weaponList.Length - 1)
        {
            posCurrWeapon = 0;
        }
		else
        {
            posCurrWeapon = posCurrWeapon + 1;
        }

        ShowCurrentWeapon(posCurrWeapon);
	}

    private void ShowCurrentWeapon(int currentWeaponInt)
    {
        posCurrWeapon = currentWeaponInt;
        currentWeapon.Hide();
        currentWeapon = weaponList[posCurrWeapon];
        currentWeapon.Show();
    }
}
