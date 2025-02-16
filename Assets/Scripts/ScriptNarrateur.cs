using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptNarrateur : MonoBehaviour
{

    [SerializeField] AudioClip[] _listeNarrateurLevelComment;
    [SerializeField] AudioClip[] _listeNarrateurDeathComment;
    GameObject sourceNarrateur;
    [SerializeField] AudioSource maVoix;
    private int randClip;
    public GamerManager GamerManager;
    int _clipSelect;
   
    public Death KillManager;
    // Start is called before the first frame update
    void Start()
    {

    }
    private void Awake()
    {
        KillManager = GameObject.FindGameObjectWithTag("DeathManager").GetComponent<Death>();
        GamerManager = GameObject.FindGameObjectWithTag("GamerManager").GetComponent<GamerManager>();
        DontDestroyOnLoad(this.gameObject);

    }
    // Update is called once per frame
    void Update()
    {
        sourceNarrateur = GameObject.FindGameObjectWithTag("Player");
        this.gameObject.transform.position = new Vector3(sourceNarrateur.transform.position.x, sourceNarrateur.transform.position.y + 5, sourceNarrateur.transform.position.z);
    }
    public void disableClip(int clipNumber)
    {
        GamerManager._shitList.Add(clipNumber);
    }
    public void levelComment(int _clipSelect)
    {

            if (GamerManager._shitList.Contains(_clipSelect) == false)
            {
                maVoix.Stop();
                PlayClip(_listeNarrateurLevelComment[_clipSelect]);
                disableClip(_clipSelect);
            }
        
    }
    public void sassCheck()
    {
            randClip = Random.Range(3, _listeNarrateurDeathComment.Length);
            PlayClip(_listeNarrateurDeathComment[randClip]);
    }
    public void PlayClip(AudioClip a)
    {
        if (maVoix.isPlaying == false)
        {
            
            maVoix.PlayOneShot(a);
        }
        
    }
    public void fallFromLevel()
    {

        //maVoix.Stop();
        //randClip = Random.Range(0, 2);
        //PlayClip(_listeNarrateurDeathComment[randClip]);
        
    }

}
