using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float moveSpeed =3f;
	public float jumpHeight = 10f;

	private bool grounded;

	private Animator anim;
	private Rigidbody2D rb;

	public Transform groundProbe;
	public float groundProbeRadius = 0.1f;
	public LayerMask groundLayer;

	private AudioSource jumpSound;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		rb = GetComponent<Rigidbody2D> ();
		jumpSound = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		anim.SetFloat ("SpeedY", rb.velocity.y);
		anim.SetBool ("Land", grounded);
		anim.SetBool ("Running", false);
		anim.SetBool ("Jump", false);
		anim.SetBool ("Grounded", grounded);


		if (Input.GetKey (KeyCode.D)) {
			transform.localScale = new Vector3 (1, 1, 1);
			rb.velocity = new Vector2 (moveSpeed, rb.velocity.y);
			anim.SetBool ("Running", true);
		}
		if (Input.GetKey (KeyCode.A)) {
			transform.localScale = new Vector3 (-1, 1, 1);
			rb.velocity = new Vector2 (-moveSpeed, rb.velocity.y);
			anim.SetBool ("Running", true);
		}
		if (Input.GetKeyDown (KeyCode.Space)) {
			rb.velocity = new Vector2 (rb.velocity.x, jumpHeight);
			anim.SetBool ("Jump", true);
			jumpSound.Play ();
		}
		grounded = Physics2D.OverlapCircle (groundProbe.position, groundProbeRadius, groundLayer);
		if (Input.GetKeyDown (KeyCode.Space) && grounded) {
			rb.velocity = new Vector2 (rb.velocity.x, jumpHeight);
		}
	}
}
