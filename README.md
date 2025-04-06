# Learn Balatro

当卡牌闲置时，在 x 轴上使用正弦函数，在 y 轴上使用余弦函数，使卡牌沿圆形路径旋转

```csharp
private void Idle()
{
    float sine = Mathf.Sin(Time.time);
    float cosine = Mathf.Cos(Time.time);

    transform.rotation = Quaternion.Euler(sine * rotationAngle, cosine * rotationAngle, 0);
}
```

## Reference

[Unity Tutoria - Mix and Jam: 重现卡牌游戏Balatro的游戏手感](https://www.bilibili.com/video/BV17t421w73n/)
