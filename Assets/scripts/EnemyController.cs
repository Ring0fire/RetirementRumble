using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
	public float healthPoints;
	public int damage;
	public int enemyMaxHealth = 15;
	private float enemyPercHP;
	public float enemyMoveSpeed;
	public Transform debugMarker;
	public GameObject eHitBox;
	
	public Vector2 enemyMoveTarget;
	private Vector2 curEnemyMvTarg;
	public float vertMvmt;
	public float horzMvmt;
	
	public GameObject enemyHpBar;
	public GameObject enemyHp;
	
	public Rigidbody2D myRigidbody;
	public CircleCollider2D playerDetect;
	
	public GameManager theGameManager;
	public AttackGlossary attList;
	
	private float enemyHealth;
	private float recTE;
	public Animator myAnimator;
	private float despawnTimer = 1f;
	
	public bool isHostile;
	public bool movingVert;
	public bool movingHorz;
    // Start is called before the first frame update
    void Start()
    {
		isHostile = false;
		healthPoints = enemyMaxHealth;
        myRigidbody = GetComponent<Rigidbody2D > ();   
   		myAnimator = GetComponent<Animator>();
		playerDetect = GetComponent<CircleCollider2D>();
		eHitBox.transform.localScale = new Vector2 (myRigidbody.transform.position.x, myRigidbody.transform.position.y + 0.5f);
		recTE = gameObject.GetComponentInChildren<RectTransform>().sizeDelta.x;
    }

    // Update is called once per frame
    void Update()
    {
		if(healthPoints < enemyMaxHealth)
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
	enemyPercHP = enemyHealth / enemyMaxHealth;
	}	
	void FixedUpdate()
	{	
	debugMarker.transform.position =  new Vector2 (enemyMoveTarget.x,enemyMoveTarget.y);
		horzMvmt = curEnemyMvTarg.x - transform.position.x;
		vertMvmt = curEnemyMvTarg.y - transform.position.y;
		
		if(curEnemyMvTarg.x == transform.position.x && curEnemyMvTarg.y == transform.position.y)
			{
				curEnemyMvTarg = enemyMoveTarget;
				movingHorz = false;
				movingVert = false;
			}
		else if (curEnemyMvTarg.x != transform.position.x || curEnemyMvTarg.y != transform.position.y)
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
		}
/*		else if (col.gameObject.tag == "Player")
		{
			attList.enemyPunch();
		}*/
	}
	void EnemyMovement()
	{	
		if(vertMvmt !=0 )
		{
			movingVert = true;	
		}	
		else if (vertMvmt == 0)
		{
			movingVert = false;
			movingHorz = true;
		}
		else if(horzMvmt == 0)
		{
			movingHorz = false;
		}
		if(movingVert)
		{
		movingHorz = false;	
		transform.position = Vector2.MoveTowards (transform.position, new Vector2 (transform.position.x,curEnemyMvTarg.y),  (enemyMoveSpeed / 3) * Time.deltaTime);
		}		
		 if(movingHorz)
		{
		movingVert = false;	
		transform.position = Vector2.MoveTowards (transform.position, new Vector2 (curEnemyMvTarg.x, transform.position.y),  enemyMoveSpeed * Time.deltaTime);
		}
	}

	}

