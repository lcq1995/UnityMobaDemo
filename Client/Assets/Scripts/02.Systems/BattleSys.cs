﻿/****************************************************
    文件：BattleSys.cs
    作者：CodingCodingK
    博客：CodingCodingK.github.io
    邮箱：2470939431@qq.com
    日期：#DATE#
    功能：Unknown
*****************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using cfg;
using proto.HOKProtocol;
using PEMath;
using PEPhysx;
using UnityEngine;

public class BattleSys : SystemBase
{
    public static BattleSys Instance;

    private int mapId;
    public bool isTickFight;
    private List<BattleHeroData> battleHeroList;
    private GameObject fightGO;
    private FightMgr fightMgr;
    private AudioSource battleAudio;
    
    private int _keyId = 0;

    /// UI映射模型Position用倍乘器
    public float SkillDisMultiplier;
    
    /// 自增 移动逻辑帧Id
    public int KeyId
    {
        get
        {
            return ++_keyId;
        }
    }
    
    public override void InitSys()
    {
        base.InitSys();
        
        Instance = this;
        this.Log("BattleSys Init Completed.");
    }

    public void EnterBattle()
    {
        audioSvc.StopBGM();
        gameRootResources.loadWindow.SetWindowState(true);

        mapId = GameRoot.MapId;
        battleHeroList = GameRoot.battleHeroList;
        resSvc.AsyncLoadScene("map_" + mapId,　SceneLoadProgress,　SceneLoadDone);
    }

    private int lastPercent = 0;
    void SceneLoadProgress(int percent)
    {
        GameMsg msg = new GameMsg()
        {
            cmd = CMD.SendLoadPrg,
            sendLoadPrg = new SendLoadPrg()
            {
                percent = percent,
                roomId = GameRoot.ActiveRoomId,
            }
        };
        netSvc.SendMsg(msg);
        lastPercent = percent;
        
    }

    void SceneLoadDone()
    {
        // 初始化UI
        gameRootResources.playWindow.SetWindowState();
        gameRootResources.hpWindow.SetWindowState();
        
        // 加载角色资源
        
        
        //初始化战斗
        fightGO = new GameObject()
        {
            name = "fight"
        };
        fightMgr = fightGO.AddComponent<FightMgr>();
        battleAudio = fightGO.AddComponent<AudioSource>();
        var mapCfg = resSvc.GetMapConfigById(mapId);
        fightMgr.Init(battleHeroList,mapCfg);
        
        GameMsg msg = new GameMsg()
        {
            cmd = CMD.ReqBattleStart,
            reqBattleStart = new ReqBattleStart()
            {
                roomId = GameRoot.ActiveRoomId,
            }
        };
        netSvc.SendMsg(msg);


    }

    public void NotifyLoadPrg(GameMsg msg)
    {
        gameRootResources.loadWindow.RefreshPrgData(msg.notifyLoadPrg.percentList);
    }

    public void RspBattleStart(GameMsg msg)
    {
        fightMgr.InitCamFollowTrans(GameRoot.SelfPosIndex);
        gameRootResources.playWindow.InitSKillInfo();
        gameRootResources.loadWindow.SetWindowState(false);
        
        audioSvc.PlayBGMusic(Constants.BGBattle);

        isTickFight = true;
        GameRoot.ActiveRoomStartTime = DateTime.Now;
        PlayBattleFieldAudio("welcombattle");
    }
    
    public void RspBattleEnd(GameMsg msg) {
        gameRootResources.playWindow.SetWindowState(false);
        gameRootResources.hpWindow.SetWindowState(false);
        gameRootResources.resultWindow.SetWindowState(false);
        if(fightMgr != null) {
            fightMgr.UnInit();
        }
        DestroyImmediate(fightGO);
        LobbySys.Instance.EnterLobby();
        audioSvc.PlayBGMusic(Constants.BGMainCity);
    }
    
    public List<PEColliderBase> GetEnvColliders()
    {
        return fightMgr.GetEnvColliders();
    }
    
    public void NotifyOpKey(GameMsg msg)
    {
        if (isTickFight)
        {
            fightMgr.InputKey(msg.notifyOpKey.keyList);
            fightMgr.Tick();
        }
    }
    
    public void NotifyChat(GameMsg msg)
    {
        string chatMsg = msg.notifyChat.chatMsg;
        gameRootResources.playWindow.AddChatMsg(chatMsg);
    }

    public bool CheckUIInput() {
        return gameRootResources.playWindow.IsUIInput();
    }

    public void EnterCDState(int skillId,int cdTime)
    {
        gameRootResources.playWindow.EnterCDState(skillId,cdTime);
    }
    
    public void RefreshSkillWndInfo(SkillCfg cfg, int skillIndex)
    {
        gameRootResources.playWindow.RefreshSkillInfo(cfg,skillIndex);
    }


    public MainLogicUnit GetSelfHero()
    {
        return fightMgr.GetSelfHero(GameRoot.SelfPosIndex);
    }

    public TeamEnum GetCurrentUserTeam()
    {
        int sep = battleHeroList.Count / 2;
        return GameRoot.SelfPosIndex < sep ? TeamEnum.Blue : TeamEnum.Red;
    }

    #region api func

    /// <summary>
    /// 发送移动帧操作到服务器
    /// </summary>
    public bool SendMoveKey(PEVector3 logicDir)
    {
        if (!CanMove())
        {
            return false;
        }
        
        GameMsg msg = new GameMsg()
        {
            cmd = CMD.SendOpKey,
            sendOpKey = new SendOpKey()
            {
                roomId = GameRoot.ActiveRoomId,
                opKey = new OpKey()
                {
                    opIndex = GameRoot.SelfPosIndex,
                    keyType = KeyType.Move,
                    moveKey = new MoveKey(),
                }
            }
        };

        msg.sendOpKey.opKey.moveKey.x = logicDir.x.ScaledValue;
        msg.sendOpKey.opKey.moveKey.z = logicDir.z.ScaledValue;
        msg.sendOpKey.opKey.moveKey.keyId = KeyId;
        
        netSvc.SendMsg(msg);
        return true;
    }

    public void SendSkillKey(int skillId) {
        SendSkillKey(skillId, Vector3.zero);
    }
    
    public void SendSkillKey(int skillId,Vector3 vec)
    {
        if (!CanReleaseSkill(skillId))
        {
            // 当前技能不能释放!
            return;
        }
        
        // 发送技能释放指令
        GameMsg netSKillMsg = new GameMsg()
        {
            cmd = CMD.SendOpKey,
            sendOpKey = new SendOpKey()
            {
                roomId = GameRoot.ActiveRoomId,
                opKey = new OpKey()
                {
                    opIndex = GameRoot.SelfPosIndex,
                    keyType = KeyType.Skill,
                    skillKey = new SkillKey()
                    {
                        x_val = ((PEInt)vec.x).ScaledValue,
                        z_val = ((PEInt)vec.z).ScaledValue,
                        skillId = skillId,
                    }
                }
            }
        };
        netSvc.SendMsg(netSKillMsg);
    }
    
    bool CanMove() {
        return fightMgr.CanMove(GameRoot.SelfPosIndex);
    }
    /// <summary>
    /// 是否禁止本玩家释放当前技能
    /// </summary>
    /// <param name="skillID"></param>
    /// <returns></returns>
    bool CanReleaseSkill(int skillID) {
        return fightMgr.CanReleaseSkill(GameRoot.SelfPosIndex, skillID);
    }
    
    /// <summary>
    /// 是否禁止本玩家释放所有技能
    /// </summary>
    public bool IsForbidAllSkill_SelfPlayer() {
        return fightMgr.IsForbidAllSkill(GameRoot.SelfPosIndex);
    }
    
    public void AddBullet(Bullet bullet) {
        fightMgr.AddBullet(bullet);
    }
    
    public void PlayBattleFieldAudio(string name) {
        audioSvc.PlayEntityAudio(name, battleAudio);
    }

    public void EndBattle(bool isSucc)
    {
        gameRootResources.playWindow.SetWindowState(false);
        StartCoroutine(ShowResult(isSucc));
    }
    
    IEnumerator ShowResult(bool isSucc) {
        yield return new WaitForSeconds(0.5f);
        gameRootResources.resultWindow.SetWindowState();
        gameRootResources.resultWindow.SetBattleResult(isSucc);
    }
    
    public void RefreshIncome(int income) {
        gameRootResources.playWindow.RefreshIncome(income);
    }
    
    public void SetKillData(TeamEnum killHeroTeam) {
        gameRootResources.playWindow.SetKillData(killHeroTeam);
    }

    public void SetReviveState(bool isRevive, int reviveMs = 0)
    {
        gameRootResources.playWindow.SetReviveTime(isRevive, reviveMs / 1000);
    }
    #endregion
}
