using UnityEngine;

public class HealthBarFollow : MonoBehaviour
{
    [SerializeField]
    Transform enemy;
    Canvas canvas;

    public Vector3 offset = new Vector3(0, 0.3f, 0);

    private void Start()
    {
        canvas = GetComponent<Canvas>();
    }
    void LateUpdate()
    {
        gameObject.layer = enemy.gameObject.layer;
        canvas.sortingLayerID = enemy.GetComponent<SpriteRenderer>().sortingLayerID;
        canvas.sortingOrder = enemy.GetComponent<SpriteRenderer>().sortingOrder + 1;
    }
}