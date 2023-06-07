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
            if (player.HasKitchenObject())
            {
                // player carrying something
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    
                   // player holding plate
                    if (plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO()))
                    {   // if couter is hollding plate
                        GetKitchenObject().DestroySelf();
                    }

                }
                else
                {
                    // player not carrying plate but soething else
                    if(GetKitchenObject().TryGetPlate( out plateKitchenObject))
                    {
                        plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO());
                        player.GetKitchenObject().DestroySelf();
                    }
                }
            }
            else
            {
                //player not carrying something
                GetKitchenObject().SetKitchenObjectParent(player);

            }
        }
      
    }
   
}
