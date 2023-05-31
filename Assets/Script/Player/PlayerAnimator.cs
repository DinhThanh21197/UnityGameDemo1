using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private const string IS_WALKING = "isWalk";




    private Animator playerAnimator;
    public Player player;
    
    private void Awake()
    {
       playerAnimator = GetComponent<Animator>();
       
    }
    private void Update()
    {
        playerAnimator.SetBool(IS_WALKING,player.IsWalking());
    }
}
