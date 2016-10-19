using UnityEngine;
using System.Collections;

public abstract class WeaponDisplay : MonoBehaviour
{
    protected MeshRenderer CooldownRenderer;

    // Cooldowns
    public Color CouleurCooldown = new Color(1, 0, 0, 0);
    public GameObject CooldownObject;

    // Matériel pour le visuel
    public Material cannonCharging;
    public Material cannonReady;

    // Display
    public MeshRenderer displayPlane;
    
    public abstract Vector3 ShootVector();
    public abstract void move();

    private bool weapReady;

    public virtual bool inRange() { return true; }

    public virtual void Init()
    {
        CooldownRenderer = CooldownObject.GetComponent<MeshRenderer>();
        Hide();
    }

    public abstract void Hide();

    public virtual void Show() { displayCooldown(0); }

    public void displayCooldown(float pourc)
    {

            Mesh CooldownMesh = CooldownObject.GetComponent<MeshFilter>().mesh;
            Vector3[] vertices = CooldownMesh.vertices;
            Color[] colors = new Color[vertices.Length];

            float nbr = CooldownMesh.vertices.Length * pourc;

            for (int i = 0; i < Mathf.Min(nbr, CooldownMesh.vertices.Length); i++)
            {
                colors[i] = CouleurCooldown;
            }

            CooldownMesh.colors = colors;

        if (pourc != 0)
        { 
            weapReady = false;
            onCooldown();
        }
        else
        {
            weapReady = true;
            readyToFire();
        }
    }

    public void readyToFire()
    {
        if (weapReady)
            displayPlane.material = cannonReady;
    }

    public void onCooldown()
    {
        displayPlane.material = cannonCharging;
    }

}