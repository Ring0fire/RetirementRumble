using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour
{
	public RawImage healthBar;
	public RawImage currentHealth;
	
//	public GameObject enemyHpBar;
	 
	public GameObject enemyHpPrefab;
	public GameObject enemyHpBar;
	public GameObject enemyHp; 
	
	public PlayerController thePlayer;
	public GameObject theEnemy;
	
	private float percentOfHealth;
	private float enemyPercHP;
	private float playerHealth;
	private float enemyHealth;
	private float rectTransformP;
	
//	public List <GameObject> activeEmemies;

    // Start is called before the first frame update
    void Start()
    {
	//	activeEmemies = new List<GameObject>();
		rectTransformP = currentHealth.GetComponent<RectTransform>().sizeDelta.x;
	
    }

    // Update is called once per frame
    void Update()
    {		
	//	testimage.transform.SetParent(thePlayer.transform, true);
		
		if(playerHealth <= thePlayer.maxHealth)
			{
				currentHealth.transform.localScale = new Vector2 (percentOfHealth, 1f);
				currentHealth.transform.localPosition = new Vector2 ((healthBar.transform.localPosition.x -(rectTransformP/2)) + ((percentOfHealth*rectTransformP) /2)  , currentHealth.transform.localPosition.y);
			}

			playerHealth = thePlayer.healthPoints;
			percentOfHealth = playerHealth/ thePlayer.maxHealth;   
		
	
/*		if(healthPoints < enemyMaxHealth)
		{
			enemyHp.transform.localScale = new Vector2 (enemyPercHP , 1f);
			enemyHp.transform.localPosition = new Vector2 ((enemyHpBar.transform.localPosition.x +(recTE/2)) - ((enemyPercHP * recTE) /2),enemyHp.transform.localPosition.y);
		}	*/	
	}
	void EnemyHealthVisual()
	{
		enemyHpPrefab.SetActive(true);
		
	/*	theEnemy = GameObject.FindGameObjectsWithTag("hurtBox");
		GameObject hp = (GameObject) Instantiate (enemyHpPrefab);
		hp.transform.position = new Vector2 (theEnemy.transform.position.x, theEnemy.transform.position.y +1f);
		Debug.Log("bar at " + hp.transform.position + " Enemy at " + theEnemy.transform.position);
		hp.SetActive(true);*/
		
	}
}
