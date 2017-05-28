using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reactions : MonoBehaviour {

    public List<AudioClip> sounds;

    private AudioSource audio;
    private Animation animation;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
        animation = GetComponent<Animation>();
    }
    
    public void PlayAnimation(string name)
    {
        animation.Play(name);
    }

    public void PlayReaction(string name, bool queue = false)
    {
        List<int> list = new List<int>();
        for (int i = 0; i < sounds.Count; i++)
        {
            if (sounds[i].name.Contains(name))
                list.Add(i);
        }
        int nb = Random.Range(0, list.Count);
        audio.clip = sounds[list[nb]];
        if (queue)
            animation.PlayQueued(name);
        else
            animation.Play(name);
        audio.Play();
    }
}
