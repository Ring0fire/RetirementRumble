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
	private float eHitBoxTimer;
	private float despawnTimer;
	
	public GameObject eHitBox;
	public Transform debugMarker;
//	public Transform attRange;
	
	public Vector2 enemyMoveTarget;
	private Vector2 curEnemyMvTarg;
	
	public Transform enBoundpA;
	
	public float vertMvmt;
	public float horzMvmt;
	
	public GameObject enemyHealthBar;
	public GameObject enemyHp;
	
	public Rigidbody2D myRigidbody;
	public CircleCollider2D playerDetect;
	
	public AttackGlossary attList;
	
	private float recTE;
	private float enemyHealth;
	public Animator myAnimator;
	public LayerMask PlayerHurtBox;

	public bool isHostile;
	public bool enemyIsAtt;
	public bool closeEnoughToAttack;
	public bool movingVert;
	public bool movingHorz;
	
    // Start is called before the first frame update
    void Start()
    {
	//	eHitBox.transform.localPosition = new Vector2 (-0.3f,0.5f);
		despawnTimer = .5f;
		isHostile = false;
		enemyIsAtt = false;
		closeEnoughToAttack = false;
		healthPoints = enemyMaxHealth;
        myRigidbody = GetComponent<Rigidbody2D > ();   
   		myAnimator = GetComponent<Animator>();
		playerDetect = GetComponent<CircleCollider2D>();
		eHitBox.transform.localScale = new Vector2 (0, 0.5f);
		recTE = enemyHp.GetComponent<RectTransform>().sizeDelta.x;
    }

    // Update is called once per frame
    void Update()
    {
/*	
		if ((Mathf.Abs(theGameManager.thePlayer.transform.position.x - transform.position.x) < 0.75f)&& (Mathf.Abs(theGameManager.thePlayer.transform.position.y - transform.position.y) < 0.25f))
		{
			closeEnoughToAttack = true;			
		//	Debug.Log("The player should be at " + theGameManager.thePlayer.transform.position.x + " and the enemy should be at " + transform.position.x );
		}
		else if(Mathf.Abs(theGameManager.thePlayer.transform.position.x - transform.position.x) > 0.75f && (Mathf.Abs(theGameManager.thePlayer.transform.position.y - transform.position.y) > 0.25f))
		{
			closeEnoughToAttack = false;
		}
	*/
		if(healthPoints < enemyMaxHealth)
		{
			enemyHealthBar.SetActive(true);
			enemyHp.SetActive(true);
			enemyHp.transform.localScale = new Vector2 (enemyPercHP , 1f);
			enemyHp.transform.localPosition = new Vector2 ((enemyHealthBar.transform.localPosition.x +(recTE/2)) - ((enemyPercHP * recTE) /2),enemyHp.transform.localPosition.y);
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
		enemyPercHP = enemyHealth / enemyMaxHealth;
		myAnimator.SetBool("EnemyAttack",enemyIsAtt);
		
		
//		debugMarker.transform.position =  new Vector2 (curEnemyMvTarg.x,curEnemyMvTarg.y);
		horzMvmt = curEnemyMvTarg.x - transform.position.x;
		vertMvmt = curEnemyMvTarg.y - transform.position.y;
	
		closeEnoughToAttack = Physics2D.OverlapCircle( enBoundpA.position ,.2f, PlayerHurtBox);
		
		if (isHostile)
		{	
			if(closeEnoughToAttack)
			{
				EnemyAttack();
			}
			else if (!closeEnoughToAttack)
			{
				curEnemyMvTarg = enemyMoveTarget;
			}	
		}
		if (attCoolDownTimer > 0 && eHitBoxTimer ==0)
		{
			attCoolDownTimer -= Time.deltaTime;
		}	
		else if (attCoolDownTimer <= 0)
		{
			attCoolDownTimer = 0;
		}
		if(enemyIsAtt)
		{
		eHitBoxTimer -= Time.deltaTime;	
	//	curEnemyMvTarg =  Vector2.Lerp (transform.position, theGameManager.currentPlayerPos, .75f);
		
		
			if (eHitBoxTimer <=0 )
			 {
				eHitBoxTimer = 0;
			//	attCoolDownTimer = attList.coolDownTime;
				eHitBox.SetActive (false);
				eHitBox.transform.localPosition = new Vector2 (-0.3f,0.5f);
				eHitBox.transform.localScale = new Vector2 (0.1f, 0.1f);
				enemyIsAtt = false;
			//	attCoolDownTimer -=Time.deltaTime;
			}
		}
		else if(!enemyIsAtt)
		{
			if (attCoolDownTimer > 0)
				{
				//	enemyIsAtt = false;
					attCoolDownTimer -= Time.deltaTime;
				}	
				if (attCoolDownTimer <= 0)
				{
					attCoolDownTimer = 0;
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
		//	debugMarker.transform.position =  new Vector2 (curEnemyMvTarg.x,curEnemyMvTarg.y);
		}	
		myAnimator.SetBool("EnemyAttack",enemyIsAtt);
	}
	
	void OnCollisionEnter2D (Collision2D other)
	{
		if (other.gameObject.tag == "playerHitBox" )
		{
			healthPoints = (healthPoints - theGameManager.attackerDmg);
			attCoolDownTimer = (attList.coolDownTime);	
			myAnimator.SetTrigger("EnemyHit");
		}
	}
	void OnTriggerEnter2D (Collider2D col)
	{
		if ((col.gameObject.tag == "PlayerRange"))
		{
			enemyMoveTarget = GameObject.FindWithTag("Player").transform.position;
			isHostile = true;
			Debug.Log ( GameObject.FindWithTag("Player").transform.position.ToString());
		}

	}
	void OnTriggerExit2D (Collider2D col2d)
	{
		if (col2d.gameObject.tag == "PlayerRange")
		{
			isHostile = false;
		}
	}
	void EnemyMovement()
	{	
		if(horzMvmt != 0 )
		{
			movingHorz = true;	
			
		}	
		else if (Mathf.Approximately(horzMvmt, 0f) )
		{
			movingHorz = false;
			horzMvmt = 0;
			movingVert = true;	
		}
		else if(Mathf.Approximately(vertMvmt, 0f))
		{
			movingVert = false;
			vertMvmt  = 0;
		}
		 if(movingHorz)
		{	
			transform.position = Vector2.MoveTowards (transform.position, new Vector2 (curEnemyMvTarg.x, transform.position.y),  enemyMoveSpeed * Time.deltaTime);	

			if(curEnemyMvTarg.x < transform.position.x)
			{
				transform.localScale = new Vector2 (1,1);
			}
			else if(curEnemyMvTarg.x > transform.position.x)
			{
				transform.localScale = new Vector2 (-1,1);
			}
		}
		else if(movingVert)
		{
		transform.position = Vector2.MoveTowards (transform.position, new Vector2 (curEnemyMvTarg.x, curEnemyMvTarg.y),  (enemyMoveSpeed /2) * Time.deltaTime);
	//	Debug.Log ("moving vert");
		}
	}
	void EnemyAttack ()
	{	
		movingHorz =false;
		movingVert =false;
		enemyIsAtt = true;
		attList.ePunch();
		eHitBox.SetActive(true);
	
		if (eHitBoxTimer ==0 && attCoolDownTimer == 0)
		{
			attCoolDownTimer = attList.coolDownTime;	
				eHitBoxTimer = attList.eHitBoxActive;
		}

		curEnemyMvTarg = transform.position;
		eHitBox.transform.localScale = new Vector2 (attList.eHitBoxShape.x, attList.eHitBoxShape.y);
		eHitBox.transform.localPosition = new Vector2 (-0.3f,0.5f);
	//	Debug.Log ("I punch now");
	}

}

