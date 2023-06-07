using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class PlateCounterVisual : MonoBehaviour
{
    [SerializeField] private PlatesCounter platesCounter;   
    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private Transform plateVisualPrefab;
    private List<GameObject> plateVisualGameobjectList; 
    private void Start()
    {
        platesCounter.OnPlateSpawned += PlatesCounter_onPlateSpawned;
        platesCounter.OnRemovePlate += PlatesCounter_OnRemovePlate;
        plateVisualGameobjectList = new List<GameObject>();
    }

    private void PlatesCounter_OnRemovePlate(object sender, System.EventArgs e)
    {
        GameObject plateObject= plateVisualGameobjectList[plateVisualGameobjectList.Count-1];
        plateVisualGameobjectList.Remove(plateObject);
        Destroy(plateObject);
    }

    private void PlatesCounter_onPlateSpawned(object sender, System.EventArgs e)
    {
        Transform plateVisualTransform = Instantiate(plateVisualPrefab, counterTopPoint);
        float plateOffSetY = .1f;
        plateVisualTransform.localPosition = new Vector3(0, plateOffSetY * plateVisualGameobjectList.Count, 0);
        plateVisualGameobjectList.Add(plateVisualTransform.gameObject);
    }
}
