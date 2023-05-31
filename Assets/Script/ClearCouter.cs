using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCouter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
   
   
   
    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            // if no has kitchenOnject
            if (player.HasKitchenObject())
            {
                // player carrying kitchen object something
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
            else
            {
                //player not carrying something

            }
        }
        else
        {
            // there is a kitchenObject
            if (!HasKitchenObject())
            {
                // player carrying something
            }
            else
            {
                //player not carrying something
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
      
    }
   
}
