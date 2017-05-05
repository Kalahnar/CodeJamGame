using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour {
    public GameObject player;

    private float minHorizontal;
    private float minVertical;
    private float maxHorizontal;

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "SceneVanilla")
        {
            minHorizontal = 0;
            minVertical = 0;
            maxHorizontal = 69;
        }
        else if (SceneManager.GetActiveScene().name == "SceneWater")
        {
            minHorizontal = 0;
            minVertical = 0;
            maxHorizontal = 38.5f;
        }
        else
        {
            minHorizontal = -2;
            minVertical = 0;
            maxHorizontal = 29.5f;
        }
    }

    void FixedUpdate () {
        if (player.transform.position.x < minHorizontal && player.transform.position.y > minVertical)
        {
            this.transform.position = new Vector3(minHorizontal, player.transform.position.y, -10);
        }
        else if (player.transform.position.x < minHorizontal && player.transform.position.y < minVertical)
        {
            this.transform.position = new Vector3(minHorizontal, minVertical, -10);
        }
        else if (player.transform.position.x > maxHorizontal && player.transform.position.y > 0)
        {
            this.transform.position = new Vector3(maxHorizontal, player.transform.position.y, -10);
        }
        else if (player.transform.position.x > maxHorizontal && player.transform.position.y < minVertical)
        {
            this.transform.position = new Vector3(maxHorizontal, minVertical, -10);
        }
        else if (player.transform.position.y < minVertical)
        {
            this.transform.position = new Vector3(player.transform.position.x, minVertical, -10);
        }
        else
        {
            this.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);
        }
    }
}
