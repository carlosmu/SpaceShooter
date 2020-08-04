using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Es una clase estática
public static class Utils
{
    public static Vector2 GetHalfDimensionsInWorldUnits()
    {
        float width, height;
        // Crea la variable de la cámara
        Camera cam = Camera.main;
        // Calcular la relación de aspecto. Pongo un valor en float para que el resultado me de float.
        float ratio = cam.pixelWidth / (float)cam.pixelHeight;
        // Obtiene el alto, basado en el valor del size de la cámara ortográfica x2
        height = cam.orthographicSize * 2;
        // Calcula el ancho en base al alto x la relación de aspecto
        width = height * ratio;


        return new Vector2(width, height) / 2f;
    }
}
