using UnityEngine;
using System.Collections;

public class UnlockWeapon : MonoBehaviour
{
    private GameObject m_Player;
    private SelectedWeapon m_SelectedWeaponScript;
    void Start()
    {
        m_Player = GameObject.FindGameObjectWithTag("Player");
        m_SelectedWeaponScript = m_Player.GetComponent<SelectedWeapon>();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("Player"))
        {
            for(int i = 0; i < m_SelectedWeaponScript.weaponList.Length; ++i)
            {
                if(!m_SelectedWeaponScript.weapons[i].unlock)
                {
                    m_SelectedWeaponScript.weapons[i].unlock = true;
                    GameObject mg = GameObject.FindGameObjectWithTag("MapGenerator");
                    mg.GetComponent<MapGenerator>().ItemTaken();
                    Destroy(gameObject);
                    break;
                }                
            }
        }
    }
}
