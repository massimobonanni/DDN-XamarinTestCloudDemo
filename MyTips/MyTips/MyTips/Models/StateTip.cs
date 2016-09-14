using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTips.Models
{
    public class StateTip
    {

        public string State { get; set; }

        public Tip[] Tips { get; set; } = new Tip[4];

        public Tip GetByFeedback(Feedback feedback)
        {
            return Tips.FirstOrDefault(a => a.Feedback == feedback);
        }


        public override string ToString()
        {
            return State;
        }
    }
}
