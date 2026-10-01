using System;
using Terraria;
using Terraria.Localization;

namespace CalamityMod.UI.CalamitasEnchants;

public struct Enchantment
{
	public LocalizedText Name;

	public LocalizedText Description;

	public string IconTexturePath;

	public Action<Item> CreationEffect;

	public Action<Player> HoldEffect;

	internal Predicate<Item> ApplyRequirement;

	public int ID { get; internal set; }

	public Enchantment(LocalizedText name, LocalizedText description, int id, string iconTexturePath, Action<Item> creationEffect, Action<Player> holdEffect, Predicate<Item> requirement)
	{
		Name = name;
		Description = description;
		CreationEffect = creationEffect;
		HoldEffect = holdEffect;
		ApplyRequirement = requirement;
		ID = id;
		IconTexturePath = iconTexturePath;
	}

	public Enchantment(LocalizedText name, LocalizedText description, int id, string iconTexturePath, Action<Player> holdEffect, Predicate<Item> requirement)
	{
		Name = name;
		Description = description;
		CreationEffect = null;
		HoldEffect = holdEffect;
		ApplyRequirement = requirement;
		ID = id;
		IconTexturePath = iconTexturePath;
	}

	public Enchantment(LocalizedText name, LocalizedText description, int id, string iconTexturePath, Action<Item> creationEffect, Predicate<Item> requirement)
	{
		Name = name;
		Description = description;
		CreationEffect = creationEffect;
		HoldEffect = null;
		ApplyRequirement = requirement;
		ID = id;
		IconTexturePath = iconTexturePath;
	}

	public bool CanBeAppliedTo(Item item)
	{
		if (item == null || item.IsAir)
		{
			return false;
		}
		if (ApplyRequirement == null)
		{
			return true;
		}
		return ApplyRequirement(item);
	}
}
