using System;
using CalamityMod.NPCs.VanillaNPCAIOverrides.Bosses.BrainOfCthulhu;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Boss.BrainOfCthulhu;

public class CirclingBloodScythe : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Boss";

	public override string Texture => "CalamityMod/Particles/VerticalSmearRagged";

	private static float RotationSpeed => (float)Math.PI / 8f;

	private static int Lifetime => BrainOfCthulhuAI.CrimsonEyeAttackDuration;

	private static float MaxCircleSpeed => (float)Math.PI / 30f;

	private ref float CircleAngle => ref base.Projectile.ai[0];

	private ref float CircleRadius => ref base.Projectile.ai[1];

	private ref float CircleRadiusVelocity => ref base.Projectile.ai[2];

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 16;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 300;
		base.Projectile.height = 300;
		base.Projectile.penetrate = -1;
		base.Projectile.Opacity = 1f;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft = Lifetime;
		base.Projectile.damage = 10;
		base.Projectile.scale = 0.1f;
		base.Projectile.hostile = true;
	}

	public override void OnSpawn(IEntitySource source)
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.rotation = CircleAngle;
		for (int i = 0; i < 3; i++)
		{
			GeneralParticleHandler.SpawnParticle(new BloodParticle(base.Projectile.Center, base.Projectile.velocity.RotatedBy(Main.rand.NextFloat(-(float)Math.PI / 6f, (float)Math.PI / 6f)) * Main.rand.NextFloat(0.5f, 1f), 32, 1f, Color.Red));
		}
		GeneralParticleHandler.SpawnParticle(new BloodParticle2(base.Projectile.Center, base.Projectile.velocity * 0.75f, 16, 0.5f, Color.Red));
	}

	public override void AI()
	{
		//IL_0243: Unknown result type (might be due to invalid IL or missing references)
		//IL_024f: Unknown result type (might be due to invalid IL or missing references)
		//IL_025b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0260: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0217: Unknown result type (might be due to invalid IL or missing references)
		if (NPC.crimsonBoss == -1)
		{
			base.Projectile.active = false;
			return;
		}
		int UpTime = Lifetime - base.Projectile.timeLeft;
		if ((float)UpTime < 30f)
		{
			CircleRadius = MathHelper.Lerp(0f, 128f, CalamityUtils.CircOutEasing((float)UpTime / 30f, 1));
		}
		else if (UpTime < Lifetime - 180)
		{
			CircleRadius = 128f;
		}
		else
		{
			if (UpTime == Lifetime - 180)
			{
				CircleRadiusVelocity = -3f;
			}
			CircleRadius += CircleRadiusVelocity;
			CircleRadiusVelocity += 0.1f;
		}
		if ((float)UpTime >= 15f)
		{
			if ((float)UpTime < 30f)
			{
				CircleAngle += MathHelper.Lerp(0f, MaxCircleSpeed, CalamityUtils.SineInEasing((float)(UpTime - 15) / 15f, 1));
			}
			else if (UpTime < 870)
			{
				CircleAngle += MaxCircleSpeed;
			}
			else if (UpTime < 900)
			{
				CircleAngle += MathHelper.Lerp(MaxCircleSpeed, MaxCircleSpeed / 3f, CalamityUtils.SineOutEasing((float)(UpTime - 870) / 30f, 1));
			}
			else
			{
				CircleAngle += MaxCircleSpeed / 3f;
			}
			if (Main.rand.NextBool(6))
			{
				GeneralParticleHandler.SpawnParticle(new BloodParticle(base.Projectile.Center + Main.rand.NextVector2CircularEdge(32f, 32f), (base.Projectile.DirectionTo(Main.npc[NPC.crimsonBoss].Center).RotatedBy(1.5707963705062866) * 16f).RotatedBy(Main.rand.NextFloat(-(float)Math.PI / 6f, (float)Math.PI / 6f)) * Main.rand.NextFloat(0.25f, 0.75f), Main.rand.Next(10, 17), 1f, Color.Red));
			}
		}
		NPC boss = Main.npc[NPC.crimsonBoss];
		base.Projectile.Center = boss.Center + CircleAngle.ToRotationVector2() * CircleRadius;
		base.Projectile.rotation += RotationSpeed;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		Main.spriteBatch.SetBlendState(BlendState.Additive);
		Texture2D tex = TextureAssets.Projectile[base.Type].Value;
		Vector2 drawPos = base.Projectile.Center - Main.screenPosition;
		Color drawColor = Color.Red;
		if (!ChildSafety.Disabled)
		{
			drawColor = Main.DiscoColor;
		}
		if (CalamityClientConfig.Instance.Afterimages)
		{
			for (int i = 0; i < base.Projectile.oldPos.Length; i++)
			{
				float afterimageRot = base.Projectile.oldRot[i];
				drawPos = base.Projectile.oldPos[i] + base.Projectile.Size / 2f - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY);
				if (i != 0)
				{
					drawColor *= 0.9f;
				}
				float interpolant = (float)(base.Projectile.oldPos.Length - i) / (float)base.Projectile.oldPos.Length;
				Main.spriteBatch.Draw(tex, drawPos, (Rectangle?)null, drawColor, afterimageRot, tex.Size() * 0.5f, base.Projectile.scale * interpolant, (SpriteEffects)0, 0f);
			}
		}
		else
		{
			Main.EntitySpriteDraw(tex, drawPos, tex.Frame(), drawColor, base.Projectile.rotation, tex.Size() * 0.5f, base.Projectile.scale, (SpriteEffects)0);
		}
		Main.spriteBatch.SetBlendState(BlendState.AlphaBlend);
		return false;
	}
}
