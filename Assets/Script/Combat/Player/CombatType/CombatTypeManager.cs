using System;
using System.Collections.Generic;
using UnityEngine;

public  class CombatTypeManager
{
    public static LiveTime _liveTime = new LiveTime();
    
    public enum TypeElement { Fire, Ice, Electrical, HiTech, Physic }
    public enum DamagesType
    {
        BaseDamages, Dot, TrueDamages
    }

    #region Main Method
    public static GetTypeElementInfo TypeElementInfo(TypeElement TYPE_OF_ELEMENT_WEAPON)
    {
       
        switch (TYPE_OF_ELEMENT_WEAPON)
        {
            case TypeElement.Fire:
                // color red
               
                var getColorElement= Color.red;
                var getMultiplyElement = 0;
                return new GetTypeElementInfo(getColorElement,getMultiplyElement);
                // burn 
            case TypeElement.Ice:
                var getColorElement1 = Color.blue;
                var getMultiplyElement1 = 1;
                return new GetTypeElementInfo(getColorElement1, getMultiplyElement1);
        }
        throw new Exception();
    }
    
    public static void DealDamages(GameObject TARGET,float USER_DAMAGES,TypeElement TYPE_OF_ELEMENT)
    {
         // take damages 
    }
   
    public  static void DamegesOverTime(GameObject TARGET, float USER_DAMAGES,float  TIME_DURATION
                                      ,float MIN_TIME_DURATION,TypeElement TYPE_OF_ELEMENT)
    {

        // bien tat ca cook thanh dameovertime
        float timeRunning = Time.deltaTime;

        float amountFrameStart = TIME_DURATION / MIN_TIME_DURATION;
        float minDot = USER_DAMAGES / amountFrameStart;
        if (TIME_DURATION < _liveTime.LiveTimeOut()) 
        {
            _liveTime.ResetMinTimeOut();
            return;
        }
        float minTimeRunning = Time.deltaTime;
        _liveTime.AccessLiveTime(timeRunning,minTimeRunning);
        if (MIN_TIME_DURATION <= _liveTime.LiveMinTimeOut())
        {
            TARGET.GetComponent<IEnemy>().enemyHP.takeDamages((int)minDot, TYPE_OF_ELEMENT);
            _liveTime.ResetMinTimeOut();
        }
    }
    public static void TrueDamages(GameObject TARTGET ,float USER_DAMAGES) 
    {
       
    }
    #endregion
   
    #region Resauble Method 
    public  struct GetTypeElementInfo 
    {
        public Color colorWeapon ;
        public int elementMultiply;
        
        public GetTypeElementInfo(Color COLOR, int MULTIPLY) 
        {
            colorWeapon = COLOR;
            elementMultiply = MULTIPLY;
        }
    }
    public  class LiveTime 
    {
        private float timeDuration = 0;
        private float timeMinDuration = 0;
        public void AccessLiveTime(float TIME_DURATION, float TIME_MIN_DURATION) 
        {
              timeDuration += TIME_DURATION;
              timeMinDuration += TIME_MIN_DURATION;
        }
        public float LiveTimeOut() 
        {
            return timeDuration;
        }
        public void ResetTimeOut() 
        {
            timeDuration = 0;
        }
        public float LiveMinTimeOut()
        {
            return timeMinDuration;
        }
        public void ResetMinTimeOut()
        {
            timeMinDuration = 0;
        }
    }
    #endregion

}

