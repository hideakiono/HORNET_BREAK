using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Hornet.Stage
{
    // 『ステージ画面』のシーン遷移を管理します。
    public class SceneController : MonoBehaviour
    {
        // シーンの状態遷移を表します。
        enum SceneState
        {
            None,
            Start,
            PlayStage,
            Pause,
            GameOver,
            StageClear,
        }
        // 現在の状態
        SceneState sceneState = SceneState.None;

        // シーン開始からの経過時間
        float sceneTime = 0;
        // ポーズ画面
        public GameObject togglePause;
        // UI表示
        [SerializeField]
        GameObject messageUI = null;

        // Use this for initialization
        void Start()
        {
            messageUI.SetActive(false);
            togglePause.SetActive(false);
            sceneState = SceneState.Start;
        }

        // 「GAME OVER」に設定します。
        public void GameOver()
        {
            StartCoroutine(OnGameOver());
        }

        // 「GAME OVER」演出
        IEnumerator OnGameOver()
        {
            // 3秒待ってシーン遷移
            sceneState = SceneState.GameOver;
            ShowMessage("GAME OVER");
            yield return new WaitForSeconds(3.0f);
            SceneManager.LoadScene("Title");
        }

        // メッセージを表示します。
        private void ShowMessage(string message)
        {
            messageUI.GetComponentInChildren<Text>().text = message;
            messageUI.SetActive(true);
        }

        // 「STAGE CLEAR」に設定します。
        public void StageClear()
        {
            StartCoroutine(OnStageClear());
        }

        // 「STAGE CLEAR」演出
        IEnumerator OnStageClear()
        {
            // 3秒待ってシーン遷移
            sceneState = SceneState.StageClear;
            ShowMessage("STAGE CLEAR");
            yield return new WaitForSeconds(3.0f);
            SceneManager.LoadScene("StageClear");
        }

        public void TogglePause()
        {
            // プレイ中
            if (!togglePause.activeInHierarchy)
            {
                Time.timeScale = 0;
            }
            // ポーズ中
            else
            {
                Time.timeScale = 1;
            }
            togglePause.SetActive(!togglePause.activeInHierarchy);
        }

        // Update is called once per frame
        void Update()
        {
            sceneTime += Time.deltaTime;

            if (Input.GetKeyUp(KeyCode.Escape))
            {
                TogglePause();
            }

            switch (sceneState)
            {
                case SceneState.Start:
                    sceneState = SceneState.PlayStage;

                    break;
                case SceneState.PlayStage:
                    {
                        // ステージクリア―したかどうかを判定
                        var enemyObjects = GameObject.FindGameObjectsWithTag("BigEnemy");
                        if (enemyObjects.Length == 0)
                        {
                            StageClear();
                        }
                    }
                    break;
                case SceneState.Pause:
                    break;
                case SceneState.GameOver:
                    break;
                case SceneState.StageClear:
                    break;
                default:
                    break;
            }
        }
    }
}
