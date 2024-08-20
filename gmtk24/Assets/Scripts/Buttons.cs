using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Buttons : MonoBehaviour
{
    
    [SerializeField] private TextMeshProUGUI buttonText;
    private float initialFontSize;

    // Start is called before the first frame update
    void Start()
    {
        initialFontSize = buttonText.fontSize;
    }

    public void IncreaseSize() {
        buttonText.fontSize += 4;
    }

    public void ResetSize() {
        buttonText.fontSize = initialFontSize;
    }
}
// DDE6FF
// B0C2F2
// 90A8ED