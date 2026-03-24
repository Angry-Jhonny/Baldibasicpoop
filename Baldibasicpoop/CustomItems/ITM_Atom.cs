using UnityEngine;

namespace Baldibasicpoop.CustomItems
{
    public class ITM_Atom : Item
    {
        public QuickExplosion explosion;
        public override bool Use(PlayerManager pm)
        {
            QuickExplosion e = Object.Instantiate(explosion);
            e.transform.localPosition = pm.transform.localPosition;
            e.transform.localPosition += Vector3.forward * 3f;
            return true;
        }
    }
}