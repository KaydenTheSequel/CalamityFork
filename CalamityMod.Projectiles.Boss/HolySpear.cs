using System;
using System.IO;
using CalamityMod.NPCs.Providence;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Boss;

public class HolySpear : ModProjectile, ILocalizedModType, IModType
{
	private Vector2 velocity;

	public new string LocalizationCategory => "Projectiles.Boss";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 15;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.Calamity().DealsDefenseDamage = true;
		base.Projectile.width = 30;
		base.Projectile.height = 30;
		base.Projectile.hostile = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 200;
		base.CooldownSlot = 1;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		writer.Write(base.Projectile.localAI[0]);
		writer.Write(base.Projectile.localAI[1]);
		writer.WriteVector2(velocity);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.localAI[0] = reader.ReadSingle();
		base.Projectile.localAI[1] = reader.ReadSingle();
		velocity = reader.ReadVector2();
	}

	public override void AI()
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0382: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_023a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0241: Unknown result type (might be due to invalid IL or missing references)
		//IL_0246: Unknown result type (might be due to invalid IL or missing references)
		//IL_0532: Unknown result type (might be due to invalid IL or missing references)
		//IL_054b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0559: Unknown result type (might be due to invalid IL or missing references)
		//IL_055e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0569: Unknown result type (might be due to invalid IL or missing references)
		//IL_05bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b4: Unknown result type (might be due to invalid IL or missing references)
		ProvUtils.ApplyGFBDamage(base.Projectile, 120, 10);
		Lighting.AddLight(base.Projectile.Center, 0.45f * base.Projectile.Opacity, 0.35f * base.Projectile.Opacity, 0f);
		if (base.Projectile.localAI[0] == 0f)
		{
			base.Projectile.localAI[0] = 1f;
			if (base.Projectile.ai[0] == 1f)
			{
				velocity = base.Projectile.velocity;
			}
		}
		bool commanderSpear = base.Projectile.ai[0] == -1f;
		bool enragedCommanderSpear = base.Projectile.ai[0] == -2f;
		float timeGateValue = ((!ProvUtils.StandardAI()) ? 420f : ((commanderSpear | enragedCommanderSpear) ? 360f : 540f));
		if (base.Projectile.ai[0] <= 0f)
		{
			base.Projectile.ai[1]++;
			float slowGateValue = ((!ProvUtils.StandardAI()) ? 60f : ((commanderSpear | enragedCommanderSpear) ? 30f : 90f));
			float fastGateValue = 30f;
			float minVelocity = ((!ProvUtils.StandardAI()) ? 4f : (enragedCommanderSpear ? 6f : (commanderSpear ? 4.5f : 3f)));
			float maxVelocity = minVelocity * 4f;
			float extremeVelocity = maxVelocity * 2f;
			float deceleration = 0.95f;
			float acceleration = 1.2f;
			if (base.Projectile.localAI[1] >= timeGateValue)
			{
				if (((Vector2)(ref base.Projectile.velocity)).Length() < extremeVelocity)
				{
					Projectile projectile = base.Projectile;
					projectile.velocity *= acceleration;
				}
			}
			else if (base.Projectile.ai[1] <= slowGateValue)
			{
				if (((Vector2)(ref base.Projectile.velocity)).Length() > minVelocity)
				{
					Projectile projectile2 = base.Projectile;
					projectile2.velocity *= deceleration;
				}
			}
			else if (base.Projectile.ai[1] < slowGateValue + fastGateValue)
			{
				if (((Vector2)(ref base.Projectile.velocity)).Length() < maxVelocity)
				{
					Projectile projectile3 = base.Projectile;
					projectile3.velocity *= acceleration;
				}
			}
			else
			{
				base.Projectile.ai[1] = 0f;
			}
		}
		else
		{
			float frequency = ((!ProvUtils.StandardAI()) ? 0.2f : 0.1f);
			float amplitude = ((!ProvUtils.StandardAI()) ? 4f : 2f);
			base.Projectile.ai[1] += frequency;
			float wavyVelocity = (float)Math.Sin(base.Projectile.ai[1]);
			base.Projectile.velocity = velocity + Utils.RotatedBy(new Vector2(wavyVelocity, wavyVelocity), (double)MathHelper.ToRadians(velocity.ToRotation()), default(Vector2)) * amplitude;
		}
		if (base.Projectile.localAI[1] < timeGateValue)
		{
			base.Projectile.localAI[1]++;
			if (base.Projectile.timeLeft < 160)
			{
				base.Projectile.timeLeft = 160;
			}
		}
		base.Projectile.Opacity = MathHelper.Lerp(240f, 220f, (float)base.Projectile.timeLeft);
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		bool aimedSpear = base.Projectile.ai[0] > 0f;
		Color baseColor = default(Color);
		((Color)(ref baseColor))._002Ector(255, aimedSpear ? 255 : 125, aimedSpear ? 25 : 125);
		if (Main.zenithWorld)
		{
			if (Main.GlobalTimeWrappedHourly % 6f >= 5f)
			{
				((Color)(ref baseColor))._002Ector(aimedSpear ? 125 : 255, (!aimedSpear) ? 255 : 0, 125);
			}
			else if (Main.GlobalTimeWrappedHourly % 6f >= 4f)
			{
				((Color)(ref baseColor))._002Ector(aimedSpear ? 100 : 175, aimedSpear ? 255 : 175, 255);
			}
			else if (Main.GlobalTimeWrappedHourly % 6f >= 3f)
			{
				((Color)(ref baseColor))._002Ector(0, 255, (!aimedSpear) ? 175 : 0);
			}
			else if (Main.GlobalTimeWrappedHourly % 6f >= 2f)
			{
				((Color)(ref baseColor))._002Ector(255, aimedSpear ? 255 : 125, aimedSpear ? 25 : 125);
			}
			else if (Main.GlobalTimeWrappedHourly % 6f >= 1f)
			{
				((Color)(ref baseColor))._002Ector(255, 125, (!aimedSpear) ? 175 : 0);
			}
			else
			{
				((Color)(ref baseColor))._002Ector(255, (!aimedSpear) ? 125 : 0, (!aimedSpear) ? 255 : 0);
			}
		}
		else if (!ProvUtils.StandardAI())
		{
			((Color)(ref baseColor))._002Ector(aimedSpear ? 100 : 175, aimedSpear ? 255 : 175, 255);
		}
		GeneralParticleHandler.SpawnParticle(new SparkParticle(base.Projectile.Center + Utils.RotatedByRandom(new Vector2(Main.rand.NextFloat(20f), 0f), 6.2831854820251465), base.Projectile.velocity, affectedByGravity: false, 15, Main.rand.NextFloat(0.5f, 1.5f) * MathHelper.Clamp(((Vector2)(ref base.Projectile.velocity)).Length() / 15f, 0f, 2f), aimedSpear ? baseColor : ProvUtils.GetProjectileColor(255)));
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_0246: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0257: Unknown result type (might be due to invalid IL or missing references)
		//IL_0342: Unknown result type (might be due to invalid IL or missing references)
		//IL_0351: Unknown result type (might be due to invalid IL or missing references)
		//IL_0360: Unknown result type (might be due to invalid IL or missing references)
		//IL_0366: Unknown result type (might be due to invalid IL or missing references)
		//IL_0368: Unknown result type (might be due to invalid IL or missing references)
		//IL_036d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0372: Unknown result type (might be due to invalid IL or missing references)
		//IL_0374: Unknown result type (might be due to invalid IL or missing references)
		//IL_0383: Unknown result type (might be due to invalid IL or missing references)
		//IL_0384: Unknown result type (might be due to invalid IL or missing references)
		//IL_038a: Unknown result type (might be due to invalid IL or missing references)
		//IL_039a: Unknown result type (might be due to invalid IL or missing references)
		//IL_039c: Unknown result type (might be due to invalid IL or missing references)
		//IL_039e: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_03dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_027e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0283: Unknown result type (might be due to invalid IL or missing references)
		//IL_0285: Unknown result type (might be due to invalid IL or missing references)
		//IL_0290: Unknown result type (might be due to invalid IL or missing references)
		//IL_029a: Unknown result type (might be due to invalid IL or missing references)
		//IL_029f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_02af: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0307: Unknown result type (might be due to invalid IL or missing references)
		//IL_030c: Unknown result type (might be due to invalid IL or missing references)
		Texture2D drawTexture = TextureAssets.Projectile[base.Type].Value;
		bool aimedSpear = base.Projectile.ai[0] > 0f;
		Color baseColor = default(Color);
		((Color)(ref baseColor))._002Ector(255, aimedSpear ? 255 : 125, aimedSpear ? 25 : 125);
		Color baseColor2 = default(Color);
		((Color)(ref baseColor2))._002Ector(15, 35, 50, 0);
		if (Main.zenithWorld)
		{
			if (Main.GlobalTimeWrappedHourly % 6f >= 5f)
			{
				((Color)(ref baseColor))._002Ector(aimedSpear ? 125 : 255, (!aimedSpear) ? 255 : 0, 125);
			}
			else if (Main.GlobalTimeWrappedHourly % 6f >= 4f)
			{
				((Color)(ref baseColor))._002Ector(aimedSpear ? 100 : 175, aimedSpear ? 255 : 175, 255);
			}
			else if (Main.GlobalTimeWrappedHourly % 6f >= 3f)
			{
				((Color)(ref baseColor))._002Ector(0, 255, (!aimedSpear) ? 175 : 0);
			}
			else if (Main.GlobalTimeWrappedHourly % 6f >= 2f)
			{
				((Color)(ref baseColor))._002Ector(255, aimedSpear ? 255 : 125, aimedSpear ? 25 : 125);
			}
			else if (Main.GlobalTimeWrappedHourly % 6f >= 1f)
			{
				((Color)(ref baseColor))._002Ector(255, 125, (!aimedSpear) ? 175 : 0);
			}
			else
			{
				((Color)(ref baseColor))._002Ector(255, (!aimedSpear) ? 125 : 0, (!aimedSpear) ? 255 : 0);
			}
		}
		else if (!ProvUtils.StandardAI())
		{
			((Color)(ref baseColor))._002Ector(aimedSpear ? 100 : 175, aimedSpear ? 255 : 175, 255);
		}
		if (!aimedSpear)
		{
			baseColor = ProvUtils.GetProjectileColor(0);
			baseColor2 = ProvUtils.GetProjectileColor(0, Outline: true);
		}
		Vector2 projDirection = Main.screenPosition - new Vector2(0f, base.Projectile.gfxOffY);
		Vector2 halfTextureSize = drawTexture.Size() / 2f;
		_ = baseColor2 * 0.5f;
		float squish = MathHelper.Clamp(((Vector2)(ref base.Projectile.velocity)).Length() / 20f, -0.3f, 0.3f);
		Vector2 sc = default(Vector2);
		((Vector2)(ref sc))._002Ector(1f - squish, 1f + squish);
		SpriteEffects spriteEffects = (SpriteEffects)0;
		if (base.Projectile.spriteDirection == -1)
		{
			spriteEffects = (SpriteEffects)1;
		}
		if (CalamityClientConfig.Instance.Afterimages)
		{
			for (int i = 0; i < base.Projectile.oldPos.Length; i++)
			{
				Main.EntitySpriteDraw(drawTexture, base.Projectile.oldPos[i] - projDirection + base.Projectile.Size / 2f, null, Color.Lerp(baseColor, baseColor2, MathHelper.Lerp((float)i / (float)base.Projectile.oldPos.Length, 1f, 0.5f)), base.Projectile.rotation, halfTextureSize, sc * MathHelper.Lerp(1f, 0f, (float)i / (float)base.Projectile.oldPos.Length), spriteEffects);
			}
		}
		for (float i2 = 0f; i2 < 360f; i2 += 90f)
		{
			Main.EntitySpriteDraw(drawTexture, base.Projectile.Center + Utils.RotatedBy(new Vector2(4f, 0f), (double)MathHelper.ToRadians(i2), default(Vector2)) - projDirection, null, Color.Lerp(baseColor, baseColor2, 0.75f), base.Projectile.rotation, halfTextureSize, sc, spriteEffects);
		}
		Main.EntitySpriteDraw(drawTexture, base.Projectile.Center - projDirection, null, baseColor, base.Projectile.rotation, halfTextureSize, sc, spriteEffects);
		return false;
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		if (info.Damage > 0 && !target.creativeGodMode)
		{
			ProvUtils.ApplyDebuffs(target, 120);
		}
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		return ProvUtils.GetProjectileColor(0);
	}

	public HolySpear()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		velocity = Vector2.Zero;
		base._002Ector();
	}
}
