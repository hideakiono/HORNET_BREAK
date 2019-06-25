using System.Collections;
using UnityEngine;

namespace Hornet.Stage
{


    // 敵機の動きを表します。
    public class BigEnemy : MonoBehaviour
    {
        // 敵機の状態を表します。
        public enum EnemyState
        {
            None,
            NonActive,
            Active,
            motion,
            GameOver,
        }
        // 爆発エフェクトを指定します。
        public GameObject explosion;
        // 現在の状態を表します。
        public EnemyState enemyState = EnemyState.None;
        // ゲームオーバーを指定します。
        public GameObject gameOver;
        // エネミーが撃つ弾を指定します。
        public GameObject enemybullet;
        // 弾の出る所を指定します。
        public Transform shotPos;
        // 自分のHPを指定します。
        public const int hp = 100;
        public int currentHP = hp;
        // 壁を指定します。
        public GameObject wall;
        // 地面を指定します。
        public GameObject ground;
        // 天井を指定します。
        public GameObject ceil;
        // スコアを指定します。
        public int scoreValue;
        // スコアマネージャーを指定します。
        private ScoreManager sm;

        void Start()
        {
            enemyState = EnemyState.NonActive;
            // 「sm」を「ScoreManager指定
            sm = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
        }
        // 弾の出る初期値
        float shotTime = 0;

        IEnumerator OnHitBullet()
        {
 
            // 爆発エフェクト再生
            Instantiate(explosion, transform.position, explosion.transform.rotation, transform);
            // スコア加点
            sm.Addscore(scoreValue);
            yield return new WaitForSeconds(3);
            // 敵機を削除
            Destroy(gameObject);
        }

        // 画面外にいる時は消えてる
        void OnBecameInvisible()
        {

            enabled = false;

        }
  
        void Update()
        {
            switch (enemyState)
            {
                case EnemyState.None:
                    break;
                case EnemyState.NonActive:
                    if (GetComponentInChildren<ActiveArea>().isActive)
                    {
                        enemyState = EnemyState.Active;
                    }
                    break;
                case EnemyState.Active:
                    {
                        // 敵の動くスピード
                        float moveSpeedx = 0;
                        //float moveSpeedy = 0;
                        var position = transform.position;
                        moveSpeedx = -2.5f;
                        position.x += moveSpeedx * Time.deltaTime;
                        transform.position = position;

                        shotTime += Time.deltaTime;

                        //弾のクールタイム
                        if (shotTime > 0.9f)
                        {
                            Instantiate(enemybullet, shotPos.transform.position + new Vector3(0f, -0.5f, 0), enemybullet.transform.rotation);
                            Instantiate(enemybullet, shotPos.transform.position + new Vector3(0f, +0.2f, 0), enemybullet.transform.rotation);
                            Instantiate(enemybullet, shotPos.transform.position + new Vector3(0f, +0.9f, 0), enemybullet.transform.rotation);
                            Instantiate(enemybullet, shotPos.transform.position + new Vector3(3.2f, -1f, 0), enemybullet.transform.rotation);
                            shotTime = 0;
                        }

                        if (transform.position.x - wall.transform.position.x < -4.0f)
                        {
                            moveSpeedx = +5.0f;
                            //moveSpeedy = +0.5f;
                            //position.y += moveSpeedy * Time.deltaTime;
                            position.x += moveSpeedx * Time.deltaTime;
                            transform.position = position;

                            /*if (transform.position.y > 1.5f)
                            {
                                //moveSpeedy = -3.0f;
                                position.y -= moveSpeedy // * Time.deltaTime;
                            } else if (transform.position.y < -1.5f)
                            {
                               // moveSpeedy = +3.0f;
                                position.y += moveSpeedy * Time.deltaTime;
                            }*/

                        }
                    }
                    break;
                case EnemyState.motion:
                    StartCoroutine(OnHitBullet());
                    enemyState = EnemyState.GameOver;
                    break;
                case EnemyState.GameOver:
                    break;
                default:
                    break;
            }
        }

        // この敵機のコライダーに他のオブジェクトが接触した際に呼び出されます。
        private void OnTriggerEnter2D(Collider2D collider)
        {
            // 接触相手の処理 
            if (collider.gameObject.tag == "Bullet")
            {
                currentHP -= 10;
                // ボスの死亡判定
                if (currentHP <= 0)
                {
                    currentHP = 0;
                    GetComponent<Collider2D>().enabled = false;
                    enemyState = EnemyState.motion;
                }
            }
        }
    }
}
