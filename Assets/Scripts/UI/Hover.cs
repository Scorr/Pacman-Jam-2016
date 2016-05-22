using UnityEngine;

public class Hover : MonoBehaviour {

    [SerializeField] private float hoverTime = 1f;
    [SerializeField] private float hoverDistance = 5f;

	private void Start () {
	    float newY = transform.position.y + hoverDistance;
	    LeanTween.moveY(gameObject, newY, hoverTime).setLoopPingPong();
	}
}
