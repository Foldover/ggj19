/// <summary>
/// Contains the strings to the relevant events in Fmod. Is used to avoid typo-errors.
/// </summary>
public class _Fmod
{
	public const string testEvent = "event:/TEST";

	public struct Events
	{
		public struct Bunny
		{
			public const string death = testEvent;
			public const string cheer = testEvent;
			public const string speak = testEvent;
		}

		public struct Misc
		{
			public const string ambController = "event:/ambienceController";
			public const string throwWhoosh = "event:/Misc/throwWhoosh";
			public const string onLevelEnd = "event:/Misc/onLevelEnd";
			public const string onLevelStart = "event:/Misc/onLevelStart";
			public const string onLevelRestart = "event:/Misc/onLevelRestart";
			public const string hookPickup = "event:/hookPickup";
			public const string hookDrop = "event:/hookDrop";
		}

		public struct UI
		{
			public const string accept = "event:/UI/accept";
			public const string back = "event:/UI/back";
			public const string error = "event:/UI/error";
			public const string move = "event:/UI/move";
		}

		public struct Music
		{
			public const string musicController = "event:/musicController";
		}
	}

	public struct Snapshots
	{
		public const string onPause = "snapshot:/onPause";
	}

	public struct Params
	{
		public const string velocity = "velocity";
		public const string material = "material";
		public const string song = "song";
		public const string variation = "variation";
	}
}