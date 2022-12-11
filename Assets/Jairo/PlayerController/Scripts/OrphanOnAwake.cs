using UnityEngine;

public class OrphanOnAwake : MonoBehaviour
{
    private void Awake() => transform.SetParent(null);
}
