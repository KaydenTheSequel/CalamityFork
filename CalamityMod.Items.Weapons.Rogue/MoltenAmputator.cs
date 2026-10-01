using System;
using CalamityMod.Dusts;
using CalamityMod.Projectiles.Rogue;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Rogue;

public class MoltenAmputator : RogueWeapon
{
	public float speed = 16f;

	public override void SetDefaults()
	{
		base.Item.width = 92;
		base.Item.height = 80;
		base.Item.damage = 200;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.autoReuse = true;
		base.Item.useStyle = 1;
		base.Item.useTime = (base.Item.useAnimation = 25);
		base.Item.knockBack = 2f;
		base.Item.value = CalamityGlobalItem.RarityTurquoiseBuyPrice;
		base.Item.rare = ModContent.RarityType<Turquoise>();
		base.Item.shoot = ModContent.ProjectileType<MoltenAmputatorProj>();
		base.Item.shootSpeed = speed;
		base.Item.DamageType = RogueDamageClass.Instance;
	}

	public override void HoldItem(Player player)
	{
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		player.Calamity().mouseWorldListener = true;
		if (!player.Calamity().mouseRight)
		{
			return;
		}
		if (player.Calamity().StealthStrikeAvailable())
		{
			player.Calamity().focusFlurryAttackCount = 30;
			player.Calamity().ConsumeStealthByAttacking();
			SoundStyle style = new SoundStyle("CalamityMod/Sounds/Custom/ProfanedGuardians/GuardianRay");
			style.Volume = 1f;
			style.Pitch = Main.rand.NextFloat(0.2f, 0.3f);
			SoundEngine.PlaySound(in style, player.Center);
			for (int i = 0; i < 20; i++)
			{
				Dust dust = Dust.NewDustPerfect(player.Center, ModContent.DustType<LightDust>());
				dust.velocity = ((float)Math.PI * 2f * (float)i / 20f).ToRotationVector2() * 15.5f * ((i % 2 == 0) ? 0.88f : 1f);
				dust.scale = Main.rand.NextFloat(1.3f, 1.6f) * 0.8f * ((i % 2 == 0) ? 2.2f : 1.8f);
				dust.noGravity = true;
				dust.color = Color.Goldenrod;
				dust.noLightEmittence = true;
			}
		}
		for (int x = 0; x < Main.maxProjectiles; x++)
		{
			Projectile projectile = Main.projectile[x];
			if (projectile.active && projectile.type == ModContent.ProjectileType<MoltenAmputatorProj>() && projectile.ai[2] < 5f && projectile.timeLeft < 840)
			{
				projectile.ai[2] = 5f;
				SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/SwingMid");
				style.Volume = 0.4f;
				style.Pitch = Main.rand.NextFloat(0.4f, 0.5f);
				SoundEngine.PlaySound(in style, player.Center);
			}
		}
	}

	public override float UseSpeedMultiplier(Player player)
	{
		if (player.Calamity().focusFlurryAttackCount <= 0)
		{
			return 1f;
		}
		return 3f;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		bool fastToss = player.Calamity().focusFlurryAttackCount > 0;
		SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/SpearofDestiny");
		style.Volume = 0.5f;
		style.Pitch = ((player.Calamity().focusFlurryAttackCount > 0) ? (-0.4f + (float)player.Calamity().focusFlurryAttackCount * 0.02f) : Main.rand.NextFloat(-0.4f, -0.65f));
		SoundEngine.PlaySound(in style, position);
		Vector2 staticSpeed = position.DirectionTo(position + velocity) * position.Distance(player.ClampedMouseWorld()) * 0.022f;
		int fastTossDamage = (int)((float)damage * 0.65f);
		Projectile scythe = Projectile.NewProjectileDirect(source, position, staticSpeed.RotatedByRandom((player.Calamity().focusFlurryAttackCount > 0) ? 0.7f : 0f), type, fastToss ? fastTossDamage : damage, knockback, player.whoAmI);
		if (fastToss)
		{
			scythe.extraUpdates = 6;
			scythe.Calamity().stealthStrike = true;
			player.Calamity().focusFlurryAttackCount--;
		}
		player.Calamity().ConsumeStealthByAttacking();
		return false;
	}
}
