using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeperScript : MonoBehaviour {
    public GameObject JovanCoinGO;
    public int points;
    public bool isDeactivated = false;
    //Random random = new Random();
    
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(isDeactivated)
        {
            //float x = Random.value * 30;
            //float y = Random.value * 30;

            //float x = 5;
            //float y = 3;

            //JovanCoinGO.transform.position.Set(x, y, 0);
            //JovanCoinGO.SetActive(true);
            //isDeactivated = false;
        }
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "JovanCoinTag")
        {
            points += 10;
            float x = 5;
            float y = 3;

            other.gameObject.transform.position.Set(x, y, 0);
            //other.gameObject.SetActive(true);
            //other.gameObject.SetActive(false);
            //isDeactivated = true;
        }       
    }
}
