using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionsScreen : MonoBehaviour
{
    [SerializeField] Slider _redSlider;
    [SerializeField] Slider _greenSlider;
    [SerializeField] Slider _blueSlider;

    [SerializeField] Image _rFill;
    [SerializeField] Image _gFill;
    [SerializeField] Image _bFill;

    [SerializeField] TMP_Text _redValue;
    [SerializeField] TMP_Text _greenValue;
    [SerializeField] TMP_Text _blueValue;

    [SerializeField] Image _currentColor;



    private Color _redUpdatedColor;
    private Color _greenUpdatedColor;
    private Color _blueUpdatedColor;

    [SerializeField] Material _M_protector;

    public Color _finalColor;

    int _rColor;
    int _gColor;
    int _bColor;

    private void Start()
    {
        _finalColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        _M_protector.color = _finalColor;
        _redSlider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
        _greenSlider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
        _blueSlider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
        
    }

    public void ValueChangeCheck()
    {
        _redUpdatedColor = new Color(_redSlider.value, 0f, 0f);
        _greenUpdatedColor = new Color(0f, _greenSlider.value, 0f);
        _blueUpdatedColor = new Color(0f, 0f, _blueSlider.value);

        ColorBlock cbRed = _redSlider.colors;
        ColorBlock cbGreen = _greenSlider.colors;
        ColorBlock cbBlue = _blueSlider.colors;

        cbRed.normalColor = _redUpdatedColor;
        cbRed.highlightedColor = _redUpdatedColor;
        cbRed.pressedColor = _redUpdatedColor;
        cbRed.selectedColor = _redUpdatedColor;
        _rFill.color = _redUpdatedColor;
        _redSlider.colors = cbRed;

        cbGreen.normalColor = _greenUpdatedColor;
        cbGreen.highlightedColor = _greenUpdatedColor;
        cbGreen.pressedColor = _greenUpdatedColor;
        cbGreen.selectedColor = _greenUpdatedColor;
        _gFill.color = _greenUpdatedColor;
        _greenSlider.colors = cbGreen;

        cbBlue.normalColor = _blueUpdatedColor;
        cbBlue.highlightedColor = _blueUpdatedColor;
        cbBlue.pressedColor = _blueUpdatedColor;
        cbBlue.selectedColor = _blueUpdatedColor;
        _bFill.color = _blueUpdatedColor;
        _blueSlider.colors = cbBlue;

        _rColor = (int) (255f * _redSlider.value);
        _redValue.text = _rColor.ToString();

        _gColor = (int)(255f * _greenSlider.value);
        _greenValue.text = _gColor.ToString();

        _bColor = (int)(255f * _blueSlider.value);
        _blueValue.text = _bColor.ToString();

        _finalColor = new Color(_redSlider.value, _greenSlider.value, _blueSlider.value);

        _currentColor.color = _finalColor;
        _M_protector.color = _finalColor;
    }
}
