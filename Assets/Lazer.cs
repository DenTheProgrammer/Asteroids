using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazer : MonoBehaviour
{

    [SerializeField]
    private int destroyAfterMs;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, destroyAfterMs/1000f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
