using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform player;
    public float spawnInterval = 3f;
    public int maxEnemies = 5;

    private int currentEnemies = 0;

    void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy), 2f, spawnInterval);
    }

    void SpawnEnemy()
    {
        if (currentEnemies >= maxEnemies) return;

        GameObject enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);

        EnemyFollower follower = enemy.GetComponent<EnemyFollower>();
        if (follower != null) follower.target = player;

        currentEnemies++;
    }

}
