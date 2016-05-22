using UnityEngine;

public class AudioManager : Singleton<AudioManager> {

    // this shit is bad and unmaintainable
    // but it is a jam and i am bad
    public AudioSource pickupSound;
    public AudioSource getScwhifty;
    public AudioSource goodJob;
    public AudioSource wubbalubbadubdub;
    public AudioSource showMeWhatYouGot;

    public void PlaySound(string name) {
        // ditto here
        switch (name) {
            case "schwifty":
                getScwhifty.Play();
                break;
            case "pickup":
                pickupSound.Play();
                break;
            case "good_job":
                goodJob.Play();
                break;
            case "wubbalubbadubdub":
                wubbalubbadubdub.Play();
                break;
            case "show_me_what_you_got":
                showMeWhatYouGot.Play();
                break;
            default:
                Debug.LogError("sound " + name + " not found");
                break;
        }
    }
}
