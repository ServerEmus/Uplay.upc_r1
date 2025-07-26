using DllShared;
using System.IO;
using System.Text.Json;

namespace upc_r1;

public class UPC_Json
{
    private static readonly string path = Path.Combine(AOTHelper.CurrentPath, "upc.json");
    private static Root? instance;

    public static Root Instance
    {
        get
        {
            if (instance != null)
                return instance;
            if (!File.Exists(path))
            {
                instance = new();
                File.WriteAllText(path, JsonSerializer.Serialize(instance, JsonSourceGen.Default.Root));
                return instance;
            }
            instance = JsonSerializer.Deserialize(File.ReadAllText(path), JsonSourceGen.Default.Root);
            instance ??= new();
            return instance;
        }
    }

    public static void SaveToJson()
    {
        File.WriteAllText(path, JsonSerializer.Serialize(instance, JsonSourceGen.Default.Root));
    }

    public class Account
    {
        public string AccountId { get; set; } = Guid.NewGuid().ToString();
        public string Email { get; set; } = "user@uplayemu.com";
        public string Name { get; set; } = "user";
        public string Password { get; set; } = "user";
        public string Country { get; set; } = "en-US";
        public string Ticket { get; set; } = string.Empty;
    }

    public class Save
    {
        public string Path { get; set; } = string.Empty;
        public bool UseAppIdInName { get; set; }
    }

    public class Others
    {
        public string ApplicationId { get; set; } = string.Empty;
        public bool OfflineMode { get; set; } = true;
    }

    public class CDKey
    {
        public uint ProductId { get; set; }
        public string Key { get; set; } = string.Empty;
    }

    public class ChunkInfo
    {
        public List<uint> ChunkIds { get; set; } = [];
        public List<uint> InstalledChunkIds { get; set; } = [];
        public bool UseInstalledChunkIds { get; set; } = false;
    }

    public class Achi
    {
        public uint Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool Achieved { get; set; }
    }

    public class Reward
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public uint Cost { get; set; }
        public string GameCode { get; set; } = string.Empty;
        public string PlatformCode { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public bool Redeemed { get; set; }
    }

    public class Root
    {
        public bool UseDebug { get; set; } = false;
        public Account Account { get; set; } = new();
        public Save Save { get; set; } = new();
        public Others Others { get; set; } = new();
        public List<CDKey> CDKeys { get; set; } = [];
        public ChunkInfo ChunkInfo { get; set; } = new();
        public List<Achi> Achis { get; set; } = [];
        public List<Reward> Rewards { get; set; } = [];
        public string AvatarsPath { get; set; } = string.Empty;
    }

}
