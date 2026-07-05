using UnityEngine;

public class POPSpawner : MonoBehaviour
{
    [SerializeField] private GameObject regularSeedPrefab; // 通常のタネ
    [SerializeField] private GameObject giantSeedPrefab;   // 巨大タネ（HP=2に設定したもの）

    public float bpm = 120f;
    public float spawnRangeX = 3.0f; // 左右にどれくらいランダムにずらすか（フライパンの横幅に合わせる）
    [Range(0, 100)] public int giantChance = 20; // 巨大タネが降ってくる確率（％）

    private float beatInterval;
    private float timer;

    void Start()
    {
        beatInterval = 60f / bpm;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= beatInterval)
        {
            SpawnSeed();
            timer -= beatInterval;
        }
    }

    void SpawnSeed()
    {
        // スポナーの現在位置を中心に、X座標をランダムにずらす
        float randomX = Random.Range(-spawnRangeX, spawnRangeX);
        Vector3 spawnPosition = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z);

        // 確率に応じて生成するプレハブを変える
        GameObject prefabToSpawn = regularSeedPrefab;
        if (Random.Range(0, 100) < giantChance)
        {
            prefabToSpawn = giantSeedPrefab;
        }

        // タネを生成
        Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
    }

    // Unityの編集画面で、ランダムに湧く横幅のラインを視覚的に分かりやすくする線（おまけ）
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position + Vector3.left * spawnRangeX, transform.position + Vector3.right * spawnRangeX);
    }
}
