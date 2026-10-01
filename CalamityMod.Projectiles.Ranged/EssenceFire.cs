using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class EssenceFire : ModProjectile, ILocalizedModType, IModType
{
	public int MistType = -1;

	public new string LocalizationCategory => "Projectiles.Ranged";

	public override string Texture => "CalamityMod/Projectiles/FireProj";

	public static int Lifetime => 96;

	public static int Fadetime => 80;

	public ref float Time => ref base.Projectile.ai[0];

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 10);
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.penetrate = 7;
		base.Projectile.MaxUpdates = 4;
		base.Projectile.timeLeft = Lifetime;
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = 3;
	}

	public override void AI()
	{
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_030b: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_032f: Unknown result type (might be due to invalid IL or missing references)
		//IL_033f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0358: Unknown result type (might be due to invalid IL or missing references)
		//IL_0365: Unknown result type (might be due to invalid IL or missing references)
		//IL_036b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0238: Unknown result type (might be due to invalid IL or missing references)
		//IL_0243: Unknown result type (might be due to invalid IL or missing references)
		//IL_0248: Unknown result type (might be due to invalid IL or missing references)
		//IL_0261: Unknown result type (might be due to invalid IL or missing references)
		//IL_0267: Unknown result type (might be due to invalid IL or missing references)
		//IL_026c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01de: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_0293: Unknown result type (might be due to invalid IL or missing references)
		//IL_028c: Unknown result type (might be due to invalid IL or missing references)
		//IL_029d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bb: Unknown result type (might be due to invalid IL or missing references)
		Time++;
		if (MistType == -1)
		{
			MistType = Main.rand.Next(3);
		}
		if (Time > (float)Fadetime)
		{
			Projectile projectile = base.Projectile;
			projectile.velocity *= 0.95f;
		}
		if (Time > 6f && Time < (float)Fadetime)
		{
			if (Main.rand.NextBool(16))
			{
				Dust dust = Dust.NewDustDirect(base.Projectile.Center + Main.rand.NextVector2Circular(60f, 60f) * Utils.Remap(Time, 0f, Fadetime, 0.5f, 1f), 4, 4, 295, base.Projectile.velocity.X * 0.2f, base.Projectile.velocity.Y * 0.2f, 100);
				if (Main.rand.NextBool(5))
				{
					dust.noGravity = true;
					dust.scale *= 2f;
					dust.velocity *= 0.8f;
				}
				dust.velocity *= 1.1f;
				dust.velocity += base.Projectile.velocity * Utils.Remap(Time, 0f, (float)Fadetime * 0.75f, 1f, 0.1f) * Utils.Remap(Time, 0f, (float)Fadetime * 0.1f, 0.1f, 1f);
			}
			if (Main.rand.NextBool(19))
			{
				Main.rand.NextBool();
				float size = Utils.Remap(Utils.GetLerpValue(0f, Lifetime, Time), 0.2f, 0.5f, 0.25f, 1f);
				GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center, base.Projectile.velocity + Vector2.UnitY * Main.rand.NextFloat(-10f, -24f) * size, "CalamityMod/Particles/BloomCircle", affectedByGravity: false, 14, 0.9f * size, (Main.rand.NextBool() ? Color.DarkBlue : Color.BlueViolet) * 0.5f, new Vector2(Main.rand.NextFloat(2f, 3f), 1f), useAddativeBlend: true, glowCenter: true, 0f, fadeIn: false, affectedByLight: false, 0.3f, 1f, 0.5f));
			}
		}
		else if (Time == 5f)
		{
			Dust dust2 = Dust.NewDustPerfect(base.Projectile.Center, Main.rand.NextBool(3) ? 295 : 181, base.Projectile.velocity.RotatedByRandom(MathHelper.ToRadians(30f)) * Main.rand.NextFloat(0.5f, 1f));
			dust2.scale = Main.rand.NextFloat(0.8f, 1.8f);
			dust2.noGravity = true;
			dust2.fadeIn = 0.5f;
		}
	}

	public override void ModifyDamageHitbox(ref Rectangle hitbox)
	{
		int size = (int)Utils.Remap(Time, 0f, Fadetime, 10f, 40f);
		if (Time > (float)Fadetime)
		{
			size = (int)Utils.Remap(Time, Fadetime, Lifetime, 40f, 0f);
		}
		((Rectangle)(ref hitbox)).Inflate(size, size);
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<GodSlayerInferno>(), 1200);
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		if (base.Projectile.numHits > 0)
		{
			base.Projectile.damage = (int)((float)base.Projectile.damage * 0.75f);
		}
		if (base.Projectile.damage < 1)
		{
			base.Projectile.damage = 1;
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		//IL_020c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		//IL_0216: Unknown result type (might be due to invalid IL or missing references)
		//IL_0262: Unknown result type (might be due to invalid IL or missing references)
		//IL_0269: Unknown result type (might be due to invalid IL or missing references)
		//IL_0270: Unknown result type (might be due to invalid IL or missing references)
		//IL_027a: Unknown result type (might be due to invalid IL or missing references)
		//IL_027f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0282: Unknown result type (might be due to invalid IL or missing references)
		//IL_0284: Unknown result type (might be due to invalid IL or missing references)
		//IL_0286: Unknown result type (might be due to invalid IL or missing references)
		//IL_0295: Unknown result type (might be due to invalid IL or missing references)
		//IL_029c: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_030e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0313: Unknown result type (might be due to invalid IL or missing references)
		//IL_0316: Unknown result type (might be due to invalid IL or missing references)
		//IL_0318: Unknown result type (might be due to invalid IL or missing references)
		//IL_031f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0321: Unknown result type (might be due to invalid IL or missing references)
		//IL_032b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0330: Unknown result type (might be due to invalid IL or missing references)
		//IL_033a: Unknown result type (might be due to invalid IL or missing references)
		//IL_033e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0340: Unknown result type (might be due to invalid IL or missing references)
		//IL_034a: Unknown result type (might be due to invalid IL or missing references)
		//IL_035d: Unknown result type (might be due to invalid IL or missing references)
		//IL_035f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0366: Unknown result type (might be due to invalid IL or missing references)
		//IL_0368: Unknown result type (might be due to invalid IL or missing references)
		//IL_0372: Unknown result type (might be due to invalid IL or missing references)
		//IL_0376: Unknown result type (might be due to invalid IL or missing references)
		//IL_0378: Unknown result type (might be due to invalid IL or missing references)
		//IL_0382: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		Texture2D fire = TextureAssets.Projectile[base.Type].Value;
		Texture2D mist = ModContent.Request<Texture2D>("CalamityMod/Particles/MediumMist", (AssetRequestMode)2).Value;
		Color color1 = default(Color);
		((Color)(ref color1))._002Ector(160, 100, 255, 200);
		Color color2 = default(Color);
		((Color)(ref color2))._002Ector(160, 50, 255, 70);
		Color color3 = default(Color);
		((Color)(ref color3))._002Ector(120, 100, 255, 100);
		Color color4 = default(Color);
		((Color)(ref color4))._002Ector(30, 50, 200, 100);
		float length = ((Time > (float)Fadetime - 10f) ? 0.1f : 0.15f);
		float vOffset = Math.Min(Time, 20f);
		float timeRatio = Utils.GetLerpValue(0f, Lifetime, Time);
		float fireSize = Utils.Remap(timeRatio, 0.2f, 0.5f, 0.25f, 1f);
		if (timeRatio >= 1f)
		{
			return false;
		}
		for (float j = 1f; j >= 0f; j -= length)
		{
			Color fireColor = ((timeRatio < 0.1f) ? Color.Lerp(Color.Transparent, color1, Utils.GetLerpValue(0f, 0.1f, timeRatio)) : ((timeRatio < 0.2f) ? Color.Lerp(color1, color2, Utils.GetLerpValue(0.1f, 0.2f, timeRatio)) : ((timeRatio < 0.35f) ? color2 : ((timeRatio < 0.7f) ? Color.Lerp(color2, color3, Utils.GetLerpValue(0.35f, 0.7f, timeRatio)) : ((timeRatio < 0.85f) ? Color.Lerp(color3, color4, Utils.GetLerpValue(0.7f, 0.85f, timeRatio)) : Color.Lerp(color4, Color.Transparent, Utils.GetLerpValue(0.85f, 1f, timeRatio)))))));
			fireColor *= (1f - j) * Utils.GetLerpValue(0f, 0.2f, timeRatio, clamped: true);
			Vector2 firePos = base.Projectile.Center - Main.screenPosition - base.Projectile.velocity * vOffset * j;
			float mainRot = ((0f - j) * ((float)Math.PI / 2f) - Main.GlobalTimeWrappedHourly * (j + 1f) * 2f / length) * (float)Math.Sign(base.Projectile.velocity.X);
			float trailRot = (float)Math.PI / 4f - mainRot;
			Vector2 trailOffset = base.Projectile.velocity * vOffset * length * 0.5f;
			Main.EntitySpriteDraw(fire, firePos - trailOffset, null, fireColor * 0.25f, trailRot, fire.Size() * 0.5f, fireSize, (SpriteEffects)0);
			Main.EntitySpriteDraw(fire, firePos, null, fireColor, mainRot, fire.Size() * 0.5f, fireSize, (SpriteEffects)0);
			if (MistType > 2 || MistType < 0)
			{
				return false;
			}
			Rectangle frame = mist.Frame(1, 3, 0, MistType);
			Rectangle? sourceRectangle = frame;
			Color color5 = Color.Lerp(fireColor, Color.White, 0.3f);
			((Color)(ref color5)).A = 0;
			Main.EntitySpriteDraw(mist, firePos, sourceRectangle, color5, mainRot, frame.Size() * 0.5f, fireSize, (SpriteEffects)0);
			Rectangle? sourceRectangle2 = frame;
			color5 = fireColor;
			((Color)(ref color5)).A = 0;
			Main.EntitySpriteDraw(mist, firePos, sourceRectangle2, color5, mainRot, frame.Size() * 0.5f, fireSize * 3f, (SpriteEffects)0);
		}
		return false;
	}
}
