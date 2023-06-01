using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCouterVisual : MonoBehaviour
{
    [SerializeField] private StoveCounter   stoveCounter;
    [SerializeField] private GameObject stoveOnGameObject;
    [SerializeField] private GameObject particlesGameObject;


    private void Start()
    {
        stoveCounter.OnStateChange += StoveCounter_OnStateChange; ;
    }

    private void StoveCounter_OnStateChange(object sender, StoveCounter.OnStateChangerEventAgrs e)
    {
        bool showVisual = e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Burned;
        stoveOnGameObject.SetActive(showVisual);
        particlesGameObject.SetActive(showVisual);
    }
}
