using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
	public int healthPoints = 10;
	public int damage = 1;
	public Rigidbody2D myRigidbody;
	public GameManager theGameManager;
	private Animator myAnimator;
	private float despawnTimer = 1f;
	
    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D > ();   
   		myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
    if (healthPoints <= 0)
	{
		despawnTimer -= Time.deltaTime;	 
		myAnimator.SetFloat("Dead", healthPoints);
			 if (despawnTimer <= 0)
			 {
				 despawnTimer = 0;
				 theGameManager.enemyDeath();
			 }
		}
	 
    }
	void OnCollisionEnter2D (Collision2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			theGameManager.doDamage();
			myAnimator.SetTrigger("EnemyHit");
		}
		
	}
}
