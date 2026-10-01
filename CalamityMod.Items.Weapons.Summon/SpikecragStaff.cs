using System.Collections.Generic;
using CalamityMod.Projectiles.Summon;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Summon;

public class SpikecragStaff : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Summon";

	public override void SetDefaults()
	{
		base.Item.width = 50;
		base.Item.height = 50;
		base.Item.damage = 56;
		base.Item.mana = 10;
		base.Item.DamageType = DamageClass.Summon;
		base.Item.sentry = true;
		base.Item.useAnimation = (base.Item.useTime = 30);
		base.Item.useStyle = 1;
		base.Item.noMelee = true;
		base.Item.knockBack = 2f;
		base.Item.value = CalamityGlobalItem.RarityYellowBuyPrice;
		base.Item.rare = 8;
		base.Item.autoReuse = true;
		base.Item.shootSpeed = 20f;
		base.Item.UseSound = SoundID.Item78;
		base.Item.shoot = ModContent.ProjectileType<Spikecrag>();
	}

	public override void ModifyTooltips(List<TooltipLine> list)
	{
		list.FindAndReplace("[GFB]", this.GetLocalizedValue(Main.zenithWorld ? "TooltipGFB" : "TooltipNormal"));
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		player.FindSentryRestingSpot(type, out var XPosition, out var YPosition, out var YOffset);
		YOffset -= 6;
		((Vector2)(ref position))._002Ector((float)XPosition, (float)(YPosition - YOffset));
		int p = Projectile.NewProjectile(source, position, Vector2.Zero, type, damage, knockback, player.whoAmI, 120f);
		if (Main.projectile.IndexInRange(p))
		{
			Main.projectile[p].originalDamage = base.Item.damage;
		}
		player.UpdateMaxTurrets();
		return false;
	}

	public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
	{
		float buffAMT = ((!DownedBossSystem.downedProvidence || !Main.zenithWorld) ? 1 : 3);
		damage *= buffAMT;
	}
}
