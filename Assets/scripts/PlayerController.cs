using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
		public float moveSpeed;
		public Rigidbody2D myRigidbody;
		public CircleCollider2D myFistCollider;
		public AttackGlossary attList;
	
		public int maxHealth = 10;
		public int healthPoints;
		public int playerDamage = 2;
		public GameManager theGameManager;
		//public float punchDuration;
		
		private float horizontalMovement;
		private float verticalMovement;
//		private float timeHitBoxActive;
		public bool isAttacking;
		public bool isHit;
		
		private Animator myAnimator;
		private bool isMoving;
		
	void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D > ();
		myFistCollider = GetComponent<CircleCollider2D > ();
		myAnimator = GetComponent<Animator>();

		
		healthPoints = maxHealth;
		myFistCollider.offset = new Vector2 (myRigidbody.transform.position.x, myRigidbody.transform.position.y + 0.5f);
		//timeHitBoxActive = punchDuration;
		horizontalMovement = 0;
		verticalMovement = 0;
		isAttacking = false;
		isHit = false;
		isMoving = false;
	}

    // Update is called once per frame
    void Update()
    {	

		
		if(Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
		{
			horizontalMovement = -1 * moveSpeed;
		}
			else if(Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
			{
				horizontalMovement = moveSpeed;
			}
			else if(!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.A) )
			{
				horizontalMovement = 0;
			}	
			
		if(Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
		{
			verticalMovement = (moveSpeed) / 3;
		}
			else if(Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W))
			{
				verticalMovement = (-1 * moveSpeed) / 3;
			}
			else if(!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.S) )
			{
				verticalMovement = 0;
			}	
		if(Input.GetKeyDown(KeyCode.Space) && !isAttacking)
		{
			attList.Punch();	
		
		}
			
		myRigidbody.velocity = new Vector2 (horizontalMovement ,verticalMovement);
		
		if (verticalMovement !=  0 || horizontalMovement != 0)
			{
				isMoving = true;
			}
		else if(verticalMovement == 0 && horizontalMovement == 0)
			{
				isMoving = false;
			}
		//	myAnimator.SetFloat("SideSpeed", horizontalMovement);
			myAnimator.SetBool("Moving", isMoving);
			myAnimator.SetBool("Punching", isAttacking);
		//	myAnimator.SetBool("PlayerHit", isHit);
		
	}
	
	void OnCollisionEnter2D (Collision2D other)
	{
		if (other.gameObject.tag == "Enemy")
		{
			theGameManager.takeDamage();
		}
		
	}
}
