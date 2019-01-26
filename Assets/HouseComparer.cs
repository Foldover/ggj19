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
    public int score;

    private void Update() {
        BlueprintCamera.Render();
        var targetPixels = getPixelsFromRenderTexture(BlueprintCamera.targetTexture);
        HouseCamera.Render();
        var actualPixels = getPixelsFromRenderTexture(HouseCamera.targetTexture);
        float totalDiff = 0;
        var comparedPixels = 0;
        for (var i = 0; i < targetPixels.Length; i++) {
            var targetPixel = targetPixels[i];
            var actualPixel = actualPixels[i];
            if (targetPixel.Compare(Color.black))
                continue; 
            if (actualPixel.Compare(Color.black)){
                totalDiff += 1;
            }
            comparedPixels += 1;
        }

        if (comparedPixels == 0) {
            score = 100;
            return;
        }
        score = (int) (Math.Round(100 - (totalDiff / (float)comparedPixels) * 100.0f) );
        score = (int) Mathf.Clamp(Mathf.Pow(score, 1.1f), 0 , 100);
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