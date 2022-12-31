using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    public MainManager Manager;
    public MenuUI UI;

    private void OnTriggerEnter(Collider other)
    {
        {
            Destroy(other.gameObject);
            Manager.GameOver();
        }
    }
}
