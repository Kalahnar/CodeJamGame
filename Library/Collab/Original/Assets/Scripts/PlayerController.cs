using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {
    public Text text;
    public GameObject door;
    public GameObject healthBar;
    public Sprite[] spr;

    private Rigidbody2D body;
    private Animator self;
    private int stamina;
    private int staminaRecover;
    private int jumpCount;
    private int stunCount;
    private int health;
    private bool isStunned;

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

    void OnCollisionEnter2D(Collision2D col)
    {
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

            if (col.transform.position.x < body.position.x)
                body.velocity = new Vector2(3, 1);
            else
                body.velocity = new Vector2(-3, 1);
        }
    }

    private void Update()
    {
        text.text = body.position.y.ToString() + " " + jumpCount.ToString() + " " + stamina.ToString() +
                    "\n" + this.transform.position.ToString() + "\n" +
                    GameObject.FindWithTag("MainCamera").transform.position.ToString();
    }

    private void FixedUpdate()
    {
        if (health == 0)
        {
            self.SetInteger("animState", 4);
        }
        if (stamina < 10)
        {
            staminaRecover++;
        }
        
        if (staminaRecover >= 150)
        {
            staminaRecover = 0;
            stamina++;
        }

        if (this.transform.position.x < -8.5f)
        {
            this.transform.position = new Vector3(-8.5f, this.transform.position.y, this.transform.position.z);
        }

        if (this.transform.position.x > 77.0f)
        {
            this.transform.position = new Vector3(77.0f, this.transform.position.y, this.transform.position.z);
        }

        if (!isStunned)
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
            if (stunCount > 75)
            {
                isStunned = false;
                stunCount = 0;
            }
        }
    }
}
