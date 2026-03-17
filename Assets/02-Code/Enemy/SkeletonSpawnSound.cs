using UnityEngine;

public class SkeletonSpawnSound : MonoBehaviour
{
    public AudioClip sonAoujuer; 

    void Start()
    {
        AudioSource maSource = GetComponent<AudioSource>();

        if (maSource == null)
        {
            maSource = gameObject.AddComponent<AudioSource>();
        }

        if (sonAoujuer != null)
        {
            maSource.PlayOneShot(sonAoujuer);
        }
    }
}