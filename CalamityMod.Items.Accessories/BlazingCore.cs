using System;
using System.Collections.Generic;
using CalamityMod.Projectiles.Typeless;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

public class BlazingCore : ModItem, ILocalizedModType, IModType
{
	private static float offset = 0f;

	public static readonly SoundStyle ParryActivateSound = new SoundStyle("CalamityMod/Sounds/Item/BlazingCoreParryActivate")
	{
		Volume = 0.7f
	};

	public static readonly SoundStyle ParrySuccessSound = new SoundStyle("CalamityMod/Sounds/Item/BlazingCoreParry")
	{
		Volume = 0.6f
	};

	public new string LocalizationCategory => "Items.Accessories";

	public override void SetStaticDefaults()
	{
		Main.RegisterItemAnimation(base.Item.type, new DrawAnimationVertical(5, 6));
		ItemID.Sets.AnimatesAsSoul[base.Type] = true;
	}

	public override void ModifyTooltips(List<TooltipLine> list)
	{
		list.IntegrateHotkey(CalamityKeybinds.AccessoryParryHotKey);
	}

	public override void SetDefaults()
	{
		base.Item.width = 42;
		base.Item.height = 46;
		base.Item.accessory = true;
		base.Item.value = CalamityGlobalItem.RarityTurquoiseBuyPrice;
		base.Item.rare = ModContent.RarityType<Turquoise>();
		base.Item.expert = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.Calamity().blazingCore = true;
		player.noKnockback = true;
	}

	public static void HandleStars(Player player)
	{
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		bool empowered = player.Calamity().blazingCoreEmpoweredParry;
		float divisor = 3f;
		int chains = 3;
		float interval = (float)(45 / chains) * divisor;
		double num = Math.Floor((float)player.Calamity().blazingCoreSuccessfulParry / interval);
		if (player.Calamity().blazingCoreSuccessfulParry % 4 == 0)
		{
			SoundEngine.PlaySound(in SoundID.DD2_BetsyFireballImpact, player.Center);
		}
		if (num % 2.0 == 0.0)
		{
			double radians = (float)Math.PI * 2f / (float)chains;
			double angleA = radians * 0.5;
			double angleB = (double)MathHelper.ToRadians(90f) - angleA;
			float velocityX = (float)(2.0 * Math.Sin(angleA) / Math.Sin(angleB));
			Vector2 spinningPoint = default(Vector2);
			((Vector2)(ref spinningPoint))._002Ector(velocityX, -2f);
			for (int i = 0; i < chains; i++)
			{
				int projectileType = ModContent.ProjectileType<BlazingStarThatDoesNotHeal>();
				int dmgAmt = 250;
				if (!empowered && Main.rand.NextBool(5))
				{
					projectileType = ModContent.ProjectileType<BlazingStarHeal>();
				}
				Vector2 vector2 = spinningPoint.RotatedBy(radians * (double)i + (double)MathHelper.ToRadians(offset));
				if (!Main.dedServ)
				{
					spinningPoint *= 1.5f;
					dmgAmt = (int)player.GetBestClassDamage().ApplyTo(dmgAmt);
					Projectile.NewProjectile(player.GetSource_FromThis(), player.Center, vector2, projectileType, dmgAmt, 0f, Main.myPlayer);
				}
			}
			offset += 10f;
		}
		player.Calamity().blazingCoreSuccessfulParry--;
		if (player.Calamity().blazingCoreSuccessfulParry <= 0)
		{
			offset = 0f;
			player.Calamity().blazingCoreEmpoweredParry = false;
		}
	}

	public static void HandleParryCountdown(Player player)
	{
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		player.Calamity().blazingCoreParry--;
		if (player.Calamity().blazingCoreParry > 0)
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
				int theDust = Dust.NewDust(player.position, player.width, player.height, 205, 0f, 0f, 100, new Color(255, 255, 255), 2f);
				Main.dust[theDust].noGravity = true;
			}
		}
	}
}
