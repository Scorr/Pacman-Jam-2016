using UnityEngine;

public class AudioManager : Singleton<AudioManager> {

    private AudioSource _audioSource;

    private void Awake() {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound() {
        if (!_audioSource.isPlaying)
            _audioSource.Play();
    }
}
