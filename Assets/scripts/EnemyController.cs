using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
	public GameManager theGameManager;
	public int enemyMaxHealth = 15;

	public float healthPoints;
	private float enemyPercHP;
	public float enemyMoveSpeed;
	
	public float attCoolDownTimer;
	public float eHitBoxTimer;
	
	public GameObject eHitBox;
	public Transform debugMarker;
	
	public Vector2 enemyMoveTarget;
	private Vector2 curEnemyMvTarg;
	public float vertMvmt;
	public float horzMvmt;
	
//	public GameObject enemyHealthBar;
	
	public Rigidbody2D myRigidbody;
	public CircleCollider2D playerDetect;
	
	
	public AttackGlossary attList;
	
	private float enemyHealth;
//	private float recTE;
	public Animator myAnimator;
//	private float despawnTimer = 1f;
	
	public bool isHostile;
	public bool enemyIsAtt;
	public bool closeEnoughToAttack;
	public bool movingVert;
	public bool movingHorz;
    // Start is called before the first frame update
    void Start()
    {
	//	eHitBox.transform.localPosition = new Vector2 (-0.3f,0.5f);
		isHostile = false;
		enemyIsAtt = false;
		closeEnoughToAttack = false;
		healthPoints = enemyMaxHealth;
        myRigidbody = GetComponent<Rigidbody2D > ();   
   		myAnimator = GetComponent<Animator>();
		playerDetect = GetComponent<CircleCollider2D>();
		eHitBox.transform.localScale = new Vector2 (0, 0.5f);
	//	recTE = gameObject.GetComponentInChildren<RectTransform>().sizeDelta.x;
    }

    // Update is called once per frame
    void Update()
    {
		closeEnoughToAttack = (Mathf.Abs(theGameManager.currentPlayerPos.x - transform.position.x) < 0.75f);
	/* to be fixed with updating enemy health
	
	
		if (Mathf.Abs(theGameManager.thePlayer.transform.position.x - transform.position.x) < 0.9f)
		{
			closeEnoughToAttack = true;			
			Debug.Log("The player should be at " + theGameManager.thePlayer.transform.position.x + " and the enemy should be at " + transform.position.x );
		}
		else if(Mathf.Abs(theGameManager.thePlayer.transform.position.x - transform.position.x) > 0.9f)
		{
			closeEnoughToAttack = false;
		}*/
	/*	if(healthPoints < enemyMaxHealth)
		{
			enemyHp.transform.localScale = new Vector2 (enemyPercHP , 1f);
			enemyHp.transform.localPosition = new Vector2 ((enemyHpBar.transform.localPosition.x +(recTE/2)) - ((enemyPercHP * recTE) /2),enemyHp.transform.localPosition.y);
		}	
			
		if (healthPoints <= 0)
		{		
			despawnTimer -= Time.deltaTime;	 
			myAnimator.SetFloat("Dead", healthPoints);
			enemyHp.transform.localScale = new Vector2 (0f , 1f);
			if (despawnTimer <= 0)
			{
				despawnTimer = 0;
				gameObject.SetActive(false);
			}
		}
	enemyHealth	 = healthPoints;
	enemyPercHP = enemyHealth / enemyMaxHealth;*/
	myAnimator.SetBool("EnemyAttack",enemyIsAtt);
	}	
	void FixedUpdate()
	{			
		
		debugMarker.transform.position =  new Vector2 (curEnemyMvTarg.x,curEnemyMvTarg.y);
		horzMvmt = curEnemyMvTarg.x - transform.position.x;
		vertMvmt = curEnemyMvTarg.y - transform.position.y;
		
		if (attCoolDownTimer > 0)
		{
			attCoolDownTimer -= Time.deltaTime;
			if (attCoolDownTimer <= 0)
			{
				attCoolDownTimer = 0;
			}
		}
		if (closeEnoughToAttack && isHostile)
		{
			if (attCoolDownTimer <= 0)
			{
				enemyIsAtt = true;
				EnemyAttack();
			}
		}
	
		if(eHitBoxTimer > 0)
		{
		curEnemyMvTarg.x = transform.position.x;
			eHitBoxTimer -= Time.deltaTime;
			if (eHitBoxTimer <=0 )
			 {
				eHitBoxTimer = 0;
				attCoolDownTimer = attList.coolDownTime;
				eHitBox.SetActive (false);
				eHitBox.transform.localPosition = new Vector2 (-0.3f,0.5f);
				eHitBox.transform.localScale = new Vector2 (0.1f, 0.1f);
				enemyIsAtt = false;
			 }
		}
			if(curEnemyMvTarg.x == transform.position.x && curEnemyMvTarg.y == transform.position.y)
			{
				movingHorz = false;
				movingVert = false;
				
				if(!isHostile)
				{
					curEnemyMvTarg = enemyMoveTarget;
				}
			}
		if (curEnemyMvTarg.x != transform.position.x || curEnemyMvTarg.y != transform.position.y)
		{
			EnemyMovement();
		}	
	}
	
	void OnCollisionEnter2D (Collision2D other)
	{
		if (other.gameObject.tag == "hitBox" )
		{
			healthPoints = (healthPoints - theGameManager.attackerDmg);
			myAnimator.SetTrigger("EnemyHit");
		}
	}
	void OnTriggerEnter2D (Collider2D col)
	{
		if ((col.gameObject.tag == "PlayerRange"))
		{
			enemyMoveTarget = col.GetComponent<Collider2D>().transform.position;
		//	enemyMoveTarget = theGameManager.currentPlayerPos;
		}
		if (col.gameObject.tag == "playerHurtBox")
		{
			isHostile = true;
			curEnemyMvTarg.y = theGameManager.currentPlayerPos.y;
			Debug.Log ("hostile y correction");
		}
	}
	void OnTriggerExit2D (Collider2D col2d)
	{
		if (col2d.gameObject.tag == "playerHurtBox")
		{
			isHostile = false;
			movingHorz = false;
			closeEnoughToAttack = false;
		}
	}
	
	void EnemyMovement()
	{	
		if(vertMvmt != 0 )
		{
			movingVert = true;	
			
		}	
		else if (Mathf.Approximately(vertMvmt, 0f) )
		{
			movingVert = false;
			vertMvmt = 0;
			movingHorz = true;
			
		}
		else if(Mathf.Approximately(horzMvmt, 0f))
		{
			movingHorz = false;
			horzMvmt  = 0;
		}
		if(movingVert)
		{
		transform.position = Vector2.MoveTowards (transform.position, new Vector2 (transform.position.x,curEnemyMvTarg.y),  (enemyMoveSpeed /2) * Time.deltaTime);
		Debug.Log ("moving vert");
		}		
		 else if(movingHorz)
		{	
			transform.position = Vector2.MoveTowards (transform.position, new Vector2 (curEnemyMvTarg.x, curEnemyMvTarg.y),  enemyMoveSpeed * Time.deltaTime);	
			Debug.Log ("moving horz");
			if(curEnemyMvTarg.x < transform.position.x)
			{
				transform.localScale = new Vector2 (1,1);
			}
			else if(curEnemyMvTarg.x > transform.position.x)
			{
				transform.localScale = new Vector2 (-1,1);
			}
		}
	}
	void EnemyAttack ()
	{	
		movingHorz =false;
		movingVert =false;
		attList.ePunch();
		eHitBox.SetActive(true);
		eHitBoxTimer = attList.eHitBoxActive;
	//	attCoolDownTimer = attList.coolDownTime;
		curEnemyMvTarg = transform.position;
		eHitBox.transform.localScale = new Vector2 (attList.eHitBoxShape.x, attList.eHitBoxShape.y);
		eHitBox.transform.localPosition = new Vector2 (-0.3f,0.5f);
	//	Debug.Log ("I punch now");

	}
}

