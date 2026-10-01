using CalamityMod.Projectiles.Typeless;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

public class LuxorsGift : ModItem, ILocalizedModType, IModType
{
	public const int meleeAttackSpeed = 100;

	public const int rangedAttackSpeed = 25;

	public const int magicAttackSpeed = 75;

	public const int summonerAttackSpeed = 140;

	public const int rogueAttackSpeed = 36;

	public const int classlessAttackSpeed = 50;

	public const int meleeDamage = 10;

	public const int rangedDamage = 5;

	public const int magicDamage = 13;

	public const int summonerDamage = 22;

	public const int rogueDamage = 8;

	public const int classlessDamage = 9;

	public const int luxArmorPen = 35;

	public new string LocalizationCategory => "Items.Accessories";

	public override void SetDefaults()
	{
		base.Item.width = 66;
		base.Item.height = 46;
		base.Item.value = CalamityGlobalItem.RarityOrangeBuyPrice;
		base.Item.rare = 3;
		base.Item.accessory = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		player.Calamity().luxorsGift = true;
		if (player.ownedProjectileCounts[ModContent.ProjectileType<Luxor>()] < 1 && !player.dead)
		{
			Projectile.NewProjectileDirect(player.GetSource_FromThis(), player.Center, Vector2.Zero, ModContent.ProjectileType<Luxor>(), 0, 0f, player.whoAmI);
		}
	}

	public override void UpdateVanity(Player player)
	{
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		player.Calamity().luxorsGiftVanity = true;
		if (player.ownedProjectileCounts[ModContent.ProjectileType<Luxor>()] < 1 && !player.dead)
		{
			Projectile.NewProjectileDirect(player.GetSource_FromThis(), player.Center, Vector2.Zero, ModContent.ProjectileType<Luxor>(), 0, 0f, player.whoAmI);
		}
	}
}
