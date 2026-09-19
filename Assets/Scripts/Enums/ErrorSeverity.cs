/// <summary>
/// None represents a regular debug.log,
/// Warning represents a debug.logWarning
/// Error represents a debug.logError
/// FatalError throws an exception preventing the game from building
/// </summary>
public enum ErrorSeverity
{
    None,
    Warning,
    Error,
    ScenePivotal,
    FatalError
}