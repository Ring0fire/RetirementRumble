using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour
{
	public RawImage healthBar;
	public RawImage currentHealth;
	public RectTransform healthRect;
	
	public PlayerController thePlayer;
	public EnemyController theEnemy;
	
	private float percentOfHealth;
	private float healthUpdate;
	private float healthScale;
	private float playerhealth;
    // Start is called before the first frame update
    void Start()
    {
		healthRect = GetComponent<RectTransform>();

		
	//	currentHealth.RectTransform.size.x = healthScale;
    }

    // Update is called once per frame
    void Update()
    {
		percentOfHealth = thePlayer.healthPoints / thePlayer.maxHealth; 
		healthUpdate = percentOfHealth * healthScale;
		healthBar.transform.position = new Vector2 (healthBar.transform.position.x - healthUpdate, healthBar.transform.position.y);
		playerhealth = thePlayer.healthPoints;
		
    }
}
