using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameScript : MonoBehaviour {

	public void loadStart()
    {
        SceneManager.LoadScene("SceneVanilla", LoadSceneMode.Single);
    }

    public void loadSettings()
    {
        SceneManager.LoadScene("SceneSettings", LoadSceneMode.Single);
    }

    public void quit()
    {
        Application.Quit();
    }
}