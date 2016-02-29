using UnityEngine;
using System.Collections;

public class Navigation : MonoBehaviour
{

    private Vector3 m_ProgressivePosition;
    public float velocite;
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

    [Tooltip("Ranlentit pendant qu'il tourne")]
    public bool slowDuringTurning = true;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float valueH = Input.GetAxis("Horizontal");
        float valueV = Input.GetAxis("Vertical");
        bool backward = Input.GetButton("GoingBackward");
        float backwardFactor = 1;//va être mis à -1 si on appuie sur la touche de reculons
        Vector3 myVector;
        //Debug.Log("H:" + valueH + "V:" + valueV);
        myVector = new Vector3(1, 0, 0) * valueH;
        myVector += new Vector3(0, 0, 1) * valueV;

        myVector.Normalize();

        myVector *= 20;

        if (backward)
        {
            backwardFactor = -1;
            myVector *= -1;
        }

        directionBateau.transform.position = transform.position;
        directionBateau.transform.position += myVector;


        //on est en train de faire un input sur le controller
        if ((valueH > deadZone || valueH < -deadZone) || (valueV > deadZone || valueV < -deadZone))
        {
            m_ProgressivePosition = Vector3.Lerp(m_ProgressivePosition, directionBateau.transform.position, turnSpeed * Time.deltaTime);

            testAngle.transform.LookAt(m_ProgressivePosition);
            float monAngleAvantRotation = Quaternion.Angle(transform.rotation, testAngle.transform.rotation);

            if (Mathf.Abs(velocite) > speedMinToRotate)//si on ne peut pas trouner sur soi-même, on on vérifie qu'on va assez vite
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, testAngle.transform.rotation, turnSpeed * Time.deltaTime);
            }
            else if (monAngleAvantRotation > angleAvance) //le joueur ne va pas assez vite pour tourner et il veut touner donc on l'accélère. 
            {
                velocite += speed * Time.deltaTime * backwardFactor;
            }

            float monAngleApresRotation = Quaternion.Angle(transform.rotation, testAngle.transform.rotation);

            if (monAngleApresRotation < angleAvance)//si on pointe vers le devant du bateau 
                velocite += speed * Time.deltaTime * backwardFactor;
            else if (slowDuringTurning)//si on est dans le cas
            {
                velocite -= (0.75f * speedSlowing) * Time.deltaTime * backwardFactor;
                if (Mathf.Abs(velocite) < speedMinToRotate)
                //si on ne peut pas tourner sur sois-meme et qu'on est rendu plus lent que la vitesse minimal on va annuler le ralentissement
                {
                    velocite += (0.75f * speedSlowing) * Time.deltaTime * backwardFactor;
                }
            }

        }
        else//on fait pas d'input donc on ralentit
        {
            //Vu qu'on additionne ou soustrait tout le temps je suis conscient qu'on va jamais totalement arrêter
            //Mais un bateau serrait jamais totalement immobile. 
            if (velocite > 0)//si on est à 0 on ne va pas changer la vitesse
            {
                velocite -= speedSlowing * Time.deltaTime;
            }
            else if(velocite < 0)//on ralentit dans l'autre sens
            {
                velocite += speedSlowing * Time.deltaTime;
            }
        }

        if (velocite > speedMaxFoward)//on limite la vitesse
        {
            velocite = speedMaxFoward;
        }
        else if (velocite < -speedMaxBackward)
        {
            velocite = -speedMaxBackward;
        }


        transform.position += transform.forward * velocite;
    }
}
