using System;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Boss.BrainOfCthulhu;

public class BloodBombRTS : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Boss";

	public override string Texture => "CalamityMod/Projectiles/Boss/BrainOfCthulhu/BloodBomb";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 8;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 30;
		base.Projectile.height = 30;
		base.Projectile.penetrate = 1;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.damage = 10;
		base.Projectile.friendly = true;
		base.Projectile.hostile = false;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft = 360;
	}

	public override void AI()
	{
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		NPC npc = Main.npc[(int)base.Projectile.ai[0]];
		if (npc.active && !npc.dontTakeDamage)
		{
			base.Projectile.velocity = base.Projectile.DirectionTo(npc.Center) * 16f;
		}
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		modifiers.SetCrit();
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.DD2_ExplosiveTrapExplode, base.Projectile.Center);
		GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.OrangeRed, "CalamityMod/Particles/FlameExplosion", Vector2.One, Main.rand.NextFloat(0f, (float)Math.PI), 0f, 0.1f, 24, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
		GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.Red, "CalamityMod/Particles/FlameExplosion", Vector2.One, Main.rand.NextFloat(0f, (float)Math.PI), 0f, 0.075f, 24, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		Texture2D tex = TextureAssets.Projectile[base.Type].Value;
		Texture2D overlayTex = BloodBomb.BloodBombYellow.Value;
		for (int i = 0; i < ((!CalamityClientConfig.Instance.Afterimages) ? 1 : base.Projectile.oldPos.Length); i++)
		{
			float time = Main.GlobalTimeWrappedHourly * 30f - (float)i / 2f;
			float lerp = MathF.Sin(time) / 2f + 0.5f;
			Color color = Color.White;
			float afterimageRot = base.Projectile.oldRot[i];
			Vector2 drawPos = base.Projectile.oldPos[i] + base.Projectile.Size / 2f - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY);
			if (i != 0)
			{
				color *= 0.75f;
			}
			float interpolant = (float)(base.Projectile.oldPos.Length - i) / (float)base.Projectile.oldPos.Length;
			Main.spriteBatch.Draw(tex, drawPos, (Rectangle?)null, color * (1f - lerp), afterimageRot, tex.Size() * 0.5f, (base.Projectile.scale + MathF.Sin(time) / 2f + 0.5f) * interpolant, (SpriteEffects)0, 0f);
			Main.spriteBatch.Draw(overlayTex, drawPos, (Rectangle?)null, color * lerp, afterimageRot, overlayTex.Size() * 0.5f, (base.Projectile.scale + MathF.Sin(time) / 2f + 0.5f) * interpolant, (SpriteEffects)0, 0f);
		}
		return false;
	}
}
