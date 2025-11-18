using UnityEngine;

public class Shot : MonoBehaviour
{
    public float timetolive = 2f;
    private float Expiretime = 0f;

    public void Update()
    {
        Expiretime += Time.deltaTime;
        if(Expiretime >= timetolive)
        {
            Destroy(gameObject);
        }
    }
}
