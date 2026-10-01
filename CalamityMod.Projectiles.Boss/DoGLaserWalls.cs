using System;
using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Boss;

public class DoGLaserWalls : ModProjectile, ILocalizedModType, IModType
{
	public float time;

	public bool doneAttack;

	public int attackTime;

	public float laserFX;

	public float storedTime;

	public Color drawColor;

	public float sine;

	public Vector2 storedTargetPos;

	public Player targeted;

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public new string LocalizationCategory => "Projectiles.Boss";

	public ref float attackSpeed => ref base.Projectile.ai[0];

	public ref float laserDist => ref base.Projectile.ai[1];

	public ref float laserType => ref base.Projectile.ai[2];

	public bool canDamage
	{
		get
		{
			if (doneAttack)
			{
				return laserFX >= 1f;
			}
			return false;
		}
	}

	public int laserCount => (int)(6000f / Math.Max(laserDist, 1f));

	public float laserLength => laserDist * (float)laserCount / 2f;

	public Vector2 targetPos
	{
		get
		{
			//IL_001a: Unknown result type (might be due to invalid IL or missing references)
			//IL_000e: Unknown result type (might be due to invalid IL or missing references)
			if (targeted != null)
			{
				return targeted.Center;
			}
			return base.Projectile.Center;
		}
	}

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.DrawScreenCheckFluff[base.Type] = 10000;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 1;
		base.Projectile.height = 1;
		base.Projectile.hostile = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 6000;
	}

	public override void AI()
	{
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0223: Unknown result type (might be due to invalid IL or missing references)
		//IL_0228: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		if (laserFX > 0f)
		{
			laserFX = MathHelper.Lerp(laserFX, 0f, (time > 15f) ? 0.12f : 0.01f);
		}
		sine = (float)Math.Sin(time * 4f / (float)Math.PI);
		if (time == 0f)
		{
			targeted = Main.player[Player.FindClosest(base.Projectile.Center, 1, 1)];
			if (attackSpeed == 0f)
			{
				attackSpeed = 0.5f;
			}
			if (laserDist == 0f)
			{
				laserDist = 250f;
			}
			base.Projectile.velocity = Vector2.Zero;
			laserFX = 1f;
			SoundStyle style = new SoundStyle("CalamityMod/Sounds/Custom/DoGLaserWallSpawn");
			style.Volume = 1f;
			style.Pitch = 0f;
			style.MaxInstances = -1;
			SoundEngine.PlaySound(in style, targetPos);
			storedTargetPos = targetPos;
			base.Projectile.ForceNetUpdate();
		}
		if (time >= (float)attackTime && !doneAttack)
		{
			SoundStyle attack = new SoundStyle("CalamityMod/Sounds/Custom/DoGLaserWallLightAttack");
			for (int i = 0; i < 2; i++)
			{
				SoundEngine.PlaySound(attack with
				{
					Volume = 0.9f,
					Pitch = 0f,
					MaxInstances = -1
				}, targetPos);
			}
			laserFX = 3f;
			doneAttack = true;
			storedTime = time;
			base.Projectile.ForceNetUpdate();
		}
		float endTime = storedTime + 10f;
		if (time >= endTime && doneAttack)
		{
			base.Projectile.Kill();
			return;
		}
		if (doneAttack)
		{
			drawColor = Color.Lerp(Color.Cyan, Color.Magenta, (float)Math.Pow(Utils.GetLerpValue(endTime, storedTime, time, clamped: true), 2.0));
		}
		time += attackSpeed;
	}

	public override bool CanHitPlayer(Player target)
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		if (canDamage)
		{
			if (laserType != 6f)
			{
				return true;
			}
			float distance = Vector2.Distance(base.Projectile.Center, target.Center);
			float expand = MathF.Min(target.width, target.height);
			if (distance < laserDist * 1.5f || distance > laserDist * 10f + 40f)
			{
				return false;
			}
			if (distance % laserDist > laserDist - Utils.Remap(distance, 240f, 1440f, 8f, 56f) - expand || distance % laserDist < expand)
			{
				return true;
			}
		}
		return false;
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(ModContent.BuffType<GodSlayerInferno>(), 60);
		base.OnHitPlayer(target, info);
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01da: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		if (!canDamage)
		{
			return false;
		}
		if (laserType == 6f)
		{
			return true;
		}
		float _ = float.NaN;
		bool hit = false;
		Vector2 X = Vector2.UnitX.RotatedBy((laserType == 1f || laserType == 3f || laserType == 5f) ? ((float)Math.PI / 4f) : 0f);
		Vector2 Y = Vector2.UnitY.RotatedBy((laserType == 1f || laserType == 3f || laserType == 5f) ? ((float)Math.PI / 4f) : 0f);
		for (int l = 0; l < 2; l++)
		{
			bool horizontal = l != 0;
			bool crossLasers = (laserType == 2f && !horizontal) || (laserType == 3f && !horizontal) || ((laserType == 4f) & horizontal) || ((laserType == 5f) & horizontal);
			float length = laserLength * (horizontal ? 1f : 1f);
			for (int i = 0; i < laserCount; i += ((!crossLasers) ? 1 : 2))
			{
				Vector2 val = base.Projectile.Center - (horizontal ? Y : X) * length + (horizontal ? X : Y) * (laserDist * (float)i) - (horizontal ? X : Y) * (laserDist * (float)laserCount / 2f);
				Vector2 rot = val.DirectionTo(storedTargetPos);
				Vector2 start = val;
				Vector2 end = val + (crossLasers ? rot : (horizontal ? Y : X)) * laserLength * 2f;
				if (Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), start, end, 10f, ref _))
				{
					hit = true;
					i = laserCount;
				}
			}
		}
		return hit;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0251: Unknown result type (might be due to invalid IL or missing references)
		//IL_025e: Unknown result type (might be due to invalid IL or missing references)
		//IL_025a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0262: Unknown result type (might be due to invalid IL or missing references)
		//IL_0267: Unknown result type (might be due to invalid IL or missing references)
		//IL_0274: Unknown result type (might be due to invalid IL or missing references)
		//IL_0270: Unknown result type (might be due to invalid IL or missing references)
		//IL_0281: Unknown result type (might be due to invalid IL or missing references)
		//IL_0286: Unknown result type (might be due to invalid IL or missing references)
		//IL_0293: Unknown result type (might be due to invalid IL or missing references)
		//IL_028f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_02af: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_03af: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0393: Unknown result type (might be due to invalid IL or missing references)
		//IL_0396: Unknown result type (might be due to invalid IL or missing references)
		//IL_039b: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_040c: Unknown result type (might be due to invalid IL or missing references)
		//IL_041b: Unknown result type (might be due to invalid IL or missing references)
		//IL_042b: Unknown result type (might be due to invalid IL or missing references)
		if (laserFX == 0f)
		{
			return false;
		}
		Texture2D beam = ModContent.Request<Texture2D>("CalamityMod/Particles/BloomLineThick", (AssetRequestMode)2).Value;
		Texture2D bBeam = ModContent.Request<Texture2D>("CalamityMod/Particles/LineThick", (AssetRequestMode)2).Value;
		_ = ModContent.Request<Texture2D>("CalamityMod/Particles/BloomCircle", (AssetRequestMode)2).Value;
		float opacity = (doneAttack ? 0.65f : 0.3f) * (float)Math.Pow(Math.Min(laserFX, 1f), 2.0);
		if (CalamityClientConfig.Instance.Photosensitivity)
		{
			opacity = 0.2f;
		}
		Color val = drawColor;
		((Color)(ref val)).A = 0;
		Color beamColor = val;
		if (laserType == 6f)
		{
			Texture2D ring = ModContent.Request<Texture2D>("CalamityMod/Particles/BloomRingThinLarge", (AssetRequestMode)2).Value;
			for (float scale = 0.25f * laserDist / 120f; scale < 1.25f; scale += 0.125f * laserDist / 120f)
			{
				Main.EntitySpriteDraw(ring, base.Projectile.Center - Main.screenPosition, null, beamColor * opacity, 0f, ring.Size() * 0.5f, scale, (SpriteEffects)0);
			}
			return false;
		}
		Vector2 X = Vector2.UnitX.RotatedBy((laserType == 1f || laserType == 3f || laserType == 5f) ? ((float)Math.PI / 4f) : 0f);
		Vector2 Y = Vector2.UnitY.RotatedBy((laserType == 1f || laserType == 3f || laserType == 5f) ? ((float)Math.PI / 4f) : 0f);
		for (int l = 0; l < 2; l++)
		{
			bool horizontal = l != 0;
			float length = laserLength;
			bool crossLasers = (laserType == 2f && !horizontal) || (laserType == 3f && !horizontal) || ((laserType == 4f) & horizontal) || ((laserType == 5f) & horizontal);
			for (int i = 0; i < laserCount; i += ((!crossLasers) ? 1 : 2))
			{
				Vector2 laserPoint = base.Projectile.Center - (horizontal ? Y : X) * length + (horizontal ? X : Y) * (laserDist * (float)i) - (horizontal ? X : Y) * (laserDist * (float)laserCount / 2f);
				for (int t = 0; t < ((!doneAttack) ? 1 : 5); t++)
				{
					bool black = t > 0;
					Texture2D texture = (black ? bBeam : beam);
					float beamThickness = 0.03f * (black ? (0.8f - 0.15f * (float)t) : 1f) * ((laserFX <= 1f) ? ((float)Math.Pow(Math.Min(laserFX, 1f), 2.0)) : laserFX) * Utils.Remap(sine, -1f, 1f, 0.8f, 1.1f);
					float rot = (horizontal ? ((float)Math.PI) : ((float)Math.PI / 2f)) + ((laserType == 1f || laserType == 3f || laserType == 5f) ? ((float)Math.PI / 4f) : 0f);
					if (crossLasers)
					{
						rot = laserPoint.DirectionTo(storedTargetPos).ToRotation() + (float)Math.PI / 2f;
					}
					Main.EntitySpriteDraw(texture, laserPoint - Main.screenPosition, null, (black ? (Color.Black * opacity) : (beamColor * opacity)) * (black ? (0.2f + 0.15f * (float)t) : 1f), rot, new Vector2((float)(beam.Width / 2), (float)beam.Height), new Vector2(beamThickness, length / 975f) * base.Projectile.scale, (SpriteEffects)0);
				}
			}
		}
		return false;
	}

	public DoGLaserWalls()
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		attackTime = 30;
		drawColor = Color.Cyan;
		base._002Ector();
	}
}
