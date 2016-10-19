using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HP : MonoBehaviour {

    public PlayerStats PS;

    public Material HPLit;
    public Material HPUnlit;

    public Image[] HPVisual;
    
    private int currentHP;

    // Use this for initialization
    void Start ()
    {
	    
	}

    // Update is called once per frame
	void Update ()
    {
        if (currentHP != PS.getHP())
        {  
            currentHP = PS.getHP();

            for (int i = 0; i < HPVisual.Length; i++)
            {

                if (i < currentHP)
                    HPVisual[i].material = HPLit;
                else
                    HPVisual[i].material = HPUnlit;
            }
        }

    }
}
