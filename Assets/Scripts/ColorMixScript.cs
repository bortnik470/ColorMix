using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System;

public class ColorMixScript : MonoBehaviour
{
    private Material _material;
    public List<Color> _colors = new List<Color>();
    [SerializeField] private TextMeshProUGUI _percentText;
    [SerializeField] private Image _image;

    private Color mixColors()
    {
        Color _mixer = new Color(0f, 0f, 0f, 0f);

        foreach (Color c in _colors)
        {
            _mixer += c;
        }

        _mixer += _material.color;

        _mixer /= _colors.Count + 1;

        _colors.Clear();

        return _mixer;
    }

    private int colorComprison(Color _requiredColor)
    {
        float _rDiff = Math.Abs(_requiredColor.r - _material.color.r);
        float _gDiff = Math.Abs(_requiredColor.g - _material.color.g);
        float _bDiff = Math.Abs(_requiredColor.b - _material.color.b);

        return (int)(100 - ((_rDiff + _gDiff + _bDiff) / 3 * 100));
    }

    private void startMix()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = true;
        _material.color = mixColors();
        int _percent = colorComprison(_image.color);
        _percentText.text = $"Similarly on {_percent} % ";
        if (_percent >= 90)
            EventManagerScript._newLevel += newLevel;
    }

    public void newLevel()
    {
        _material.color = Color.white;
        _percentText.text = "";
        _image.color = mixColors();
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        EventManagerScript._newLevel -= newLevel;
    }

    private void Start()
    {
        EventManagerScript._event.AddListener(startMix);
        _material = GetComponent<MeshRenderer>().materials[0];
    }
}
