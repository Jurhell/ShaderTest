void DistortUV_float(float2 UV, float Amount, out float2 Out)
{
float time = _Time.y;

UV.y += Amount * 0.01f * (sin(UV.x * 3.5f + time * 0.35f) + sin(UV.x * 4.8f + time * 1.05f) + sin(UV.x * 7.3f + time * 0.45f)) / 3.0f;
UV.x += Amount * 0.12f * (sin(UV.y * 4.0f + time * 0.50f) + sin(UV.y * 6.8f + time * 0.75f) + sin(UV.y * 11.3f + time * 0.2f)) / 3.0f;
UV.y += Amount * 0.12f * (sin(UV.x * 4.2f + time * 0.64f) + sin(UV.x * 6.3f + time * 1.65f) + sin(UV.x * 8.2f + time * 0.45f)) / 3.0f;

Out = UV;
}