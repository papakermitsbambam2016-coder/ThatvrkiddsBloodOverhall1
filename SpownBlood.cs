private void SpawnBlood(Vector3 point, Vector3 normal, float speed)
{
    GameObject blood = new GameObject("ScratchBlood");
    blood.transform.position = point + normal * 0.01f;
    blood.transform.rotation = Quaternion.LookRotation(normal);

    ParticleSystem ps = blood.AddComponent<ParticleSystem>();
    ps.Stop();

    float strength = Mathf.InverseLerp(
        MinimumSwipeSpeed,
        MaximumSwipeSpeed,
        speed);

    var main = ps.main;
    main.loop = false;
    main.playOnAwake = false;

    main.startColor = new ParticleSystem.MinMaxGradient(
        new Color(
            Random.Range(0.42f, 0.55f),
            0.005f,
            0.005f,
            Random.Range(0.82f, 0.95f)));

    main.startLifetime =
        new ParticleSystem.MinMaxCurve(
            0.25f,
            Mathf.Lerp(0.45f, 0.8f, strength));

    main.startSpeed =
        new ParticleSystem.MinMaxCurve(
            0.35f,
            Mathf.Lerp(0.8f, 2.2f, strength));

    main.startSize =
        new ParticleSystem.MinMaxCurve(
            0.006f,
            Mathf.Lerp(0.02f, 0.04f, strength));

    main.maxParticles = Mathf.RoundToInt(
        Mathf.Lerp(10, 40, strength));

    main.simulationSpace =
        ParticleSystemSimulationSpace.World;

    var emission = ps.emission;
    emission.enabled = true;
    emission.rateOverTime = 0;

    short burstCount = (short)Mathf.RoundToInt(
        Mathf.Lerp(6, 35, strength));

    var bursts =
        new Il2CppInterop.Runtime.InteropTypes.Arrays
        .Il2CppReferenceArray<ParticleSystem.Burst>(1);

    bursts[0] = new ParticleSystem.Burst(
        0f,
        burstCount);

    emission.SetBursts(bursts, 1);

    var shape = ps.shape;
    shape.enabled = true;
    shape.shapeType = ParticleSystemShapeType.Cone;

    shape.angle = Mathf.Lerp(
        10f,
        40f,
        strength);

    shape.radius = 0.01f;

    shape.randomDirectionAmount = 0.35f;

    var velocity = ps.velocityOverLifetime;
    velocity.enabled = true;

    velocity.space =
        ParticleSystemSimulationSpace.Local;

    velocity.x =
        new ParticleSystem.MinMaxCurve(
            -0.25f,
            0.25f);

    velocity.y =
        new ParticleSystem.MinMaxCurve(
            -0.25f,
            0.25f);

    velocity.z =
        new ParticleSystem.MinMaxCurve(
            0.1f,
            Mathf.Lerp(0.5f, 2f, strength));

    ParticleSystemRenderer renderer =
        blood.GetComponent<ParticleSystemRenderer>();

    if (renderer != null)
    {
        renderer.sharedMaterial =
            ScratchMaterials.BloodMaterial();

        renderer.shadowCastingMode =
            UnityEngine.Rendering.ShadowCastingMode.Off;

        renderer.receiveShadows = false;
    }

    ps.Play();

    Track(blood);
