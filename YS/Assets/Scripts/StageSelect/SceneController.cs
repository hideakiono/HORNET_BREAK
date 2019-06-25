using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnClickStage1()
    {
        SceneManager.LoadScene("Stage");
    }
    public void OnClickBackButton()
    {
        SceneManager.LoadScene("Title");
    }
    public void OnClickStage2()
    {
        SceneManager.LoadScene("Stage2");
    }
    public void OnClickStage3()
    {
        SceneManager.LoadScene("Stage3");
    }
    public void OnClickStage4()
    {
        SceneManager.LoadScene("Stage4");
    }
    public void OnClickStage5()
    {
        SceneManager.LoadScene("Stage5");
    }
    public void OnClickStage6()
    {
        SceneManager.LoadScene("Stage6");
    }

}
