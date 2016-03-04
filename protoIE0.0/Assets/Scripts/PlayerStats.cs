using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour
{

    public int StartHP;
    private const int maxHP = 12;

    //mettre prive quand les tests sont terminés
    public int HP;

    public int getHP() { return HP; }
    public void AffectHP(int diff)
    {
        HP = HP + diff;

        if(HP > maxHP)
        {
            HP = maxHP;
        }
        else if(HP < 0)
        {
            //on ne veut pas que les hp du joueurs soient en bas de 0
            HP = 0;
        }
    }

    // Use this for initialization
    void Start()
    {
        HP = StartHP;
    }

    // Update is called once per frame
    void Update()
    {

    }

}
