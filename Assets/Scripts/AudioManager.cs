using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioClip mergeSound;
    [SerializeField]
    private AudioClip dropSound;
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
        source.PlayOneShot(mergeSound);
    }

    private void PlayDropSound()
    {
        source.PlayOneShot(dropSound);
    }
    private void OnDestroy()
    {
        drag.OnDragFinished -= PlayDropSound;
    }
}
