using UnityEngine;
using System.Collections;

public class WeaponDisplay : MonoBehaviour
{
    public GameObject cooldownDisplay;

    public Material cannonCharging;
    public Material cannonReady;

    public MeshRenderer displayPlane;
    public MeshRenderer cooldownPlane;

    public MeshRenderer area;

    private Vector3 scaleDepart;

    public virtual void move() { }

    public void Init()
    {
        scaleDepart = cooldownDisplay.transform.localScale;
    }

    public void stopDisplay()
    {
        displayPlane.enabled = false;
        cooldownPlane.enabled = false;

        if (area != null)
            area.enabled = false;
    }

    public void startDisplay()
    {
        displayPlane.enabled = true;
        cooldownPlane.enabled = true;

        if (area != null)
            area.enabled = true;
    }

    public void displayCooldown(float pourc)
    {
        if (pourc > scaleDepart.z)
            pourc = scaleDepart.z;

        cooldownDisplay.transform.localScale = new Vector3(scaleDepart.x, scaleDepart.y, pourc);
    }

    public void readyToFire()
    {
        displayPlane.material = cannonReady;
    }

    public void onCooldown()
    {
        displayPlane.material = cannonCharging;
    }
}