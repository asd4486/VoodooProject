using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;

    public static AudioManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<AudioManager>();
                DontDestroyOnLoad(_instance);
            }
            return _instance;
        }

        set
        {

        }
    }
    public FMOD.Studio.EventInstance FMODEvent_Creature_Walk;
    public FMOD.Studio.EventInstance FMODEvent_Creature_footStep;

    public FMOD.Studio.EventInstance FMODEvent_Creature_Attack_base;
    public FMOD.Studio.EventInstance FMODEvent_Creature_Attack_longArm;
    public FMOD.Studio.EventInstance FMODEvent_Creature_Attack_feet;
    public FMOD.Studio.EventInstance FMODEvent_Creature_Attack_head_nice;
    public FMOD.Studio.EventInstance FMODEvent_Creature_Attack_head_aggro;

    public FMOD.Studio.EventInstance FMODEvent_Creature_LowLife;
    public FMOD.Studio.EventInstance FMODEvent_Creature_Healing;
    public FMOD.Studio.EventInstance FMODEvent_Creature_Dead;
    public FMOD.Studio.EventInstance FMODEvent_Creature_LooseMember;

    public FMOD.Studio.EventInstance FMODEvent_Ennemi_Walk;

    public FMOD.Studio.EventInstance FMODEvent_Ennemi_Attack_Lance;
    public FMOD.Studio.EventInstance FMODEvent_Ennemi_Attack_Arrow;

    public FMOD.Studio.EventInstance FMODEvent_Ennemi_BeingHit;

    public FMOD.Studio.EventInstance FMODEvent_Environnement;

    private void Awake()
    {
        FMODEvent_Creature_Walk = FMODUnity.RuntimeManager.CreateInstance("event:/Creature/Creature_Walk");
        FMODEvent_Creature_footStep = FMODUnity.RuntimeManager.CreateInstance("event:/Creature/Creature_footStep");
        FMODEvent_Creature_Attack_base = FMODUnity.RuntimeManager.CreateInstance("event:/Creature/Creature_Attack_base");
        FMODEvent_Creature_Attack_longArm = FMODUnity.RuntimeManager.CreateInstance("event:/Creature/Creature_Attack_longArm");
        FMODEvent_Creature_Attack_feet = FMODUnity.RuntimeManager.CreateInstance("event:/Creature/Creature_Attack_feet");
        FMODEvent_Creature_LowLife = FMODUnity.RuntimeManager.CreateInstance("event:/Creature/Creature_LowLife");
        FMODEvent_Creature_Healing = FMODUnity.RuntimeManager.CreateInstance("event:/Creature/Creature_Healing");
        FMODEvent_Creature_Dead = FMODUnity.RuntimeManager.CreateInstance("event:/Creature/Creature_Dead");
        FMODEvent_Creature_LooseMember = FMODUnity.RuntimeManager.CreateInstance("event:/Creature/Creature_LooseMember");
        FMODEvent_Creature_Attack_head_nice = FMODUnity.RuntimeManager.CreateInstance("event:/Creature/Creature_Attack_head_nice");
        FMODEvent_Creature_Attack_head_aggro = FMODUnity.RuntimeManager.CreateInstance("event:/Creature/Creature_Attack_head_agro");

        FMODEvent_Ennemi_Walk = FMODUnity.RuntimeManager.CreateInstance("event:/Ennemi/Ennemi_Walk");
        FMODEvent_Ennemi_Attack_Lance = FMODUnity.RuntimeManager.CreateInstance("event:/Ennemi/Ennemi_Attack_lance");
        FMODEvent_Ennemi_Attack_Arrow = FMODUnity.RuntimeManager.CreateInstance("event:/Ennemi/Ennemi_Attack_arrow");

        FMODEvent_Ennemi_BeingHit = FMODUnity.RuntimeManager.CreateInstance("event:/Ennemi/Ennemi_BeingHit");

        FMODEvent_Environnement = FMODUnity.RuntimeManager.CreateInstance("event:/Enviro/Environnement");
    }
}
