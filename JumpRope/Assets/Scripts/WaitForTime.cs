﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitForTime : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerable Cycle()
    {
        yield return new WaitForSeconds(2);
    }
}
