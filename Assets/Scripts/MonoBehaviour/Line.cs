using UnityEngine;

public class Line : MonoBehaviour
{
    [SerializeField]LineRenderer lineRenderer;
    public float offsetSpeed=0.1f;

    private void Update() {
        if(lineRenderer != null)
        {
            var offset=lineRenderer.material.mainTextureOffset;
            offset.x+=offsetSpeed*Time.deltaTime;
            lineRenderer.material.mainTextureOffset=offset;
        }
    }
}
