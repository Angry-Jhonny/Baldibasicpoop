using System;

namespace Baldibasicpoop
{
    internal class MisterBenz_StateBase : NpcState
    {
        public MisterBenz_StateBase(MisterBenz benz) : base(benz)
        {
            this.benz = benz;
        }

        protected MisterBenz benz;
    }
}