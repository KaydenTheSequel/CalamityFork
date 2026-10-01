using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Graphics.Primitives;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class TauCannonBolt : ModProjectile, ILocalizedModType, IModType
{
	public Color turquoiseColor;

	public Color coralColor;

	public new string LocalizationCategory => "Projectiles.Ranged";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 8;
	}

	public override void SetDefaults()
	{
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.width = (base.Projectile.height = 32);
		base.Projectile.timeLeft = 600;
		base.Projectile.tileCollide = false;
		base.Projectile.friendly = true;
		base.Projectile.extraUpdates = 1;
	}

	public override void AI()
	{
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		if (!Main.dedServ)
		{
			if (Main.rand.NextBool(10))
			{
				Vector2 position = base.Projectile.position;
				int width = base.Projectile.width;
				int height = base.Projectile.height;
				float scale = Main.rand.NextFloat(0.5f, 0.8f);
				Dust dust = Dust.NewDustDirect(position, width, height, 278, 0f, 0f, 0, default(Color), scale);
				dust.noGravity = true;
				dust.color = (Main.rand.NextBool(3) ? coralColor : turquoiseColor);
			}
			GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center, base.Projectile.velocity, "CalamityMod/Projectiles/StarProj", affectedByGravity: false, 2, 1f, Color.Lerp(turquoiseColor, Color.White, 0.7f), new Vector2(1f, 1f)));
			if (base.Projectile.ai[2] == 5f)
			{
				CalamityUtils.HomeInOnNPC(base.Projectile, ignoreTiles: true, 500f, 15f, 50f);
			}
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		if (!Main.dedServ)
		{
			int dustAmount = Main.rand.Next(5, 10);
			for (int i = 0; i < dustAmount; i++)
			{
				Vector2 center = base.Projectile.Center;
				Vector2? velocity = ((float)Math.PI * 2f / (float)dustAmount * (float)i).ToRotationVector2() * Main.rand.NextFloat(5f, 8f);
				float scale = Main.rand.NextFloat(0.6f, 1f);
				Dust dust = Dust.NewDustPerfect(center, 278, velocity, 0, default(Color), scale);
				dust.noGravity = true;
				dust.color = (Main.rand.NextBool(3) ? coralColor : turquoiseColor);
			}
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<AstralInfectionDebuff>(), 60);
	}

	private float WidthFunction(float completionRatio, Vector2 vertexPos)
	{
		return base.Projectile.scale * 32f * CalamityUtils.Convert01To010(completionRatio);
	}

	private Color ColorFunction(float completionRatio, Vector2 vertexPos)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		return Color.Lerp(turquoiseColor, Color.Transparent, completionRatio) * Utils.GetLerpValue(0f, 30f, base.Projectile.timeLeft, clamped: true);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		GameShaders.Misc["CalamityMod:TrailStreak"].SetShaderTexture(ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Trails/ZapTrail", (AssetRequestMode)2));
		PrimitiveRenderer.RenderTrail(base.Projectile.oldPos, new PrimitiveSettings(WidthFunction, ColorFunction, delegate
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			return base.Projectile.Size * 0.5f;
		}, smoothen: true, pixelate: false, GameShaders.Misc["CalamityMod:TrailStreak"]));
		return false;
	}

	public TauCannonBolt()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		turquoiseColor = Color.MediumTurquoise;
		coralColor = Color.Coral;
		base._002Ector();
	}
}
