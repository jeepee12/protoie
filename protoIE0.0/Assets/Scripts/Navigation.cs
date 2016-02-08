using UnityEngine;
using System.Collections;

public class Navigation : MonoBehaviour {

    private Vector3 m_ProgressivePosition;
    public float velocite;
    private float deadZone = 0.1f;

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
    [Tooltip("Vitesse maximum")]
    public float speedMax = 2.5f;
    [Tooltip("Vitesse minimum pour tourner")]
    public float speedMinToRotate = 0.25f;

    [Tooltip("Ranlentit pendant qu'il tourne")]
    public bool slowDuringTurning = true;

    // Use this for initialization
    void Start ()
    {
    }
	
	// Update is called once per frame
	void Update ()
    {
        float valueH = Input.GetAxis("Horizontal");
        float valueV = Input.GetAxis("Vertical");
        Vector3 myVector;

        myVector = new Vector3(1,0,0) * valueH;
        myVector += new Vector3(0, 0, 1) * valueV;

        myVector.Normalize();

        myVector *= 20;

        directionBateau.transform.position = transform.position;
        directionBateau.transform.position += myVector;

        //on est en train de faire un input sur le controller
        if ((valueH > deadZone || valueH < -deadZone) || (valueV > deadZone || valueV < -deadZone))
        {
            m_ProgressivePosition = Vector3.Lerp(m_ProgressivePosition, directionBateau.transform.position, turnSpeed * Time.deltaTime);

            testAngle.transform.LookAt(m_ProgressivePosition);
            float monAngleAvantRotation = Quaternion.Angle(transform.rotation, testAngle.transform.rotation);

            if (velocite > speedMinToRotate)//si on ne peut pas trouner sur soi-même, on on vérifie qu'on va assez vite
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, testAngle.transform.rotation, turnSpeed * Time.deltaTime);
            }
            else if (monAngleAvantRotation > angleAvance) //le joueur ne va pas assez vite pour tourner et il veut touner donc on l'accélère. 
            {
                velocite += speed * Time.deltaTime;
            }

            float monAngleApresRotation = Quaternion.Angle(transform.rotation, testAngle.transform.rotation);

            if (monAngleApresRotation < angleAvance)//si on pointe vers le devant du bateau 
                velocite += speed * Time.deltaTime;
            else if (slowDuringTurning)//si on est dans le cas
            {
                velocite -= (0.75f * speedSlowing) * Time.deltaTime;
                if (velocite < speedMinToRotate)
                //si on ne peut pas tourner sur sois-meme et qu'on est rendu plus lent que la vitesse minimal on va annuler le ralentissement
                {
                    velocite += (0.75f * speedSlowing) * Time.deltaTime;
                }
            }

        }
        else//on fait pas d'input donc on ralentit
            velocite -= speedSlowing * Time.deltaTime;

        if (velocite > speedMax)//on limite la vitesse
            velocite = speedMax;

        if (velocite < 0)//on peut juste avancer, on ne peut pas reculer
            velocite = 0;

        transform.position += transform.forward * velocite;
    }
}
