//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using Bright.Serialization;
using System.Collections.Generic;



namespace cfg
{

public sealed partial class NSSpeedBuffCfg :  BuffCfg 
{
    public NSSpeedBuffCfg(ByteBuf _buf)  : base(_buf) 
    {
        moveSpeedPer = _buf.ReadInt();
        atkSpeedPer = _buf.ReadInt();
        resetTime = _buf.ReadInt();
        audio_buffOn = _buf.ReadString();
        PostInit();
    }

    public static NSSpeedBuffCfg DeserializeNSSpeedBuffCfg(ByteBuf _buf)
    {
        return new NSSpeedBuffCfg(_buf);
    }

    /// <summary>
    /// 移动速度改变百分比
    /// </summary>
    public int moveSpeedPer { get; private set; }
    /// <summary>
    /// 攻击速度改变百分比
    /// </summary>
    public int atkSpeedPer { get; private set; }
    /// <summary>
    /// 速度改变持续时间
    /// </summary>
    public int resetTime { get; private set; }
    /// <summary>
    /// 被动buff启动时音效
    /// </summary>
    public string audio_buffOn { get; private set; }

    public const int __ID__ = -1384948945;
    public override int GetTypeId() => __ID__;

    public override void Resolve(Dictionary<string, object> _tables)
    {
        base.Resolve(_tables);
        PostResolve();
    }

    public override void TranslateText(System.Func<string, string, string> translator)
    {
        base.TranslateText(translator);
    }

    public override string ToString()
    {
        return "{ "
        + "buffId:" + buffId + ","
        + "buffName:" + buffName + ","
        + "buffType:" + buffType + ","
        + "attacher:" + attacher + ","
        + "staticPosType:" + staticPosType + ","
        + "impacter:" + impacter + ","
        + "buffDelay:" + buffDelay + ","
        + "buffInterval:" + buffInterval + ","
        + "buffDuration:" + buffDuration + ","
        + "buffAudio:" + buffAudio + ","
        + "buffEffect:" + buffEffect + ","
        + "hitTickAudio:" + hitTickAudio + ","
        + "effectDestoryExtend:" + effectDestoryExtend + ","
        + "moveSpeedPer:" + moveSpeedPer + ","
        + "atkSpeedPer:" + atkSpeedPer + ","
        + "resetTime:" + resetTime + ","
        + "audio_buffOn:" + audio_buffOn + ","
        + "}";
    }
    
    partial void PostInit();
    partial void PostResolve();
}

}