using UnityEngine;
using System.Collections;

public class SelectedWeapon : MonoBehaviour
{

    [System.Serializable]
    public class Weapons
    {
        public string name;
        public GameObject projectile;
        public Transform[] cannonHole;
        public GameObject cannonFireEffect;
        public float fireRate;
        public float Cooldown = 0;
        [System.NonSerialized]
        public float curCooldown = 0;
        public bool ready = true;
    }

    public Weapons[] weapons;

    private WeaponDisplay currentWeapon;

    public WeaponDisplay[] weaponList;
    
    private int posCurrWeapon;
    private int cannonTurn;

    // Use this for initialization
    void Start()
    {
        posCurrWeapon = 0;

        currentWeapon = weaponList[posCurrWeapon];


        for (int i = 0; i < weaponList.Length; i++)
            weaponList[i].Init();

        currentWeapon.Show();
        cannonTurn = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetButton("Fire1") || Input.GetAxis("Fire1") > 0) && weapons[posCurrWeapon].ready)
        {
            InvokeRepeating("Attack", 0, Random.Range(0.1F,0.5F));
            weapons[posCurrWeapon].curCooldown = 0;
            weapons[posCurrWeapon].ready = false;
        }

        //Left 1 Bumper
        if (Input.GetButtonDown("SwitchWeaponLeft"))
            switchWeaponLeft();

        //Right 1 Bumper
        if (Input.GetButtonDown("SwitchWeaponRight"))
            switchWeaponRight();

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


        currentWeapon.move();

        for (int i = 0; i < weapons.Length; i++)
        {
            if (weapons[i].curCooldown < weapons[i].Cooldown)
                weapons[i].curCooldown += Time.deltaTime;
        }

        if (!weapons[posCurrWeapon].ready)
        {
            currentWeapon.displayCooldown(weapons[posCurrWeapon].curCooldown / weapons[posCurrWeapon].Cooldown);

            if (weapons[posCurrWeapon].curCooldown > weapons[posCurrWeapon].Cooldown)
            {
                weapons[posCurrWeapon].ready = true;
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

    void Attack()
    {
        if (cannonTurn >= weapons[posCurrWeapon].cannonHole.Length)
        {
            cannonTurn = 0;
            CancelInvoke();
            return;
        }

        GameObject projectile =
        (GameObject)Instantiate(weapons[posCurrWeapon].projectile,
                                weapons[posCurrWeapon].cannonHole[cannonTurn].position,
                                weapons[posCurrWeapon].cannonHole[cannonTurn].rotation);

        if (projectile.name.Contains("Oil"))
        {
            ++cannonTurn;
            //Where to end
            projectile.GetComponent<OilBarrel>().GiveTarget(weapons[posCurrWeapon].cannonHole[cannonTurn].transform);
        }

        if (projectile.name.Contains("Fire"))
        {
            projectile.transform.parent = weapons[posCurrWeapon].cannonHole[cannonTurn].transform;
        }

        if (weapons[posCurrWeapon].cannonFireEffect)
        {
            Instantiate(weapons[posCurrWeapon].cannonFireEffect, weapons[posCurrWeapon].cannonHole[cannonTurn].position, weapons[posCurrWeapon].cannonHole[cannonTurn].rotation);
        }

        ++cannonTurn;
    }

    private void ShowCurrentWeapon(int currentWeaponInt)
    {
        posCurrWeapon = currentWeaponInt;
        currentWeapon.Hide();
        currentWeapon = weaponList[posCurrWeapon];
        currentWeapon.Show();
    }
}
