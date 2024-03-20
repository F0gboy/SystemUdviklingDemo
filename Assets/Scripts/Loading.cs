using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    [SerializeField] GameObject Title;

    // Start is called before the first frame update
    void Start()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
   
    // Update is called once per frame
    void Update()
    {
        
    }
}
