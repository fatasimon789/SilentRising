using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    private TextMesh _textMesh;
    private float _timeDestroy ;
    private Color _textColor; 
    private Vector3 _offset = new Vector3(0, 2, 0);
    private Vector3 randomInsensity = new Vector3(0.5f,0,0);
    void Start()
    {
        transform.localPosition += _offset;
        _textMesh = GetComponent<TextMesh>();
        _textColor = GetComponent<TextMesh>().color;
        _timeDestroy = 0f;
        transform.localPosition += new Vector3(Random.Range(-randomInsensity.x, randomInsensity.x),
        Random.Range(-randomInsensity.y,randomInsensity.y),
        Random.Range(-randomInsensity.z,randomInsensity.z));
    }
    private void Update()
    {
        if (_timeDestroy == 0)
        {
            // start destroy
            float disapearSpeed = 1.2f;
            _textColor.a -= disapearSpeed * Time.deltaTime;
            _textMesh.color = _textColor;
            if (_textColor.a < 0)
            {
                Destroy(gameObject);
            }
        }
    }


}
