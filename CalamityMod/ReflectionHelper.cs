using System;
using System.Collections.Generic;
using System.Linq;
using Terraria.ModLoader;
using Terraria.ModLoader.Core;

namespace CalamityMod;

[Obsolete("This is kept for GeneralParticleHandler. Consider use ILoadable or ModType If possible.")]
public static class ReflectionHelper
{
	public static IEnumerable<Type> GetEveryModsTypes()
	{
		return ModLoader.Mods.SelectMany((Mod mod) => AssemblyManager.GetLoadableTypes(mod.Code));
	}

	public static bool IsSubclass(Type baseType, Type type, bool includeBaseType)
	{
		if (type.IsSubclassOf(baseType) && !type.IsAbstract)
		{
			if (!includeBaseType)
			{
				return type != baseType;
			}
			return false;
		}
		return false;
	}

	public static void IterateEveryModsTypes<T>(Action<Type> action, bool includeBaseType = false)
	{
		if (action == null)
		{
			return;
		}
		Type baseType = typeof(T);
		foreach (Type type in from t in GetEveryModsTypes()
			where IsSubclass(baseType, t, includeBaseType)
			select t)
		{
			action(type);
		}
	}
}
