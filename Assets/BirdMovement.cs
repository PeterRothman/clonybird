using UnityEngine;
using System.Collections;

public class BirdMovement : MonoBehaviour {

	Vector3 velocity = Vector3.zero;
	public float flapSpeed    = 100f;
	public float forwardSpeed = 0.01f;

	bool didFlap = false;
    bool up;
    bool down;

	Animator animator;

	public bool dead = false;
	float deathCooldown;

	public bool godMode = false;

	// Use this for initialization
	void Start () {
		animator = transform.GetComponentInChildren<Animator>();

		if(animator == null) {
			Debug.LogError("Didn't find animator!");
		}
	}

	// Do Graphic & Input updates here
	void Update() {
		if(dead) {
			deathCooldown -= Time.deltaTime;

			if(deathCooldown <= 0) {
				if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) ) {
					Application.LoadLevel( Application.loadedLevel );
				}
			}
		}
		else {
			if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) ) {
				didFlap = true;
			}

		    if (Input.GetKey(KeyCode.UpArrow))
		    {
		        up = true;
		    }
		    if (Input.GetKey(KeyCode.DownArrow))
		    {
		        down = true;
		    }
        }
	}

	
	// Do physics engine updates here
	void FixedUpdate () {

		if(dead)
			return;

        //GetComponent<Rigidbody2D>().AddForce( Vector2.right * forwardSpeed );
	    Vector3 pos = transform.position;
	    pos.x += forwardSpeed * Time.deltaTime;
	    transform.position = pos;

        animator.SetTrigger("DoFlap");

	    if (up)
	    {

	        pos = transform.position;
	        pos.y += forwardSpeed * Time.deltaTime;
	        transform.position = pos;

            up = false;
	    }
	    else if (down)
	    {

	        pos = transform.position;
	        pos.y -= forwardSpeed * Time.deltaTime;
	        transform.position = pos;

	        down = false;
	    }

        //if (didFlap) {
        //	GetComponent<Rigidbody2D>().AddForce( Vector2.up * flapSpeed );
        //	animator.SetTrigger("DoFlap");


        //	didFlap = false;
        //}

        //if(GetComponent<Rigidbody2D>().velocity.y > 0) {
        //	transform.rotation = Quaternion.Euler(0, 0, 0);
        //}
        //else {
        //	float angle = Mathf.Lerp (0, -90, (-GetComponent<Rigidbody2D>().velocity.y / 3f) );
        //	transform.rotation = Quaternion.Euler(0, 0, angle);
        //}
    }

	void OnCollisionEnter2D(Collision2D collision) {
		if(godMode)
			return;

		animator.SetTrigger("Death");
		dead = true;
		deathCooldown = 0.5f;
	}
}
