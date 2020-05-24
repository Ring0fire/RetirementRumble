using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour
{
	public RawImage healthBar;
	public RawImage currentHealth;
	public Camera mainCamera;
	
	public PlayerController thePlayer;
	public GameObject theEnemy;
	
	private float percentOfHealth;
	private float playerHealth;
	private float rectTransformP;


    // Start is called before the first frame update
    void Start()
    {

		rectTransformP = currentHealth.GetComponent<RectTransform>().sizeDelta.x;
	
    }
/*
 
   
*/
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

	}
}
