private void SpawnScratchMarks(
    Transform target, Vector3 point, Vector3 normal, float strength)
{
    Vector3 tangent = Vector3.Cross(normal, Vector3.up);
    if (tangent.sqrMagnitude < 0.01f)
        tangent = Vector3.Cross(normal, Vector3.right);

    tangent.Normalize();

    Vector3 across = Vector3.Cross(normal, tangent).normalized;

    int scratches = UnityEngine.Random.Range(5, 8);

    for (int i = 0; i < scratches; i++)
    {
        GameObject mark = new GameObject("ScratchMark");
        mark.transform.position = point;
        mark.transform.SetParent(target, true);

        LineRenderer line = mark.AddComponent<LineRenderer>();

        line.positionCount = 2;
        line.useWorldSpace = false;

        float width = UnityEngine.Random.Range(0.0035f, 0.0065f);
        line.startWidth = width;
        line.endWidth = width * 0.6f;

        line.numCapVertices = 6;
        line.numCornerVertices = 6;

        line.sharedMaterial = ScratchMaterials.MarkMaterial();

        line.shadowCastingMode =
            UnityEngine.Rendering.ShadowCastingMode.Off;

        line.receiveShadows = false;

        float length = UnityEngine.Random.Range(
            0.05f,
            Mathf.Lerp(0.08f, 0.16f, strength));

        float angle = UnityEngine.Random.Range(-15f, 15f);

        Quaternion rotation =
            Quaternion.AngleAxis(angle, normal);

        Vector3 direction = rotation * tangent;

        Vector3 offset =
            across * UnityEngine.Random.Range(-0.018f, 0.018f);

        offset += direction * UnityEngine.Random.Range(-0.008f, 0.008f);

        Vector3 start =
            point + offset -
            direction * (length * 0.5f) +
            normal * 0.004f;

        Vector3 end =
            point + offset +
            direction * (length * 0.5f) +
            normal * 0.004f;

        line.SetPosition(0,
            mark.transform.InverseTransformPoint(start));

        line.SetPosition(1,
            mark.transform.InverseTransformPoint(end));

        Track(mark);
    }
}
