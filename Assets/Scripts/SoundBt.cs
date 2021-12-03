using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class SoundBt : MonoBehaviour
{
    
public AudioClip poemFX;
public AudioClip clickFX;
public AudioClip choiceplaceFX;  
public AudioSource myFx;
public AudioClip winFX;  
public AudioClip errorFX;  

 
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

    public IEnumerator winSound()
    {
    // Start is called before the first frame update
     yield return new WaitForSeconds(0.1f);// продолжить примерно через 100ms
     myFx.PlayOneShot(winFX);
     

    }


    public IEnumerator errorSound()
    {
    // Start is called before the first frame update
     yield return new WaitForSeconds(0.1f);// продолжить примерно через 100ms
     myFx.PlayOneShot(errorFX);
     

    }

    public async void ChoicePlaceAsync()
    {
        await Task.Run(() =>
        {
            myFx.PlayOneShot(choiceplaceFX);
        }
        );
    }

}
