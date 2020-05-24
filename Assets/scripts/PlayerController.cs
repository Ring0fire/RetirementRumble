using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
		public float moveSpeed;
		public Rigidbody2D myRigidbody;
		public AttackGlossary attList;
	
		public int maxHealth;
		public float healthPoints;
		public float playerDamage = 2f;
		
		public GameObject pHitBox;

		public GameManager theGameManager;
		
		private float horizontalMovement;
		private float verticalMovement;
//		private float timeHitBoxActive;
		public bool isAttacking;
		public bool isDefending;
		public bool isHit;
		
		private Animator myAnimator;
		public bool isMoving;
//		public bool punching;
		
	void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D > ();
		myAnimator = GetComponent<Animator>();
		
		//hitBoxReturn = GetComponentInChildren<BoxCollider2D>();
		
		healthPoints = maxHealth;
		pHitBox.transform.localScale = new Vector2 (myRigidbody.transform.position.x, myRigidbody.transform.position.y + 0.5f);
		//timeHitBoxActive = punchDuration;
		horizontalMovement = 0;
		verticalMovement = 0;
		isAttacking = false;
		//isDefending = false;
		isHit = false;
		isMoving = false;
		
	}

    // Update is called once per frame
    void Update()
    {	

		if(Input.GetKeyDown(KeyCode.Space) && !isAttacking)
		{
				attList.Punch();	
		}
		
		if((Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D)) &&!isAttacking)
		{
			horizontalMovement = -1 * moveSpeed;
			transform.localScale = new Vector2 (-1f,1f);
		}
			else if((Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A)) &&!isAttacking)
			{
				horizontalMovement = moveSpeed;
				transform.localScale = new Vector2 (1f,1f);
			}
			else if(!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.A) || isAttacking )
			{
				horizontalMovement = 0;
			}	
			
		if((Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S)) && !isAttacking)
		{
			verticalMovement = (moveSpeed) / 3;
		}
			else if((Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W)) && !isAttacking)
			{
				verticalMovement = (-1 * moveSpeed) / 3;
			}
			else if(!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.S) || isAttacking)
			{
				verticalMovement = 0;
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
			myAnimator.SetBool("Moving", isMoving);
			myAnimator.SetBool("Punching", isAttacking);
	}
		void OnCollisionEnter2D (Collision2D other)
		{
			if (other.gameObject.tag == "hitBox" )
			{
				healthPoints = (healthPoints - theGameManager.enemyAttack);
				myAnimator.SetTrigger("PlayerHit");
				Debug.Log(other.gameObject.tag.ToString() + " of " + other.gameObject.name);
			}

		}
}
