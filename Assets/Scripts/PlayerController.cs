using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {
    public GameObject text;
    public GameObject door;
    public GameObject healthBar;
    public GameObject stealthBar;
    public Sprite[] spr;
    public Sprite[] staminaSpr;

    private Rigidbody2D body;
    private Animator self;
    private int stamina;
    private int staminaRecover;
    private int jumpCount;
    private int stunCount;
    private int health;
    private bool isStunned;
    private float leftBound;
    private float rightBound;

	// Use this for initialization
	void Start () {
        body = GetComponent<Rigidbody2D>();
        body.freezeRotation = true;
        self = GetComponent<Animator>();
        jumpCount = 0;
        stamina = 10;
        staminaRecover = 0;
        isStunned = false;
        stunCount = 0;
        health = 20;
	}

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "star")
        {
            isStunned = true;
            stunCount = 0;

            Destroy(col.gameObject);

            health -= 2;
            if (health < 0)
            {
                health = 0;
            }
            healthBar.GetComponent<SpriteRenderer>().sprite = spr[health];
            if (col.transform.position.x < body.position.x)
                body.velocity = new Vector2(2, 0.5f);
            else
                body.velocity = new Vector2(-2, 0.5f);
        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "death")
        {
            health -= 1;
            if (health < 0)
            {
                health = 0;
            }
            healthBar.GetComponent<SpriteRenderer>().sprite = spr[health];

            body.velocity = new Vector2(0, 10f);
        }
        if (col.gameObject.tag == "platform")
            jumpCount = 0;
        if (col.gameObject.tag == "Finish")
            if (SceneManager.GetActiveScene().name == "SceneWater")
            {
                SceneManager.LoadScene("SceneEnd", LoadSceneMode.Single);
            }
            else if (SceneManager.GetActiveScene().name == "SceneVanilla")
            {
                SceneManager.LoadScene("SceneWater", LoadSceneMode.Single);
            }         
        if (col.gameObject.tag == "key")
        {
            col.gameObject.SetActive(false);
            door.SetActive(true);
        }
        if (col.gameObject.tag == "berserker")
        {
            health -= 3;
            if (health < 0)
            {
                health = 0;
            }
            healthBar.GetComponent<SpriteRenderer>().sprite = spr[health];
            isStunned = true;
            stunCount = -50;

            if (col.transform.position.x < body.position.x)
                body.velocity = new Vector2(3, 1);
            else
                body.velocity = new Vector2(-3, 1);
        }        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SceneManager.LoadScene("SceneVanilla", LoadSceneMode.Single);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SceneManager.LoadScene("SceneWater", LoadSceneMode.Single);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SceneManager.LoadScene("SceneEnd", LoadSceneMode.Single);
        }
    }

    private void FixedUpdate()
    {
        if (SceneManager.GetActiveScene().name == "SceneVanilla")
        {
            leftBound = -8.5f;
            rightBound = 77.5f;
        }
        else if (SceneManager.GetActiveScene().name == "SceneWater")
        {
            leftBound = -8.5f;
            rightBound = 47.0f;
        }
        else
        {
            leftBound = -10.5f;
            rightBound = 38.0f;
        }

        if (health == 0)
        {
            text.SetActive(true);
        }

        if (stamina < 10)
        {
            staminaRecover++;
        }
        
        if (staminaRecover >= 150)
        {
            staminaRecover = 0;
            stamina++;
            stealthBar.GetComponent<SpriteRenderer>().sprite = staminaSpr[stamina];
        }

        if (this.transform.position.x < leftBound)
        {
            this.transform.position = new Vector3(leftBound, this.transform.position.y, this.transform.position.z);
        }

        if (this.transform.position.x > rightBound)
        {
            this.transform.position = new Vector3(rightBound, this.transform.position.y, this.transform.position.z);
        }

        if (!isStunned && health > 0)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) && jumpCount < 2)
            {
                body.velocity = new Vector2(0, 5);
                jumpCount++;
            }

            if (Input.GetKeyDown(KeyCode.D) && stamina > 0)
            {
                if (this.transform.localScale.x > 0)
                    body.velocity = new Vector2(5, 1.5f);
                else
                    body.velocity = new Vector2(-5, 1.5f);

                stamina--;
                stealthBar.GetComponent<SpriteRenderer>().sprite = staminaSpr[stamina];
            }

            Vector2 movement = new Vector2(0.10f, 0);

            if (Input.GetKeyDown(KeyCode.A))
            {
                self.SetInteger("animState", 2);
            }           

            else if (Input.GetKey(KeyCode.RightArrow))
            {
                if (body.velocity.x < 0)
                {
                    body.velocity = new Vector2(0, body.velocity.y);
                }
                self.SetInteger("animState", 1);
                this.transform.localScale = new Vector3(0.05f, 0.05f, 1);
                body.position = body.position + movement;
            }

            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                if (body.velocity.x > 0)
                {
                    body.velocity = new Vector2(0, body.velocity.y);
                }
                self.SetInteger("animState", 1);
                this.transform.localScale = new Vector3(-0.05f, 0.05f, 1);
                body.position = body.position - movement;
            }

            else
            {
                self.SetInteger("animState", 0);
            }
        }
        else
        {
            self.SetInteger("animState", 0);
            stunCount++;
            if (stunCount > 25)
            {
                isStunned = false;
                stunCount = 0;
            }
        }
    }
}
