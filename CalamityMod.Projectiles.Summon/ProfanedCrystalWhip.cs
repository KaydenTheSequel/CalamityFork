using System;
using System.Collections.Generic;
using CalamityMod.Buffs.Summon.Whips;
using CalamityMod.Items.Accessories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class ProfanedCrystalWhip : ModProjectile, ILocalizedModType, IModType
{
	private Color specialColor;

	public new string LocalizationCategory => "Projectiles.Summon";

	public Color SpecialDrawColor
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return specialColor;
		}
	}

	private float Timer
	{
		get
		{
			return base.Projectile.ai[0];
		}
		set
		{
			base.Projectile.ai[0] = value;
		}
	}

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.IsAWhip[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.DefaultToWhip();
		base.Projectile.WhipSettings.Segments = 40;
		base.Projectile.WhipSettings.RangeMultiplier = 2.25f;
	}

	public override bool PreAI()
	{
		ExtraBehavior();
		return true;
	}

	public void ExtraBehavior()
	{
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		specialColor = ProfanedSoulCrystal.GetColorForPsc((!Main.dayTime) ? player.Calamity().pscState : 0, Main.dayTime);
	}

	public override void AI()
	{
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_0212: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Unknown result type (might be due to invalid IL or missing references)
		//IL_0219: Unknown result type (might be due to invalid IL or missing references)
		//IL_0220: Unknown result type (might be due to invalid IL or missing references)
		//IL_0235: Unknown result type (might be due to invalid IL or missing references)
		//IL_0249: Unknown result type (might be due to invalid IL or missing references)
		//IL_024e: Unknown result type (might be due to invalid IL or missing references)
		//IL_026d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0278: Unknown result type (might be due to invalid IL or missing references)
		//IL_027d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0282: Unknown result type (might be due to invalid IL or missing references)
		//IL_028d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0297: Unknown result type (might be due to invalid IL or missing references)
		//IL_029c: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e1: Unknown result type (might be due to invalid IL or missing references)
		Player owner = Main.player[base.Projectile.owner];
		base.Projectile.damage = (int)owner.GetTotalDamage<SummonDamageClass>().ApplyTo(base.Projectile.originalDamage);
		if (base.Projectile.ai[1] == 0f)
		{
			owner.itemAnimation = owner.itemAnimationMax;
			owner.ApplyItemAnimation(owner.HeldItem);
			base.Projectile.ai[1] = 1f;
		}
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		base.Projectile.Center = Main.GetPlayerArmPosition(base.Projectile) + base.Projectile.velocity * Timer;
		base.Projectile.spriteDirection = ((base.Projectile.velocity.X >= 0f) ? 1 : (-1));
		float swingTime = owner.itemAnimationMax * base.Projectile.MaxUpdates;
		if (Timer >= swingTime)
		{
			base.Projectile.Kill();
			return;
		}
		owner.heldProj = base.Projectile.whoAmI;
		if (Timer == swingTime / 2f)
		{
			List<Vector2> points = base.Projectile.WhipPointsForCollision;
			Projectile.FillWhipControlPoints(base.Projectile, points);
			SoundEngine.PlaySound(in SoundID.Item153, points[points.Count - 1]);
		}
		float swingProgress = Timer / swingTime;
		if (Utils.GetLerpValue(0.1f, 0.7f, swingProgress, clamped: true) * Utils.GetLerpValue(0.9f, 0.7f, swingProgress, clamped: true) > 0.5f && !Main.rand.NextBool(3))
		{
			List<Vector2> points2 = base.Projectile.WhipPointsForCollision;
			points2.Clear();
			Projectile.FillWhipControlPoints(base.Projectile, points2);
			int pointIndex = points2.Count - 1;
			Rectangle spawnArea = Utils.CenteredRectangle(points2[pointIndex], new Vector2(30f, 30f));
			int dustType = 205;
			for (int i = 0; i < 2; i++)
			{
				Dust dust = Dust.NewDustDirect(spawnArea.TopLeft(), spawnArea.Width, spawnArea.Height, dustType, 0f, 0f, 100, Color.White);
				dust.position = points2[pointIndex];
				dust.fadeIn = 0.3f;
				dust.scale = 2f;
				Vector2 spinningpoint = points2[pointIndex] - points2[pointIndex - 1];
				dust.noGravity = true;
				dust.velocity *= 0.5f;
				dust.velocity += spinningpoint.RotatedBy((float)owner.direction * ((float)Math.PI / 2f));
				dust.velocity *= 0.5f;
			}
		}
	}

	private void DrawLine(List<Vector2> list)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = TextureAssets.FishingLine.Value;
		Rectangle frame = texture.Frame();
		Vector2 origin = default(Vector2);
		((Vector2)(ref origin))._002Ector((float)(frame.Width / 2), 0f);
		Vector2 pos = list[0];
		Vector2 scale = default(Vector2);
		for (int i = 0; i < list.Count - 2; i++)
		{
			Vector2 element = list[i];
			Vector2 diff = list[i + 1] - element;
			float rotation = diff.ToRotation() - (float)Math.PI / 2f;
			((Vector2)(ref scale))._002Ector(1f, ((Vector2)(ref diff)).Length() / (float)frame.Height);
			Main.EntitySpriteDraw(texture, pos - Main.screenPosition, frame, SpecialDrawColor, rotation, origin, scale, (SpriteEffects)0);
			pos += diff;
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		List<Vector2> list = new List<Vector2>();
		Projectile.FillWhipControlPoints(base.Projectile, list);
		DrawLine(list);
		SpriteEffects flip = (SpriteEffects)1;
		Main.instance.LoadProjectile(base.Type);
		Texture2D texture = TextureAssets.Projectile[base.Type].Value;
		Vector2 pos = list[0];
		Rectangle frame = default(Rectangle);
		Vector2 origin = default(Vector2);
		for (int i = 0; i < list.Count - 1; i++)
		{
			((Rectangle)(ref frame))._002Ector(0, 0, 16, 22);
			((Vector2)(ref origin))._002Ector(5f, 8f);
			float scale = 1f;
			if (i == list.Count - 2)
			{
				frame.Y = 126;
				frame.Height = 34;
				Projectile.GetWhipSettings(base.Projectile, out var timeToFlyOut, out var _, out var _);
				float t = Timer / timeToFlyOut;
				scale = MathHelper.Lerp(0.5f, 1.5f, Utils.GetLerpValue(0.1f, 0.7f, t, clamped: true) * Utils.GetLerpValue(0.9f, 0.7f, t, clamped: true));
			}
			else if (i > 6)
			{
				frame.Y = 102;
				frame.Height = 18;
			}
			else if (i > 3)
			{
				frame.Y = 70;
				frame.Height = 18;
			}
			else if (i > 0)
			{
				frame.Y = 38;
				frame.Height = 18;
			}
			Vector2 element = list[i];
			Vector2 diff = list[i + 1] - element;
			float rotation = diff.ToRotation() - (float)Math.PI / 2f;
			Main.EntitySpriteDraw(texture, pos - Main.screenPosition, frame, Color.White, rotation, origin, scale, flip);
			pos += diff;
		}
		return false;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		int buffTime = CalamityUtils.SecondsToFrames(30);
		target.AddBuff(ModContent.BuffType<ProfanedCrystalWhipDebuff>(), buffTime);
		Main.player[base.Projectile.owner].AddBuff(ModContent.BuffType<ProfanedCrystalWhipBuff>(), buffTime);
		Main.player[base.Projectile.owner].MinionAttackTargetNPC = target.whoAmI;
		base.Projectile.damage = (int)((float)base.Projectile.damage * 0.7f);
	}

	public ProfanedCrystalWhip()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		specialColor = Color.Orange;
		base._002Ector();
	}
}
