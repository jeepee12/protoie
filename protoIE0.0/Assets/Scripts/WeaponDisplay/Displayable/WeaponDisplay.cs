using UnityEngine;
using System.Collections;

public abstract class WeaponDisplay : MonoBehaviour
{
    private MeshRenderer CooldownRenderer;

    // Cooldowns
    public Color CouleurCooldown = new Color(1, 0, 0, 0);
    public GameObject CooldownObject;

    // Matériel pour le visuel
    public Material cannonCharging;
    public Material cannonReady;

    // Display
    public MeshRenderer displayPlane;

    // Area
    public MeshRenderer area;

    public abstract Vector3 ShootVector();
    public abstract void move();

    public void Init()
    {
        CooldownRenderer = CooldownObject.GetComponent<MeshRenderer>();
    }

    public void stopDisplay()
    {
        displayPlane.enabled = false;
        CooldownRenderer.enabled = false;

        if (area != null)
            area.enabled = false;
    }

    public void startDisplay()
    {
        displayPlane.enabled = true;
        CooldownRenderer.enabled = true;

        if (area != null)
            area.enabled = true;
    }

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