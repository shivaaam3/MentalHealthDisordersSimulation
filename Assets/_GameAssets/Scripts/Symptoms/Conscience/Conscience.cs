using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conscience : MonoBehaviour, ISymptoms
{

    public AudioSource audioSource;
    private Coroutine coroutine;

    private List<Voices> selectedMoods;

    // Start is called before the first frame update
    void Start()
    {
        Random.InitState(Constants.RANDOM_SEED);
        audioSource = GetComponentInChildren<AudioSource>();
    }

    private IEnumerator SimulateCoroutine(AudioSource audioSource)
    {
        int moodIndex = Random.Range(0, selectedMoods.Count);
        Voices mood = selectedMoods[moodIndex];

        int clipIndex = Random.Range(0, mood.clips.Count);
        AudioClip clip = mood.clips[clipIndex];
        audioSource.PlayOneShot(clip);

        float interval = Random.Range(mood.timeAfterClip.min, mood.timeAfterClip.max);
        yield return new WaitForSeconds(interval);
    }

    public void Simulate()
    {
        coroutine = StartCoroutine(SimulateCoroutine(audioSource));
    }

    public void Stop()
    {
        StopCoroutine(coroutine);
    }
}
