using CalamityMod.Projectiles.Typeless;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

public class YharimsGift : ModItem, ILocalizedModType, IModType
{
	public int dragonTimer = 60;

	public new string LocalizationCategory => "Items.Accessories";

	public override void SetDefaults()
	{
		base.Item.width = 20;
		base.Item.height = 22;
		base.Item.defense = 12;
		base.Item.accessory = true;
		base.Item.value = CalamityGlobalItem.RarityVioletBuyPrice;
		base.Item.rare = ModContent.RarityType<BurnishedAuric>();
		base.Item.expert = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		IEntitySource source = player.GetSource_Accessory(base.Item);
		player.moveSpeed += 0.15f;
		player.GetDamage<GenericDamageClass>() += 0.15f;
		player.noKnockback = true;
		if (!player.StandingStill())
		{
			dragonTimer--;
			if (dragonTimer <= 0)
			{
				if (player.whoAmI == Main.myPlayer)
				{
					int damage = (int)player.GetBestClassDamage().ApplyTo(175f);
					int projectile1 = Projectile.NewProjectile(source, player.Center, Vector2.Zero, ModContent.ProjectileType<DragonDust>(), damage, 5f, player.whoAmI);
					Main.projectile[projectile1].timeLeft = 60;
				}
				dragonTimer = 60;
			}
		}
		else
		{
			dragonTimer = 60;
		}
		if (player.immune && player.miscCounter % 8 == 0 && player.whoAmI == Main.myPlayer)
		{
			int damage2 = (int)player.GetBestClassDamage().ApplyTo(375f);
			CalamityUtils.ProjectileRain(source, player.Center, 400f, 100f, 500f, 800f, 22f, ModContent.ProjectileType<SkyFlareFriendly>(), damage2, 9f, player.whoAmI);
		}
	}
}
