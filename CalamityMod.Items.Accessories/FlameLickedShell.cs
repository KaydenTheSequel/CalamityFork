using System;
using System.Collections.Generic;
using CalamityMod.CalPlayer;
using CalamityMod.Projectiles.Typeless;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

[LegacyName(new string[] { "FabledTortoiseShell" })]
public class FlameLickedShell : ModItem, ILocalizedModType, IModType
{
	internal const int flameLickedParry = 30;

	public new string LocalizationCategory => "Items.Accessories";

	public override void ModifyTooltips(List<TooltipLine> list)
	{
		list.IntegrateHotkey(CalamityKeybinds.AccessoryParryHotKey);
	}

	public override void SetDefaults()
	{
		base.Item.width = 36;
		base.Item.height = 42;
		base.Item.value = CalamityGlobalItem.RarityPinkBuyPrice;
		base.Item.rare = 5;
		base.Item.accessory = true;
		base.Item.expert = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.Calamity().flameLickedShell = true;
		player.noKnockback = true;
	}

	public static void handleParry(Player player)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_020b: Unknown result type (might be due to invalid IL or missing references)
		CalamityPlayer calPlayer = player.Calamity();
		bool empowered = calPlayer.flameLickedShellEmpoweredParry;
		Vector2 val = player.Center.ClosestNPCAt(1300f, ignoreTiles: true, bossPriority: true)?.Center ?? Main.MouseWorld;
		float projectileSpeed = 8f;
		float radialOffset = 0.2f;
		float diameter = 80f;
		Vector2 projectileVelocity = val - player.Center;
		projectileVelocity = Vector2.Normalize(projectileVelocity) * projectileSpeed;
		Vector2 velocity = projectileVelocity;
		((Vector2)(ref velocity)).Normalize();
		velocity *= diameter;
		int totalProjectiles = 6;
		float offsetAngle = (float)Math.PI * radialOffset;
		int type = ModContent.ProjectileType<FlameLickedHellblast>();
		int damage = (int)player.GetBestClassDamage().ApplyTo(200f);
		if (player.whoAmI == Main.myPlayer)
		{
			if (empowered)
			{
				for (int j = 0; j < totalProjectiles; j++)
				{
					float radians = (float)j - ((float)totalProjectiles - 1f) / 2f;
					Vector2 offset = velocity.RotatedBy(offsetAngle * radians);
					Projectile.NewProjectile(player.GetSource_FromThis(), player.Center + offset, projectileVelocity * 1.5f, type, damage, 2f, Main.myPlayer);
				}
			}
			totalProjectiles = 12;
			float radians2 = (float)Math.PI * 2f / (float)totalProjectiles;
			type = ModContent.ProjectileType<FlameLickedBarrage>();
			damage = (int)player.GetBestClassDamage().ApplyTo(70f);
			double angleA = (double)radians2 * 0.5;
			double angleB = (double)MathHelper.ToRadians(90f) - angleA;
			float velocityX = (float)((double)projectileSpeed * Math.Sin(angleA) / Math.Sin(angleB));
			Vector2 spinningPoint = ((!empowered) ? new Vector2(0f, 0f - projectileSpeed) : new Vector2(0f - velocityX, 0f - projectileSpeed));
			for (int k = 0; k < totalProjectiles; k++)
			{
				Vector2 vector255 = spinningPoint.RotatedBy(radians2 * (float)k);
				int proj = Projectile.NewProjectile(player.GetSource_FromAI(), player.Center + Vector2.Normalize(vector255) * 5f, vector255 * (empowered ? 0.99f : 1.15f), type, damage, 1f, Main.myPlayer, empowered ? 0f : 1f);
				if (empowered && proj.WithinBounds(Main.maxProjectiles))
				{
					Main.projectile[proj].extraUpdates++;
				}
			}
		}
		calPlayer.flameLickedShellEmpoweredParry = false;
	}

	public static void HandleParryCountdown(Player player)
	{
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		player.Calamity().flameLickedShellParry--;
		if (player.Calamity().flameLickedShellParry > 0)
		{
			player.controlJump = false;
			player.controlDown = false;
			player.controlLeft = false;
			player.controlRight = false;
			player.controlUp = false;
			player.controlUseItem = false;
			player.controlUseTile = false;
			player.controlThrow = false;
			player.gravDir = 1f;
			player.velocity = Vector2.Zero;
			player.velocity.Y = -0.1f;
			player.RemoveAllGrapplingHooks();
		}
		else
		{
			for (int i = 0; i < 8; i++)
			{
				int theDust = Dust.NewDust(player.position, player.width, player.height, 235, 0f, 0f, 100, new Color(255, 255, 255), 2f);
				Main.dust[theDust].noGravity = true;
			}
		}
	}
}
