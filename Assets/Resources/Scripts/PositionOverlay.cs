using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PositionOverlay : MonoBehaviour
{
    public RectTransform Parent;
    public TextMeshProUGUI TextMesh;
    public Vector2 Buffer;
    
    private void Update()
    {
        Vector2 screenSize = new Vector2(Screen.width, Screen.height);
        Vector2 screenExtents = screenSize / 2;
        Vector2 textBoxExtents = new Vector2(TextMesh.bounds.extents.x, TextMesh.bounds.extents.y);
        Vector2 bufferOffset = screenSize * Buffer;
        Vector2 position = bufferOffset + textBoxExtents;
        position = new Vector2(position.x, screenSize.y - position.y);

        Parent.position = position;
    }
}
