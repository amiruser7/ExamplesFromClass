using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTrigger : MonoBehaviour
{
    [SerializeField] private AudioClip triggerClip;
    [SerializeField] private AudioSource playSource;
    [SerializeField] private Animator anim;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") == true)
        {
            Debug.Log("Player has entered collider.");

            if(anim != null)
            {
                anim.SetTrigger("toggle anim");
            }

            if(playSource != null && triggerClip != null )
            {
                //LIMIT TO ONE SOUND BEING PLAYED
                //playSource.Stop();
                //playSource.clip = triggerClip;
                //playSource.Play();

                //STACK SFXs PLAYED ON THIS SOURCE
                playSource.PlayOneShot(triggerClip);
            }
        }
    }
}