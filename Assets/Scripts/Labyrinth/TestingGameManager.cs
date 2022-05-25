using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TestingGameManager : MonoBehaviour
{
  public static TestingGameManager Instance;

  private Transform groundTransform;

  private GameObject portalBase;
  private int collectedPortalsCount;
  private Text portalPanelText;

  private bool gameHasEnded;

  void Start()
  {
    if (Instance == null)
      Instance = this;

    groundTransform = GameObject.Find("GroundCube").transform;
    portalBase = GameObject.Find("Portal");
    CreateMazeWithPortalOnCubeFace(Vector3.right * 90f);
    CreateMazeWithPortalOnCubeFace(Vector3.right * 180f);
    CreateMazeWithPortalOnCubeFace(Vector3.right * 270f);
    CreateMazeWithPortalOnCubeFace(Vector3.forward * 90f);
    CreateMazeWithPortalOnCubeFace(Vector3.forward * -90f);
    CreateMazeWithPortalOnCubeFace(Vector3.zero);

    collectedPortalsCount = 0;
    portalPanelText = GameObject.Find("PortalPanelText").GetComponent<Text>();
    UpdatePortalTooltipUI();

  }

  private void CreateMazeWithPortalOnCubeFace(Vector3 groundRotation){

    var maze = new Ellers(10,10);
    var groundPos = groundTransform.position;
    Vector3 pos;
    var parent = portalBase.transform.parent;

    groundTransform.rotation = Quaternion.Euler(groundRotation);
    maze.TranslateMazeIntoCubeFace(groundPos, groundTransform.lossyScale.x);
    pos = groundPos + new Vector3(-0.5f, 0.5f, 0.5f) * 10+ new Vector3(0.5f + Random.Range(0, 10) * 1f, 0f, -0.5f - Random.Range(0, 10) * 1f);
    GameObject.Instantiate(portalBase, pos, Quaternion.identity, parent);
  }

  public void TakePortalPoint(GameObject portal){
    GameObject.Destroy(portal.gameObject);
    collectedPortalsCount++;
    UpdatePortalTooltipUI();
    if ( collectedPortalsCount >=6){
      EndGame();
    }
  }

  private void UpdatePortalTooltipUI(){
    portalPanelText.text = string.Format("Close all portals\nPortals closed: {0} / 6", collectedPortalsCount);
  }

  public void EndGame(){
    string panelStr;
    int secondsToEnd;
    if (!gameHasEnded){
      gameHasEnded = true;
      panelStr = "Well done!\n";
      secondsToEnd = 3;
      StartCoroutine(EndCountdown());
    }

    IEnumerator EndCountdown(){
      for(int i = 0; i<3; i++){
        portalPanelText.text = panelStr + string.Format("Game restarts in {0} seconds", secondsToEnd);
        secondsToEnd -= 1;
        yield return new WaitForSeconds(1f);
      }
      RestartGame();
    }
  }

  private void RestartGame(){
    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
  }

}
