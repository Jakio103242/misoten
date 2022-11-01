using UnityEngine;
using UniRx;

namespace Player
{
    public class PlayerGizmo : MonoBehaviour
    {
        //[SerializeField] private BoolReactiveProperty visual;

        [SerializeField] private Transform look;
        [SerializeField] private PlayerTalk playerTalk;

        private void Start()
        {
        }

        private void OnDrawGizmos()
        {
            DrawCapsuleGizmo(look.position, look.forward * playerTalk.Range, playerTalk.Radius);
        }

        public void DrawCapsuleGizmo(Vector3 start, Vector3 end, float radius)
        {
            var preMatrix = Gizmos.matrix;

            // カプセル空間（(0, 0)からZ軸方向にカプセルが伸びる空間）からワールド座標系への変換行列
            Gizmos.matrix = Matrix4x4.TRS(start, Quaternion.FromToRotation(Vector3.forward, end), Vector3.one);

            // 球体を描画
            var distance = (end - start).magnitude;
            var capsuleStart    = Vector3.zero;
            var capsuleEnd      = Vector3.forward * distance;
            Gizmos.DrawWireSphere(capsuleStart, radius);
            Gizmos.DrawWireSphere(capsuleEnd, radius);
                    
            // ラインを描画
            var offsets = new Vector3[]{ new Vector3(-1.0f, 0.0f, 0.0f), new Vector3(0.0f, 1.0f, 0.0f), new Vector3(1.0f, 0.0f, 0.0f), new Vector3(0.0f, -1.0f, 0.0f) };
            for (int i = 0; i < offsets.Length; i++) {
                Gizmos.DrawLine(capsuleStart + offsets[i] * radius, capsuleEnd + offsets[i] * radius);
            }

            Gizmos.matrix = preMatrix;
        }
    }
}
