using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public PlayerController thePlayer; 
	public EnemyController theEnemy;
	public AttackGlossary attList;
	public GUIManager theGUI;
	
	public GameObject[] activeEnemies;
	public GameObject[] spawners;
	
	public float playerAttack;
	public float enemyAttack;
	
	private Vector3 playerStartPoint;
	public Vector2 currentPlayerPos;
	public float attackerDmg;
	public float damageToTake;
	
	public GameObject gameOverScreen;
	public bool resetSpawner;
   
   // Start is called before the first frame update
    void Start()
    {
        playerStartPoint = thePlayer.transform.position;

	}
    // Update is called once per frame
    void Update()
    {
	playerAttack = thePlayer.playerDamage * attList.PlayerDamageMod;
	attackerDmg = playerAttack;
	currentPlayerPos = new Vector2( thePlayer.transform.position.x, thePlayer.transform.position.y);

	enemyAttack = attList.enemyStndDmg ;

	activeEnemies = GameObject.FindGameObjectsWithTag("hurtBox");
	spawners = GameObject.FindGameObjectsWithTag("spawnPnt");
	}	
	
	public void GameOver()
	{
		foreach (GameObject Enemy in activeEnemies)
		{
			Enemy.SetActive(false);
		}
		gameOverScreen.gameObject.SetActive(true);
		Time.timeScale = 0f;
	}
	
	public void Reset()
	{
		resetSpawner = true;
		gameOverScreen.gameObject.SetActive(false);
		thePlayer.transform.position = playerStartPoint;
		thePlayer.healthPoints = thePlayer.maxHealth;
		thePlayer.gameObject.SetActive(true);
	
		foreach (GameObject EnemySpawnBox in spawners)
			{
			EnemySpawnBox.SetActive(true);
			EnemySpawnBox.GetComponent<Spawner>().ResetSpawn();
			}
		Time.timeScale = 1f;
	}
	
	
}

