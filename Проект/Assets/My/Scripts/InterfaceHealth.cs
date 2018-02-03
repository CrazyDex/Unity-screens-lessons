using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceHealth : MonoBehaviour {

    public Texture2D green;
    public Texture2D black;
    public Texture2D red;
    public Rect currentHealth;

    private void Awake()
    {
        currentHealth = new Rect(10, 10, 200, 20);
    }

    public void OnGUI()
    {
        GUI.DrawTexture(new Rect(8, 8, 204, 24), red);
        GUI.DrawTexture(new Rect(10, 10, 200, 20), black);
        GUI.DrawTexture(currentHealth, green);
    }
}
