using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class StoveCounter : BaseCounter,IHasProgress
{
    public event EventHandler<OnStateChangerEventAgrs> OnStateChange;
    public event EventHandler<IHasProgress.OnProgcessChangeEventAgrs> OnProgressChange;

    public class OnStateChangerEventAgrs:EventArgs
    {
        public State state;
    }

   
    [SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;
    [SerializeField] private FryingRecipeSO fryingRecipeSO;
    [SerializeField] private BuringRecipeSO[] buringRecipeSOArray;
    [SerializeField] private BuringRecipeSO buringRecipeSO;
    public enum State
    {
        Idle,
        Frying,
        Fried,
        Burned
        
    }
    [SerializeField]private State state;
    
    private float fryingTimer;
    private float buringTimer;
    private void Start()
    {
        state = State.Idle;
    }
    private void Update()
    {
        switch (state)
        {   case State.Idle:
                break;
            case State.Frying:
                fryingTimer += Time.deltaTime;
                OnProgressChange?.Invoke(this, new IHasProgress.OnProgcessChangeEventAgrs
                {
                    progressNormalized = fryingTimer / fryingRecipeSO.fryingTimeMax
                });
                if (fryingTimer > fryingRecipeSO.fryingTimeMax)
                {    
                    fryingTimer = 0f;
                    GetKitchenObject().DestroySelf();
                    KitchenObject.SpawnKitchenObject(fryingRecipeSO.output, this);
                    Debug.Log("Kitchen Fried !!!");
                    state = State.Fried;
                    buringTimer = 0f;
                    buringRecipeSO = GetBuringSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                   
                }
                break;
            case State.Fried:
                buringTimer += Time.deltaTime;
                OnProgressChange?.Invoke(this, new IHasProgress.OnProgcessChangeEventAgrs
                {
                    progressNormalized = buringTimer / buringRecipeSO.burningTimeMax
                });
                if (buringTimer >= buringRecipeSO.burningTimeMax)
                {
                    buringTimer = 0f;
                    GetKitchenObject().DestroySelf();

                    KitchenObject.SpawnKitchenObject(buringRecipeSO.output,this);
                    Debug.Log("Kitchen Object Burned !! ");
                    state=State.Burned;
                  
                }
                break;
            case State.Burned:
                break;
               

        }
        Debug.Log(this.state.ToString());
      

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
                    fryingRecipeSO = GetFryingSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                    if (fryingRecipeSO == null)
                    {
                        Debug.LogError("null roi sep oi");
                    }
                    state = State.Frying;
                    fryingTimer = 0f;
                    OnStateChange?.Invoke(this, new OnStateChangerEventAgrs
                    {
                        state = state
                    });


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
                        state = State.Idle;
                        OnStateChange?.Invoke(this, new OnStateChangerEventAgrs
                        {
                            state = state
                        });
                        OnProgressChange?.Invoke(this, new IHasProgress.OnProgcessChangeEventAgrs { progressNormalized = 0f });
                    }

                }
            }
            else
            {
                //player it not anything
                GetKitchenObject().SetKitchenObjectParent(player);
                state = State.Idle;
                OnStateChange?.Invoke(this, new OnStateChangerEventAgrs
                {
                    state = state
                });
                OnProgressChange?.Invoke(this, new IHasProgress.OnProgcessChangeEventAgrs { progressNormalized = 0f });
            }
        }
    }
    public override void InteractAlternate(Player player)
    {
        //  Debug.Log("InteractAlternate");
        if (HasKitchenObject() && HasRecipeKitchenWithInput(GetKitchenObject().GetKitchenObjectSO()))
        {
            // there is a kitchenObject Here
           

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
            return fryingRecipeSO.output;
        }
        return null;
    }
    private FryingRecipeSO GetFryingSOWithInput(KitchenObjectSO input)
    {
        //  Debug.Log("KitchenObjectSO");
        foreach (FryingRecipeSO fryingRecipeSO in fryingRecipeSOArray)
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
       

        return fryingRecipeSO != null;
    }

    private BuringRecipeSO GetBuringSOWithInput(KitchenObjectSO input)
    {
        
        foreach (BuringRecipeSO buringRecipeSO in buringRecipeSOArray)
        {
            if (buringRecipeSO.input == input)
            {
                return buringRecipeSO;
            }
        }
        Debug.LogError("not matching recipe found!! Pls Review");
        return null;
    } 
}
