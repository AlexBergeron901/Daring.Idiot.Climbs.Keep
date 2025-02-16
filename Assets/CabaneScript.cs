using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CabaneScript : MonoBehaviour
{
    public GamerManager _GamerManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Awake()
    {
        _GamerManager = GameObject.FindGameObjectWithTag("GamerManager").GetComponent<GamerManager>();
        _GamerManager.fellFromCabin = true;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
