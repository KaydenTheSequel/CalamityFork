using System;
using System.Collections.Generic;
using CalamityMod.Buffs.StatBuffs;
using CalamityMod.Dusts;
using CalamityMod.Items.Accessories;
using CalamityMod.Systems.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Healing;

public class AbsorberAura : ModProjectile, ILocalizedModType, IModType
{
	private int AbDust = ModContent.DustType<LightDust>();

	public int ShinkGrow;

	public int Framecounter;

	public int CleanseOnce = 1;

	public int PulseOnce = 1;

	public int PulseOnce2 = 1;

	public int PulseOnce3 = 1;

	public static readonly SoundStyle Spawnsound = new SoundStyle("CalamityMod/Sounds/Custom/OrbHeal3")
	{
		Volume = 0.5f
	};

	public List<bool> cleanseList = new List<bool>(new bool[255]);

	public new string LocalizationCategory => "Projectiles.Healing";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public ref int CleansingEffect => ref Main.player[base.Projectile.owner].Calamity().CleansingEffect;

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 336);
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft = TheAbsorber.AuraLifetime + 10;
	}

	public override void AI()
	{
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_020c: Unknown result type (might be due to invalid IL or missing references)
		//IL_021a: Unknown result type (might be due to invalid IL or missing references)
		//IL_021f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0224: Unknown result type (might be due to invalid IL or missing references)
		//IL_0226: Unknown result type (might be due to invalid IL or missing references)
		//IL_023b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0241: Unknown result type (might be due to invalid IL or missing references)
		//IL_0283: Unknown result type (might be due to invalid IL or missing references)
		//IL_027c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0288: Unknown result type (might be due to invalid IL or missing references)
		//IL_0293: Unknown result type (might be due to invalid IL or missing references)
		//IL_0298: Unknown result type (might be due to invalid IL or missing references)
		//IL_029a: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_030a: Unknown result type (might be due to invalid IL or missing references)
		//IL_030f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0327: Unknown result type (might be due to invalid IL or missing references)
		//IL_032d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_036e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0367: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0373: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		Framecounter++;
		float sine = Math.Abs((float)Math.Sin(Main.GlobalTimeWrappedHourly * 1.1f * 5f / (float)Math.PI));
		for (int playerIndex = 0; playerIndex < 255; playerIndex++)
		{
			Player player = Main.player[playerIndex];
			if (!(Vector2.Distance(player.Center, base.Projectile.Center) < 310f))
			{
				continue;
			}
			player.AddBuff(ModContent.BuffType<AbsorberRegen>(), 600);
			if (cleanseList[playerIndex])
			{
				continue;
			}
			cleanseList[playerIndex] = true;
			CleansingEffect = 1;
			for (int l = 0; l < Player.MaxBuffs; l++)
			{
				int buffID = player.buffType[l];
				if (player.buffTime[l] > 2 && CalamityBuffSets.IsDebuff[buffID])
				{
					player.buffTime[l] = 0;
				}
			}
			for (int i = 0; i < 55; i++)
			{
				int dust = Dust.NewDust(player.Center, player.width + 4, player.height + 4, AbDust, player.velocity.X * 0.2f, player.velocity.Y * 0.2f, 100, default(Color), 5.5f);
				Main.dust[dust].noGravity = true;
				Dust obj = Main.dust[dust];
				obj.velocity *= 1.5f;
				Main.dust[dust].velocity.Y -= 0.5f;
				Main.dust[dust].color = (Main.rand.NextBool(3) ? Color.PaleGreen : Color.DarkSeaGreen);
			}
			SoundStyle style = Spawnsound with
			{
				Pitch = -0.9f
			};
			SoundEngine.PlaySound(in style, base.Projectile.Center);
		}
		if (Framecounter >= 10)
		{
			for (int j = 0; j < 3; j++)
			{
				float areaSize = 305f;
				Vector2 spawnSpot = base.Projectile.Center + Main.rand.NextVector2CircularEdge(areaSize, areaSize);
				Dust dust2 = Dust.NewDustPerfect(spawnSpot, AbDust);
				dust2.scale = Main.rand.NextFloat(1.2f, 2.3f);
				dust2.noGravity = true;
				dust2.color = (Main.rand.NextBool(3) ? Color.PaleGreen : Color.DarkSeaGreen);
				dust2.velocity = (base.Projectile.Center.DirectionTo(spawnSpot) * Main.rand.NextFloat(1.5f, 4.5f) * sine).RotatedByRandom(0.4000000059604645);
			}
			for (int k = 0; k < 1; k++)
			{
				float areaSize2 = 272.5f + 20f * sine;
				Dust dust3 = Dust.NewDustPerfect(base.Projectile.Center + Main.rand.NextVector2Circular(areaSize2, areaSize2), AbDust);
				dust3.scale = Main.rand.NextFloat(0.3f, 0.9f);
				dust3.noGravity = true;
				dust3.color = (Main.rand.NextBool(3) ? Color.PaleGreen : Color.DarkSeaGreen);
			}
		}
		base.Projectile.rotation += 0.15f * sine;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		Texture2D tex = ModContent.Request<Texture2D>("CalamityMod/Particles/HighResFoggyCircleHardEdge", (AssetRequestMode)2).Value;
		Texture2D tex2 = ModContent.Request<Texture2D>("CalamityMod/Particles/HighResHollowCircleHardEdge", (AssetRequestMode)2).Value;
		Color drawColor1 = Color.DarkSeaGreen;
		Color drawColor2 = Color.PaleGreen;
		float sine = Math.Abs((float)Math.Sin(Main.GlobalTimeWrappedHourly * 1.1f * 5f / (float)Math.PI));
		float areaScale = Math.Min(Utils.GetLerpValue(TheAbsorber.AuraLifetime + 10, TheAbsorber.AuraLifetime, base.Projectile.timeLeft, clamped: true), Utils.GetLerpValue(0f, 10f, base.Projectile.timeLeft, clamped: true));
		Vector2 position = base.Projectile.Center - Main.screenPosition;
		Color val = drawColor1;
		((Color)(ref val)).A = 0;
		Main.EntitySpriteDraw(tex, position, null, val * 0.6f, 0f, tex.Size() / 2f, (0.305f - 0.006f * sine) * areaScale, (SpriteEffects)0);
		for (int i = 1; i <= 8; i++)
		{
			if (i != 0)
			{
				float rot = (float)Math.PI * 2f * (float)i / 8f;
				Vector2 position2 = base.Projectile.Center - Main.screenPosition;
				val = drawColor2;
				((Color)(ref val)).A = 0;
				Main.EntitySpriteDraw(tex2, position2, null, val * 0.03f, base.Projectile.rotation + rot, tex2.Size() / 2f, new Vector2(0.2f * sine + 0.8f, 1f) * 0.29f * areaScale, (SpriteEffects)0);
			}
		}
		return false;
	}

	public override bool? CanDamage()
	{
		return false;
	}
}
