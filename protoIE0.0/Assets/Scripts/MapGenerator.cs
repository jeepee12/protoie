using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [System.Serializable]
    public class MapComponent
    {
        public GameObject[] enemies;
        public Vector3 enemyOffSet;
        public GameObject land;
        public GameObject weather;
        public GameObject items;

        public float spawnTime = 3f;
        public int waveEnemies = 15;
        [System.NonSerialized]
        public bool StageCompleted = false;
        public bool StageInit = false;
        private float distanceFromPlayer;
        public void InitStage()
        {
            if(enemies.Length > 0)
            {
                int i = 0;
                foreach(GameObject enemy in enemies)
                {
                    GameObject enemyTransform = (GameObject) Instantiate(enemy,
                        new Vector3((enemy.transform.localScale.x + enemyOffSet.x)* i,
                        enemyOffSet.y * i,
                        enemyOffSet.z * i),
                        Quaternion.identity);
                    Debug.Log(enemyTransform.transform.position);
                    ++i;
                }
            }
        }
    }
    public GameObject player;
    public MapComponent[] MapList;
    
    static int waveNumber = 1;
    
    private int currentMap;

    void Awake()
    {
        currentMap = 0;
    }

    void Update()
    {
        if(!MapList[currentMap].StageCompleted)
        {
            if(!MapList[currentMap].StageInit)
            {
                MapList[currentMap].StageInit = true;
                MapList[currentMap].InitStage();
            }
        }
    }
}