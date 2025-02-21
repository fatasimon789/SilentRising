using UnityEngine;

public class CombatTypeManager
{
   public enum DamegesType { Fire, Ice , Electrical , HiTech, Physic }
   public static int DealDamages(float USER_DAMAGES ) 
   {
     
        return 0;  
   }

   public static int  DamegesOverTime(float USER_DAMAGES,ref float TIME_DURATION,ref float UPDATE_EVERY_TIME,float REPETING_TIME) 
   {

        // bien tat ca cook thanh dameovertime
        float timeStarting = Time.deltaTime;
        TIME_DURATION -= timeStarting;
        if (TIME_DURATION > 0) 
        {
            float timeUpdateStaring = Time.deltaTime;
            UPDATE_EVERY_TIME -= timeUpdateStaring;
            if (UPDATE_EVERY_TIME  <= 0) 
            {
                // deal daamges
                UPDATE_EVERY_TIME = REPETING_TIME;
            }
        }
        return 0;
   } 
}
