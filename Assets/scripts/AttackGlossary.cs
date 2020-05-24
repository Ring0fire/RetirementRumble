using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackGlossary : MonoBehaviour
{
	public float activeHitBox;
	public float coolDownTime;
	private float hitBoxTimer;
	
	public Vector2 eHitBoxShape;
	public float eHbsX;
	public float eHbsY;
	public float eHitBoxActive;
	
	public PlayerController thePlayer;
	public EnemyController theEnemy;
	public GameManager theGameManager;
	public float PlayerDamageMod;
	
	public int enemyStndDmg = 2;
//	public bool enemyAtt;
//	public bool ePunch;
	
void Start()
{
	PlayerDamageMod = 1f;
	//enemyAtt = false;
	//ePunch = false;
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
//	eHitBoxShape = new Vector2 (eHbsX,eHbsY);
	}

	public void	Punch ()
	{
	hitBoxTimer = activeHitBox;	
	thePlayer.isAttacking = true;
	
	thePlayer.pHitBox.transform.localPosition = new Vector2(0.3f,0.5f);
	thePlayer.pHitBox.transform.localScale = new Vector2 (0.25f, 0.5f);
			Debug.Log ("I want to punch");
	}
	public void ePunch()
	{
		eHitBoxActive = 0.5f;
		eHitBoxShape = new Vector2 (0.3f,0.5f);
		coolDownTime = 1f;
//		Debug.Log("attGlossary knows about ePunch");	
	}
}
