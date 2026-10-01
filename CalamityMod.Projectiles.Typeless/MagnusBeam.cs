using System;
using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.Dusts;
using CalamityMod.Graphics.Primitives;
using CalamityMod.Items.Weapons.Typeless;
using CalamityMod.Particles;
using CalamityMod.Projectiles.Healing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Typeless;

public class MagnusBeam : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Typeless";

	public ref float ProximityFactor => ref base.Projectile.ai[1];

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 30;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 16);
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = 1;
		base.Projectile.MaxUpdates = 4;
		base.Projectile.timeLeft = 120 * base.Projectile.MaxUpdates;
	}

	public override void AI()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0210: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_0221: Unknown result type (might be due to invalid IL or missing references)
		//IL_0226: Unknown result type (might be due to invalid IL or missing references)
		//IL_022d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0234: Unknown result type (might be due to invalid IL or missing references)
		//IL_023e: Unknown result type (might be due to invalid IL or missing references)
		//IL_024e: Unknown result type (might be due to invalid IL or missing references)
		//IL_025e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0268: Unknown result type (might be due to invalid IL or missing references)
		//IL_0275: Unknown result type (might be due to invalid IL or missing references)
		//IL_027b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02da: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0301: Unknown result type (might be due to invalid IL or missing references)
		//IL_0306: Unknown result type (might be due to invalid IL or missing references)
		//IL_0307: Unknown result type (might be due to invalid IL or missing references)
		//IL_0320: Unknown result type (might be due to invalid IL or missing references)
		//IL_032d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0333: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.rotation = base.Projectile.velocity.ToRotation();
		if (base.Projectile.ai[0] == 0f)
		{
			for (int i = 0; i < 6; i++)
			{
				Vector2 velocity = ((float)Math.PI * 2f * (float)i / 6f + base.Projectile.rotation + MathHelper.ToRadians(30f)).ToRotationVector2() * 0.8f;
				Color crossColor = ((i % 2 == 1) ? Color.MidnightBlue : Color.CornflowerBlue);
				GeneralParticleHandler.SpawnParticle(new GlowSparkParticle(base.Projectile.Center, velocity, affectedByGravity: false, 6, 0.015f, crossColor, Vector2.One, quickShrink: true));
			}
			base.Projectile.ai[0] = 1f;
		}
		Color trailColor = Color.CornflowerBlue;
		float range = 320f;
		int targetNPC = -1;
		ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
		while (enumerator.MoveNext())
		{
			NPC target = enumerator.Current;
			if (target.CanBeChasedBy(base.Projectile))
			{
				float distance = Vector2.Distance(target.Center, base.Projectile.Center);
				if (distance < range && Collision.CanHit(base.Projectile, target))
				{
					range = distance;
					targetNPC = target.whoAmI;
				}
			}
		}
		if (targetNPC > -1)
		{
			NPC target2 = Main.npc[targetNPC];
			Vector2 idealVelocity = base.Projectile.SafeDirectionTo(target2.Center) * 12f;
			base.Projectile.velocity = (base.Projectile.velocity * 29f + idealVelocity) / 30f;
			base.Projectile.velocity = base.Projectile.velocity.MoveTowards(idealVelocity, 1f);
			ProximityFactor = Utils.GetLerpValue(320f, 0f, Vector2.Distance(base.Projectile.Center, target2.Center), clamped: true);
		}
		trailColor = Color.Lerp(Color.CornflowerBlue, Color.Magenta, ProximityFactor);
		Lighting.AddLight(base.Projectile.Center, ((Color)(ref trailColor)).ToVector3() * 0.5f);
		Dust dust = Dust.NewDustPerfect(base.Projectile.Center, ModContent.DustType<LightDust>(), base.Projectile.velocity * 0.05f);
		dust.noGravity = true;
		dust.scale = Main.rand.NextFloat(0.8f, 1f);
		dust.color = trailColor;
		Vector2 sinOffset = (Vector2.UnitY * MathF.Sin((float)base.Projectile.timeLeft * (float)Math.PI * 0.05f) * 24f).RotatedBy(base.Projectile.rotation);
		Dust dust2 = Dust.NewDustPerfect(base.Projectile.Center + sinOffset, 175, Main.rand.NextVector2Circular(0.2f, 0.2f));
		dust2.noGravity = true;
		dust2.scale = Main.rand.NextFloat(1.2f, 1.8f);
		dust2.alpha = Main.rand.Next(120, 181);
	}

	internal float WidthFunction(float completionRatio, Vector2 vertexPos)
	{
		return base.Projectile.scale * 24f;
	}

	internal Color ColorFunction(float completionRatio, Vector2 vertexPos)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		Vector3 val = Main.rgbToHsl(Color.Lerp(Color.CornflowerBlue, Color.Magenta, ProximityFactor));
		Vector3 endColor = val + new Vector3(0.1f + MathF.Sin(Main.GlobalTimeWrappedHourly * 5f) * 0.05f, 0f, 0.1f);
		return Main.hslToRgb(Vector3.Lerp(val, endColor, Utils.GetLerpValue(0f, 0.72f, completionRatio, clamped: true))) * Utils.GetLerpValue(0.8f, 0.54f, completionRatio, clamped: true) * base.Projectile.Opacity;
	}

	public override void PostDraw(Color lightColor)
	{
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		GameShaders.Misc["CalamityMod:ImpFlameTrail"].SetShaderTexture(ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Trails/ScarletDevilStreak", (AssetRequestMode)2));
		PrimitiveRenderer.RenderTrail(base.Projectile.oldPos, new PrimitiveSettings(WidthFunction, ColorFunction, delegate
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			return base.Projectile.Size * 0.5f;
		}, smoothen: true, pixelate: false, GameShaders.Misc["CalamityMod:ImpFlameTrail"]), 30);
		Texture2D glow = TextureAssets.Projectile[base.Type].Value;
		Main.EntitySpriteDraw(glow, base.Projectile.Center - Main.screenPosition, null, Color.White, base.Projectile.rotation, glow.Size() * 0.5f, base.Projectile.scale, (SpriteEffects)0);
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<MarkedforDeath>(), 480);
		Player obj = Main.player[base.Projectile.owner];
		obj.statMana += 25;
		obj.ManaEffect(25);
		Main.player[base.Projectile.owner].SpawnLifeStealProjectile(target, base.Projectile, ModContent.ProjectileType<RoyalHeal>(), (int)Math.Round((double)hit.Damage * 0.1), 0.75f);
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in EyeofMagnus.ImpactSound, base.Projectile.Center);
		if (base.Projectile.owner == Main.myPlayer)
		{
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Vector2.Zero, ModContent.ProjectileType<MagnusBoom>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner);
		}
	}
}
