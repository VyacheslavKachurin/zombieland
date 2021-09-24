using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrosshairUI : MonoBehaviour
{
    private RectTransform _rectTransform;
    // Start is called before the first frame update
    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        //Debug.Log(_rectTransform.anchoredPosition);
    }

    // Update is called once per frame
    void Update()
    {
        Aim();
    }
    private void Aim()
    {
        Vector2 mousePosition= Camera.main.ScreenToWorldPoint(Input.mousePosition); 
        
        _rectTransform.position = new Vector2(mousePosition.x,mousePosition.y);
        Debug.Log(mousePosition);
    }
    
}
