using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class PauseMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    
    private void Start()
    {
        LoadVolume(); 
    }

    public void PlayGame()
    {
        LevelManager.Instance.LoadScene("Game", "CrossFade");
        MusicManager.Instance.PlayMusic("GameMusic");
    }

    public void PlayMenu()
    {
        LevelManager.Instance.LoadScene("Menu", "CrossFade");
        MusicManager.Instance.PlayMusic("MenuMusic");
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
    
    public void UpdateMasterVolume(float volume)
    {
        audioMixer.SetFloat("MasterVolume", volume);
    }
    
    public void UpdateMusicVolume(float volume)
    {
        audioMixer.SetFloat("MusicVolume", volume);
    }

    public void UpdateSoundVolume(float volume)
    {
        audioMixer.SetFloat("SFXVolume", volume);
    }
    
    private void LoadVolume()
    {
        float masterVolume = PlayerPrefs.GetFloat("MasterVolume");
        audioMixer.SetFloat("MasterVolume", masterVolume);
        
        float musicVolume = PlayerPrefs.GetFloat("MusicVolume");
        audioMixer.SetFloat("MusicVolume", musicVolume);

        float sfxVolume = PlayerPrefs.GetFloat("SFXVolume");
        audioMixer.SetFloat("SFXVolume", sfxVolume);
    }

    public void SaveVolume()
    {
        audioMixer.GetFloat("MasterVolume", out float masterVolume);
        PlayerPrefs.SetFloat("MasterVolume", masterVolume);
        
        audioMixer.GetFloat("MusicVolume", out float musicVolume);
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);

        audioMixer.GetFloat("SFXVolume", out float sfxVolume);
        PlayerPrefs.SetFloat("SFXVolume", sfxVolume);
    }
}