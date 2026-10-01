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

public class VengefulSunSpiritMinion : ModProjectile, ILocalizedModType, IModType
{
	private static Texture2D AllWhiteVersion;

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
		player.AddBuff(ModContent.BuffType<SolarGodSpiritBuff>(), 3600);
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
		if (base.Projectile.type == ModContent.ProjectileType<VengefulSunSpiritMinion>())
		{
			if (player.dead)
			{
				modPlayer.vengefulSunMinion = false;
			}
			if (modPlayer.vengefulSunMinion)
			{
				base.Projectile.timeLeft = 2;
			}
		}
		base.Projectile.Center = player.Center + Vector2.UnitY * (player.gfxOffY + player.gravDir * -80f);
		Lighting.AddLight(base.Projectile.Center, (float)(255 - base.Projectile.alpha) * 0.25f / 255f, (float)(255 - base.Projectile.alpha) * 0.25f / 255f, (float)(255 - base.Projectile.alpha) * 0f / 255f);
		NPC target = null;
		int targetID = -1;
		base.Projectile.Minion_FindTargetInRange(1600, ref targetID, skipIfCannotHitWithOwnBody: false);
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
			Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), base.Projectile.Center - velocity, velocity, ModContent.ProjectileType<VengefulSunBeam>(), (int)((float)base.Projectile.damage * (0.75f + base.Projectile.minionSlots * 0.25f)), base.Projectile.knockBack, base.Projectile.owner, 0f, (base.Projectile.minionSlots - 1f) / 6f).DamageType = DamageClass.Summon;
			base.Projectile.ai[1] += 60f / (0.75f + base.Projectile.minionSlots * 0.25f);
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

	public static Texture2D GetWhiteTex()
	{
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Expected O, but got Unknown
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		if (AllWhiteVersion == null)
		{
			Texture2D texture = TextureAssets.Projectile[ModContent.ProjectileType<VengefulSunSpiritMinion>()].Value;
			AllWhiteVersion = new Texture2D(Main.graphics.GraphicsDevice, texture.Width, texture.Height);
			Color[] BaseArray = (Color[])(object)new Color[AllWhiteVersion.Width * AllWhiteVersion.Height];
			Color[] ColorArray = (Color[])(object)new Color[AllWhiteVersion.Width * AllWhiteVersion.Height];
			texture.GetData<Color>(BaseArray);
			for (int i = 0; i < BaseArray.Length; i++)
			{
				ColorArray[i] = new Color(255, 255, 255) * ((float)(int)((Color)(ref BaseArray[i])).A / 255f);
			}
			AllWhiteVersion.SetData<Color>(ColorArray);
		}
		return AllWhiteVersion;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_025e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0270: Unknown result type (might be due to invalid IL or missing references)
		//IL_0286: Unknown result type (might be due to invalid IL or missing references)
		//IL_028c: Unknown result type (might be due to invalid IL or missing references)
		//IL_028e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0293: Unknown result type (might be due to invalid IL or missing references)
		//IL_029e: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d8: Unknown result type (might be due to invalid IL or missing references)
		Texture2D spTex = TextureAssets.Projectile[base.Type].Value;
		Texture2D whiteTex = GetWhiteTex();
		_ = CalamityUtils.GetTextureEfficient(ref circle, "CalamityMod/ExtraTextures/GreyscaleOpenCircleButBigger").Value;
		float completion = (base.Projectile.minionSlots - 1f) / 6f;
		Color color = Color.Lerp(Color.Yellow, Color.DarkOrange, completion);
		if (completion >= 1f)
		{
			color = Color.LightBlue;
		}
		for (float i = 0f; i < (float)Math.PI * 2f; i += (float)Math.PI / 2f)
		{
			Main.spriteBatch.Draw(whiteTex, base.Projectile.Center - Main.screenPosition + Utils.RotatedBy(new Vector2(MathHelper.Min(2f, completion * 2.2f), 0f), (double)i, default(Vector2)), (Rectangle?)null, color, Main.GlobalTimeWrappedHourly, spTex.Size() * 0.5f, 0.75f, (SpriteEffects)0, 0f);
		}
		Main.spriteBatch.Draw(spTex, base.Projectile.Center - Main.screenPosition, (Rectangle?)null, Color.White, Main.GlobalTimeWrappedHourly, spTex.Size() * 0.5f, 0.75f, (SpriteEffects)0, 0f);
		Main.spriteBatch.Draw(whiteTex, base.Projectile.Center - Main.screenPosition, (Rectangle?)null, Color.Black * completion, Main.GlobalTimeWrappedHourly, spTex.Size() * 0.5f, 0.75f, (SpriteEffects)0, 0f);
		using (Main.spriteBatch.Scope())
		{
			Main.spriteBatch.Begin((SpriteSortMode)0, BlendState.Additive, SamplerState.PointClamp, DepthStencilState.None, Main.Rasterizer, (Effect)null, Main.GameViewMatrix.TransformationMatrix);
			color = Color.Lerp(Color.Yellow, Color.OrangeRed, completion);
			if (completion >= 1f)
			{
				color = Color.LightBlue;
			}
			float count = MathHelper.Min(base.Projectile.minionSlots * 2f, 40f);
			for (int j = 0; (float)j < count; j++)
			{
				float comp = (float)j / count;
				float offset = (float)(Main.mouseTextColor - 190) / 64f * 8f;
				if (j % 2 == 0)
				{
					offset = 8f - offset;
				}
				Main.spriteBatch.DrawLineBetter(base.Projectile.Center + Utils.RotatedBy(new Vector2(26f + offset, 0f), (double)((float)Math.PI * 2f * comp - Main.GlobalTimeWrappedHourly), default(Vector2)), base.Projectile.Center + Utils.RotatedBy(new Vector2(40f + offset, 0f), (double)((float)Math.PI * 2f * comp - Main.GlobalTimeWrappedHourly), default(Vector2)), color, 1f);
			}
			Main.spriteBatch.End();
		}
		return false;
	}
}
