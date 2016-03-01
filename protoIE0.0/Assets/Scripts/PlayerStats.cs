using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour {

    public int StartHP;

    public int HP;
    
    public int getHP() { return HP; }
    public int AffectHP(int diff) { return HP + diff; }

    // Use this for initialization
    void Start () {
        HP = StartHP;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

}
