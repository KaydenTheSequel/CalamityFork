using System;
using System.IO;
using CalamityMod.Buffs.Summon;
using CalamityMod.CalPlayer;
using CalamityMod.Utilities.Daybreak;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class SunSpiritMinion : ModProjectile, ILocalizedModType, IModType
{
	public static Asset<Texture2D> circle;

	public new string LocalizationCategory => "Projectiles.Summon";

	public int MinionSlotsToAdd
	{
		get
		{
			return (int)base.Projectile.ai[0];
		}
		set
		{
			base.Projectile.ai[0] = value;
		}
	}

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.MinionSacrificable[base.Type] = true;
		ProjectileID.Sets.MinionTargettingFeature[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 50;
		base.Projectile.height = 50;
		base.Projectile.netImportant = true;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.minionSlots = 1f;
		base.Projectile.timeLeft = 18000;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft *= 5;
		base.Projectile.minion = true;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override void AI()
	{
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0228: Unknown result type (might be due to invalid IL or missing references)
		//IL_0234: Unknown result type (might be due to invalid IL or missing references)
		//IL_023e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0243: Unknown result type (might be due to invalid IL or missing references)
		//IL_0257: Unknown result type (might be due to invalid IL or missing references)
		//IL_025c: Unknown result type (might be due to invalid IL or missing references)
		//IL_025e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0263: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		CalamityPlayer modPlayer = player.Calamity();
		player.AddBuff(ModContent.BuffType<SolarSpirit>(), 3600);
		if (MinionSlotsToAdd > 0)
		{
			float minionSlotsAvaliable = player.maxMinions;
			ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
			while (enumerator.MoveNext())
			{
				Projectile item = enumerator.Current;
				if (item.owner == base.Projectile.owner)
				{
					minionSlotsAvaliable -= item.minionSlots;
				}
			}
			while (minionSlotsAvaliable >= 1f && MinionSlotsToAdd > 0)
			{
				base.Projectile.minionSlots++;
				minionSlotsAvaliable--;
				MinionSlotsToAdd--;
				base.Projectile.netUpdate = true;
			}
			MinionSlotsToAdd = 0;
		}
		if (base.Projectile.type == ModContent.ProjectileType<SunSpiritMinion>())
		{
			if (player.dead)
			{
				modPlayer.sunSpirit = false;
			}
			if (modPlayer.sunSpirit)
			{
				base.Projectile.timeLeft = 2;
			}
		}
		base.Projectile.Center = player.Center + Vector2.UnitY * (player.gfxOffY + player.gravDir * -80f);
		Lighting.AddLight(base.Projectile.Center, (float)(255 - base.Projectile.alpha) * 0.25f / 255f, (float)(255 - base.Projectile.alpha) * 0.25f / 255f, (float)(255 - base.Projectile.alpha) * 0f / 255f);
		NPC target = null;
		int targetID = -1;
		base.Projectile.Minion_FindTargetInRange(800, ref targetID, skipIfCannotHitWithOwnBody: false);
		if (targetID < 0)
		{
			return;
		}
		target = Main.npc[targetID];
		if (base.Projectile.owner == Main.myPlayer)
		{
			if (base.Projectile.ai[1] > 0f)
			{
				base.Projectile.ai[1]--;
				return;
			}
			float shootSpeed = 15f;
			_ = base.Projectile.Center;
			Vector2 velocity = CalamityUtils.CalculatePredictiveAimToTargetMaxUpdates(base.Projectile.Center, target, shootSpeed, 2);
			Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), base.Projectile.Center - velocity, velocity, ModContent.ProjectileType<SunSpiritBeam>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner).DamageType = DamageClass.Summon;
			base.Projectile.ai[1] += 50f / base.Projectile.minionSlots;
		}
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(base.Projectile.minionSlots);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		base.Projectile.minionSlots = reader.ReadSingle();
	}

	public override bool? CanDamage()
	{
		return false;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		Texture2D spTex = TextureAssets.Projectile[base.Type].Value;
		_ = CalamityUtils.GetTextureEfficient(ref circle, "CalamityMod/ExtraTextures/GreyscaleOpenCircleButBigger").Value;
		Main.spriteBatch.Draw(spTex, base.Projectile.Center - Main.screenPosition, (Rectangle?)null, Color.White, Main.GlobalTimeWrappedHourly, spTex.Size() * 0.5f, 0.75f, (SpriteEffects)0, 0f);
		using (Main.spriteBatch.Scope())
		{
			Main.spriteBatch.Begin((SpriteSortMode)0, BlendState.Additive, SamplerState.PointClamp, DepthStencilState.None, Main.Rasterizer, (Effect)null, Main.GameViewMatrix.TransformationMatrix);
			float count = MathHelper.Min(base.Projectile.minionSlots * 2f, 40f);
			for (int i = 0; (float)i < count; i++)
			{
				float comp = (float)i / count;
				float offset = (float)(Main.mouseTextColor - 190) / 64f * 8f;
				if (i % 2 == 0)
				{
					offset = 8f - offset;
				}
				Main.spriteBatch.DrawLineBetter(base.Projectile.Center + Utils.RotatedBy(new Vector2(20f + offset, 0f), (double)((float)Math.PI * 2f * comp - Main.GlobalTimeWrappedHourly), default(Vector2)), base.Projectile.Center + Utils.RotatedBy(new Vector2(34f + offset, 0f), (double)((float)Math.PI * 2f * comp - Main.GlobalTimeWrappedHourly), default(Vector2)), (i % 2 == 3) ? Color.OrangeRed : Color.Gold, 2f);
			}
			Main.spriteBatch.End();
		}
		return false;
	}
}
