using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackGlossary : MonoBehaviour
{

	public float coolDownTime;
//  time the hit box will be active
	public float activeHitBox;
	private float hitBoxTimer;
	public PlayerController thePlayer;
	public EnemyController theEnemy;
	
void Start()
{

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
				thePlayer.myFistCollider.offset = new Vector2 (0f,.7f);
				//thePlayer.myFistCollider.enabled = false;
			}	
		}
	}
 	
	public void	Punch ()
	{
	hitBoxTimer = activeHitBox;	
	thePlayer.isAttacking = true;	
	thePlayer.myFistCollider.offset = new Vector2 (.5f, 0.7f);
	//thePlayer.myFistCollider.enabled = true;
	}
	

}
