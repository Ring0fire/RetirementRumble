using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour
{
	public RawImage healthBar;
	public RawImage currentHealth;
	
	private RawImage enemyHpBar;
	private RawImage enemyHp; 
	
	public PlayerController thePlayer;
	public EnemyController theEnemy;
	
	private float percentOfHealth;
	private float enemyPercHP;
	private float playerHealth;
	private float enemyHealth;
	private float rectTransformP;

    // Start is called before the first frame update
    void Start()
    {
		rectTransformP = currentHealth.GetComponent<RectTransform>().sizeDelta.x;
	
    }

    // Update is called once per frame
    void Update()
    {		
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
}
