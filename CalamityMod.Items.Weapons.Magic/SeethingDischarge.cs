using System;
using CalamityMod.NPCs.BrimstoneElemental;
using CalamityMod.Projectiles.Magic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Magic;

public class SeethingDischarge : ModItem, ILocalizedModType, IModType
{
	public int DartTimer;

	public new string LocalizationCategory => "Items.Weapons.Magic";

	public override void SetDefaults()
	{
		base.Item.width = 28;
		base.Item.height = 32;
		base.Item.damage = 40;
		base.Item.DamageType = DamageClass.Magic;
		base.Item.mana = 20;
		base.Item.useTime = 60;
		base.Item.useAnimation = 60;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 6.75f;
		base.Item.UseSound = BrimstoneElemental.ShellFireSound;
		base.Item.value = CalamityGlobalItem.RarityPinkBuyPrice;
		base.Item.rare = 5;
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<SeethingDischargeBrimstoneBarrage>();
		base.Item.shootSpeed = 10f;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		DartTimer++;
		float diameter = 50f;
		Vector2 projVelocity = velocity.SafeNormalize(Vector2.UnitY);
		projVelocity *= diameter;
		int totalProjectiles = 5;
		float offsetAngle = (float)Math.PI / 5f;
		for (int j = 0; j < totalProjectiles; j++)
		{
			float radians = (float)j - ((float)totalProjectiles - 1f) / 2f;
			Vector2 offset = projVelocity.RotatedBy(offsetAngle * radians);
			Projectile.NewProjectile(source, player.Center + offset, velocity, ModContent.ProjectileType<SeethingDischargeBrimstoneHellblast>(), damage, knockback, player.whoAmI);
		}
		if (DartTimer == 3)
		{
			DartTimer = 0;
			totalProjectiles = 8;
			Vector2 initialOffset = Vector2.UnitX.RotatedBy(Main.rand.NextFloat(0f, (float)Math.PI / 4f));
			for (int k = 0; k < totalProjectiles; k++)
			{
				velocity = initialOffset.RotatedBy((float)Math.PI * 2f * ((float)k / (float)totalProjectiles)) * base.Item.shootSpeed;
				Projectile.NewProjectile(source, player.Center, velocity, type, damage, 0f, player.whoAmI, 0f, 1f);
			}
		}
		return false;
	}
}
