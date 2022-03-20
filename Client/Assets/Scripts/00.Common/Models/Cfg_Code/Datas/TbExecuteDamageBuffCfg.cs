//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using Bright.Serialization;
using System.Collections.Generic;

namespace cfg.Datas
{
   
public partial class TbExecuteDamageBuffCfg
{
    private readonly Dictionary<int, ExecuteDamageBuffCfg> _dataMap;
    private readonly List<ExecuteDamageBuffCfg> _dataList;
    
    public TbExecuteDamageBuffCfg(ByteBuf _buf)
    {
        _dataMap = new Dictionary<int, ExecuteDamageBuffCfg>();
        _dataList = new List<ExecuteDamageBuffCfg>();
        
        for(int n = _buf.ReadSize() ; n > 0 ; --n)
        {
            ExecuteDamageBuffCfg _v;
            _v = ExecuteDamageBuffCfg.DeserializeExecuteDamageBuffCfg(_buf);
            _dataList.Add(_v);
            _dataMap.Add(_v.buffId, _v);
        }
        PostInit();
    }

    public Dictionary<int, ExecuteDamageBuffCfg> DataMap => _dataMap;
    public List<ExecuteDamageBuffCfg> DataList => _dataList;

    public ExecuteDamageBuffCfg GetOrDefault(int key) => _dataMap.TryGetValue(key, out var v) ? v : null;
    public ExecuteDamageBuffCfg Get(int key) => _dataMap[key];
    public ExecuteDamageBuffCfg this[int key] => _dataMap[key];

    public void Resolve(Dictionary<string, object> _tables)
    {
        foreach(var v in _dataList)
        {
            v.Resolve(_tables);
        }
        PostResolve();
    }

    public void TranslateText(System.Func<string, string, string> translator)
    {
        foreach(var v in _dataList)
        {
            v.TranslateText(translator);
        }
    }
    
    partial void PostInit();
    partial void PostResolve();
}

}