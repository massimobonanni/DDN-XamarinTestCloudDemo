using MyTips.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTips.Services
{
    public class StateTipService
    {
        private static IEnumerable<StateTip> stateTips;

        static StateTipService()
        {
            var stateTipList = new List<StateTip>();
            stateTipList.Add(new StateTip()
            {
                State = "Italia",
                Tips = new Tip[] { new Tip() { AmoutPercent = 0, Feedback = Feedback.Unlike },
                                    new Tip() { AmoutPercent = 5, Feedback = Feedback.Neutral  },
                                    new Tip() { AmoutPercent = 10, Feedback = Feedback.Like  },
                                    new Tip() { AmoutPercent = 15, Feedback = Feedback.Love  }}
            });
            stateTipList.Add(new StateTip()
            {
                State = "USA",
                Tips = new Tip[] { new Tip() { AmoutPercent = 10, Feedback = Feedback.Unlike },
                                    new Tip() { AmoutPercent = 15, Feedback = Feedback.Neutral  },
                                    new Tip() { AmoutPercent = 18, Feedback = Feedback.Like  },
                                    new Tip() { AmoutPercent = 20, Feedback = Feedback.Love  }}
            });

            stateTipList.Add(new StateTip()
            {
                State = "Francia",
                Tips = new Tip[] { new Tip() { AmoutPercent = 0, Feedback = Feedback.Unlike },
                                    new Tip() { AmoutPercent = 5, Feedback = Feedback.Neutral  },
                                    new Tip() { AmoutPercent = 10, Feedback = Feedback.Like  },
                                    new Tip() { AmoutPercent = 15, Feedback = Feedback.Love  }}
            });

            stateTipList.Add(new StateTip()
            {
                State = "Inghilterra",
                Tips = new Tip[] { new Tip() { AmoutPercent = 0, Feedback = Feedback.Unlike },
                                    new Tip() { AmoutPercent = 5, Feedback = Feedback.Neutral  },
                                    new Tip() { AmoutPercent = 10, Feedback = Feedback.Like  },
                                    new Tip() { AmoutPercent = 15, Feedback = Feedback.Love  }}
            });

            stateTipList.Add(new StateTip()
            {
                State = "Spagna",
                Tips = new Tip[] { new Tip() { AmoutPercent = 0, Feedback = Feedback.Unlike },
                                    new Tip() { AmoutPercent = 5, Feedback = Feedback.Neutral  },
                                    new Tip() { AmoutPercent = 10, Feedback = Feedback.Like  },
                                    new Tip() { AmoutPercent = 15, Feedback = Feedback.Love  }}
            });

            stateTipList.Add(new StateTip()
            {
                State = "Germania",
                Tips = new Tip[] { new Tip() { AmoutPercent = 0, Feedback = Feedback.Unlike },
                                    new Tip() { AmoutPercent = 5, Feedback = Feedback.Neutral  },
                                    new Tip() { AmoutPercent = 10, Feedback = Feedback.Like  },
                                    new Tip() { AmoutPercent = 15, Feedback = Feedback.Love  }}
            });

            stateTips = stateTipList.OrderBy(a => a.State);
        }

        public IEnumerable<StateTip> GetAll()
        {
            return stateTips.ToList();
        }
    }
}
