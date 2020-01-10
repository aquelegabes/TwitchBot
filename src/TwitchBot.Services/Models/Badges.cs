using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace TwitchBot.Services.Models
{
    
    public static class TwitchBadges
    {
        [Flags]
        public enum Badges
        {
            None        = 0,
            /// <summary>
            /// Is chat's broadcaster.
            /// </summary>
            Broadcaster = 1 << 0,
            /// <summary>
            /// Is chat's moderator.
            /// </summary>
            Moderator   = 1 << 1,
            /// <summary>
            /// Is a subscriber.
            /// </summary>
            Subscriber  = 1 << 2,
            /// <summary>
            /// Is a twitch turbo user.
            /// </summary>
            Turbo       = 1 << 3,
            /// <summary>
            /// Is a twitch staff member.
            /// </summary>
            Staff       = 1 << 4,
            /// <summary>
            /// Has donated at least 100bits.
            /// </summary>
            Bits        = 1 << 5,
            /// <summary>
            /// Twitch global moderator.
            /// </summary>
            Global_Mod  = 1 << 6,
            /// <summary>
            /// Twitch admin.
            /// </summary>
            Admin       = 1 << 7,
            /// <summary>
            /// Regular chatter.
            /// </summary>
            General     = 1 << 8,
            /// <summary>
            /// Chat vip.
            /// </summary>
            Vip         = 1 << 9,
            /// <summary>
            /// Has gifted at least one sub.
            /// </summary>
            Sub_Gifter  = 1 << 10,
            /// <summary>
            /// Amazon prime member.
            /// </summary>
            Premium     = 1 << 11,
            /// <summary>
            /// Twitch verified.
            /// </summary>
            Partner     = 1 << 12,
        }

        public static ICollection<string> AvailableBadges 
            = new string[] { "broadcaster", "moderator", "admin",
                "turbo", "subscriber", "staff", "global_mod", "bits",
                "premium", "partner", "vip"
            };
    }
}
