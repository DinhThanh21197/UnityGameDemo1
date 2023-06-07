using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounter : BaseCounter
{
    public event EventHandler OnPlateSpawned;
    public event EventHandler OnRemovePlate;
    [SerializeField] private KitchenObjectSO PlateKitchenObjectSO;
    private float spawnPlateTimer;
    private float spawnPlateTimerMax = 4f;
    private int plateSpawnedAmount;
    private int plateSpawnedAmountMax=4;
    private void Update()
    {
        spawnPlateTimer += Time.deltaTime;
        if (spawnPlateTimer >= spawnPlateTimerMax)
        {
            spawnPlateTimer = 0f;
            // KitchenObject.SpawnKitchenObject(PlateKitchenObjectSO, this);
            if (plateSpawnedAmount < plateSpawnedAmountMax)
            {
                plateSpawnedAmount++;
                OnPlateSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }
    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            // player is empty handed
            if(plateSpawnedAmount > 0) 
            {
                // there is at least one palte here
                plateSpawnedAmount--;
                KitchenObject.SpawnKitchenObject(PlateKitchenObjectSO, player);
                OnRemovePlate?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
