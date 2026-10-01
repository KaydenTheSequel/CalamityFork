using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.NPCs;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

[PierceResistException(true)]
public class AuroraFire : ModProjectile, ILocalizedModType, IModType
{
	public Color OrangeFogColor;

	public float OrangeFogRot;

	public float OrangeFogScale;

	public Color BlueFogColor;

	public float BlueFogRot;

	public float BlueFogScale;

	public float damageMult;

	public new string LocalizationCategory => "Projectiles.Ranged";

	public override string Texture => "CalamityMod/Projectiles/Magic/RancorFog";

	public static int Lifetime => 480;

	public static int Fadetime => 450;

	public ref float Time => ref base.Projectile.ai[0];

	public ref float LightPower => ref base.Projectile.ai[1];

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 64);
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.penetrate = -1;
		base.Projectile.MaxUpdates = 4;
		base.Projectile.timeLeft = Lifetime;
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = 8;
	}

	public override void AI()
	{
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_020b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0212: Unknown result type (might be due to invalid IL or missing references)
		//IL_021d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0227: Unknown result type (might be due to invalid IL or missing references)
		//IL_022c: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0303: Unknown result type (might be due to invalid IL or missing references)
		//IL_0313: Unknown result type (might be due to invalid IL or missing references)
		//IL_027e: Unknown result type (might be due to invalid IL or missing references)
		//IL_027f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0289: Unknown result type (might be due to invalid IL or missing references)
		//IL_028e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0296: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0393: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c5: Unknown result type (might be due to invalid IL or missing references)
		if (Time == 0f)
		{
			((Color)(ref OrangeFogColor)).B = (byte)Main.rand.Next(100, 181);
			OrangeFogScale = Main.rand.NextFloat(0.8f, 1f);
			OrangeFogRot = Main.rand.NextFloat((float)Math.PI * 2f);
			((Color)(ref BlueFogColor)).G = (byte)Main.rand.Next(120, 251);
			BlueFogScale = OrangeFogScale * Main.rand.NextFloat(0.9f, 1.1f);
			BlueFogRot = OrangeFogRot + MathHelper.ToRadians(Main.rand.NextFloat(30f, 330f));
		}
		Time++;
		int TurnRate = Lifetime / 5;
		if (Time % (float)TurnRate == (float)(TurnRate - 1))
		{
			base.Projectile.velocity = base.Projectile.velocity.RotatedBy(MathHelper.ToRadians(-216f));
			damageMult = 1f;
		}
		base.Projectile.rotation = base.Projectile.velocity.ToRotation();
		if (Time >= (float)Fadetime)
		{
			base.Projectile.scale = Utils.GetLerpValue(MathHelper.Lerp((float)Fadetime, (float)Lifetime, 0.5f), Fadetime, Time, clamped: true);
			if (base.Projectile.scale <= 0.01f)
			{
				base.Projectile.Kill();
			}
		}
		else
		{
			if (!(Time >= 6f))
			{
				return;
			}
			base.Projectile.scale = Utils.GetLerpValue(6f, 36f, Time, clamped: true);
		}
		float smokeRot = MathHelper.ToRadians(3f);
		Color smokeColor = Color.Lerp(OrangeFogColor, BlueFogColor, 0.6f + 0.4f * MathF.Sin(Main.GlobalTimeWrappedHourly * 5f));
		GeneralParticleHandler.SpawnParticle(new HeavySmokeParticle(base.Projectile.Center, base.Projectile.velocity * 0.5f, smokeColor, 8, base.Projectile.scale * Main.rand.NextFloat(0.6f, 1.2f), 0.8f, smokeRot, glowing: false, 0f, required: true));
		if (Main.rand.NextBool(8))
		{
			Color glowColor = Color.Lerp(smokeColor, Color.White, 0.25f);
			GeneralParticleHandler.SpawnParticle(new HeavySmokeParticle(base.Projectile.Center, base.Projectile.velocity * 0.5f, glowColor, 6, base.Projectile.scale * Main.rand.NextFloat(0.4f, 0.7f), 0.6f, smokeRot, glowing: true, 0.005f, required: true));
		}
		Lighting.AddLight(base.Projectile.Center, ((Color)(ref smokeColor)).ToVector3() * base.Projectile.scale);
		OrangeFogRot += MathHelper.ToRadians(1f);
		BlueFogRot -= MathHelper.ToRadians(1f);
		base.Projectile.Opacity = Utils.GetLerpValue(0f, 15f, Time, clamped: true) * Utils.GetLerpValue(450f, 360f, Time, clamped: true);
		if (!Main.dedServ)
		{
			Color color = Lighting.GetColor((int)base.Projectile.Center.X / 16, (int)base.Projectile.Center.Y / 16 + 6);
			Vector3 val = ((Color)(ref color)).ToVector3();
			float lightPowerBelow = ((Vector3)(ref val)).Length() / (float)Math.Sqrt(3.0);
			LightPower = MathHelper.Lerp(LightPower, lightPowerBelow, 0.15f);
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<AstralInfectionDebuff>(), 240);
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		modifiers.SourceDamage *= damageMult;
		damageMult *= 0.8f;
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(ModContent.BuffType<AstralInfectionDebuff>(), 240);
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		return CalamityUtils.CircularHitboxCollision(base.Projectile.Center, (float)base.Projectile.width * base.Projectile.scale * 0.5f, targetHitbox);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		Main.spriteBatch.SetBlendState(BlendState.Additive);
		Texture2D fog = TextureAssets.Projectile[base.Type].Value;
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition;
		float opacity = Utils.GetLerpValue(0f, 0.08f, LightPower, clamped: true) * base.Projectile.Opacity * 0.3f;
		Main.EntitySpriteDraw(fog, drawPosition, null, OrangeFogColor * opacity, base.Projectile.rotation + OrangeFogRot, fog.Size() * 0.5f, base.Projectile.scale * OrangeFogScale, (SpriteEffects)0);
		Main.EntitySpriteDraw(fog, drawPosition, null, BlueFogColor * opacity, base.Projectile.rotation + BlueFogRot, fog.Size() * 0.5f, base.Projectile.scale * BlueFogScale, (SpriteEffects)0);
		Main.spriteBatch.SetBlendState(BlendState.AlphaBlend);
		return false;
	}

	public AuroraFire()
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		OrangeFogColor = new Color(255, 160, 100);
		OrangeFogScale = 1f;
		BlueFogColor = new Color(150, 120, 255);
		BlueFogScale = 1f;
		damageMult = 1f;
		base._002Ector();
	}
}
