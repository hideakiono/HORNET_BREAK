using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Hornet.StageClear
{

    public class SceneController2 : MonoBehaviour
    {
        // Update is called once per frame
        void Update()
        {
            StartCoroutine(Retry());
        }

        IEnumerator Retry()
        {
            // 7秒たったらタイトルに移動
            yield return new WaitForSeconds(7);
            SceneManager.LoadScene("Title");
        }
    }
}
