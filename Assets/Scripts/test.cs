using System;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    [SerializeField] private Material mm;
    [SerializeField] private Color[] _color;
    public Color c;

    private Color mixColors()
    {
        Color _mixer = new Color(0f, 0f, 0f, 0f);

        foreach(Color c in _color)
        {
            _mixer += c;
        }

        _mixer /= _color.Length;

        return _mixer;
    }

    private float colorComprison(Color _requiredColor)
    {
        float _rDiff = Math.Abs(_requiredColor.r - mm.color.r);
        float _gDiff = Math.Abs(_requiredColor.g - mm.color.g);
        float _bDiff = Math.Abs(_requiredColor.b - mm.color.b);

        return 100 - (_rDiff + _gDiff + _bDiff) / 3 * 100;
    }

    // Start is called before the first frame update
    void Start()
    {
        mm = GetComponent<MeshRenderer>().materials[0];
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            mm.color = mixColors();
            Debug.Log(colorComprison(c));
        }
    }
}
