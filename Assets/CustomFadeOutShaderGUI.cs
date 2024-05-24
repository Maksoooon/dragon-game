using UnityEngine;

public class ApplyShaderToMaterial : MonoBehaviour
{
    public Shader fadeOutShader;
    public Texture2D mainTexture;
    [Range(0, 1)]
    public float cutoffValue = 0.5f;

    void Start()
    {
        if (fadeOutShader == null)
        {
            Debug.LogError("Fade Out Shader not assigned!");
            return;
        }

        Material material = new Material(fadeOutShader);
        material.SetTexture("_MainTex", mainTexture);
        material.SetFloat("_Cutoff", cutoffValue);

        GetComponent<Renderer>().material = material;
    }
}
