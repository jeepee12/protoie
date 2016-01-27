using UnityEngine;

public class Fireproof : MonoBehaviour
{
	public bool flamable;
	private bool onFire;

    //Hp would be in the stats script of the game object
    private int hp = 10;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{   
        if (hp <= 0)
        {
            //Kill the object
            Destroy(gameObject);
        }
	}

	public bool isFlamable()
	{
		return flamable;
	}

	public void setIsOnFire(bool newOnFire)
	{
		onFire = newOnFire;

        if(onFire)
        {
            InvokeRepeating("dealDamage", 0, 0.5f);
        }
	}

    public void dealDamage()
    {
        hp -= 1;
    }

	public bool isOnFire()
	{
		return onFire;
	}
}
