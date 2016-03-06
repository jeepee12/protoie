using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [System.Serializable]
    public class MapComponent
    {
        public GameObject land;
        public GameObject[] enemies;
        public GameObject weather;
        public GameObject[] items;

        [System.NonSerialized]
        public bool StageCompleted = false;
        [System.NonSerialized]
        public bool StageInit = false;

        private bool haveLand = false;
        private GameObject landClone;
        private GameObject weatherClone;

        public void InitStage()
        {
            if (land)
            {
                haveLand = true;
                landClone = (GameObject)Instantiate(land, Vector3.zero, Quaternion.identity);
            }

            if (enemies.Length > 0)
            {
                Transform[] listOfTransforms = landClone.GetComponentsInChildren<Transform>();
                Transform[] listOfSpawns;
 
                foreach (Transform spawnLocation in listOfTransforms)
                {
                    if (spawnLocation.name.Contains("SpawnLocations"))
                    {
                        listOfSpawns = spawnLocation.GetComponentsInChildren<Transform>();
                        //listOfSpawns[0] is use for SpawnLocations
                        //That's why listOfSpawns use i + 1
                        for (int i = 0; i < enemies.Length; ++i)
                        {
                            if (listOfSpawns[i]) //If we have a SpawnLocations
                            {
                                Instantiate(enemies[i],
                                    new Vector3(listOfSpawns[i+1].position.x,
                                    (enemies[i].transform.localScale.y / 2),
                                    listOfSpawns[i+1].position.z),
                                    listOfSpawns[i+1].rotation);
                            }
                        }
                        break;
                    }
                }
            }

            if(items.Length > 0)
            {
                //int zPosition = 0;
                for (int i = 0; i < items.Length; ++i)
                {
                    /*Instantiate(items[i],
                        new Vector3((items[i].transform.localScale.x) * (i % (items.Length / 2)),
                        (items[i].transform.localScale.y / 2),
                        (items[i].transform.localScale.z ) * zPosition),
                        Quaternion.identity);*/
                    Instantiate(items[i], Vector3.zero, Quaternion.identity);
                    /*if ((i % (items.Length / 2)) == 0)
                    {
                        ++zPosition;
                    }*/
                }
            }

            if(weather)
            {
                weatherClone = (GameObject)Instantiate(weather, Vector3.zero, Quaternion.identity);
                if(weatherClone.name.Contains("Rain"))
                {
                    weatherClone.transform.parent = Camera.main.transform;
                    weatherClone.transform.localEulerAngles = new Vector3(35, 0, 0);
                    weatherClone.transform.localPosition = new Vector3(0, 72, 50);
                }
            }
        }

        public void DestroyStage()
        {
            if(landClone)
            {
                Destroy(landClone);
            }
            if(weatherClone)
            {
                Destroy(weatherClone);
            }
        }

        public int enemiesNumber()
        {
            return enemies.Length;
        }
    }

    public GameObject player;
    public Transform ToDestroy;
    public MapComponent[] MapList;
    private int enemiesAlive;
    private int itemsDrop;

    private bool NextMapReady = false;
    private bool allEnemiesDead = true;
    private bool noItemLeft;

    private int currentMap;

    void Awake()
    {
        currentMap = 0;
    }

    void Update()
    {
        if(currentMap < MapList.Length)
        {
            if (!MapList[currentMap].StageCompleted)
            {
                if (!MapList[currentMap].StageInit)
                {
                    MapList[currentMap].StageInit = true;
                    MapList[currentMap].InitStage();
                    enemiesAlive = MapList[currentMap].enemiesNumber();
                    allEnemiesDead = MapList[currentMap].enemiesNumber() <= 0;
                    itemsDrop = MapList[currentMap].items.Length;
                    noItemLeft = MapList[currentMap].items.Length <= 0;
                }
            }
            else if (NextMapReady)
            {
                NextMapReady = false;
                MapList[currentMap].DestroyStage();

                for(int i = 0; i < ToDestroy.childCount; ++i)
                {
                    Destroy(ToDestroy.GetChild(i).gameObject);
                }

                if (currentMap < MapList.Length)
                {
                    ++currentMap;
                }
                else
                {
                    currentMap = MapList.Length - 1;
                }
            }
        }
    }

    public void Collision()
    {
        if(MapList[currentMap].StageCompleted)
        {
            NextMapReady = true;
        }
    }

    public void StageIsCompleted()
    {
        if(allEnemiesDead && noItemLeft)
        {
            MapList[currentMap].StageCompleted = true;
        }
    }

    public void EnemyDead()
    {
        if(enemiesAlive > 0)
        {
            --enemiesAlive;
            if(enemiesAlive <= 0)
            {
                allEnemiesDead = true;
            }
        }
        else
        {
            allEnemiesDead = true;
        }
    }

    public void ItemTaken()
    {
        --itemsDrop;
        if(itemsDrop<=0)
        {
            noItemLeft = true;
        }
    }

    public void ItemDropped()
    {
        ++itemsDrop;
    }

    public void RestartStage()
    {
        MapList[currentMap].StageInit = false;
    }
}