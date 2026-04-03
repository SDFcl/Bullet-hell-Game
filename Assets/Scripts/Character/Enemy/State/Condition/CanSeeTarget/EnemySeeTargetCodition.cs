using UnityEngine;

public class EnemySeeTargetCondition : ICondition<EnemyContext>
{
   public bool IsMet(EnemyContext ctx)
   {
       if (ctx.Target == null)
           return false;

       return ctx.LineOfSight.CanSeeTarget();
   }
}
