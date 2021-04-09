using Assets.Scripts.World;
using Assets.Scripts.World.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawnTrigger : MonoBehaviour {

    public List<Transform> SpawnPositions;
    
    [Range(0.1f, 10000)]
    public float SpawnRate = 2.0f;
    
    [Range(0, 100)]
    public int SpawnTimes = 5;
    protected EnemySelector EnemySelector;

    public List<GameObject> Enemies;

    public string PlayerTag = GlobalTagManager.PlayerTag;

    [Range(0, 10000)]
    public int MaxSpawns = 100;

    [Range(0, 10000)]
    public float SpawnCooldownTime = 100;

    private int currentSpawns = 0;

    public Boolean isEnabled;

    private System.Random random = new System.Random();

    // Start is called before the first frame update
    void Start()
    {
        isEnabled = false;
        EnemySelector = new RandomEnemySelector(Enemies);
    }

    void OnEnable()
    {
        StartCoroutine(nameof(InstantiateEnemy));
        EventManager.RegisterListener(EventName.PlayerDead, new UnityAction(OnPlayerDead));
    }

    private void OnDisable()
    {
        StopAllCoroutines();
        EventManager.UnregisterListener(EventName.PlayerDead, new UnityAction(OnPlayerDead));
    }

    private void OnPlayerDead()
    {
        isEnabled = false;
    }

    IEnumerator InstantiateEnemy()
    {
        while (true)
        {
            if (isEnabled)
            {
                if (currentSpawns >= MaxSpawns)
                {
                    currentSpawns = 0;
                    yield return new WaitForSeconds(SpawnCooldownTime);
                }

                List<GameObject> nextEnemies = EnemySelector.NextEnemies(SpawnTimes);
                foreach (GameObject enemy in nextEnemies)
                {
                    Transform spawnPos = SpawnPositions[random.Next(0, SpawnPositions.Count)];
                    //Instantiate(enemy, spawnPos.position, spawnPos.rotation);
                    GameObject currentEnemy = ObjectPooler.INSTANCE.GetObjectFromPool(enemy);
                    currentEnemy.transform.position = spawnPos.position;
                    currentEnemy.transform.rotation = spawnPos.rotation;
                    currentEnemy.SetActive(true);
                    currentSpawns++;
                }
            }
            yield return new WaitForSeconds(SpawnRate);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(PlayerTag))
        {
            isEnabled = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(PlayerTag))
        {
            isEnabled = false;
        }
    }
}
