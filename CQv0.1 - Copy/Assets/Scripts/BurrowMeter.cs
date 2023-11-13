using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class BurrowMeter : MonoBehaviour
{
    
    public GameObject player;
    public Slider slider;

    // Start is called before the first frame update
   public void OnSliderChanged(float value)
   {
    

   }
   void Update()
   {
    PlayerController playerController = player.GetComponent<PlayerController>();
   // Slider slider = GetComponent<Slider>();
    //min value = 0;
    //max value = maxAmmo;
       // valueText.text = value.ToString();
       //slider.maxValue = playerController.maxAmmo;
       slider.maxValue = playerController.maxBurrowMeter;
       slider.value = playerController.burrowMeter;
       Debug.Log(slider.maxValue);
   }
}
