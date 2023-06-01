using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private GameObject hasProgressGameObject;    
    [SerializeField] private IHasProgress hasProgess;
    [SerializeField] private Image barImage;
    private void Start()
    {
        this.hasProgess.OnProgressChange += HasProgress_OnProgressChange;
        barImage.fillAmount = 0f;
        Hide();
    }

    private void HasProgress_OnProgressChange(object sender, IHasProgress.OnProgcessChangeEventAgrs e)
    {
        Debug.Log("CuttingCounter_OnProgressChange " + e.progressNormalized);
        barImage.fillAmount = e.progressNormalized;
        
        if (barImage.fillAmount==0||barImage.fillAmount==1)
        {
            Hide();
        }
        else
        {
            Show();
        }
    }
    private void Show()
    {
        gameObject.SetActive(true);
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
