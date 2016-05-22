using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChargeBar : MonoBehaviour {

    private Image image;

    private void Awake() {
        image = GetComponent<Image>();
    }

    public void AddCharge() {
        image.fillAmount += 0.01f;

        if (image.fillAmount >= 1f) {
            image.color = Color.yellow;
            PlayerController.CanPortal = true;
        }
    }
}
