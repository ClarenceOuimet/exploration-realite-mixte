using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BoutonMusique : MonoBehaviour
{
    [SerializeField] private AudioClip[] pistes;

    private AudioSource source;
    private int index = -1;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
        source.playOnAwake = false;
        source.loop = true;
    }

    public void PisteSuivante()
    {
        if (pistes == null || pistes.Length == 0)
            return;

        index = (index + 1) % pistes.Length;
        source.clip = pistes[index];
        source.Play();
    }
}