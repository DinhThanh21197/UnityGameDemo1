using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounter : BaseCounter
{
    public override void Interact(Player player)
    {    
        if (player.HasKitchenObject())
        {   // player carrying something
            if(player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
            {
                player.GetKitchenObject().DestroySelf();
            }
        }
    }

}
