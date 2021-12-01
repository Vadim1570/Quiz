using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundBt : MonoBehaviour
{
    
public AudioClip poemFX;
public AudioClip clickFX;
public AudioClip choiceplaceFX;  
public AudioSource myFx;
 
    public void PoemSoundPlay()
    {
    // Start is called before the first frame update
    myFx.Play();

    }
    public void PoemSoundStop()
    {
    // Start is called before the first frame update
     myFx.Stop();

    }

    public void PoemSoundPause()
    {
    // Start is called before the first frame update
     myFx.Pause();

    }

    public void ClickSound()
    {
    // Start is called before the first frame update
     myFx.PlayOneShot(clickFX);

    }

    public void ChoicePlace()
    {
    // Start is called before the first frame update
     myFx.PlayOneShot(choiceplaceFX);

    }

}
