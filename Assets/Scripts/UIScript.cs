using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour {

    public GameObject scorePopup;
    public int high, score;

	public List<Image> lives = new List<Image>(3);

	Text txt_score, txt_high, txt_level;
	
	// Use this for initialization
	void Start () 
	{
		txt_score = GetComponentsInChildren<Text>()[1];
		txt_high = GetComponentsInChildren<Text>()[0];
        txt_level = GetComponentsInChildren<Text>()[2];

	    for (int i = 0; i < 3 - GameManager.lives; i++)
	    {
	        Destroy(lives[lives.Count-1]);
            lives.RemoveAt(lives.Count-1);
	    }

        high = GameObject.Find("Game Manager").GetComponent<ScoreManager>().High();
    }
	
	// Update is called once per frame
	void Update () 
	{
        // update score text
	    if (GameManager.score > score) {
            Quaternion randomRot = Quaternion.Euler(new Vector3(0f, 0f, Random.Range(-30f, 30f)));
	        var go = (GameObject)Instantiate(scorePopup, txt_score.transform.position, randomRot);
            go.transform.SetParent(txt_score.transform);
            go.GetComponent<ScorePopup>().SetText((GameManager.score - score).ToString());

            score = GameManager.score;
        }
		txt_score.text = "Score\n" + score;
		txt_high.text = "High Score\n" + high;
	    txt_level.text = "Level\n" + (GameManager.Level + 1);

	}


}
