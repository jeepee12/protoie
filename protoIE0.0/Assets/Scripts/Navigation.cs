using UnityEngine;
using System.Collections;

public class Navigation : MonoBehaviour {

    private Vector3 m_ProgressivePosition;
    private float velocite;
    private float deadZone = 0.1f;

    public GameObject testAngle;
    public Transform directionBateau;
    [Tooltip("Vitesse a laquel le bateau tourne")]
    public float turnSpeed = 2.0f;
    [Tooltip("Angle a laquel le bateau commence a avancer")]
    public float angleAvance = 5.0f;
    [Tooltip("Vitesse a laquel le bateau avance")]
    public float speed = 5.0f;
    [Tooltip("Vitesse de ralentisement")]
    public float speedSlowing = 5.0f;
    [Tooltip("Vitesse maximum")]
    public float speedMax = 10.0f;

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

        if ((valueH > deadZone || valueH < -deadZone) || (valueV > deadZone || valueV < -deadZone))
        {
            m_ProgressivePosition = Vector3.Lerp(m_ProgressivePosition, directionBateau.transform.position, turnSpeed * Time.deltaTime);

            testAngle.transform.LookAt(m_ProgressivePosition);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, testAngle.transform.rotation, turnSpeed * Time.deltaTime);

            float monAngle = Quaternion.Angle(transform.rotation, testAngle.transform.rotation);

            if (monAngle < angleAvance)
                velocite += speed * Time.deltaTime;
            else
                velocite -= (0.75f * speedSlowing) * Time.deltaTime;
        }
        else
            velocite -= speedSlowing * Time.deltaTime;

        if (velocite > speedMax)
            velocite = speedMax;

        if (velocite < 0)
            velocite = 0;

        transform.position += transform.forward * velocite;
    }
}
