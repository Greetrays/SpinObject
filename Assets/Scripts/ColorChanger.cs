using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;

[RequireComponent(typeof(MeshRenderer))]

public class ColorChanger : MonoBehaviour
{
    [SerializeField] private float _minEdge;
    [SerializeField] private float _maxEdge;
    [SerializeField] private float _speed;
    [SerializeField] private List<Color> _colors = new List<Color>();
    [SerializeField] private UnityEvent _clicked;

    private const string BaseColor = "_BaseColor";
    private const string ReplaceableColor = "_ReplaceableColor";
    private const string Edge = "_Edge";

    private Material _material;
    private Coroutine _changeColor;

    private void Awake()
    {
        _material = GetComponent<MeshRenderer>().material;
        SetColor(_colors[Random.Range(0, _colors.Count)], GetRandomColor());
    }

    private void OnMouseDown()
    {
        _clicked?.Invoke();
    }

    public void TryChangeColor()
    {
        if (_changeColor != null)
            return;

        _changeColor = StartCoroutine(ChangeColor());
    }

    private void SetColor(Color baseColor, Color replaceableColor)
    {
        _material.SetColor(BaseColor, baseColor);
        _material.SetColor(ReplaceableColor, replaceableColor);
    }

    private Color GetRandomColor()
    {
        var colors = _colors.Where(color => color != _material.GetColor(BaseColor)).ToList();
        return colors[Random.Range(0, colors.Count)];
    }

    private IEnumerator ChangeColor()
    {
        float currentEdge = _minEdge;

        while (currentEdge < _maxEdge)
        {
            currentEdge += Time.deltaTime * _speed;
            _material.SetFloat(Edge, currentEdge);
            yield return null;
        }

        _changeColor = null;
        _material.SetFloat(Edge, _minEdge);
        SetColor(_material.GetColor(ReplaceableColor), GetRandomColor());
    }
}
