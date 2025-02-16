using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClipHolder : MonoBehaviour
{
    [SerializeField] public int _clip;
    public ScriptNarrateur narrateurScript;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        narrateurScript = GameObject.FindGameObjectWithTag("Narrateur").GetComponent<ScriptNarrateur>();
    }
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("egg");
        if(other.transform.CompareTag("Narrateur"))
        {
            narrateurScript.levelComment(_clip);
        }
        
    }
}
