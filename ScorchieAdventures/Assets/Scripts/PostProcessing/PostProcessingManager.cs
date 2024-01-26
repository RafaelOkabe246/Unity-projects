using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcessingManager : MonoBehaviour
{
    public static PostProcessingManager instance;

    private Volume globalVolume;
    private Bloom bloom;
    private ChromaticAberration chromaticAberration;
    private Vignette vignette;
    private LensDistortion lensDistortion;
    private ColorAdjustments colorAdjustments;

    #region INITIAL VALUES
    //Bloom
    private float bThreshold;
    private float bIntensity;
    private float bScatter;
    //Vignette
    private Color vColor;
    private Vector2 vCenter;
    private float vIntensity;
    private float vSmoothness;
    private bool vRounded;
    //Chromatic Aberration
    private float caIntensity;
    //Lens Distortion
    private float ldIntensity;
    private float ldXMultiplier;
    private float ldYMultiplier;
    private Vector2 ldCenter;
    private float ldScale;
    //ColorAdjustments
    private Color caFilterColor;
    #endregion

    private void Awake()
    {
        instance = this;

        globalVolume = GetComponent<Volume>();
        globalVolume.profile.TryGet(out bloom);
        globalVolume.profile.TryGet(out chromaticAberration);
        globalVolume.profile.TryGet(out vignette);
        globalVolume.profile.TryGet(out lensDistortion);
        globalVolume.profile.TryGet(out colorAdjustments);
    }

    private void Start()
    {
        #region SETTING INITIAL VALUES
        bThreshold = bloom.threshold.value;
        bIntensity = bloom.intensity.value;
        bScatter = bloom.scatter.value;

        vColor = vignette.color.value;
        vCenter = vignette.center.value;
        vIntensity = vignette.intensity.value;
        vSmoothness = vignette.smoothness.value;
        vRounded = vignette.rounded.value;

        caIntensity = chromaticAberration.intensity.value;

        ldIntensity = lensDistortion.intensity.value;
        ldXMultiplier = lensDistortion.xMultiplier.value;
        ldYMultiplier = lensDistortion.yMultiplier.value;
        ldCenter = lensDistortion.center.value;
        ldScale = lensDistortion.scale.value;

        caFilterColor = colorAdjustments.colorFilter.value;
        #endregion
    }

    public IEnumerator SetTemporaryBloom(float threshold, float intensity, float scatter, float time)
    {
        bloom.threshold.value = threshold;
        bloom.intensity.value = intensity;
        bloom.scatter.value = scatter;

        yield return new WaitForSeconds(time);

        bloom.threshold.value = bThreshold;
        bloom.intensity.value = bIntensity;
        bloom.scatter.value = bScatter;
    }

    public IEnumerator SetTemporaryVignette(Color color, Vector2 center, float intensity, float smoothness, bool isRounded, float time)
    {
        vignette.color.value = color;
        vignette.intensity.value = intensity;
        vignette.smoothness.value = smoothness;
        vignette.rounded.value = isRounded;

        yield return new WaitForSeconds(time);

        vignette.color.value = vColor;
        vignette.intensity.value = vIntensity;
        vignette.smoothness.value = vSmoothness;
        vignette.rounded.value = vRounded;
    }

    public IEnumerator SetTemporaryChromaticAberration(float intensity, float time)
    {
        chromaticAberration.intensity.value = intensity;

        yield return new WaitForSeconds(time);

        chromaticAberration.intensity.value = caIntensity;
    }

    public IEnumerator SetTemporaryLensDistortion(float intensity, float xMultiplier, float yMultiplier, Vector2 center, float scale, float time)
    {
        lensDistortion.intensity.value = intensity;
        lensDistortion.xMultiplier.value = xMultiplier;
        lensDistortion.yMultiplier.value = yMultiplier;
        lensDistortion.center.value = center;
        lensDistortion.scale.value = scale;

        yield return new WaitForSeconds(time);

        lensDistortion.intensity.value = ldIntensity;
        lensDistortion.xMultiplier.value = ldXMultiplier;
        lensDistortion.yMultiplier.value = ldYMultiplier;
        lensDistortion.center.value = ldCenter;
        lensDistortion.scale.value = ldScale;
    }

    public void SetColorAdjustmentColor(Color newColor) 
    {
        colorAdjustments.colorFilter.value = newColor;
    }
}