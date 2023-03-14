
public static class GameData
{
    //Scene statistics
    public static string CurrentScene { get; set; }

    //Player Status
    public static bool Completionist { get; set; }
    public static int Health { get; set; }
    public static bool IsOverCharged { get; set; }
    public static bool IsInvincible { get; set; }
    public static int TotalScrap { get; set; }
    public static int ScrapCollected { get; set; }
    public static int TotalDeaths { get; set; }


    //Main Components
    public static bool HasGrappler { get; set; }
    public static bool HasMover { get; set; }
    public static bool HasRocket { get; set; }
    public static bool HasBattery { get; set; }
    public static bool HasRazor { get; set; }

    //Sub-Components
    public static bool HasArmor { get; set; }

    //Zone Times
    public static float TotalTime { get; set; }
    public static float FastestTime { get; set; }



}
