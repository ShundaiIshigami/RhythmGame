using UnityEngine;

public class POPSeeds : MonoBehaviour
{
    public float speed = 5f;
    public int hp = 1; // 通常のタネは1、巨大タネは2に設定する
    public bool isGiant = false; // 巨大タネならTrueにする（見た目や演出の分岐用）

    private void Start()
    {
        if (isGiant)
        {
            hp = 2;
        }
    }


    void Update()
    {
        // 毎フレーム下に移動させる
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        // フライパン（判定エリア）を通り過ぎて画面外に出たら自動で削除（見逃し/Miss）
        if (transform.position.y < -6f)
        {
            Destroy(gameObject);
        }
    }

    // ダメージ（タップ）を受けたときの処理
    public bool TakeDamage()
    {
        hp--;

        if (hp <= 0)
        {
            return true; // 完全に破壊（弾けた）
        }

        // 巨大タネが1回叩かれた時、少し色を変えたり縮ませたりすると分かりやすい
        if (isGiant)
        {
            transform.localScale *= 0.8f; // ちょっと縮む
            GetComponent<SpriteRenderer>().color = Color.orange; // 色が変わる
        }

        return false; // まだ残っている
    }
}
