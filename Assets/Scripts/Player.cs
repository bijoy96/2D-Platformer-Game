using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

	public float speed = 5.0f;
	public float jumpForce = 5.0f;
	public LayerMask _groundLayer;
    private int count;
    private Rigidbody2D _rigid;
	private Animator _anim;
	private SpriteRenderer _playerSprite;
	private bool _grounded = false;
	private bool _resetJump = false;
    public GameObject prefab;
    public int xmin1 = 0, xmax1 = -5;
    public int xmin2 = -12, xmax2 = -17;
    public GameObject enemy;
    private int life;
    public float timer;
    public int id;
    public static Player instace;
    public Text score;
    public Slider health;
    // Use this for initialization
    void Start ()
	{
        instace=this;
        for (int i = 0; i < 2; i++)
        {
            Instantiate(prefab, new Vector2(Random.Range(xmin1, xmax1), 1), Quaternion.identity);
        }
        for (int i = 0; i < 2; i++)
        {
            Instantiate(prefab, new Vector2(Random.Range(xmin2, xmax2),-1), Quaternion.identity);
        }
        
        _rigid = GetComponent<Rigidbody2D>();
		_anim = GetComponentInChildren<Animator>();
		_playerSprite = GetComponent<SpriteRenderer>();
        count = 0;
        life = 4;
    }
	
	// Update is called once per frame
	void Update ()
	{
        health.value = life;
		Movement();
        timer = timer + Time.deltaTime;
        //Debug.Log("timer=" + timer);
        if (timer > 1f && timer<1.015648F)
        {
            id = 1;
            Instantiate(enemy, new Vector2(0, 0), Quaternion.identity);
        }
        if (timer > 10.5f && timer < 10.515648F) {
            id++;
            Instantiate(enemy, new Vector2(-9, -2), Quaternion.identity);
        }
        if (timer > 20f && timer < 20.015648F)
        {
            id++;
            Instantiate(enemy, new Vector2(-13, -2), Quaternion.identity);
        }
        score.text = "Score: "+count.ToString();
    }

    void Movement()
	{
        
		float move = Input.GetAxisRaw("Horizontal");
		_rigid.velocity = new Vector2(move * speed, _rigid.velocity.y);
		_anim.SetFloat("speed", Mathf.Abs(move));
		_grounded = IsGrounded();

		Flip(move);


		if (Input.GetKeyDown(KeyCode.UpArrow) && IsGrounded() == true)
		{
			//Debug.Log("Jump");
			_rigid.velocity = new Vector2(_rigid.velocity.x, jumpForce);
			StartCoroutine(ResetJumpNeededRoutine());
			//tell animator to jump
			_anim.SetBool("jump", true);
		}
	}

	void Flip(float move)
	{
		//if move is greater than 0
		if (move > 0)
		{
			//facing right
			_playerSprite.flipX = false;

		}
		//else if move < 0
		else if (move < 0)
		{
			//facing left
			_playerSprite.flipX = true;

		}
	}


	bool IsGrounded()
	{
		RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, 1.0f, _groundLayer.value);
		//Debug.Log("Raycast");
		if (hitInfo.collider != null)
		{
			//Debug.Log("collider != null");
			if (_resetJump == false)
			{
				//set animator bool to false
				_anim.SetBool("jump",false);
				//Debug.Log("jump = false");
				return true;
				
			}
		}

		return false;
	}

	IEnumerator ResetJumpNeededRoutine()
	{
		_resetJump = true;
		yield return new WaitForSeconds(0.1f);
		_resetJump = false;
	}
    void OnTriggerEnter2D(Collider2D other)
    {
        //Check the provided Collider2D parameter other to see if it is tagged "PickUp", if it is...
        if (other.gameObject.CompareTag("PickUp"))
        {
            //... then set the other object we just collided with to inactive.
            Destroy(other.gameObject);

            //Add one to the current value of our count variable.
            count = count + 1;
            //score.text=count.ToString();

            Debug.Log(count);
            //Update the currently displayed count by calling the SetCountText function.
            //SetCountText ();
            if (count == 4) {
                SceneManager.LoadScene(0,LoadSceneMode.Single);
            }
            
        }
        if (other.gameObject.CompareTag("Finish"))
        {
            SceneManager.LoadScene(0, LoadSceneMode.Single);
        }

        }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) {
            life--;
            Debug.Log("life="+life);
            if (life == 0) {
                SceneManager.LoadScene(0,LoadSceneMode.Single);
            }
        }
    }

}
