using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    private Slider _slider;
  
    // Start is called before the first frame update
    void Start()
    {
        _slider=GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateHealth(float damageAmount)
    {
        _slider.value -= damageAmount;
    }
}
