using UnityEngine;
using UnityEngine.UI;

public class ToggleEye : MonoBehaviour
{
  [SerializeField]
  private Color enabledNormalColor;
  [SerializeField]
  private Color enabledHighlightedColor;
  [SerializeField]
  private Color enabledPressedColor;
  [SerializeField]
  private Color disabledNormalColor;
  [SerializeField]
  private Color disabledHighlightedColor;
  [SerializeField]
  private Color disabledPressedColor;

  private Toggle toggle;

  void Start()
  {
    toggle = GetComponent<Toggle>();
    OnToggleValueChanged(toggle.isOn);
    toggle.onValueChanged.AddListener( OnToggleValueChanged );
  }

  private void OnToggleValueChanged( bool isOn )
  {
    ColorBlock cb = toggle.colors;
    if (isOn){
      cb.normalColor = enabledNormalColor;
      cb.highlightedColor = enabledHighlightedColor;
      cb.selectedColor = enabledNormalColor;
      cb.pressedColor = enabledPressedColor;
    }
    else{
      cb.normalColor = disabledNormalColor;
      cb.highlightedColor = disabledHighlightedColor;
      cb.selectedColor = disabledNormalColor;
      cb.pressedColor = disabledPressedColor;
    }
    toggle.colors = cb;
  }
}
