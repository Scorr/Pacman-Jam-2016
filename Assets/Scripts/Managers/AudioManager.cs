using UnityEngine;

public class AudioManager : Singleton<AudioManager> {

    // this shit is bad and unmaintainable
    // but it is a jam and i am bad
    public AudioSource pickupSound;
    public AudioSource getScwhifty;

    public void PlaySound(string name) {
        // ditto here
        switch (name) {
            case "schwifty":
                getScwhifty.Play();
                break;
            case "pickup":
                pickupSound.Play();
                break;
            default:
                Debug.LogError("sound " + name + " not found");
                break;
        }
    }
}
