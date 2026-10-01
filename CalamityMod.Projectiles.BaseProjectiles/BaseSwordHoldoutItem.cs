using Terraria;
using Terraria.GameContent.Prefixes;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.BaseProjectiles;

public abstract class BaseSwordHoldoutItem : ModItem
{
	public virtual int ProjectileType { get; set; }

	public virtual bool SizeModifiers { get; set; } = true;

	public virtual bool RClickAutoswing { get; set; }

	public override void SetStaticDefaults()
	{
		if (RClickAutoswing)
		{
			ItemID.Sets.ItemsThatAllowRepeatedRightClick[base.Type] = true;
		}
	}

	public override bool MeleePrefix()
	{
		return SizeModifiers;
	}

	public override void SetDefaults()
	{
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.shoot = ProjectileType;
		base.Item.autoReuse = true;
		base.Item.useTurn = false;
		base.Item.useStyle = 5;
		if (SizeModifiers)
		{
			PrefixLegacy.ItemSets.SwordsHammersAxesPicks[base.Item.type] = true;
		}
	}

	public override bool CanUseItem(Player player)
	{
		if (player.itemTime > 0)
		{
			return false;
		}
		for (int i = 0; i < 1000; i++)
		{
			Projectile proj = Main.projectile[i];
			if (proj.type == ProjectileType && proj.owner == player.whoAmI && proj.active)
			{
				return false;
			}
		}
		return base.CanUseItem(player);
	}
}
