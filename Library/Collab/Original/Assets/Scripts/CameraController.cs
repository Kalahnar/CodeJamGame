using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public GameObject player;

	void Start () {
	}

	void FixedUpdate () {
        if (player.transform.position.x < 0 && player.transform.position.y > 0)
        {
            this.transform.position = new Vector3(0, player.transform.position.y, -10);
        }
        else if (player.transform.position.x < 0 && player.transform.position.y < 0)
        {
            this.transform.position = new Vector3(0, 0, -10);
        }
        else if (player.transform.position.x > 69 && player.transform.position.y > 0)
        {
            this.transform.position = new Vector3(69, player.transform.position.y, -10);
        }
        else if (player.transform.position.x > 69 && player.transform.position.y < 0)
        {
            this.transform.position = new Vector3(69, 0, -10);
        }
        else if (player.transform.position.y < 0)
        {
            this.transform.position = new Vector3(player.transform.position.x, 0, -10);
        }
        else
        {
            this.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);
        }
    }
}
