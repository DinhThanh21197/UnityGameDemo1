using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField]private KitchenObjectSO kitchenObjectSO;
    public KitchenObjectSO GetKitchenObjectSO() { return kitchenObjectSO; }
   
    private IKitchenObjectParent KitchenObjectParent;
    public void SetKitchenObjectParent(IKitchenObjectParent IKitchenObjectParent)
    {  
        if(this.KitchenObjectParent != null)
        {
            this.KitchenObjectParent.ClearKitchenObject();
        }
        this.KitchenObjectParent = IKitchenObjectParent;
        if (KitchenObjectParent.HasKitchenObject())
        {
            Debug.LogError("KitchenObjectParent alrealy has a kitchenObject");
        }
        IKitchenObjectParent.SetKitchenObject(this);
       transform.parent = IKitchenObjectParent.GetKitchenObjectClearCounterFollow();
       transform.localPosition = Vector3.zero;
    }
    public IKitchenObjectParent GetKitchenObjectParent()
    {
        return KitchenObjectParent; 
    }
    public void DestroySelf()
    {
        KitchenObjectParent.ClearKitchenObject();
        Destroy(gameObject);
    }
    public static KitchenObject SpawnKitchenObject(KitchenObjectSO kitchenObjectSO,IKitchenObjectParent kitchenObjectParent)
    {   
        Transform kitchenObjectTranform = Instantiate(kitchenObjectSO.prefab);
        KitchenObject kitchenObject = kitchenObjectTranform.GetComponent<KitchenObject>();
        kitchenObject.SetKitchenObjectParent(kitchenObjectParent);
        return kitchenObject;
    }
    public bool TryGetPlate(out PlateKitchenObject plateKitchenObject)
    {
        if(this is PlateKitchenObject)
        {
            plateKitchenObject = this as PlateKitchenObject;
            return true;
        }
        else
        {
            plateKitchenObject = null;
            return false;
        }
    }
   
}
