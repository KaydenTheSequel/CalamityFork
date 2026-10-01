using System;
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

public class FlashBolt : ModProjectile, ILocalizedModType, IModType
{
	public int time;

	public int dir;

	private bool setAltPlace;

	public Vector2 altPlace;

	public new string LocalizationCategory => "Projectiles.Typeless";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public Player Owner => Main.player[base.Projectile.owner];

	public bool invalidTarget
	{
		get
		{
			if (!(base.Projectile.ai[0] < 0f))
			{
				return base.Projectile.ai[0] > 199f;
			}
			return true;
		}
	}

	public bool simplify => base.Projectile.ai[1] > 0f;

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.DrawScreenCheckFluff[base.Type] = 10000;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 60;
		base.Projectile.height = 60;
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.DamageType = AverageDamageClass.Instance;
		base.Projectile.penetrate = 1;
		base.Projectile.extraUpdates = 0;
		base.Projectile.timeLeft = 2;
	}

	public override void AI()
	{
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		NPC target = (invalidTarget ? null : Main.npc[(int)base.Projectile.ai[0]]);
		if (target == null || !target.active || target.life <= 0)
		{
			base.Projectile.ai[0] = -1f;
		}
		bool visible = Owner.Calamity().arcFlashRingVisual;
		if (time == 0 && base.Projectile.ai[0] != -1f && !simplify && visible)
		{
			SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/ArcFlash");
			style.Volume = 0.6f;
			style.Pitch = Main.rand.NextFloat(-0.1f, 0.1f);
			SoundEngine.PlaySound(in style, Owner.Center);
		}
		time++;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_0202: Unknown result type (might be due to invalid IL or missing references)
		//IL_0204: Unknown result type (might be due to invalid IL or missing references)
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		//IL_0227: Unknown result type (might be due to invalid IL or missing references)
		//IL_0258: Unknown result type (might be due to invalid IL or missing references)
		//IL_0262: Unknown result type (might be due to invalid IL or missing references)
		//IL_0272: Unknown result type (might be due to invalid IL or missing references)
		//IL_0273: Unknown result type (might be due to invalid IL or missing references)
		//IL_0275: Unknown result type (might be due to invalid IL or missing references)
		//IL_0298: Unknown result type (might be due to invalid IL or missing references)
		//IL_029d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a2: Unknown result type (might be due to invalid IL or missing references)
		if (!setAltPlace)
		{
			NPC target = (invalidTarget ? null : Main.npc[(int)base.Projectile.ai[0]]);
			if (target == null || !target.active || target.life <= 0)
			{
				base.Projectile.ai[0] = -1f;
			}
			if (target != null && base.Projectile.ai[0] != -1f)
			{
				float size = (float)Math.Min(target.width, target.height) * 0.35f + 10f;
				altPlace = base.Projectile.Center + Main.rand.NextVector2Circular(size, size);
			}
			setAltPlace = true;
		}
		if (simplify || !Owner.Calamity().arcFlashRingVisual)
		{
			return false;
		}
		Texture2D tex = ModContent.Request<Texture2D>("CalamityMod/Particles/BloomCircle", (AssetRequestMode)2).Value;
		if (dir == 0)
		{
			dir = ((!Main.rand.NextBool()) ? 1 : (-1));
		}
		int distance = (int)(600f + (altPlace.Y - Owner.Center.Y));
		int travelAmount = 10;
		Vector2 currentPos = altPlace;
		Vector2 lastPos = currentPos;
		bool zap = false;
		Color drawColor = Color.White;
		float lifeLerp = ((base.Projectile.numHits == 0) ? 1f : ((float)Math.Pow(Utils.GetLerpValue(-5f, 15f, base.Projectile.timeLeft, clamped: true), 3.0)));
		int angleSwapTimer = 0;
		int angleSwapMax = 7;
		for (int i = 0; i < distance; i += travelAmount)
		{
			float angleLerp = Math.Min(Utils.GetLerpValue(angleSwapMax, 0f, angleSwapTimer, clamped: true), Utils.GetLerpValue(0f, angleSwapMax, angleSwapTimer, clamped: true));
			float endLerp = Utils.GetLerpValue(0f, distance, i, clamped: true);
			drawColor = Color.Lerp(Color.Cyan, Color.Orchid, endLerp);
			Vector2 position = currentPos - Main.screenPosition;
			Color color = drawColor;
			((Color)(ref color)).A = 0;
			Main.EntitySpriteDraw(tex, position, null, color, lastPos.DirectionTo(currentPos).ToRotation() + (float)Math.PI / 2f, new Vector2((float)(tex.Width / 2), (float)(tex.Height / 2)), new Vector2((2.5f - 1.5f * angleLerp) * lifeLerp * Math.Max(endLerp, 0.25f), 1f + 2f * angleLerp) * 0.2f, (SpriteEffects)0);
			lastPos = currentPos;
			currentPos -= new Vector2((float)travelAmount * 1.5f * endLerp * (float)dir * (float)((!zap) ? 1 : (-1)) * lifeLerp, (float)travelAmount);
			angleSwapTimer++;
			if (angleSwapTimer >= angleSwapMax)
			{
				zap = !zap;
				angleSwapTimer = 0;
			}
		}
		return false;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0248: Unknown result type (might be due to invalid IL or missing references)
		//IL_0249: Unknown result type (might be due to invalid IL or missing references)
		//IL_024e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0271: Unknown result type (might be due to invalid IL or missing references)
		//IL_0285: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01de: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		target.AddBuff(144, 180);
		if (simplify)
		{
			return;
		}
		base.Projectile.penetrate++;
		base.Projectile.timeLeft = 15;
		if (base.Projectile.numHits != 0)
		{
			return;
		}
		if (!setAltPlace)
		{
			if (target != null && base.Projectile.ai[0] != -1f)
			{
				float size = (float)Math.Min(target.width, target.height) * 0.35f + 10f;
				altPlace = base.Projectile.Center + Main.rand.NextVector2Circular(size, size);
			}
			setAltPlace = true;
		}
		Vector2 pos = altPlace;
		if (Owner.Calamity().arcFlashRingVisual)
		{
			for (int i = 0; i < 10; i++)
			{
				Dust dust = Dust.NewDustPerfect(pos, ModContent.DustType<SquashDust>(), Utils.RotatedByRandom(new Vector2(18f, 18f), 100.0) * Main.rand.NextFloat(0.5f, 1f), 0, default(Color), Main.rand.NextFloat(1.3f, 1.5f) * 3f);
				dust.noGravity = true;
				dust.color = (Main.rand.NextBool(3) ? Color.Cyan : Color.Orchid);
				dust.fadeIn = 7.5f;
			}
			GeneralParticleHandler.SpawnParticle(new CustomPulse(pos, Vector2.Zero, Color.Orchid, "CalamityMod/Particles/LargeBloom", new Vector2(1f, 1f), Main.rand.NextFloat(-10f, 10f), 0.68f, 0.5f, 14, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
			GeneralParticleHandler.SpawnParticle(new CustomPulse(pos, Vector2.Zero, Color.White * 0.8f, "CalamityMod/Particles/LargeBloom", new Vector2(1f, 1f), Main.rand.NextFloat(-10f, 10f), 0.225f, 0.2f, 14, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
		}
		GeneralParticleHandler.SpawnParticle(new CustomPulse(pos, Vector2.Zero, Color.Cyan * (Owner.Calamity().arcFlashRingVisual ? 1f : 0.6f), "CalamityMod/Particles/BloomRing", new Vector2(1f, 1f), 0f, 0.3f, 0.95f, 10, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
	}

	public override bool? CanHitNPC(NPC target)
	{
		if (invalidTarget || base.Projectile.ai[0] == (float)target.whoAmI)
		{
			return null;
		}
		return false;
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.numHits <= 0)
		{
			return base.Colliding(projHitbox, targetHitbox);
		}
		return false;
	}

	public override bool? CanCutTiles()
	{
		return false;
	}
}
