using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RangedController : MonoBehaviour
{

    public GameObject mainChar;
    public GameObject mainCam;
    public GameObject projectile;

    private Animator rangeAnim;
    private Rigidbody2D body;
    private float speed;
    private int attackCD;
    private GameObject star;


    private void Start()
    {
        rangeAnim = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        body.freezeRotation = true;
        speed = 0.13f;
        attackCD = 0;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
    }

    private void FixedUpdate()
    {
        if (transform.position.x - mainCam.transform.position.x <= 9 &&
            mainCam.transform.position.x - transform.position.x <= 9)
        {
            if (this.transform.position.y > mainChar.transform.position.y - 7 &&
                this.transform.position.y < mainChar.transform.position.y + 6)
            {
                attackCD++;
            }

            if (attackCD > 50)
            {
                star = Instantiate(projectile);
                star.transform.position = this.transform.position;
                star.SetActive(true);
                float shotX = mainChar.transform.position.x - this.transform.position.x;
                float shotY = mainChar.transform.position.y - this.transform.position.y;
                star.GetComponent<Rigidbody2D>().velocity = new Vector2(shotX, shotY);
                star.GetComponent<Rigidbody2D>().angularVelocity = 5;

                attackCD = -50;
            }
        }
    }

    private void Update()
    {
    }
}
