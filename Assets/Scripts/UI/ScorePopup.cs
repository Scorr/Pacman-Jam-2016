using UnityEngine;
using UnityEngine.UI;

public class ScorePopup : MonoBehaviour {

    private Text uiText;

    private void Awake() {
        uiText = GetComponent<Text>();
    }

    private void Start() {
        LeanTween.move(gameObject, transform.position + transform.up * 20f, 0.3f).setOnComplete(() => Destroy(gameObject));
    }

    public void SetText(string text) {
        uiText.text = "+" + text;
    }
}
