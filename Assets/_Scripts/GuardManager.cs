using System.Collections.Generic;
using UnityEngine;

public class GuardManager : MonoBehaviour
{
    private List<GuardAI> guards = new List<GuardAI>();

    public void RegisterGuard(GuardAI guard)
    {
        if(!guards.Contains(guard))
            guards.Add(guard);
    }

    public void AlertNearbyGuards(GuardAI alertingGuard, float radius)
    {
        foreach(GuardAI guard in guards){
            if(guard == alertingGuard) continue;

            float distance = Vector3.Distance(alertingGuard.transform.position, guard.transform.position);

            if(distance <= radius){
                Debug.LogWarning($"{guard.name} was alerted by {alertingGuard.name}");
                guard.target = alertingGuard.target;
                guard.SetAlertedPosition(alertingGuard.target.position);
                guard.ChangeState(GuardState.ALERTED);
            }
        }
    }
}
