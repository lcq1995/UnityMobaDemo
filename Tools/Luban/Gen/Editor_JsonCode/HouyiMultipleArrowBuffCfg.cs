//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using Bright.Serialization;
using System.Collections.Generic;
using SimpleJSON;



namespace editor.cfg
{

public sealed partial class HouyiMultipleArrowBuffCfg :  BuffCfg 
{
    public HouyiMultipleArrowBuffCfg()
    {
    }

    public override void LoadJson(SimpleJSON.JSONObject _json)
    {
        base.LoadJson(_json);
        { 
            var _fieldJson = _json["arrowCount"];
            if (_fieldJson != null)
            {
                if(!_fieldJson.IsNumber) { throw new SerializationException(); }  arrowCount = _fieldJson;
            }
        }
        
        { 
            var _fieldJson = _json["arrowDelay"];
            if (_fieldJson != null)
            {
                if(!_fieldJson.IsNumber) { throw new SerializationException(); }  arrowDelay = _fieldJson;
            }
        }
        
        { 
            var _fieldJson = _json["posOffset"];
            if (_fieldJson != null)
            {
                if(!_fieldJson.IsNumber) { throw new SerializationException(); }  posOffset = _fieldJson;
            }
        }
        
    }

    public override void SaveJson(SimpleJSON.JSONObject _json)
    {        
        base.SaveJson(_json);
        {
            _json["arrowCount"] = new JSONNumber(arrowCount);
        }
        {
            _json["arrowDelay"] = new JSONNumber(arrowDelay);
        }
        {
            _json["posOffset"] = new JSONNumber(posOffset);
        }
    }

    public static HouyiMultipleArrowBuffCfg LoadJsonHouyiMultipleArrowBuffCfg(SimpleJSON.JSONNode _json)
    {
        HouyiMultipleArrowBuffCfg obj = new HouyiMultipleArrowBuffCfg();
        obj.LoadJson((SimpleJSON.JSONObject)_json);
        return obj;
    }
        
    public static void SaveJsonHouyiMultipleArrowBuffCfg(HouyiMultipleArrowBuffCfg _obj, SimpleJSON.JSONNode _json)
    {
        _obj.SaveJson((SimpleJSON.JSONObject)_json);
    }

    public int arrowCount { get; set; }

    /// <summary>
    /// 每只箭的延迟
    /// </summary>
    public int arrowDelay { get; set; }

    /// <summary>
    /// 箭之间的上下偏移值
    /// </summary>
    public float posOffset { get; set; }

}
}