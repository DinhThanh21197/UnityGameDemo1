using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    [SerializeField] public event EventHandler OnPlayerGrabbedObject;

    public override void Interact(Player player)
    {

        if (!player.HasKitchenObject())
        {  // player is not carrying enything
           // Transform KitchenObjectTranform = Instantiate(kitchenObjectSO.prefab);
           // KitchenObjectTranform.GetComponent<KitchenObject>().SetKitchenObjectParent(player);
            KitchenObject.SpawnKitchenObject(kitchenObjectSO, player);
            OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
        }
    }
    
    

}
