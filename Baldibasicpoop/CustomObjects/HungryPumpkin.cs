using MTM101BaldAPI.Registers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baldibasicpoop.CustomObjects
{
    public class HungryPumpkin: EnvironmentObject, IItemAcceptor, IClickable<int>
    {
        private bool pumpkinTalking;
        private bool NeedToWelcome;

        public SoundObject[] aud_giveMe;

        public Dictionary<string, SoundObject> aud_foods;

        private string WantedFood;

        public override void LoadingFinished()
        {
            base.LoadingFinished();
            Initialize();
        }

        public void Initialize()
        {
            NeedToWelcome = true;
            pumpkinTalking = false;
        }

        void IItemAcceptor.InsertItem(PlayerManager player, EnvironmentController ec)
        {
            throw new NotImplementedException();
        }

        bool IItemAcceptor.ItemFits(Items item)
        {
            throw new NotImplementedException();
        }

        public void Clicked(int player)
        {
            throw new NotImplementedException();
        }
        public void ClickableSighted(int player) { }
        public void ClickableUnsighted(int player) { }
        public bool ClickableHidden() => pumpkinTalking;
        public bool ClickableRequiresNormalHeight() => true;
    }
}
