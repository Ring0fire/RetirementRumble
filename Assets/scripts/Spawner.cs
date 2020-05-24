﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
	public GameObject pooledEnemy;
	public int spawnCountMax;
	public float timeBetweenSpawn;
	
	private int objSpawnCounter;
	private float spawnTimer;
	
	public float spawnTransformDiff;

	private float spawnPositionDiffX;
	private float spawnPositionDiffY;	
	
	public Transform spawnPoint;
	public CircleCollider2D playerDetect;
	public GameManager theGameManager;
	
	private bool canSpawn;
	
    // Start is called before the first frame update
    void Start()
    {
		spawnTimer = timeBetweenSpawn;
	
	}

    // Update is called once per frame
    void Update()
    {	
		if((objSpawnCounter < spawnCountMax) && canSpawn)
		{
		
		spawnTimer -=Time.deltaTime;		 
			if (spawnTimer <= 0)
				{
					spawnTimer = 0;
					Spawn();
					spawnTimer += timeBetweenSpawn;
				}
		}
    }
	public void Spawn()
	{
		spawnPositionDiffX = Random.Range (spawnTransformDiff, -spawnTransformDiff);
		spawnPositionDiffY = Random.Range (spawnTransformDiff/3, -spawnTransformDiff/3);
		
		
		GameObject foe = (GameObject) Instantiate (pooledEnemy);
		objSpawnCounter++;
		foe.transform.position = new Vector2 (spawnPoint.transform.position.x + spawnPositionDiffX, spawnPoint.transform.position.y + spawnPositionDiffY);
		foe.SetActive(true);
			if(objSpawnCounter >= spawnCountMax)
			{
				canSpawn = false;
				gameObject.transform.localScale = new Vector2 (0f,0f);
			}
	}
	
	public void ResetSpawn()
	{
	
		
		objSpawnCounter = 0;
		
		gameObject.transform.localScale = new Vector2 (1f,1f);
	}
	void OnTriggerEnter2D (Collider2D other)
	{
		if ((other.gameObject.tag == "Player") &&(objSpawnCounter < spawnCountMax))
		{
			canSpawn = true;	
		}
	}
	void OnTriggerExit2D (Collider2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			canSpawn = false;	
		}
	}
}
