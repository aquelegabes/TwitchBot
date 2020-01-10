using static TwitchBot.Services.Models.TwitchBadges;

namespace TwitchBot.Services.Models
{
    /// <summary>
    /// Model class to check a user-state.
    /// </summary>
    public class UserModel
    {
        public string DisplayName { get; set; }
        public string UserColor { get; set; } = string.Empty;
        public Badges Badges { get; set; } = Badges.General;

        /// <summary>
        /// Automatic conversion of <see cref="UserModel"/> into a <see cref="string"/>.
        /// </summary>
        /// <param name="user">User model.</param>
        public static implicit operator string (UserModel user)
        {
            return user?.DisplayName ?? "";
        }

        /// <summary>
        /// Returns a <see cref="string"/> that represents the current object.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this?.DisplayName ?? "";
        }

        // Can't obtain through twitch IRC

        // public bool IsFollower { get; set; } = false;
        // public bool IsPrime { get; set; } = false;
    }
}
