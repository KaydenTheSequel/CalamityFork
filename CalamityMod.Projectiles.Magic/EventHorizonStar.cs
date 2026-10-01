using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class EventHorizonStar : ModProjectile, ILocalizedModType, IModType
{
	private bool initialized;

	private Vector2 initialPosition;

	public new string LocalizationCategory => "Projectiles.Magic";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 3;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 40;
		base.Projectile.height = 40;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.tileCollide = false;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 25;
		base.Projectile.timeLeft = 300;
		base.Projectile.alpha = 180;
	}

	public override void AI()
	{
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0296: Unknown result type (might be due to invalid IL or missing references)
		//IL_029b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0255: Unknown result type (might be due to invalid IL or missing references)
		//IL_0263: Unknown result type (might be due to invalid IL or missing references)
		//IL_026a: Unknown result type (might be due to invalid IL or missing references)
		//IL_026f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0304: Unknown result type (might be due to invalid IL or missing references)
		//IL_0342: Unknown result type (might be due to invalid IL or missing references)
		//IL_0344: Unknown result type (might be due to invalid IL or missing references)
		//IL_034e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0353: Unknown result type (might be due to invalid IL or missing references)
		//IL_0361: Unknown result type (might be due to invalid IL or missing references)
		//IL_0368: Unknown result type (might be due to invalid IL or missing references)
		//IL_036d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0371: Unknown result type (might be due to invalid IL or missing references)
		//IL_0376: Unknown result type (might be due to invalid IL or missing references)
		//IL_0383: Unknown result type (might be due to invalid IL or missing references)
		//IL_0388: Unknown result type (might be due to invalid IL or missing references)
		//IL_0317: Unknown result type (might be due to invalid IL or missing references)
		//IL_031c: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.rotation += (Math.Abs(base.Projectile.velocity.X) + Math.Abs(base.Projectile.velocity.Y)) * 0.01f * (float)base.Projectile.direction;
		if (base.Projectile.soundDelay == 0)
		{
			base.Projectile.soundDelay = 20 + Main.rand.Next(40);
			if (Main.rand.NextBool(5))
			{
				SoundEngine.PlaySound(in SoundID.Item9, base.Projectile.Center);
			}
		}
		if (Main.rand.NextBool(10))
		{
			Dust.NewDust(base.Projectile.position + base.Projectile.velocity, base.Projectile.width, base.Projectile.height, 262, base.Projectile.velocity.X * 0.5f, base.Projectile.velocity.Y * 0.5f, 0, default(Color), 0.75f);
		}
		base.Projectile.localAI[0]++;
		Vector2 playerCenter = Main.player[base.Projectile.owner].Center;
		float centerX = base.Projectile.Center.X;
		float centerY = base.Projectile.Center.Y;
		if (!initialized)
		{
			initialPosition = playerCenter;
			initialized = true;
		}
		else if (playerCenter != initialPosition)
		{
			playerCenter = initialPosition;
		}
		float num = playerCenter.X - centerX;
		float yDist = playerCenter.Y - centerY;
		float radius = (float)Math.Sqrt(num * num + yDist * yDist);
		if (base.Projectile.localAI[0] > 10f && base.Projectile.localAI[0] < 100f)
		{
			base.Projectile.ai[1] += 1f / 60f;
			if (base.Projectile.ai[1] > 0f)
			{
				base.Projectile.ai[0] += MathHelper.ToRadians(5f) / base.Projectile.ai[1];
				base.Projectile.Center = playerCenter + base.Projectile.ai[0].ToRotationVector2() * radius;
			}
		}
		if (!(base.Projectile.localAI[0] >= 100f))
		{
			return;
		}
		Vector2 center = base.Projectile.Center;
		float homingRange = 325f;
		bool homeIn = false;
		float inertia = 25f;
		float homingSpeed = 23f;
		ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
		while (enumerator.MoveNext())
		{
			NPC n = enumerator.Current;
			if (n.CanBeChasedBy(base.Projectile))
			{
				float extraDistance = (float)(n.width / 2) + (float)(n.height / 2);
				if (Vector2.Distance(n.Center, base.Projectile.Center) < homingRange + extraDistance)
				{
					center = n.Center;
					homeIn = true;
					break;
				}
			}
		}
		if (homeIn)
		{
			base.Projectile.extraUpdates = 1;
			Vector2 homeInVector = base.Projectile.SafeDirectionTo(center, Vector2.UnitY);
			base.Projectile.velocity = (base.Projectile.velocity * inertia + homeInVector * homingSpeed) / (inertia + 1f);
		}
		else
		{
			base.Projectile.extraUpdates = 0;
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.localAI[0] >= 100f)
		{
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Vector2.Zero, ModContent.ProjectileType<EventHorizonBlackhole>(), (int)((float)base.Projectile.damage * 1.5f), base.Projectile.knockBack * 0.5f, base.Projectile.owner);
			base.Projectile.Kill();
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		return false;
	}

	public override void PostDraw(Color lightColor)
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = TextureAssets.Projectile[base.Type].Value;
		Main.EntitySpriteDraw(texture, base.Projectile.Center - Main.screenPosition, (Rectangle?)new Rectangle(0, 0, texture.Width, texture.Height), new Color(255, 255, 255, 127), base.Projectile.rotation, texture.Size() / 2f, base.Projectile.scale, (SpriteEffects)0, 0f);
	}
}
