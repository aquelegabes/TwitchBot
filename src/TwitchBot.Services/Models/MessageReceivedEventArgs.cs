using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using static TwitchBot.Services.Models.TwitchBadges;

namespace TwitchBot.Services.Models
{
    /// <summary>
    /// Message received event arguments.
    /// </summary>
    public class MessageReceivedEventArgs : EventArgs
    {
        public string Channel { get; private set; }
        public string FromSystemFormat { get; private set; }
        public bool IsUserMessage { get; private set; }
        public UserMessage UsrMessage { get; private set; }

        /// <summary>
        /// Struct responsible for transforming a system sent message into a composed user message.
        /// </summary>
        public struct UserMessage
        {
            public bool IsMessageHighlighted { get; set; }
            public string Message { get; set; }
            public UserModel User { get; set; }
        }

        private struct UserInfo
        {
            public Dictionary<string, string> Info { get; set; }
            public Dictionary<string, bool> Badges { get; set; }
        }

        /// <summary>
        /// Returns a string that represents current object.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.FromSystemFormat;
        }

        /// <summary>
        /// Constructor responsible for initializing a <see cref="MessageReceivedEventArgs"/>.
        /// </summary>
        /// <param name="msg">System received message.</param>
        public MessageReceivedEventArgs(string msg, string channel)
        {
            this.Channel = channel;
            this.FromSystemFormat = msg;

            if (msg.Contains("PRIVMSG"))
            {
                IsUserMessage = true;

                var userInfo = GetUserInfo(msg);
                var dictBadges = userInfo.Badges;
                UsrMessage = new UserMessage
                {
                    User = new UserModel
                    {
                        UserColor = userInfo.Info["color"],
                        DisplayName = userInfo.Info["display-name"],
                    },

                    // +3 Cause: (space, ':', space)
                    Message = msg.Substring(msg.IndexOf($"#{Channel}") + Channel.Length + 3),
                    IsMessageHighlighted = userInfo.Info.ContainsKey("msg-id") ?  userInfo.Info["msg-id"].Contains("highl") : false
                };

                Debug.WriteLine($"[*] System: {UsrMessage.IsMessageHighlighted}");

                foreach (var b in dictBadges)
                {
                    var chave = b.Key.Replace('-', '_');
                    if (b.Value && Enum.TryParse(chave.Substring(0, b.Key.IndexOf('/')), true, out Badges badge))
                        UsrMessage.User.Badges |= badge;
                }
                Debug.WriteLine(UsrMessage.User.Badges);
            }
        }

        /// <summary>
        /// Get user info from user received message.
        /// </summary>
        private UserInfo GetUserInfo(string message)
        {
            int userLength = Math.Abs(message.IndexOf('@') + 1 - (message.IndexOf(".tmi") + 1));
            string infoString = message.Substring(message.IndexOf('@'), userLength);
            var usrInfo = new UserInfo
            {
                Badges = new Dictionary<string, bool>(),
                Info = new Dictionary<string, string>()
            };

            foreach (var info in infoString.Split(';'))
            {
                string key = info.Substring(0, info.IndexOf('='));
                string value = info.Substring(info.IndexOf('=') + 1);

                if (key == "badges")
                {
                    if (!string.IsNullOrWhiteSpace(value))
                    {
                        value.Split(',')
                            .ToList()
                            .ForEach(val =>
                            {
                                if (AvailableBadges.Contains(val.Substring(0, val.IndexOf('/'))))
                                {
                                    usrInfo.Badges.Add(val, true);
                                }
                            });
                    }
                }
                else
                    usrInfo.Info.Add(key, value);
            }

            return usrInfo;
        }
    }
}
