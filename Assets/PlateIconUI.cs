using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateIconUI : MonoBehaviour
{
    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] private Transform iconTemplate;
    private void Awake()
    {
        iconTemplate.gameObject.SetActive(false);
    }
    private void Start()
    {
        plateKitchenObject.OnIngerdientAdded += PlateKitchenObject_OnIngerdientAdded;
    }

    private void PlateKitchenObject_OnIngerdientAdded(object sender, PlateKitchenObject.OnIngerdientAddedEventArgs e)
    {
        UpdateVisual();
    }
    private void UpdateVisual()
    {   foreach (Transform child in transform)
        {
            if (child == iconTemplate) continue;
            Destroy(child.gameObject);
        }
        foreach(KitchenObjectSO kitchenObjectSO in plateKitchenObject.GetkitchenObjectsSOList())
        {
            Transform iconTranform = Instantiate(iconTemplate, transform);
            iconTranform.GetComponent<PlateIconSingleUI>().SetKitchenObjectSO(kitchenObjectSO);
            iconTranform.gameObject.SetActive(true);


        }
    }
}
