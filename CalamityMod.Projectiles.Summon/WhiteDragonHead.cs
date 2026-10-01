using System;
using System.Collections.Generic;
using CalamityMod.Buffs.Summon;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class WhiteDragonHead : ModProjectile, ILocalizedModType, IModType
{
	private Dictionary<int, Projectile> segments = new Dictionary<int, Projectile>();

	public new string LocalizationCategory => "Projectiles.Summon";

	public override void SetStaticDefaults()
	{
		Main.projPet[base.Type] = true;
		ProjectileID.Sets.MinionSacrificable[base.Type] = true;
		ProjectileID.Sets.MinionTargettingFeature[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.timeLeft = 10;
		base.Projectile.width = 24;
		base.Projectile.height = 24;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 180;
		base.Projectile.localNPCHitCooldown = 30;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.tileCollide = false;
		base.Projectile.extraUpdates = 0;
		base.Projectile.aiStyle = -1;
		base.Projectile.DamageType = DamageClass.Summon;
		base.Projectile.netImportant = true;
		base.Projectile.minionSlots = 2f;
		base.Projectile.minion = true;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 100;
	}

	public override void AI()
	{
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_021f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0224: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0281: Unknown result type (might be due to invalid IL or missing references)
		//IL_0286: Unknown result type (might be due to invalid IL or missing references)
		//IL_0290: Unknown result type (might be due to invalid IL or missing references)
		//IL_0295: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		player.AddBuff(ModContent.BuffType<KingofConstellationsBuff>(), 1);
		if (base.Projectile.type == ModContent.ProjectileType<WhiteDragonHead>())
		{
			if (player.dead)
			{
				player.Calamity().celestialDragons = false;
			}
			if (player.Calamity().celestialDragons)
			{
				base.Projectile.timeLeft = 2;
			}
		}
		base.Projectile.ai[0]++;
		Vector2 idealPos = default(Vector2);
		((Vector2)(ref idealPos))._002Ector(player.Center.X + 200f, player.Center.Y - 50f);
		float distanceFromOwner = base.Projectile.Distance(idealPos);
		if (distanceFromOwner > 5000f)
		{
			base.Projectile.Center = player.Center;
		}
		NPC target = base.Projectile.position.MinionHoming(2500f, player, ignoreTiles: true, checksRange: true);
		if (target != null && base.Projectile.position.Distance(player.position) <= 2500f)
		{
			AttackTarget(target);
		}
		else
		{
			float hoverAcceleration = 0.2f;
			if (distanceFromOwner < 200f)
			{
				hoverAcceleration = 0.12f;
			}
			if (distanceFromOwner < 140f)
			{
				hoverAcceleration = 0.06f;
			}
			if (distanceFromOwner > 100f)
			{
				if (Math.Abs(player.Center.X - base.Projectile.Center.X) > 20f)
				{
					base.Projectile.velocity.X += hoverAcceleration * (float)Math.Sign(idealPos.X - base.Projectile.Center.X);
				}
				if (Math.Abs(player.Center.Y - base.Projectile.Center.Y) > 10f)
				{
					base.Projectile.velocity.Y += hoverAcceleration * (float)Math.Sign(idealPos.Y - base.Projectile.Center.Y);
				}
			}
			else if (((Vector2)(ref base.Projectile.velocity)).Length() > 1f)
			{
				Projectile projectile = base.Projectile;
				projectile.velocity *= 0.96f;
			}
			if (Math.Abs(base.Projectile.velocity.Y) < 1f)
			{
				base.Projectile.velocity.Y -= 0.1f;
			}
			if (((Vector2)(ref base.Projectile.velocity)).Length() > 25f)
			{
				base.Projectile.velocity = Vector2.Normalize(base.Projectile.velocity) * 25f;
			}
		}
		base.Projectile.rotation = base.Projectile.velocity.ToRotation();
		segments.Clear();
		Projectile[] projectile2 = Main.projectile;
		foreach (Projectile projectile3 in projectile2)
		{
			if (projectile3.type == ModContent.ProjectileType<WhiteDragonBody>() && projectile3.owner == base.Projectile.owner && projectile3.active && !segments.ContainsKey(projectile3.ModProjectile<WhiteDragonBody>().segmentIndex))
			{
				segments.Add(projectile3.ModProjectile<WhiteDragonBody>().segmentIndex, projectile3);
			}
			if (projectile3.type == ModContent.ProjectileType<WhiteDragonTail>() && projectile3.owner == base.Projectile.owner && projectile3.active)
			{
				segments.Add(projectile3.ModProjectile<WhiteDragonTail>().segmentIndex, projectile3);
			}
		}
		for (int j = 1; j <= segments.Count; j++)
		{
			if (j < segments.Count)
			{
				if (segments.ContainsKey(j))
				{
					segments[j].ModProjectile<WhiteDragonBody>().SegmentMove();
				}
			}
			else if (segments.ContainsKey(j))
			{
				segments[j].ModProjectile<WhiteDragonTail>().SegmentMove();
			}
		}
	}

	internal void AttackTarget(NPC target)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		//IL_020f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0219: Unknown result type (might be due to invalid IL or missing references)
		//IL_021e: Unknown result type (might be due to invalid IL or missing references)
		float idealFlyAcceleration = 0.18f;
		Vector2 destination = target.Center;
		float distanceFromDestination = base.Projectile.Distance(destination);
		if (base.Projectile.Distance(destination) > 400f)
		{
			base.Projectile.ai[2] = 0f;
			destination += (base.Projectile.ai[0] % 30f / 30f * ((float)Math.PI * 2f)).ToRotationVector2() * 145f;
			distanceFromDestination = base.Projectile.Distance(destination);
			idealFlyAcceleration *= 2.5f;
		}
		if (distanceFromDestination > 1500f)
		{
			idealFlyAcceleration = MathHelper.Min(6f, base.Projectile.ai[1] + 1f);
		}
		base.Projectile.ai[1] = MathHelper.Lerp(base.Projectile.ai[1], idealFlyAcceleration, 0.3f);
		float directionToTargetOrthogonality = Vector2.Dot(base.Projectile.velocity.SafeNormalize(Vector2.Zero), base.Projectile.SafeDirectionTo(destination));
		if (distanceFromDestination > 320f)
		{
			float speed = ((Vector2)(ref base.Projectile.velocity)).Length();
			if (speed < 23f)
			{
				speed += 0.08f;
			}
			if (speed > 32f)
			{
				speed -= 0.08f;
			}
			if (directionToTargetOrthogonality < 0.85f && directionToTargetOrthogonality > 0.5f)
			{
				speed += 16f;
			}
			if (directionToTargetOrthogonality < 0.5f && directionToTargetOrthogonality > -0.7f)
			{
				speed -= 16f;
			}
			speed = MathHelper.Clamp(speed, 16f, 34f);
			base.Projectile.velocity = base.Projectile.velocity.ToRotation().AngleTowards(base.Projectile.AngleTo(destination), base.Projectile.ai[1]).ToRotationVector2() * speed;
			base.Projectile.ai[2]++;
			if (base.Projectile.ai[2] >= 90f)
			{
				base.Projectile.velocity = base.Projectile.DirectionTo(destination) * 30f;
				base.Projectile.ai[2] = 0f;
			}
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_02b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_020f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0229: Unknown result type (might be due to invalid IL or missing references)
		//IL_022f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0231: Unknown result type (might be due to invalid IL or missing references)
		//IL_0236: Unknown result type (might be due to invalid IL or missing references)
		//IL_0253: Unknown result type (might be due to invalid IL or missing references)
		//IL_0258: Unknown result type (might be due to invalid IL or missing references)
		//IL_0276: Unknown result type (might be due to invalid IL or missing references)
		//IL_0280: Unknown result type (might be due to invalid IL or missing references)
		//IL_0297: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		Texture2D tex = TextureAssets.Projectile[base.Type].Value;
		Texture2D texBody = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Summon/WhiteDragonBody", (AssetRequestMode)2).Value;
		Texture2D texBody2 = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Summon/WhiteDragonBody2", (AssetRequestMode)2).Value;
		Texture2D texTail = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Summon/WhiteDragonTail", (AssetRequestMode)2).Value;
		Texture2D texTail2 = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Summon/WhiteDragonTail2", (AssetRequestMode)2).Value;
		for (int i = segments.Count; i > 0; i--)
		{
			if (segments.ContainsKey(i))
			{
				SpriteEffects fx = (SpriteEffects)(Math.Abs(segments[i].rotation) > (float)Math.PI / 2f);
				if (i < segments.Count - 1)
				{
					Main.EntitySpriteDraw((i == 5 || i == 12) ? texBody2 : texBody, segments[i].Center - Main.screenPosition, null, segments[i].GetAlpha(lightColor), segments[i].rotation + (float)Math.PI / 2f, texBody.Size() / 2f, segments[i].scale, fx);
				}
				else if (i < segments.Count)
				{
					Main.EntitySpriteDraw(texTail, segments[i].Center - Main.screenPosition, null, segments[i].GetAlpha(lightColor), segments[i].rotation + (float)Math.PI / 2f, texBody.Size() / 2f, segments[i].scale, fx);
				}
				else
				{
					Main.EntitySpriteDraw(texTail2, segments[i].Center - Main.screenPosition - Utils.RotatedBy(new Vector2(10f, 0f), (double)segments[i].rotation, default(Vector2)), null, segments[i].GetAlpha(lightColor), segments[i].rotation + (float)Math.PI / 2f, texTail.Size() / 2f, segments[i].scale, fx);
				}
			}
		}
		Main.EntitySpriteDraw(tex, base.Projectile.Center - Main.screenPosition, null, base.Projectile.GetAlpha(lightColor), base.Projectile.rotation + (float)Math.PI / 2f, tex.Size() / 2f, base.Projectile.scale, (SpriteEffects)(!(base.Projectile.velocity.X > 0f)));
		return false;
	}

	public override bool MinionContactDamage()
	{
		return true;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(324, 300);
	}
}
