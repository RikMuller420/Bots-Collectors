using System.Collections;
using UnityEngine;

public class MushroomGenerator : MonoBehaviour
{
    private const float SecondInMinute = 60f;
    private const float HalfPlaneSize = 5f;
    private const int HitColliderMaxSize = 16;

    [SerializeField] private MushroomPool _pool;
    [SerializeField] private Transform _groundTransform;
    [SerializeField] private float _mushroomPerMinute = 6f;
    [SerializeField] private LayerMask _buildingLayerMask;
    [SerializeField] private float _minDistanceFromBuildings = 20f;

    private WaitForSeconds _wait;
    private Coroutine _generateCoroutine;
    private Collider[] _colliderBuffer = new Collider[HitColliderMaxSize];

    private void Awake()
    {
        _wait = new WaitForSeconds(SecondInMinute / _mushroomPerMinute);
    }

    public void StartGenrate()
    {
        StopGenerateCoroutine();
        _generateCoroutine = StartCoroutine(Generating());
    }

    public void StopGenerate()
    {
        StopGenerateCoroutine();
    }

    private void StopGenerateCoroutine()
    {
        if (_generateCoroutine != null)
        {
            StopCoroutine(_generateCoroutine);
        }
    }

    private IEnumerator Generating()
    {
        while (enabled)
        {
            SpawnMushroom();

            yield return _wait;
        }
    }

    private void SpawnMushroom()
    {
        Mushroom mushroom = _pool.Get();
        mushroom.transform.position = GetSuitablePoint();
        mushroom.Activate();
    }

    private Vector3 GetRandomPointOnPlane()
    {
        float randomX = Random.Range(-HalfPlaneSize, HalfPlaneSize);
        float randomZ = Random.Range(-HalfPlaneSize, HalfPlaneSize);

        Vector3 localPoint = new Vector3(randomX, 0f, randomZ);

        return _groundTransform.TransformPoint(localPoint);
    }

    private Vector3 GetSuitablePoint()
    {
        Vector3 point = GetRandomPointOnPlane();

        while (IsPointFarAwayFromBuildings(point) == false)
        {
            point = GetRandomPointOnPlane();
        }

        return point;
    }

    private bool IsPointFarAwayFromBuildings(Vector3 localPoint)
    {
        int hitCount = Physics.OverlapSphereNonAlloc(localPoint, _minDistanceFromBuildings,
                                                    _colliderBuffer, _buildingLayerMask);

        return hitCount == 0;
    }
}
