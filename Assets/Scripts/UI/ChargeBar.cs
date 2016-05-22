using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChargeBar : MonoBehaviour {

    private Image image;

    private void Awake() {
        image = GetComponent<Image>();
    }

    public void AddCharge() {
        LeanTween.value(gameObject, (value) => image.fillAmount = value, image.fillAmount, image.fillAmount + 0.01f, 0.1f);

        if (image.fillAmount >= 1f) {
            image.color = Color.yellow;
            PlayerController.CanPortal = true;
        }
    }
}
