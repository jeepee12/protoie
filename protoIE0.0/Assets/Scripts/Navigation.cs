using UnityEngine;
using System.Collections;

public class Navigation : MonoBehaviour
{

    private Vector3 m_ProgressivePosition;
    public float boatSpeed;
    public float deadZone = 0.5f;

    public GameObject testAngle;
    public Transform directionBateau;
    [Tooltip("Vitesse a laquel le bateau tourne")]
    public float turnSpeed = 250.0f;
    [Tooltip("Angle a laquel le bateau commence a avancer")]
    public float angleAvance = 15.0f;
    [Tooltip("Vitesse a laquel le bateau avance")]
    public float speed = 0.25f;
    [Tooltip("Vitesse de ralentisement")]
    public float speedSlowing = 1.0f;
    [Tooltip("Vitesse maximum vers l'avant")]
    public float speedMaxFoward = 2.5f;
    [Tooltip("Vitesse maximum vers l'arriere")]
    public float speedMaxBackward = 1.5f;
    [Tooltip("Vitesse minimum pour tourner")]
    public float speedMinToRotate = 0.25f;

    [Tooltip("Vitesse minimum pour tourner")]
    public GameObject mainCam;

    [Tooltip("Ranlentit pendant qu'il tourne")]
    public bool slowDuringTurning = true;

    private Rigidbody rb;
    public int velociteFactor = 1000;

    //variable for the affecting the boatspeed by the current velocity of the boat
    private float lastVelocity = 0;
    private float velocityProportion = 0;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float valueH = Input.GetAxis("Horizontal");
        float valueV = Input.GetAxis("Vertical");
        bool backward = Input.GetButton("GoingBackward");
        float backwardFactor = 1;//va être mis à -1 si on appuie sur la touche de reculons
        Vector3 myVector;

        myVector = mainCam.transform.forward * valueV;
        myVector += mainCam.transform.right * valueH;

        myVector.Normalize();

        myVector *= 20;

        if (backward)
        {
            backwardFactor = -1;
            myVector *= -1;
            
        }

        if (rb.velocity.magnitude < 0.1)//si le bateau c'est fait arrêter par une source externe, ça vitesse doit retomber à zéro
        {
            boatSpeed = 0;
        }
        //test
        /*else if (lastVelocity != 0)
        {
            velocityProportion = rb.velocity.magnitude / lastVelocity;
            //if (velocityProportion < 0.8f || velocityProportion > 1.2f)
            if (velocityProportion < 0.8f)
            {
                boatSpeed = (velocityProportion) * boatSpeed;
            }
            //si la velocité a changer par un facteur externe, l'acceleration du bateau va etre changer proportionnelement
        }*/




        directionBateau.transform.position = transform.position;
        directionBateau.transform.position += myVector;

        //on est en train de faire un input sur le controller
        if ((valueH > deadZone || valueH < -deadZone) || (valueV > deadZone || valueV < -deadZone))
        {
            m_ProgressivePosition = Vector3.Lerp(m_ProgressivePosition, directionBateau.transform.position, turnSpeed * Time.deltaTime);

            testAngle.transform.LookAt(m_ProgressivePosition);
            float monAngleAvantRotation = Quaternion.Angle(transform.rotation, testAngle.transform.rotation);

            if (Mathf.Abs(boatSpeed) > speedMinToRotate)//si on ne peut pas trouner sur soi-même, on on vérifie qu'on va assez vite
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, testAngle.transform.rotation, turnSpeed * Time.deltaTime);
            }
            else if (monAngleAvantRotation > angleAvance) //le joueur ne va pas assez vite pour tourner et il veut touner donc on l'accélère. 
            {
                boatSpeed += speed * Time.deltaTime * backwardFactor;
            }

            float monAngleApresRotation = Quaternion.Angle(transform.rotation, testAngle.transform.rotation);

            if (monAngleApresRotation < angleAvance)//si on pointe vers le devant du bateau 
                boatSpeed += speed * Time.deltaTime * backwardFactor;
            else if (slowDuringTurning)//si on est dans le cas
            {
                boatSpeed -= (0.75f * speedSlowing) * Time.deltaTime * backwardFactor;
                if (Mathf.Abs(boatSpeed) < speedMinToRotate)
                //si on ne peut pas tourner sur sois-meme et qu'on est rendu plus lent que la vitesse minimal on va annuler le ralentissement
                {
                    boatSpeed += (0.75f * speedSlowing) * Time.deltaTime * backwardFactor;
                }
            }

        }
        else//on fait pas d'input donc on ralentit
        {
            if (backward)//dès que le personne appuie sur le boutton pour reculer, le bateau recule même si elle ne touche pas au joystick
            {
                boatSpeed += speed * Time.deltaTime * backwardFactor;
            }
            else
            {
                //Vu qu'on additionne ou soustrait tout le temps je suis conscient qu'on va jamais totalement arrêter
                //Mais un bateau serrait jamais totalement immobile. 
                if (boatSpeed > 0)//si on est à 0 on ne va pas changer la vitesse
                {
                    boatSpeed -= speedSlowing * Time.deltaTime;
                }
                else if (boatSpeed < 0)//on ralentit dans l'autre sens
                {
                    boatSpeed += speedSlowing * Time.deltaTime;
                }
            }
        }

        if (boatSpeed > speedMaxFoward)//on limite la vitesse
        {
            boatSpeed = speedMaxFoward;
        }
        else if (boatSpeed < -speedMaxBackward)
        {
            boatSpeed = -speedMaxBackward;
        }


        //transform.position += transform.forward * boatAccelaration;
        rb.AddForce(transform.forward * boatSpeed * velociteFactor);
        //rb.velocity = transform.forward * velociteFactor * boatAccelaration;
        if (boatSpeed == speedMaxFoward)
        {
            //Debug.Log("Boat speed a max speed" + rb.velocity.magnitude);
        }

        //lastVelocity = rb.velocity.magnitude;
    }


    //à tester plus ne profondeur, permet de faire ne sorte que la variable boatspeed est toujours proportionnel à la vrai vitesse du bateau
    //le pour moment, ça rend la navigation un peu moins fluide, donc on va pas le mettre en place tout suite
    /*
    void FixedUpdate()
    {
        if (lastVelocity != 0)
        {
            velocityProportion = rb.velocity.magnitude / lastVelocity;
            if (velocityProportion < 0.8f || velocityProportion > 1.2f)
            boatSpeed = (velocityProportion) * boatSpeed;
            //si la velocité a changer par un facteur externe, l'acceleration du bateau va etre changer proportionnelement
        }

        lastVelocity = rb.velocity.magnitude;
    }
    */

}
