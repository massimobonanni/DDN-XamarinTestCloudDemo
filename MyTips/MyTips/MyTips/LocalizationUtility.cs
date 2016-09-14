using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyTips.Models;

namespace MyTips
{
    public static class LocalizationUtility
    {
        public static string GetFeedback(Feedback feedback, CultureInfo culture)
        {
            if (string.CompareOrdinal(culture.TwoLetterISOLanguageName, "it") == 0)
            {
                switch (feedback)
                {
                    case Feedback.Like:
                        return "Soddiffatto";
                    case Feedback.Love:
                        return "Molto Soddisfatto";
                    case Feedback.Neutral:
                        return "Normale";
                    case Feedback.Unlike:
                        return "Insoddisfatto";
                }
            }
            else
            {
                switch (feedback)
                {
                    case Feedback.Like:
                        return "Like";
                    case Feedback.Love:
                        return "Very like";
                    case Feedback.Neutral:
                        return "Neutral";
                    case Feedback.Unlike:
                        return "Not like";
                }
            }
            return null;
        }
    }
}
