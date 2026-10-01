using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Terraria.ModLoader;

namespace CalamityMod.Cooldowns;

public sealed class CooldownRegistry : ModSystem
{
	public static Cooldown[] registry = new Cooldown[256];

	private const ushort defaultSize = 256;

	private static ushort nextCDNetID = 0;

	private static Dictionary<string, ushort> nameToNetID = new Dictionary<string, ushort>(256);

	public override void ResizeArrays()
	{
		IEnumerable<CooldownHandler> content = ModContent.GetContent<CooldownHandler>();
		int count = content.Count();
		Array.Resize(ref registry, count);
		MethodInfo registerBaseMethod = typeof(CooldownRegistry).GetMethod("Register", BindingFlags.Static | BindingFlags.Public);
		foreach (CooldownHandler cooldown in content)
		{
			Type type = cooldown.GetType();
			string handlerID = (string)type.GetProperty("ID").GetValue(null);
			if (handlerID == null)
			{
				handlerID = cooldown.FullName;
			}
			registerBaseMethod.MakeGenericMethod(type).Invoke(null, new object[1] { handlerID });
		}
	}

	public override void Unload()
	{
		registry = null;
		nameToNetID?.Clear();
		nameToNetID = null;
	}

	public static Cooldown Get(string id)
	{
		if (!nameToNetID.TryGetValue(id, out var netID))
		{
			return null;
		}
		return registry[netID];
	}

	public static Cooldown<HandlerT> Register<HandlerT>(string id) where HandlerT : CooldownHandler
	{
		int currentMaxID = registry.Length;
		if (nextCDNetID == currentMaxID)
		{
			return null;
		}
		Cooldown<HandlerT> cd = new Cooldown<HandlerT>(id, nextCDNetID);
		nameToNetID[cd.ID] = cd.netID;
		registry[cd.netID] = cd;
		nextCDNetID++;
		if (nextCDNetID == currentMaxID && currentMaxID < 65535)
		{
			Cooldown[] largerArray = new Cooldown[currentMaxID * 2];
			for (int i = 0; i < currentMaxID; i++)
			{
				largerArray[i] = registry[i];
			}
			registry = largerArray;
		}
		return cd;
	}
}
