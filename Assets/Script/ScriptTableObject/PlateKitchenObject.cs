using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    public event EventHandler<OnIngerdientAddedEventArgs> OnIngerdientAdded;
    public class OnIngerdientAddedEventArgs:EventArgs
    {
        public KitchenObjectSO KitchenObjectSO;
    }
    [SerializeField] private List<KitchenObjectSO> validKitchenObjectSOlist;
    private List<KitchenObjectSO> kitchenObjectSOList;
    private void Awake()
    {
        kitchenObjectSOList =new List<KitchenObjectSO>();
    }
    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO)
    {   if(!validKitchenObjectSOlist.Contains(kitchenObjectSO))
        {  //  not a valid ingerdient
            return false;
        }
        if (kitchenObjectSOList.Contains(kitchenObjectSO))
        {
            return false;
        }
        else
        {
            kitchenObjectSOList.Add(kitchenObjectSO);
            OnIngerdientAdded?.Invoke(this,new OnIngerdientAddedEventArgs { KitchenObjectSO=kitchenObjectSO });
            
            return true;
        }
    }
    public List<KitchenObjectSO> GetkitchenObjectsSOList()
    {
        return kitchenObjectSOList;
    }
  
}
