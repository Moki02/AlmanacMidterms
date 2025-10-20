using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] private SoundLibrary sfxLibrary;
    [SerializeField] private AudioSource sfx2DSource;
    [SerializeField] private AudioSource musicSource;

    void Awake()
    {
        // --- Singleton Setup ---
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // --- Auto-Find Sources (in case not manually assigned) ---
        if (sfx2DSource == null)
            sfx2DSource = FindAudioSourceByName("SFX2DSource");

        if (musicSource == null)
            musicSource = FindAudioSourceByName("MusicSource");

        if (sfxLibrary == null)
            sfxLibrary = GetComponent<SoundLibrary>();

        // --- Safety Checks ---
        if (sfx2DSource == null)
            Debug.LogWarning("⚠️ SoundManager: Missing 'SFX2DSource' AudioSource!");
        if (musicSource == null)
            Debug.LogWarning("⚠️ SoundManager: Missing 'MusicSource' AudioSource!");
        if (sfxLibrary == null)
            Debug.LogWarning("⚠️ SoundManager: Missing SoundLibrary reference!");
    }

    private AudioSource FindAudioSourceByName(string name)
    {
        var sources = GetComponentsInChildren<AudioSource>(true);
        foreach (var src in sources)
        {
            if (src.name.Contains(name))
                return src;
        }
        return null;
    }

    // --- 2D SOUND ---
    public void PlaySound2D(string soundName)
{
    if (sfx2DSource == null)
    {
        Debug.LogWarning("❌ SFX2DSource is missing!");
        return;
    }

    if (!sfx2DSource.gameObject.activeInHierarchy)
    {
        Debug.LogWarning("⚠️ SFX2DSource object was inactive, re-enabling it...");
        sfx2DSource.gameObject.SetActive(true);
    }

    if (!sfx2DSource.enabled)
    {
        Debug.LogWarning("⚠️ SFX2DSource was disabled, enabling it now...");
        sfx2DSource.enabled = true;
    }

    AudioClip clip = sfxLibrary.GetClipFromName(soundName);
    if (clip == null)
    {
        Debug.LogWarning($"⚠️ No sound found in library for '{soundName}'!");
        return;
    }

    sfx2DSource.PlayOneShot(clip);
}


    // --- 3D SOUND ---
    public void PlaySound3D(string soundName, Vector3 pos)
    {
        AudioClip clip = sfxLibrary.GetClipFromName(soundName);
        if (clip != null)
            AudioSource.PlayClipAtPoint(clip, pos);
        else
            Debug.LogWarning($"⚠️ No 3D sound found in library for '{soundName}'!");
    }

    // --- MUSIC CONTROLS ---
    public void PlayMusic(string soundName, bool loop = true)
    {
        if (musicSource == null)
        {
            Debug.LogWarning("❌ MusicSource is missing!");
            return;
        }

        AudioClip clip = sfxLibrary.GetClipFromName(soundName);
        if (clip == null)
        {
            Debug.LogWarning($"⚠️ No music found in library for '{soundName}'!");
            return;
        }

        musicSource.clip = clip;
        musicSource.loop = loop;
        musicSource.Play();
    }

    public void StopMusic()
    {
        if (musicSource != null && musicSource.isPlaying)
            musicSource.Stop();
    }
}
