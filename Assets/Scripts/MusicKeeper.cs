using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicKeeper : MonoBehaviour
{

    [SerializeField] GameObject Music;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(Music);
    }

    // Update is called once per frame
    void Update()
    {
        if (Application.loadedLevelName == "MainScene")
        {
            Destroy(this.gameObject);
        }
    }
}
