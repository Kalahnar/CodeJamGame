using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public GameObject player;

    private Vector2 position;

	void Start () {
        position = this.transform.position;
	}

	void Update () {
        if (player.transform.position.x > position.x && player.transform.position.y > position.y && player.transform.position.x < 69)
        {
            this.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);
        }
		else if (player.transform.position.x > position.x && player.transform.position.x < 69)
        {
            this.transform.position = new Vector3(player.transform.position.x, 0, -10);
        }
        else if (player.transform.position.y > position.y)
        {
            this.transform.position = new Vector3(0, player.transform.position.y, -10);
        }
    }
}
