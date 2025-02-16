using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamerManager : MonoBehaviour
{
    
    public List<int> _shitList = new List<int>();
    public bool fellFromCabin;
    public bool fellFromKeep;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
