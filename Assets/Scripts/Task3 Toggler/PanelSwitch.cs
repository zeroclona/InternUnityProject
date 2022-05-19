using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelSwitch : MonoBehaviour
{
  private bool panelEnabled = true;

  public void OnClick(){
    panelEnabled = !panelEnabled;
    gameObject.SetActive(panelEnabled);
  }

  private void Start() {
    OnClick();
  }
}
