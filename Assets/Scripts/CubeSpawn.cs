using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class CubeSpawn : MonoBehaviour
{

    [SerializeField]
    private GameObject _cube;
    
    // Update is called once per frame
   private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
           var newCube = Instantiate(_cube);
           newCube.transform.position = new Vector3(Random.Range(-5f, 10f), 11f, Random.Range(-1f,1f));
        } 
    }
}
