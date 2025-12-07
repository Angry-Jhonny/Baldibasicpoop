using System;

namespace Baldibasicpoop
{
    internal class MisterBenz_StateBase : NpcState
    {
        public MisterBenz_StateBase(MisterBenz pointPointer) : base(pointPointer)
        {
            this.pointPointer = pointPointer;
        }

        protected MisterBenz pointPointer;
    }
}
