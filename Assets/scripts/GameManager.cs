using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public PlayerController thePlayer; 
	public EnemyController theEnemy;

	private Vector3 playerStartPoint;
	private int playerHP;
	private int enemyHP;
   
   // Start is called before the first frame update
    void Start()
    {
        playerStartPoint = thePlayer.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
		
    }
	
	public void takeDamage()
	{
		playerHP = (thePlayer.healthPoints - theEnemy.damage);
		thePlayer.healthPoints = playerHP;
	}
	public void doDamage()
	{
		enemyHP = (theEnemy.healthPoints - thePlayer.playerDamage);
		theEnemy.healthPoints = enemyHP;		
	}
	public void enemyDeath()
	{
		theEnemy.gameObject.SetActive(false);
	}
		
}
