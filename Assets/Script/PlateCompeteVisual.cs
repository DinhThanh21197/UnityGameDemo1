using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCompeteVisual : MonoBehaviour
{
    [SerializeField] private PlateKitchenObject PlateKitchenObject;
    [Serializable]
    public struct KitchenObjectSo_GameObject
    {
        public KitchenObjectSO KitchenObjectSO;
        public GameObject gameObject;
    }
    [SerializeField] private List<KitchenObjectSo_GameObject> kitchenObjectSo_GameObjectsList;
   
    private void Awake()
    {
        PlateKitchenObject.OnIngerdientAdded += PlateKitchenObject_OnIngerdientAdded;
    }
    private void Start()
    {
        foreach (KitchenObjectSo_GameObject kitchenObjectSo_GameObject in kitchenObjectSo_GameObjectsList)
        {
           
                kitchenObjectSo_GameObject.gameObject.SetActive(false);
            
        }
    }

    private void PlateKitchenObject_OnIngerdientAdded(object sender, PlateKitchenObject.OnIngerdientAddedEventArgs e)
    {
        foreach(KitchenObjectSo_GameObject kitchenObjectSo_GameObject in kitchenObjectSo_GameObjectsList)
        {
            if (kitchenObjectSo_GameObject.KitchenObjectSO == e.KitchenObjectSO)
            {
                kitchenObjectSo_GameObject.gameObject.SetActive(true);
            }
        }
    }
}
