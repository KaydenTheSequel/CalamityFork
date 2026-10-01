using System;
using CalamityMod.Enums;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class DestructionBolt : ModProjectile, ILocalizedModType, IModType
{
	public float CenterX;

	public float CenterY;

	public float MouseX;

	public float MouseY;

	public int timerOffset;

	public new string LocalizationCategory => "Projectiles.Rogue";

	public ref float time => ref base.Projectile.ai[0];

	public override void SetDefaults()
	{
		base.Projectile.width = 12;
		base.Projectile.height = 12;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.extraUpdates = 3;
		base.Projectile.timeLeft = 600;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 15;
	}

	public override void AI()
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_028d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02db: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_026a: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0304: Unknown result type (might be due to invalid IL or missing references)
		//IL_0311: Unknown result type (might be due to invalid IL or missing references)
		//IL_0317: Unknown result type (might be due to invalid IL or missing references)
		//IL_0366: Unknown result type (might be due to invalid IL or missing references)
		//IL_0374: Unknown result type (might be due to invalid IL or missing references)
		//IL_0379: Unknown result type (might be due to invalid IL or missing references)
		//IL_0392: Unknown result type (might be due to invalid IL or missing references)
		//IL_0397: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a8: Unknown result type (might be due to invalid IL or missing references)
		Player Owner = Main.player[base.Projectile.owner];
		Projectile projectile = base.Projectile;
		projectile.velocity *= 0.99f;
		if (time == 0f)
		{
			MouseX = base.Projectile.Center.X;
			MouseY = base.Projectile.Center.Y;
			base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
			timerOffset = (int)(60f - base.Projectile.ai[1] * 15f);
			time += timerOffset;
		}
		if (time == (float)(180 + timerOffset))
		{
			Vector2 mouse = Owner.ClampedMouseWorld();
			MouseX = mouse.X;
			MouseY = mouse.Y;
		}
		else if (time < 180f)
		{
			MouseX = base.Projectile.Center.X;
			MouseY = base.Projectile.Center.Y;
		}
		if (time >= (float)(180 - timerOffset))
		{
			if (time == (float)(180 + timerOffset))
			{
				CenterX = base.Projectile.Center.X;
				CenterY = base.Projectile.Center.Y;
			}
			if (time >= (float)(180 + timerOffset))
			{
				base.Projectile.velocity = Vector2.Zero;
				base.Projectile.rotation = base.Projectile.rotation.AngleLerp((new Vector2(MouseX, MouseY) - base.Projectile.Center).SafeNormalize(Vector2.UnitX).ToRotation() + (float)Math.PI / 2f, 0.2f);
				base.Projectile.Center = new Vector2(MathHelper.Lerp(CenterX, MouseX, Utils.GetLerpValue(180 + timerOffset, 300f, time, clamped: true)), MathHelper.Lerp(CenterY, MouseY, Utils.GetLerpValue(180 + timerOffset, 300f, time, clamped: true)));
			}
		}
		else
		{
			base.Projectile.rotation = base.Projectile.rotation.AngleLerp(base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f, 0.08f);
		}
		if (Main.rand.NextBool(4))
		{
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center + Main.rand.NextVector2Circular(15f, 15f), Main.rand.NextBool(6) ? 278 : 263, -base.Projectile.velocity);
			dust.scale = ((dust.type == 278) ? Main.rand.NextFloat(0.3f, 0.6f) : Main.rand.NextFloat(0.6f, 1.4f));
			dust.velocity = -base.Projectile.velocity.RotatedByRandom(0.30000001192092896) * Main.rand.NextFloat(0.1f, 0.7f);
			dust.noGravity = true;
			dust.color = Color.LightGreen;
		}
		time++;
		if (time >= 300f)
		{
			base.Projectile.Kill();
		}
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		modifiers.SourceDamage *= 0.02f;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		if ((base.Projectile.ai[1] >= 4f && base.Projectile.ai[2] == 0f) || (base.Projectile.ai[1] == 4f && base.Projectile.ai[2] == 15f))
		{
			Projectile proj = Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Vector2.Zero, ModContent.ProjectileType<DestructionStar>(), base.Projectile.damage, base.Projectile.knockBack * 5f, base.Projectile.owner);
			if (base.Projectile.ai[2] >= 1f)
			{
				proj.Calamity().stealthStrike = true;
				proj.timeLeft = 240;
			}
			for (int i = 0; i < 2; i++)
			{
				Vector2 center = base.Projectile.Center;
				Vector2 zero = Vector2.Zero;
				Color lightGreen = Color.LightGreen;
				((Color)(ref lightGreen)).A = 0;
				CustomPulse customPulse = new CustomPulse(center, zero, lightGreen, "CalamityMod/Particles/BloomCircle", Vector2.One, Main.rand.NextFloat(-10f, 10f), base.Projectile.ai[1] * 0.5f, base.Projectile.ai[1] * 0.3f, 15, UseAdditiveBlend: false, 1f, fade: true, 1f, (SpriteEffects)0);
				GeneralParticleHandler.SpawnParticle(customPulse);
				customPulse.DrawLayer = GeneralDrawLayer.AfterEverything;
			}
		}
		else
		{
			for (int j = 0; j < 3; j++)
			{
				GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.Black, "CalamityMod/Particles/SmallBloom", Vector2.One, Main.rand.NextFloat(-10f, 10f), base.Projectile.ai[1] * 0.25f, base.Projectile.ai[1] * 0.2f, 10, UseAdditiveBlend: false, 1f, fade: true, 1f, (SpriteEffects)0));
			}
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		Texture2D tex = TextureAssets.Projectile[base.Type].Value;
		Asset<Texture2D> tex2 = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Rogue/DestructionBoltGhost", (AssetRequestMode)2);
		Asset<Texture2D> tex3 = ModContent.Request<Texture2D>("CalamityMod/Particles/BloomCircle", (AssetRequestMode)2);
		float fading = Utils.GetLerpValue(180f, 90f, time, clamped: true);
		float fading2 = Utils.GetLerpValue(240f, 180f, time, clamped: true);
		Main.EntitySpriteDraw(tex, base.Projectile.Center - Main.screenPosition, null, lightColor * fading, base.Projectile.rotation, tex.Size() / 2f, base.Projectile.scale, (SpriteEffects)0);
		for (int i = 0; i < 2; i++)
		{
			Texture2D value = tex2.Value;
			Vector2 position = base.Projectile.Center - Main.screenPosition;
			Color lightGreen = Color.LightGreen;
			((Color)(ref lightGreen)).A = 0;
			Main.EntitySpriteDraw(value, position, null, lightGreen * (1f - fading) * fading2, base.Projectile.rotation, tex2.Size() / 2f, base.Projectile.scale * fading2, (SpriteEffects)0);
			Texture2D value2 = tex3.Value;
			Vector2 position2 = base.Projectile.Center - Main.screenPosition;
			lightGreen = Color.LightGreen;
			((Color)(ref lightGreen)).A = 0;
			Main.EntitySpriteDraw(value2, position2, null, lightGreen * (1f - fading2), base.Projectile.rotation, tex3.Size() / 2f, base.Projectile.scale / 3f, (SpriteEffects)0);
		}
		return false;
	}
}
