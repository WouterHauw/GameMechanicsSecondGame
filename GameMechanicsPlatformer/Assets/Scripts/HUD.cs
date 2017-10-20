using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public Sprite[] HeartSprites;

    public Image HeartImage;

    private PlayerInputScript _player = null;

    void Start()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        _player = player.GetComponent<PlayerInputScript>();
    }

    void Update()
    {
        HeartImage.sprite = HeartSprites[_player.CurrentHealth];
    }

}
