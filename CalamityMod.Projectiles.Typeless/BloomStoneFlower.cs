using System;
using System.Collections.Generic;
using CalamityMod.Dusts;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Typeless;

public class BloomStoneFlower : ModProjectile, ILocalizedModType, IModType
{
	public float fadeTime;

	public bool fadeOut;

	public int maxTime = 5;

	public new string LocalizationCategory => "Projectiles.Typeless";

	public override string Texture => "CalamityMod/Projectiles/Magic/BeamingBolt";

	public ref float HookIndex => ref base.Projectile.ai[0];

	public ref float FlowerPart => ref base.Projectile.ai[1];

	public ref float timer => ref base.Projectile.ai[2];

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 30);
		base.Projectile.scale = 1f;
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.timeLeft = 150;
	}

	public override void AI()
	{
		//IL_02e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0300: Unknown result type (might be due to invalid IL or missing references)
		//IL_0306: Unknown result type (might be due to invalid IL or missing references)
		//IL_0308: Unknown result type (might be due to invalid IL or missing references)
		//IL_0312: Unknown result type (might be due to invalid IL or missing references)
		//IL_0317: Unknown result type (might be due to invalid IL or missing references)
		//IL_031c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0321: Unknown result type (might be due to invalid IL or missing references)
		//IL_0342: Unknown result type (might be due to invalid IL or missing references)
		//IL_0347: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_035d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0371: Unknown result type (might be due to invalid IL or missing references)
		//IL_0376: Unknown result type (might be due to invalid IL or missing references)
		//IL_0377: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0484: Unknown result type (might be due to invalid IL or missing references)
		//IL_0489: Unknown result type (might be due to invalid IL or missing references)
		//IL_048f: Unknown result type (might be due to invalid IL or missing references)
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		if (timer == 0f)
		{
			base.Projectile.rotation = Main.rand.NextFloat(0f, (float)Math.PI * 2f);
		}
		float flowerPart = FlowerPart;
		if (flowerPart != 0f)
		{
			if (flowerPart == 1f)
			{
				if (timer == 0f)
				{
					base.Projectile.scale = 1.8f;
					base.Projectile.extraUpdates = 1;
				}
				if (base.Projectile.width < 90)
				{
					base.Projectile.ExpandHitboxBy(90);
				}
				base.Projectile.velocity = base.Projectile.velocity.RotatedBy(0.04f * base.Projectile.localAI[0]) * 1.001f;
				Color mistColor = Color.Lerp(Color.HotPink, Color.Gold, Utils.GetLerpValue(100f, 0f, base.Projectile.timeLeft, clamped: true));
				if (base.Projectile.timeLeft % 4 == 0)
				{
					GeneralParticleHandler.SpawnParticle(new MediumMistParticle(base.Projectile.Center, Main.rand.NextVector2Circular(1f, 1f), mistColor, mistColor, 3f * base.Projectile.scale, 100f));
				}
				if (base.Projectile.timeLeft % 4 == 0)
				{
					Dust dust = Dust.NewDustDirect(base.Projectile.position, base.Projectile.width, base.Projectile.height, ModContent.DustType<LightDust>(), 0f, 0f, 0, mistColor, 0.6f * base.Projectile.scale);
					dust.noLightEmittence = true;
					dust.noGravity = true;
				}
				if (base.Projectile.scale > 0.7f)
				{
					base.Projectile.scale -= 0.025f;
				}
				Player owner = Main.player[base.Projectile.owner];
				ActiveEntityIterator<Player>.Enumerator enumerator = Main.ActivePlayers.GetEnumerator();
				while (enumerator.MoveNext())
				{
					Player p = enumerator.Current;
					if (p == owner || (p.team == owner.team && owner.team != 0))
					{
						Rectangle hitbox = base.Projectile.Hitbox;
						if (((Rectangle)(ref hitbox)).Intersects(p.Hitbox))
						{
							p.Calamity().bloomStoneBuffedHealRateTimer = 360;
						}
					}
				}
				fadeTime++;
			}
		}
		else
		{
			Projectile hook = Main.projectile[(int)HookIndex];
			if (!hook.active || hook.aiStyle != 7 || hook.ai[0] != 2f)
			{
				fadeOut = true;
			}
			float sine = (float)Math.Sin(timer * 0.01f);
			base.Projectile.rotation += fadeTime * sine * 0.005f;
			if (base.Projectile.scale > 1f)
			{
				base.Projectile.scale = MathHelper.Lerp(base.Projectile.scale, 1f, 0.15f);
			}
			base.Projectile.timeLeft++;
			if (!fadeOut && Vector2.DistanceSquared(base.Projectile.Center, Main.player[base.Projectile.owner].Center) < 4096f)
			{
				SoundEngine.PlaySound(in SoundID.Item60, base.Projectile.Center);
				if (Main.myPlayer == base.Projectile.owner)
				{
					int dusts = 6;
					int dir = ((!Main.rand.NextBool()) ? 1 : (-1));
					for (int i = 0; i < dusts; i++)
					{
						Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), base.Projectile.Center, ((float)Math.PI * 2f / (float)dusts * (float)i).ToRotationVector2() * 3f, base.Type, 0, 0f, base.Projectile.owner, 0f, 1f).localAI[0] = dir;
					}
				}
				fadeOut = true;
				base.Projectile.scale = 2.5f;
			}
			if ((fadeOut && base.Projectile.scale <= 1.05f) || (!fadeOut && fadeTime < (float)maxTime))
			{
				fadeTime += (fadeOut ? (-0.2f) : 1f);
			}
			if (fadeTime < 0f && fadeOut)
			{
				base.Projectile.Kill();
			}
		}
		timer++;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0204: Unknown result type (might be due to invalid IL or missing references)
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		//IL_021c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0226: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		if (FlowerPart == 0f)
		{
			Texture2D tex2 = ModContent.Request<Texture2D>("CalamityMod/Particles/BigHeart", (AssetRequestMode)2).Value;
			Texture2D tex3 = ModContent.Request<Texture2D>("CalamityMod/Particles/BloomCircle", (AssetRequestMode)2).Value;
			int parts = 5;
			float rot = 0f;
			float scaleFade = (float)Math.Pow(Utils.GetLerpValue(0f, maxTime, fadeTime, clamped: true), 2.0);
			Color val;
			for (int b = 0; b < 3; b++)
			{
				val = Color.Lerp(Color.HotPink, Color.Plum, (float)b * 0.3f);
				((Color)(ref val)).A = 0;
				Color drawColor = val;
				float scaleMult = (1f - 0.15f * (float)b) * scaleFade;
				for (int i = 0; i < parts; i++)
				{
					Vector2 vel = ((float)Math.PI * 2f * (float)i / (float)parts).ToRotationVector2();
					Main.EntitySpriteDraw(tex2, base.Projectile.Center - Main.screenPosition + vel.RotatedBy(base.Projectile.rotation * (float)Math.Pow(1.5f - scaleMult, 2.0) + rot) * 35f * scaleMult, null, drawColor * 0.3f, base.Projectile.rotation * (float)Math.Pow(1.5f - scaleMult, 2.0) + vel.ToRotation() + (float)Math.PI / 2f + rot, tex2.Size() / 2f, new Vector2(0.9f, 1.5f) * base.Projectile.scale * 0.15f * scaleMult, (SpriteEffects)0);
				}
				rot += (float)Math.PI / 8f;
			}
			Vector2 position = base.Projectile.Center - Main.screenPosition;
			val = Color.Gold;
			((Color)(ref val)).A = 0;
			Main.EntitySpriteDraw(tex3, position, null, val, base.Projectile.rotation, tex3.Size() / 2f, base.Projectile.scale * 0.45f * scaleFade, (SpriteEffects)0);
			return false;
		}
		return false;
	}

	public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
	{
		overPlayers.Add(index);
	}
}
