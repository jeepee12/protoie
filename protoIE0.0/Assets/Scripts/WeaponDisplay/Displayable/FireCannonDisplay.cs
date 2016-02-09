using UnityEngine;
using System.Collections;

public class FireCannonDisplay : WeaponDisplay {
    /*
    public Color maCouleur = new Color(1, 0, 0, 1);
    public Color maCouleurTransparente = new Color(1, 0, 0, 0.5f);

    private float pourc;
    private float nbr;

    public float cooldown;

    public float currCooldown;
    */
    // Use this for initialization
    void Start ()
    {

    }

	
	// Update is called once per frame
	void Update ()
    {
        /*Mesh mesh = GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = mesh.vertices;
        Color[] colors = new Color[vertices.Length];

        currCooldown += Time.deltaTime;

        pourc = currCooldown / cooldown;
        nbr = mesh.vertices.Length * pourc;


        for (int i = 0; i < Mathf.Min(nbr, mesh.vertices.Length); i++)
        {
            colors[i] = maCouleur;
        }

        mesh.colors = colors;

        if (currCooldown >= cooldown)
        { 
            currCooldown = 0;

        }*/
    }

}
