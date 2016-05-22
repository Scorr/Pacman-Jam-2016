using UnityEngine;

public class Pacdot : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other)
	{
		if(other.name == "pacman")
		{
            AudioManager.Instance.PlaySound("pickup");
            GameObject.Find("ChargeBar").GetComponent<ChargeBar>().AddCharge();

			GameManager.score += 10;
		    GameObject[] pacdots = GameObject.FindGameObjectsWithTag("pacdot");

            Destroy(gameObject);

		    if (pacdots.Length == 1)
		    {
		        GameObject.FindObjectOfType<GameGUINavigation>().LoadLevel();
		    }
		}
	}
}
