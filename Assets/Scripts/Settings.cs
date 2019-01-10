using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Settings : MonoBehaviour
{

    public void loadStart()
    {
        SceneManager.LoadScene("SceneVanilla", LoadSceneMode.Single);
    }

    public void back()
    {
        SceneManager.LoadScene("SceneStart", LoadSceneMode.Single);
    }


}
