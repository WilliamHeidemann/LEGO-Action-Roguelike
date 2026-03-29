public enum PauseMode
{
    Playing,
    Paused
}

public static class PauseManager
{
    public static PauseMode PauseMode { get; set; } = PauseMode.Playing; 
}