using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TMPro;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class HouseComparer : MonoBehaviour {
    public Camera BlueprintCamera;
    public Camera HouseCamera;
    public float TotalDiff;

    private void Update() {
        BlueprintCamera.Render();
        var pixels1 = getPixelsFromRenderTexture(BlueprintCamera.targetTexture);
        HouseCamera.Render();
        var pixels2 = getPixelsFromRenderTexture(HouseCamera.targetTexture);
        TotalDiff = 0;
        for (var i = 0; i < pixels1.Length; i++) {
            var leftPixel = pixels1[i];
            var rightPixel = pixels2[i];
            var diffRRaw = leftPixel.r - rightPixel.r;
            var diffR = Mathf.Abs(leftPixel.r - rightPixel.r);
            var diffG = Mathf.Abs(leftPixel.g - rightPixel.g);
            var diffB = Mathf.Abs(leftPixel.b - rightPixel.b);
            var pixelDiff = diffR + diffG + diffB;
            TotalDiff += pixelDiff;
        }
    }

    private Color[] getPixelsFromRenderTexture(RenderTexture renderTexture) {
        var tex = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGB24, false);
        RenderTexture.active = renderTexture;
 
        // Read pixels
        Rect rectReadPicture = new Rect(0, 0, renderTexture.width, renderTexture.height);
        tex.ReadPixels(rectReadPicture, 0, 0);
        RenderTexture.active = null; // added to avoid errors 
        return tex.GetPixels();
    }
}