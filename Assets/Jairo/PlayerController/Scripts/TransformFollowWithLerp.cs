using UnityEngine;

public class TransformFollowWithLerp : MonoBehaviour
{
    [SerializeField] private Transform target = default;
    [SerializeField] private float positionLerp = 2F;
    [SerializeField] private float rotationLerp = 2F;
    
    private Transform myTransform;

    private void Awake()
    {
        myTransform = transform;
    }

    private void Update()
    {
        if (target == null)
            return;
        myTransform.position = Vector3.Lerp(myTransform.position, target.position, positionLerp * Time.deltaTime);
        myTransform.rotation = Quaternion.Lerp(myTransform.rotation, target.rotation, rotationLerp * Time.deltaTime);
    }
}
