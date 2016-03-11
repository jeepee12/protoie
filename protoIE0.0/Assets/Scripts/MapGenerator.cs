using UnityEngine;
using UnityEngine.UI;

public class MapGenerator : MonoBehaviour
{
    [System.Serializable]
    public class MapComponent
    {
        public GameObject land;
        public GameObject[] enemies;
        public GameObject weather;
        public GameObject[] items;
        public string tutorialText;

        [System.NonSerialized]
        public bool StageCompleted = false;
        [System.NonSerialized]
        public bool StageInit = false;

        private bool m_hasLand = false;
        private GameObject landClone;
        private GameObject weatherClone;

        public void InitStage()
        {
            if (land)
            {
                m_hasLand = true;
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
                                GameObject enemyClone = (GameObject) Instantiate(enemies[i],
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

        public bool hasLand()
        {
            return m_hasLand;
        }
    }

    //UI
    public Text m_ObjectiveText;
    public Text m_TutorialText;
    public float m_TutorialTimer = 5;
    private float m_TutorialTimerStart;
    private string[] m_ObjectiveStr = new string[4];
    private bool m_UpdateObjectives = false;

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
                    m_ObjectiveStr[0] = "Objective:\n";
                    enemiesAlive = MapList[currentMap].enemiesNumber();
                    allEnemiesDead = MapList[currentMap].enemiesNumber() <= 0;

                    if(MapList[currentMap].hasLand())
                    {
                        m_ObjectiveStr[1] = "\nExplore the nearby islands.";
                    }
                    else
                    {
                        m_ObjectiveStr[1] = null;
                    }

                    if(!allEnemiesDead)
                    {
                        m_ObjectiveStr[2] = "\nKill all enemies.";
                    }

                    itemsDrop = MapList[currentMap].items.Length;
                    noItemLeft = MapList[currentMap].items.Length <= 0;

                    if (!noItemLeft)
                    {
                        m_ObjectiveStr[3] = "\nPick up items.";
                    }

                    if(MapList[currentMap].tutorialText != null)
                    {
                        m_TutorialText.text = MapList[currentMap].tutorialText;
                        m_TutorialText.CrossFadeAlpha(1f, 0.2f, true);
                        m_TutorialTimerStart = Time.time;
                    }
                    else
                    {
                        m_TutorialText.text = null;
                        m_TutorialText.CrossFadeAlpha(0f, 0f, true);
                    }

                    m_UpdateObjectives = true;
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

        if(m_UpdateObjectives)
        {
            int count = 0;
            for(int i = 1; i < m_ObjectiveStr.Length; ++i)
            {
                if(m_ObjectiveStr[i] != null)
                {
                    ++count;
                }
            }

            switch(count)
            {
                case 0:
                    m_ObjectiveStr[0] = "Objective:\n";
                    m_ObjectiveStr[1] = "\nExplore the sea.";
                    MapList[currentMap].StageCompleted = true;
                    break;
                case 1:
                    m_ObjectiveStr[0] = "Objective:\n";
                    break;
                default:
                    m_ObjectiveStr[0] = "Objectives:\n";
                    break;
            }

            m_ObjectiveText.text = m_ObjectiveStr[0];
            for (int i = 1; i < m_ObjectiveStr.Length; ++i)
            {
                if (m_ObjectiveStr[i] != null)
                {
                    m_ObjectiveText.text += m_ObjectiveStr[i];
                }
            }
        }

        if(Time.time > m_TutorialTimerStart + m_TutorialTimer)
        {
            m_TutorialText.CrossFadeAlpha(0f, 0.5f, true);
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
            m_ObjectiveStr[1] = "\nExplore the sea.";
            m_UpdateObjectives = true;
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
                m_ObjectiveStr[2] = null;
                m_UpdateObjectives = true;
            }
        }
        else
        {
            allEnemiesDead = true;
            m_ObjectiveStr[2] = null;
            m_UpdateObjectives = true;
        }
    }

    public void ItemTaken()
    {
        --itemsDrop;
        if(itemsDrop<=0)
        {
            noItemLeft = true;
            m_ObjectiveStr[3] = null;
            m_UpdateObjectives = true;
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

    public void EraseStage()
    {
        //Kill all enemies
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }

        GameObject[] items = GameObject.FindGameObjectsWithTag("Item");
        foreach (GameObject item in items)
        {
            Destroy(item);
        }

        GameObject[] isles = GameObject.FindGameObjectsWithTag("Isle");
        foreach (GameObject isle in isles)
        {
            Destroy(isle);
        }

        for (int i = 0; i < ToDestroy.childCount; ++i)
        {
            Destroy(ToDestroy.GetChild(i).gameObject);
        }
    }
}