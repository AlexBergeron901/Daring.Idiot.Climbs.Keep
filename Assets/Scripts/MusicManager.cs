using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] AudioClip[] _listeMusique;

    [SerializeField] AudioSource _jukebox;
    public int clipNumber;
    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = GameObject.FindGameObjectWithTag("Narrateur").transform.position;
    }
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        if (this.transform.position != GameObject.FindGameObjectWithTag("Narrateur").transform.position)
        {
            this.transform.position = GameObject.FindGameObjectWithTag("Narrateur").transform.position;
        }
        
    }
    public void PlayMusic()
    {
        if (_jukebox.isPlaying)
        {
            interrupt();
        }
        _jukebox.clip = _listeMusique[clipNumber];
        _jukebox.loop = true;
        _jukebox.Play();
    }
    public void interrupt()
    {
        _jukebox.Stop();
    }
}  
