using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class volumeControl : MonoBehaviour
{
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
        audioSource.volume = MasterControl.GetMaster_Volume();
    }


}
