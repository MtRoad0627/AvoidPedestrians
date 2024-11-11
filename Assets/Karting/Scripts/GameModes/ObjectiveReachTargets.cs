﻿using System.Collections;
using UnityEngine;

public class ObjectiveReachTargets : Objective
{

    [Tooltip("Choose whether you need to collect all pickups or only a minimum amount")]
    public bool mustCollectAllPickups = true;
    
    [Tooltip("If MustCollectAllPickups is false, this is the amount of pickups required")]
    public int pickupsToCompleteObjective = 5;
    
    [Header("Notification")]
    [Tooltip("Start sending notification about remaining pickups when this amount of pickups is left")]
    public int notificationPickupsRemainingThreshold = 1;
    
    

    IEnumerator Start()
    {
   
        TimeManager.OnSetTime(totalTimeInSecs, isTimed, gameMode);
        
        yield return new WaitForEndOfFrame();

        title = "すべてのチェックポイントを通って！";
        
        if (mustCollectAllPickups)
            pickupsToCompleteObjective = NumberOfPickupsTotal;
        
        Register();
    }

    protected override void ReachCheckpoint(int remaining)
    {

        if (isCompleted)
            return;
        
        if (mustCollectAllPickups) 
            pickupsToCompleteObjective = NumberOfPickupsTotal;

        m_PickupTotal = NumberOfPickupsTotal - remaining;
        int targetRemaining = mustCollectAllPickups ? remaining : pickupsToCompleteObjective - m_PickupTotal;

        // update the objective text according to how many enemies remain to kill
        if (targetRemaining == 0)
        {
            CompleteObjective(string.Empty, GetUpdatedCounterAmount(), null);
        }
        else if (targetRemaining == 2)
        {
            string notificationText = notificationPickupsRemainingThreshold >= targetRemaining
                ? "ゴールまであと少し！"
                : string.Empty;
            UpdateObjective(string.Empty, GetUpdatedCounterAmount(), notificationText);
        }
        else if (targetRemaining == 5)
        {
            string notificationText = notificationPickupsRemainingThreshold >= targetRemaining
                ? "まもなく歩行者天国、気をつけて！"
                : string.Empty;
            UpdateObjective(string.Empty, GetUpdatedCounterAmount(), notificationText);
        }
        else if (targetRemaining >= 1)
        {
            // // create a notification text if needed, if it stays empty, the notification will not be created
            // string notificationText = notificationPickupsRemainingThreshold >= targetRemaining
            //     ? targetRemaining + " " + targetName + "s to collect left"
            //     : string.Empty;

            UpdateObjective(string.Empty, GetUpdatedCounterAmount(), null);
        }

    }

    public override string GetUpdatedCounterAmount()
    {
        return m_PickupTotal + " / " + pickupsToCompleteObjective;
    }
    
}
