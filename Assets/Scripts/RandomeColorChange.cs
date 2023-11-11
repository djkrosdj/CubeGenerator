using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomeColorChange : MonoBehaviour
{
    // Переменные для хранения текущего цвета, следующего цвета и компонента Renderer
    private Color _startColor;
    private Color _nextColor;
    private Renderer _renderer;

    // Переменные для управления временем и задержкой
    private float _recoloringTime;
    private float _lastColorChangeTime;

    // Параметры цветовой смены и задержки после смены цвета
    [SerializeField] private float _colorChangeTime;
    [SerializeField] private float _delayAfterColorChange;

    // Start вызывается перед первым кадром
    private void Start()
    {
        // Получаем компонент Renderer объекта
        _renderer = GetComponent<Renderer>();

        // Генерируем следующий цвет и фиксируем время последней смены цвета
        GenerateNextColor();
        _lastColorChangeTime = Time.time;
    }

    // Метод для генерации следующего цвета
    private void GenerateNextColor()
    {
        // Сохраняем текущий цвет и генерируем случайный цвет в пространстве HSV
        _startColor = _renderer.material.color;
        _nextColor = Random.ColorHSV(0f, 1f, 0.8f, 1f, 0.7f, 1f);
    }

    // Update вызывается один раз за кадр
    private void Update()
    {
        // Увеличиваем время смены цвета на время, прошедшее с предыдущего кадра
        _recoloringTime += Time.deltaTime;

        // Вычисляем нормализованное время для интерполяции между цветами
        var recoloringTime = _recoloringTime / _colorChangeTime;

        // Интерполируем между текущим и следующим цветами
        var currentColor = Color.Lerp(_startColor, _nextColor, recoloringTime);

        // Присваиваем интерполированный цвет объекту
        _renderer.material.color = currentColor;

        // Проверяем, прошло ли достаточно времени для смены цвета
        if (Time.time - _lastColorChangeTime >= _delayAfterColorChange)
        {
            // Обновляем время последней смены цвета
            _lastColorChangeTime = Time.time;

            // Если прошло нужное время и цвет уже интерполирован до конечного,
            // сбрасываем таймер и генерируем новый следующий цвет
            if (_recoloringTime >= _colorChangeTime)
            {
                _recoloringTime = 0f;
                GenerateNextColor();
            }
        }
    }
}