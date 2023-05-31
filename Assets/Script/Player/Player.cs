using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Player : MonoBehaviour,IKitchenObjectParent
{   
    public static Player Instance { get; private set; }
    public event EventHandler<OnSeclectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSeclectedCounterChangedEventArgs : EventArgs
    {
        public BaseCounter selectedCounter;
    }
    [SerializeField]private GameInput gameInput;
    [SerializeField] private float moveSpeed = 7f;
    private bool isWalking;
    private Vector3 lastInteractDir;
    [SerializeField] private LayerMask counterLayerMask;
    [SerializeField] private BaseCounter selectedCounter;
    [SerializeField] private Transform KitchenObjectHoldPoint;
    [SerializeField] private KitchenObject kitchenObject;



    private void Start()
    {
        gameInput.OnInteractAction += GameInput_OnInteractActions;
        gameInput.OnInteractAlternateAction += GameInput_OnInteractAlternateAction;
        
    }

    private void GameInput_OnInteractAlternateAction(object sender, EventArgs e)
    {
       // Debug.Log("GameInput_OnInteractAlternateAction");
       if(selectedCounter!=null)
        {
            selectedCounter.InteractAlternate(this);
        }
    }

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("there is more than one player instance");
        }
        Instance = this;
    }

    private void GameInput_OnInteractActions(object sender, System.EventArgs e)
    {
        if (selectedCounter != null)
        {
            selectedCounter.Interact(this);
        }
      
    }

    private void Update()
    {
        HandleMovement();
        HandleInteractions();
    }
    public bool IsWalking()
    {
        return isWalking;
    }
    private void HandleMovement()
    {

        Vector2 inputVector = gameInput.GetMommentVector();
        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);
        isWalking = moveDir != Vector3.zero;
        float playerSize = 0.7f;
        float playerHeight = 2f;
        float moveDistance = moveSpeed * Time.deltaTime;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerSize, moveDir, moveDistance);
        if (!canMove)
        {
            Vector3 moveDirX = new Vector3(inputVector.x, 0, 0).normalized;
            canMove = moveDir.x!=0&&!Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerSize, moveDirX, moveDistance);
            if (canMove)
            {
                moveDir = moveDirX;
            }
            else
            {
                Vector3 moveDirZ = new Vector3(0, 0, inputVector.y).normalized;
                canMove =moveDir.z!=0&& !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerSize, moveDirZ, moveDistance);
                if (canMove)
                {
                    moveDir = moveDirZ;
                }
                else
                {


                }
            }
        }
        if (canMove)
        {
            transform.position += moveDir * moveSpeed * Time.deltaTime;

        }
        float rotationSpeed = 5f;
        if (moveDir != Vector3.zero) transform.forward = -Vector3.Slerp(transform.forward, moveDir, rotationSpeed);

    }
    private void HandleInteractions()
    {
        Vector2 inputVector = gameInput.GetMommentVector();
        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);
        if(moveDir != Vector3.zero)
        {
            lastInteractDir = moveDir;
        }
        float interactDistance = 2f;
        if (Physics.Raycast(transform.position, lastInteractDir, out RaycastHit raycastHit, interactDistance,counterLayerMask))
        {
           
            if (raycastHit.transform.TryGetComponent(out BaseCounter baseCounter))
            {
                if (baseCounter != selectedCounter)
                {
                    SetSelectedCounter(baseCounter);
                }
                
            }
            else
            {
                SetSelectedCounter(null);
            }
        }
        else
        {
            SetSelectedCounter(null);
        }
    }
    private void SetSelectedCounter(BaseCounter BaseCounterSelected)
    {
        this.selectedCounter=BaseCounterSelected;
        OnSelectedCounterChanged?.Invoke(this, new OnSeclectedCounterChangedEventArgs
        {
            selectedCounter = BaseCounterSelected
        }); 
    }

    public Transform GetKitchenObjectClearCounterFollow()
    {
        return this.KitchenObjectHoldPoint;
    }
    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;

    }
    public KitchenObject GetKitchenObject()
    {
        return this.kitchenObject;
    }
    public void ClearKitchenObject()
    {
        this.kitchenObject = null;
    }
    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
}
