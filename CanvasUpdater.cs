using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasUpdater : MonoBehaviour
{
    public Rigidbody2D _playerRB;
    public Rigidbody2D _blockRB;

    public Text _playerText;
    public Text _blockText;

    void Update()
    {
        _playerText.text = "Player Velocity : "+ _playerRB.velocity.ToString();
        _blockText.text = "Platform Velocity : "+ _blockRB.velocity.ToString();
    }
}
