using UnityEngine;
using System.Collections;

public class Pause : MonoBehaviour 
{
    public GameObject PausePlane;
    private bool askingPause = false;

    void Update()
    {
        // Si personne demande la pause et que c'est sur pause alors on stop la pause et ton cache le plane de pause
        if (Time.timeScale == 0.0f && !askingPause)
        {
            Time.timeScale = 1.0f;
            PausePlane.SetActive(false);
        }
    }

    // Ajout d'une personne qui demande la pause
    void Pauses()
    {
        askingPause = !askingPause;
        Time.timeScale = 0.0f;
        PausePlane.SetActive(askingPause);
    }
    /*
    // Soustraction d'une personne qui demande la pause
    void Play()
    {
        askingPause--;
    }*/
}
