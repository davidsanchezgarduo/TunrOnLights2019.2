using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuidoControl : MonoBehaviour
{
    public AudioClip hordeClip;
    private AudioSource source;
    private bool inStart;
    // Start is called before the first frame update
    void Start()
    {
        inStart = true;
        source = GetComponent<AudioSource>();
        GameManager.instance.hordeSource = source;
    }

    // Update is called once per frame
    void Update()
    {
        if (!source.isPlaying && inStart) {
            //Debug.Log("change ");
            ForceChange();
        }
    }

    public void ForceChange() {
        inStart = false;
        source.clip = hordeClip;
        this.enabled = false;
    }
}
