using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentManager : MonoBehaviour
{
    public GameObject house;
    public GameObject spawner;
    public GameObject houseContainer;

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "House")
        {            
            Instantiate(house, spawner.transform.position, Quaternion.identity, houseContainer.transform);
        }
        Destroy(other.gameObject);
    }
}
