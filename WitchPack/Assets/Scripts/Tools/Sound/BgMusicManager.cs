using UnityEngine;


public class BgMusicManager : MonoSingleton<BgMusicManager>
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioReverbFilter _audioReverbFilter;
    [SerializeField] private AudioLowPassFilter _audioLowPassFilter;

    public AudioSource AudioSource => _audioSource;

    public AudioReverbFilter AudioReverbFilter => _audioReverbFilter;

    public AudioLowPassFilter AudioLowPassFilter => _audioLowPassFilter;

    public void PlayMusic()
    {
        if (_audioSource.isPlaying) return;
        _audioSource.Play();
    }

    public void StopMusic()
    {
        if (!_audioSource.isPlaying) return;
        _audioSource.Stop();
    }

    public void ChangeMusicVolume(float volume)
    {
        _audioSource.volume = volume;
    }
}