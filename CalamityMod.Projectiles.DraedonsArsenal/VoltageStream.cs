using System;
using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.DraedonsArsenal;

public class VoltageStream : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Misc";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public float Time
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

	public NPC Target
	{
		get
		{
			return Main.npc[(int)base.Projectile.ai[1]];
		}
		set
		{
			base.Projectile.ai[1] = value.whoAmI;
		}
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 8;
		base.Projectile.height = 8;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.penetrate = 3;
		base.Projectile.timeLeft = 180;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 40;
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0305: Unknown result type (might be due to invalid IL or missing references)
		//IL_030f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0314: Unknown result type (might be due to invalid IL or missing references)
		//IL_032b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0331: Unknown result type (might be due to invalid IL or missing references)
		//IL_0342: Unknown result type (might be due to invalid IL or missing references)
		//IL_035b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0370: Unknown result type (might be due to invalid IL or missing references)
		//IL_0375: Unknown result type (might be due to invalid IL or missing references)
		//IL_037e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0385: Unknown result type (might be due to invalid IL or missing references)
		//IL_0398: Unknown result type (might be due to invalid IL or missing references)
		//IL_039e: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0246: Unknown result type (might be due to invalid IL or missing references)
		//IL_024d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0254: Unknown result type (might be due to invalid IL or missing references)
		//IL_0259: Unknown result type (might be due to invalid IL or missing references)
		//IL_0270: Unknown result type (might be due to invalid IL or missing references)
		//IL_0276: Unknown result type (might be due to invalid IL or missing references)
		//IL_0285: Unknown result type (might be due to invalid IL or missing references)
		//IL_028a: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c4: Unknown result type (might be due to invalid IL or missing references)
		Vector2 center = base.Projectile.Center;
		Color newColor = Color.SkyBlue;
		Lighting.AddLight(center, ((Color)(ref newColor)).ToVector3());
		if (!Target.active)
		{
			base.Projectile.Kill();
			return;
		}
		base.Projectile.Center = Target.Center;
		if (Time < 90f)
		{
			float completionRatio = Utils.GetLerpValue(0f, 90f, Time, clamped: true);
			float offsetRatioOnSprite = (float)Math.Sin(completionRatio * MathHelper.ToRadians(720f)) * 0.5f + 0.5f;
			Vector2 dustInitialPosition = Vector2.Lerp(Target.Top, Target.Bottom, offsetRatioOnSprite);
			for (int i = 0; i < 4; i++)
			{
				float angularOffset = (float)Math.PI / 2f * (float)i + Time / 90f * MathHelper.ToRadians(1080f);
				Vector2 dustSpawnPosition = dustInitialPosition + angularOffset.ToRotationVector2() * 4f;
				dustSpawnPosition.X += 10f * (float)Math.Sin(completionRatio * MathHelper.ToRadians(360f));
				Vector2 position = dustSpawnPosition;
				newColor = default(Color);
				Dust dust = Dust.NewDustPerfect(position, 261, null, 0, newColor);
				dust.velocity = Vector2.Zero;
				dust.noGravity = true;
				dustSpawnPosition = dustInitialPosition + angularOffset.ToRotationVector2() * 4f;
				dustSpawnPosition.X -= 10f * (float)Math.Sin(completionRatio * MathHelper.ToRadians(360f));
				Vector2 position2 = dustSpawnPosition;
				newColor = default(Color);
				Dust dust2 = Dust.NewDustPerfect(position2, 261, null, 0, newColor);
				dust2.velocity = Vector2.Zero;
				dust2.noGravity = true;
			}
		}
		else if (Time < 150f)
		{
			for (int j = 0; j < 50; j++)
			{
				float angle = (float)Math.PI / 25f * (float)j + Utils.GetLerpValue(90f, 150f, Time, clamped: true) * MathHelper.ToRadians(1080f);
				float radius = MathHelper.Lerp(0f, 25f, Utils.GetLerpValue(90f, 150f, Time, clamped: true));
				Vector2 position3 = Target.Center + angle.ToRotationVector2() * radius;
				newColor = default(Color);
				Dust dust3 = Dust.NewDustPerfect(position3, 226, null, 0, newColor);
				dust3.velocity = Vector2.Zero;
				if (Main.rand.NextBool(6))
				{
					dust3.velocity = Target.SafeDirectionTo(dust3.position) * 4.5f;
				}
				dust3.noGravity = true;
			}
		}
		else
		{
			for (int k = 0; k < 120; k++)
			{
				float angle2 = (float)Math.PI / 60f * (float)k;
				Vector2 position4 = Target.Center + angle2.ToRotationVector2() * 25f;
				newColor = default(Color);
				Dust dust4 = Dust.NewDustPerfect(position4, 226, null, 0, newColor);
				dust4.velocity = angle2.ToRotationVector2() * Main.rand.NextFloat(2f, 9f) * (float)Main.rand.NextBool().ToDirectionInt();
				dust4.velocity = dust4.velocity.RotatedBy(dust4.velocity.ToRotation() * -0.02f);
				dust4.velocity *= 2.1f;
				dust4.noGravity = true;
			}
			Target.AddBuff(ModContent.BuffType<StaticDischarge>(), 180);
			base.Projectile.Kill();
		}
		Time++;
		if (base.Projectile.damage <= 0)
		{
			base.Projectile.Kill();
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		base.Projectile.damage = (int)((double)base.Projectile.damage * 0.75);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		base.Projectile.damage = (int)((double)base.Projectile.damage * 0.75);
	}

	public override bool? CanHitNPC(NPC target)
	{
		if (base.Projectile.ai[1] == (float)target.whoAmI)
		{
			return null;
		}
		return false;
	}
}
