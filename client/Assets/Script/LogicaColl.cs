using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicaColl : MonoBehaviour
{
    public static bool coll { get; set; }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        coll = true;
    }
}
