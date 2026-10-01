namespace CalamityMod.Cooldowns;

public class Cooldown
{
	internal readonly ushort netID;

	public readonly string ID = "";

	internal Cooldown(string id, ushort nid)
	{
		ID = id;
		netID = nid;
	}
}
public class Cooldown<T> : Cooldown where T : CooldownHandler
{
	internal Cooldown(string id, ushort nid)
		: base(id, nid)
	{
	}
}
