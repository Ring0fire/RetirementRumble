using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public PlayerController thePlayer; 
	public EnemyController theEnemy;
	public AttackGlossary attList;
	public Spawner theSpawner;
	public GUIManager theGUI;
	
	public float playerAttack;
	public float enemyAttack;
	
	//private Vector3 playerStartPoint;
	public Vector2 currentPlayerPos;
	public float attackerDmg;
	public float damageToTake;
	
//	public LayerMask hitBox;
//	public LayerMask hurtBox;
   
   // Start is called before the first frame update
    void Start()
    {
    //    playerStartPoint = thePlayer.transform.position;
		

	}
    // Update is called once per frame
    void Update()
    {
	playerAttack = thePlayer.playerDamage * attList.PlayerDamageMod;
	attackerDmg = playerAttack;
	currentPlayerPos = new Vector2( thePlayer.transform.position.x, thePlayer.transform.position.y -.5f);
	
	
	enemyAttack = attList.enemyStndDmg ;		
		
		
	}	
	
}
