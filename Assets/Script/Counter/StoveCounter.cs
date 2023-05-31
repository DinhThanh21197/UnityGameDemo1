using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CuttingCounter;

public class StoveCounter : BaseCounter
{
    [SerializeField] private FryingRecipeSO[] fryingRecipeSO;
    [SerializeField] private float FryingTimer = 0f;

    private void Update()
    {
        if (HasKitchenObject())
        {
            this.FryingTimer += Time.deltaTime;
            FryingRecipeSO fryingRecipeSO = GetFryingSOWithInput(GetKitchenObject().GetKitchenObjectSO());
            if (FryingTimer >= fryingRecipeSO.fryingTimeMax)
            {
               GetKitchenObject().DestroySelf();
                if(this.GetKitchenObject() != null)
                {
                    ClearKitchenObject();
                }
                KitchenObject.SpawnKitchenObject(fryingRecipeSO.outPut, this);
            }
            
           
            
        }
    }
    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            // there is no kitchenObject here
            if (player.HasKitchenObject())
            {
                // player is carrying something
                if (HasRecipeKitchenWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    FryingRecipeSO fryingRecipeSO = GetFryingSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                   
                }

            }
            else
            {
                // player not carrying anything
            }
        }
        else
        {
            // there is  a  kitchenObject here
            if (player.HasKitchenObject())
            {
                //player is carrying something
            }
            else
            {
                //player it not anything
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }
    public override void InteractAlternate(Player player)
    {
        if (HasKitchenObject() && HasRecipeKitchenWithInput(GetKitchenObject().GetKitchenObjectSO()))
        {
           

                //  Debug.LogError(cuttingProgress + "  /" + cuttingRecipeSO.cuttingProgressMax +"= "+((float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax)); 
                KitchenObjectSO OutputKitchenObjectSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());
                GetKitchenObject().DestroySelf();
                KitchenObject.SpawnKitchenObject(OutputKitchenObjectSO, this);
            
        }
    }
    private KitchenObjectSO GetOutputForInput(KitchenObjectSO input)
    {
        FryingRecipeSO fryingRecipeSO = GetFryingSOWithInput(input);
        if (fryingRecipeSO != null)
        {
            return fryingRecipeSO.outPut;
        }
        return null;
    }
    private FryingRecipeSO GetFryingSOWithInput(KitchenObjectSO input)
    {
        //  Debug.Log("KitchenObjectSO");
        foreach (FryingRecipeSO fryingRecipeSO in fryingRecipeSO)
        {
            if (fryingRecipeSO.input == input)
            {
                return fryingRecipeSO;
            }
        }
        Debug.LogError("not matching recipe found!! Pls Review");
        return null;
    }
    private bool HasRecipeKitchenWithInput(KitchenObjectSO inputKitchenObject)
    {
        FryingRecipeSO fryingRecipeSO = GetFryingSOWithInput(inputKitchenObject);

        return inputKitchenObject != null;
    }
}
