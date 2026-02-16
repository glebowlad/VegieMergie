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

    private void PlayMergeSound(GameObject item)
    {
        if (mergeSounds == null || mergeSounds.Length == 0)
        {
            return;
        }
        AudioClip randomSound = mergeSounds[UnityEngine.Random.Range(0, mergeSounds.Length)];
        source.PlayOneShot(randomSound);
    }

    private void PlayDropSound()
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
