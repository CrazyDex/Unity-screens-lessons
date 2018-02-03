using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySwordSpawner : MonoBehaviour {

    public Collider2D col;
    public List<Collider2D> cols;
    public LayerMask layerMask;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.IsTouchingLayers(layerMask)) cols.Add(collision);
    }




}
