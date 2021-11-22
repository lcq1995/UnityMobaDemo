﻿/****************************************************
    文件：MainLogicUnit.cs
    作者：CodingCodingK
    博客：CodingCodingK.github.io
    邮箱：2470939431@qq.com
    日期：#DATE#
    功能：Unknown
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using HOKProtocol;
using PEMath;

public abstract partial class MainLogicUnit : LogicUnit
{
    public LogicUnitData ud;
    public UnitStateEnum unitState;
    public UnitTypeEnum unitType;

    public MainLogicUnit(LogicUnitData ud)
    {
        ud = this.ud;
        unitName = ud.unitCfg.unitName;
    }
    
    
    public override void LogicInit()
    {
        // 初始化属性
        InitProperties();
        // 初始化技能
        InitSkill();
        // 初始化移动控制
        InitMove();

        unitState = UnitStateEnum.Alive;
    }

    public override void LogicTick()
    {
        TickSkill();
        TickMove();
    }

    public override void LogicUnInit()
    {
        UnInitSkill();
        UnInitMove();
    }

    public void InputKey(OpKey key)
    {
        // 根据key的Type分发操作
        switch (key.keyType)
        {
            case KeyType.Skill:
                
                break;
            case KeyType.Move:
                // TODO 需要学习理解
                PEInt x = PEInt.zero;
                x.ScaledValue = key.moveKey.x;
                PEInt z = PEInt.zero;
                z.ScaledValue = key.moveKey.z;
                InputMoveKey(new PEVector3(x,0,z));
                break;
            
            default:
                this.Error("Key is not exist !");
                break;
        }
        
    }
}

public enum UnitStateEnum
{
    Alive,
    Dead,
}

public enum UnitTypeEnum
{
    Hero,
    Soldier,
    Tower,
}

public enum TeamEnum
{
    None,
    Blue,
    Red,
    Neutral, // 中立
}