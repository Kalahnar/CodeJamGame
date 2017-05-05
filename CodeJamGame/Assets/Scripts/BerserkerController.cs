using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BerserkerController : MonoBehaviour {

    public GameObject mainChar;
    public GameObject mainCam;
    public Text text;

    private Animator anim;
    private Rigidbody2D body;
    private float speed;
    private int attackCD;
    private string attackDir;
    

	private void Start () {
        anim = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        body.freezeRotation = true;
        speed = 0.13f;
        attackCD = 0;
        attackDir = "";
	}

    private void OnCollisionEnter2D(Collision2D col)
    {
        attackDir = "";
        attackCD = -75;
        anim.SetInteger("animState", 0);
    }

    private void FixedUpdate()
    {
        Vector2 position = this.GetComponent<Rigidbody2D>().position;
        if (transform.position.x - mainCam.transform.position.x <= 4 &&
            mainCam.transform.position.x - transform.position.x <= 4)
        {        
            if (this.transform.position.y > mainChar.transform.position.y - 2 &&
                this.transform.position.y < mainChar.transform.position.y + 3)
            {
                attackCD++;
            }

            if (attackDir == "right")
            {
                this.GetComponent<Rigidbody2D>().position = new Vector2(position.x + speed, position.y);
                anim.SetInteger("animState", 1);
                this.transform.localScale = new Vector3(0.05f, 0.05f, 1);
            }
            else if (attackDir == "left")
            {
                this.GetComponent<Rigidbody2D>().position = new Vector2(position.x - speed, position.y);
                anim.SetInteger("animState", 1);
                this.transform.localScale = new Vector3(-0.05f, 0.05f, 1);
            }
            else if (attackCD >= 25)
            {
                if (this.transform.position.x > mainChar.transform.position.x)
                    attackDir = "left";
                else
                    attackDir = "right";
            }            
        }
        else
        {
            attackDir = "";
            anim.SetInteger("animState", 0);
            attackCD = 0;
        }
    }

    private void Update () {
        text.text = this.transform.position.ToString();
	}
}
