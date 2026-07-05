using System.Collections.Generic;
using UnityEngine;

public class Fryingpan : MonoBehaviour
{
    private List<POPSeeds> seedsInZone = new List<POPSeeds>();

    [SerializeField] private GameObject popEffectPrefab; // 弾けた瞬間のエフェクト（数秒で消える設定にする）

    void Update()
    {
        // スペースキーが押され、エリア内にタネがある場合
        if (Input.GetKeyDown(KeyCode.Space) && seedsInZone.Count > 0)
        {
            POPSeeds targetSeed = seedsInZone[0];

            // タネにダメージを与え、HPが0になった（弾けた）か確認
            bool isPopped = targetSeed.TakeDamage();

            if (isPopped)
            {
                // リストから除外
                seedsInZone.RemoveAt(0);

                // 弾けたエフェクトを生成して、タネを消滅させる
                CreatePopEffect(targetSeed.transform.position, targetSeed.isGiant);
                Destroy(targetSeed.gameObject);

                // TODO: ここでGameManagerのスコアやポップコーンカウントを加算する
            }
        }
    }

    void CreatePopEffect(Vector3 spawnPosition, bool isGiantSeed)
    {
        if (popEffectPrefab == null) return;

        // エフェクトを生成
        GameObject effect = Instantiate(popEffectPrefab, spawnPosition, Quaternion.identity);

        // 巨大タネが弾けた時はエフェクトも大きくする
        if (isGiantSeed)
        {
            effect.transform.localScale *= 2f;
        }

        // 【重要】弾けたポップコーン（エフェクト）を1秒後に自動で消滅させる
        Destroy(effect, 1.0f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        POPSeeds seed = other.GetComponent<POPSeeds>();
        if (seed != null)
        {
            seedsInZone.Add(seed);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        POPSeeds seed = other.GetComponent<POPSeeds>();
        if (seed != null)
        {
            seedsInZone.Remove(seed);
        }
    }

}
