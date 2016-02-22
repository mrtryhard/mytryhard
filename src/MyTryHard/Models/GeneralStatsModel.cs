using MyTryHard.FiltersAttributes;

namespace MyTryHard.Models
{
    [AllowSqlAutoFill]
    public class GeneralStatsModel
    {
        public string LastArticleTitle { get; set; }
        public string PopularArticleTitle { get; set; }
        public int PublishedArticleCount { get; set; }
        public int DraftArticleCount { get; set; }
        public int LockedArticleCount { get; set; }
        public string LastUserNameRegistered { get; set; }
        public int RegisteredUserCount { get; set; }
        public int BannedUserCount { get; set; }
        public string LastCommentContent { get; set; }
        public string TopCommentingUserName { get; set; }
        public int ApprovedCommentCount { get; set; }

        public int PendingCommentCount { get; set; }

        public string PopularPlanetName { get; set; }

        public string TopClickerUserName { get; set; }

        public int TotalPlanetClickCount { get; set; }
    }
}
