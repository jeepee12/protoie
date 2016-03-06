using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour
{

    public int StartHP;
    private const int maxHP = 12;

    //mettre prive quand les tests sont terminés
    public int HP;

    private bool isDying = false;
    private bool isRaising = false;
    private bool isRaisingCompleted = false;

    private Rigidbody myRigidBody;

    public Camera mainCamera;

    public Camera deathCam;

    private Vector3 spawnLocation;

    private float startRaising;

    private Vector3 deathCamOffset = new Vector3(- 2, 14.5f, - 31);

    public int getHP() { return HP; }
    public void AffectHP(int diff)
    {
        HP = HP + diff;

        if(HP > maxHP)
        {
            HP = maxHP;
        }
        else if(HP <= 0)
        {
            //on ne veut pas que les hp du joueurs soient en bas de 0
            HP = 0;
            Die();
        }
    }

    // Use this for initialization
    void Start()
    { 
        spawnLocation = GameObject.FindGameObjectWithTag("PlayerSpawn").transform.position;
        ResetPosition();
        HP = StartHP;
        myRigidBody = gameObject.GetComponent<Rigidbody>();
        deathCam.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(HP <= 0)
        {
            HP = 0;
            DyingAnimation();
        }

        if(isRaising)
        {
            RaisingAnimation();
        }
        else if(isRaisingCompleted && Time.time > startRaising + 3)
        {
            isRaisingCompleted = false;
            deathCam.enabled = false;
            mainCamera.enabled = true;
            gameObject.GetComponent<Navigation>().enabled = true;
            gameObject.GetComponent<SelectedWeapon>().enabled = true;
        }
    }

    void Die()
    {
        HP = maxHP;
        RaisingAnimation();

        GameObject.FindGameObjectWithTag("MapGenerator").GetComponent<MapGenerator>().RestartStage();
    }

    void ResetPosition()
    {
        gameObject.transform.position = spawnLocation;
    }

    void DyingAnimation()
    {
        if(!isDying)
        {
            gameObject.GetComponent<Navigation>().enabled = false;
            gameObject.GetComponent<SelectedWeapon>().enabled = false;
            mainCamera.enabled = false;
            deathCam.enabled = true;
            deathCam.transform.position = new Vector3(transform.position.x + deathCamOffset.x, transform.position.y + deathCamOffset.y, transform.position.z + deathCamOffset.z);
        }

        isDying = true;

        Vector3 D = new Vector3(transform.position.x, -30f, transform.position.z) - transform.position;
        float dist = D.magnitude;
        Vector3 pullDir = D.normalized;
        float pullF = 10;
        float pullForDist = (dist - 3) / 2.0f;
        if (pullForDist > 20) pullForDist = 20;
        pullF += pullForDist;

        myRigidBody.velocity += pullDir * (pullF * Time.deltaTime);

        Quaternion targetRotation = Quaternion.LookRotation(D);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime);

        if(transform.position.y <= -13f)
        {
            isDying = false;
            deathCam.transform.position = new Vector3(spawnLocation.x - 2, spawnLocation.y + 14.5f, spawnLocation.z - 31);
            Die();
        }
    }

    void RaisingAnimation()
    {
        if (!isRaising)
        { 
            gameObject.GetComponent<Floating>().enabled = false;
            gameObject.transform.position = new Vector3(spawnLocation.x, spawnLocation.y - 20f, spawnLocation.z);
        }

        isRaising = true;

        Vector3 D = new Vector3(transform.position.x, 10f, transform.position.z) - transform.position;
        myRigidBody.isKinematic = true;
        transform.position = Vector3.MoveTowards(transform.position, spawnLocation, 0.2f);

        Quaternion targetRotation = Quaternion.LookRotation(D);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime);

        if(transform.position.y >= 0)
        {
            myRigidBody.isKinematic = false;
            gameObject.GetComponent<Floating>().enabled = true;

            startRaising = Time.time;
            isRaising = false;
            isRaisingCompleted = true;
        }
    }
}
