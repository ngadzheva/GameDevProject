using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISlider : MonoBehaviour
{
  public Slider slider;
  public Gradient gradient;
  public Image fill;

  public void SetMaxValue(int maxValue)
  {
    slider.maxValue = maxValue;
    slider.value = maxValue;
    fill.color = gradient.Evaluate(1f);
  }

  public void SetValue(int value)
  {
    slider.value = value;
    fill.color = gradient.Evaluate(slider.normalizedValue);
  }
}
