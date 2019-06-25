using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Hornet.Stage
{
    public class Pause : MonoBehaviour
    {
        // ポーズを指定します。
        public GameObject togglePause;

        // 押したボタンの処理
        public void OnClickContinue()
        {
            // 時間を止める
            togglePause.SetActive(!togglePause.activeInHierarchy);
            Time.timeScale = 1;
        }

        // 押したボタンの処理
        public void OnClickTitleButton()
        {
            // 移動先
            SceneManager.LoadScene("Title");
            Time.timeScale = 1;
        }
        // 押したボタンの処理
        public void OnClickRetry()
        {
            // 移動先
            SceneManager.LoadScene("Stage");
            Time.timeScale = 1;
        }
    }
}

