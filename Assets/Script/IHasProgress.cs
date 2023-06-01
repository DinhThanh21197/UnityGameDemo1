using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHasProgress 
{
    public event EventHandler<OnProgcessChangeEventAgrs> OnProgressChange;
    public class OnProgcessChangeEventAgrs
    {
        public float progressNormalized;
    }
}
