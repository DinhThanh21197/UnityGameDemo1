using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class CuttingCounter : BaseCounter,IHasProgress
{
    public event EventHandler<IHasProgress.OnProgcessChangeEventAgrs> OnProgressChange;
    public event EventHandler OnCut;
  
   [SerializeField] private CuttingRecipeSO[] cutKitchenObjectSO;
    private int cuttingProgress;

   

    public override void Interact(Player player)
    {  
        if (!HasKitchenObject() )
        {
            // there is no kitchenObject here
            if (player.HasKitchenObject())
            {
                // player is carrying something
                if(HasRecipeKitchenWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    CuttingRecipeSO cuttingRecipeSO = GetCuttingSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                    OnProgressChange?.Invoke(this, new IHasProgress.OnProgcessChangeEventAgrs
                    {
                        progressNormalized=(float)cuttingProgress/ cuttingRecipeSO.cuttingProgressMax
                    });
                    cuttingProgress = 0;
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
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {

                    // player holding plate
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();
                    }

                }
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
      //  Debug.Log("InteractAlternate");
        if (HasKitchenObject()&&HasRecipeKitchenWithInput(GetKitchenObject().GetKitchenObjectSO()))
        {
            // there is a kitchenObject Here
            cuttingProgress++;
            OnCut?.Invoke(this, EventArgs.Empty);
            CuttingRecipeSO cuttingRecipeSO=GetCuttingSOWithInput(GetKitchenObject().GetKitchenObjectSO());
            OnProgressChange?.Invoke(this, new IHasProgress.OnProgcessChangeEventAgrs
            {

                progressNormalized = (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax

            });
            if (cuttingProgress>= cuttingRecipeSO.cuttingProgressMax)
            {
               
              //  Debug.LogError(cuttingProgress + "  /" + cuttingRecipeSO.cuttingProgressMax +"= "+((float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax)); 
                KitchenObjectSO OutputKitchenObjectSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());
                GetKitchenObject().DestroySelf();
                KitchenObject.SpawnKitchenObject(OutputKitchenObjectSO, this);
            }
             

        }
    }
    private KitchenObjectSO GetOutputForInput(KitchenObjectSO input)
    {
       CuttingRecipeSO cuttingRecipeSO= GetCuttingSOWithInput(input);
        if(cuttingRecipeSO!=null)
        {
            return cuttingRecipeSO.output;
        }
        return null;
    }
    private CuttingRecipeSO GetCuttingSOWithInput(KitchenObjectSO input)
    {
        //  Debug.Log("KitchenObjectSO");
        foreach (CuttingRecipeSO cuttingRecipeSO in cutKitchenObjectSO)
        {
            if (cuttingRecipeSO.input == input)
            {
                return cuttingRecipeSO;
            }
        }
        Debug.LogError("not matching recipe found!! Pls Review");
        return null;
    }
    private bool HasRecipeKitchenWithInput(KitchenObjectSO inputKitchenObject)
    {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingSOWithInput(inputKitchenObject);

        return cuttingRecipeSO != null;
    }
}
