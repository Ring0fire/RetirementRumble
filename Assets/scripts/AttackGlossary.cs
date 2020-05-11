using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackGlossary : MonoBehaviour
{

	public float coolDownTime;

	public float activeHitBox;
	public float eHitBoxActive;
	private float hitBoxTimer;
	private float eHitBoxTimer;
	public PlayerController thePlayer;
	public EnemyController theEnemy;
	public GameManager theGameManager;
	public float PlayerDamageMod;
	
	public int enemyStndDmg = 2;
	
void Start()
{
	PlayerDamageMod = 1f;

}
	void Update()
	{
		
		if(thePlayer.isAttacking)
		{
			hitBoxTimer -= Time.deltaTime;
		
			 if (hitBoxTimer <=0 )
			{
				hitBoxTimer = 0;
				thePlayer.isAttacking = false;
				thePlayer.pHitBox.transform.localPosition = new Vector2(0,0);
				thePlayer.pHitBox.transform.localScale = new Vector2 (0f,1f);

			}	
		}
		if(theEnemy.isHostile)
		{
			eHitBoxTimer -= Time.deltaTime;
			 if (eHitBoxTimer <=0 )
			{
				eHitBoxTimer = 0;
				theEnemy.isHostile = false;
				theEnemy.eHitBox.transform.localPosition = new Vector2(0,0);
				theEnemy.eHitBox.transform.localScale = new Vector2 (0f,1f);

			}	
		}
	}
 	
	public void	Punch ()
	{
	hitBoxTimer = activeHitBox;	
	thePlayer.isAttacking = true;
	
	
	thePlayer.pHitBox.transform.localPosition = new Vector2(.3f,.5f);
	thePlayer.pHitBox.transform.localScale = new Vector2 (.25f, 0.5f);
	
	}
	public void	enemyPunch ()
	{
	eHitBoxTimer = eHitBoxActive;	
	theEnemy.isHostile = true;
	
	
	theEnemy.eHitBox.transform.localPosition = new Vector2(-.3f,.5f);
	theEnemy.eHitBox.transform.localScale = new Vector2 (-.25f, 0.5f);
	
	}

}
