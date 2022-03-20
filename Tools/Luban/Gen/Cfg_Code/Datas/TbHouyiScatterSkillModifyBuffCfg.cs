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
   
public partial class TbHouyiScatterSkillModifyBuffCfg
{
    private readonly Dictionary<int, HouyiScatterSkillModifyBuffCfg> _dataMap;
    private readonly List<HouyiScatterSkillModifyBuffCfg> _dataList;
    
    public TbHouyiScatterSkillModifyBuffCfg(ByteBuf _buf)
    {
        _dataMap = new Dictionary<int, HouyiScatterSkillModifyBuffCfg>();
        _dataList = new List<HouyiScatterSkillModifyBuffCfg>();
        
        for(int n = _buf.ReadSize() ; n > 0 ; --n)
        {
            HouyiScatterSkillModifyBuffCfg _v;
            _v = HouyiScatterSkillModifyBuffCfg.DeserializeHouyiScatterSkillModifyBuffCfg(_buf);
            _dataList.Add(_v);
            _dataMap.Add(_v.buffId, _v);
        }
        PostInit();
    }

    public Dictionary<int, HouyiScatterSkillModifyBuffCfg> DataMap => _dataMap;
    public List<HouyiScatterSkillModifyBuffCfg> DataList => _dataList;

    public HouyiScatterSkillModifyBuffCfg GetOrDefault(int key) => _dataMap.TryGetValue(key, out var v) ? v : null;
    public HouyiScatterSkillModifyBuffCfg Get(int key) => _dataMap[key];
    public HouyiScatterSkillModifyBuffCfg this[int key] => _dataMap[key];

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