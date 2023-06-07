using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlateIconSingleUI : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Image image;
    public void SetKitchenObjectSO(KitchenObjectSO kitchenObjectSO)
    {
        image.sprite = kitchenObjectSO.Sprite;
    }
}
