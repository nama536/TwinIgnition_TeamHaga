using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class ReflectLaser : MonoBehaviour
{
    [Header("反射回数")]
    public int maxReflections = 3;

    [Header("レーザー距離")]
    public float maxDistance = 30f;

    [Header("消える時間")]
    public float destroyTime = 2f;

    [Header("壁")]
    public LayerMask wallLayer;


    private LineRenderer line;


    void Start()
    {
        line = GetComponent<LineRenderer>();

        DrawLaser();

        Destroy(gameObject, destroyTime);
    }



    void DrawLaser()
    {
        Vector2 position = transform.position;
        Vector2 direction = transform.right;


        line.positionCount = 1;
        line.SetPosition(0, position);



        for(int i = 0; i < maxReflections; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(position, direction, maxDistance, wallLayer);

            if(hit)
            {
                line.positionCount++;

                line.SetPosition(line.positionCount - 1, hit.point);


                direction = Vector2.Reflect(direction, hit.normal);

                position = hit.point;
            }
            else
            {
                line.positionCount++;

                line.SetPosition(line.positionCount - 1, position + direction * maxDistance);

                break;
            }
        }
    }
}