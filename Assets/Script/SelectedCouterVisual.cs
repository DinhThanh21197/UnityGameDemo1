using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCouterVisual : MonoBehaviour
{
    [SerializeField] private BaseCounter BaseCounter;
    [SerializeField] private GameObject[] visualGameObject;
    private void Start()
    {
        
        Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
    }

    private void Player_OnSelectedCounterChanged(object sender, Player.OnSeclectedCounterChangedEventArgs e)
    {
        if (e.selectedCounter == BaseCounter)
        {
            Show();
        }
        else
        {
            hide();
        }
    }
    private void Show()
    {    foreach (GameObject item in visualGameObject) { item.SetActive(true); }
        
    }
    private void hide()
    { 
        foreach (GameObject item in visualGameObject)
        {  if (item == null) continue;
            item.SetActive(false); }
    }
}
