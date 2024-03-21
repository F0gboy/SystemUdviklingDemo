using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class KillsScript : MonoBehaviour
{
    public int killCount;
    public int maxKillCount;
    public Image bulletImage;
    private TextMeshProUGUI textBox;
    // Start is called before the first frame update
    void Start()
    {
        textBox = gameObject.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddKill()
    {
        killCount++;
        textBox.text = killCount.ToString();
        bulletImage.fillAmount = (killCount / (maxKillCount/100)) / 100;
    }
}
