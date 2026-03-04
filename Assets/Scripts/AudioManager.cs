using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] mergeSounds;
    [SerializeField]
    private AudioClip[] dropSounds;
    [SerializeField]
    private Drag drag;
    private AudioSource source;
    private bool isMuted=false;

    void Awake()
    {
        source = GetComponent<AudioSource>();
        Subscribe();
        
    }

    private void Subscribe()
    {
        drag.OnDragFinished += PlayDropSound;
        Merge.Merged += PlayMergeSound;
    }

    public void Mute()
    {
        isMuted = !isMuted;
        source.mute = isMuted;
    }

    public void PlayMergeSound(int level)
    {
        if (mergeSounds == null || mergeSounds.Length == 0)
        {
            return;
        }
        AudioClip randomSound = mergeSounds[UnityEngine.Random.Range(0, mergeSounds.Length)];
        source.PlayOneShot(randomSound);
    }

    public void PlayDropSound()
    {
        if (dropSounds == null || dropSounds.Length == 0)
        {
            return;
        }
        AudioClip randomSound = dropSounds[UnityEngine.Random.Range(0, dropSounds.Length)];
        source.PlayOneShot(randomSound);
    }
    private void OnDestroy()
    {
        drag.OnDragFinished -= PlayDropSound;
        Merge.Merged -= PlayMergeSound;
    }
}
