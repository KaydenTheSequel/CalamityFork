using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class RealityRuptureLance : ModProjectile, ILocalizedModType, IModType
{
	public static readonly SoundStyle Hitsound = new SoundStyle("CalamityMod/Sounds/NPCKilled/DevourerSegmentBreak2")
	{
		Volume = 0.6f,
		PitchVariance = 0.3f
	};

	public int Time;

	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Items/Weapons/Rogue/RealityRupture";

	public ref float Timer => ref base.Projectile.ai[0];

	public override void SetDefaults()
	{
		base.Projectile.width = 30;
		base.Projectile.height = 30;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = 8;
		base.Projectile.timeLeft = 900;
		base.Projectile.aiStyle = 0;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 50;
		base.Projectile.tileCollide = false;
		base.Projectile.extraUpdates = 5;
	}

	public override void AI()
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		Timer++;
		Time++;
		Lighting.AddLight(base.Projectile.Center + base.Projectile.velocity * 0.6f, 0.6f, 0.2f, 0.9f);
		float radiusFactor = MathHelper.Lerp(0f, 1f, Utils.GetLerpValue(10f, 50f, Time, clamped: true));
		for (int i = 0; i < 9; i++)
		{
			float offsetRotationAngle = base.Projectile.velocity.ToRotation() + (float)Time / 20f;
			float radius = (20f + (float)Math.Cos((float)Time / 3f) * 12f) * radiusFactor;
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center + offsetRotationAngle.ToRotationVector2().RotatedBy((float)i / 5f * ((float)Math.PI * 2f)) * radius, Main.rand.NextBool() ? 234 : 310);
			dust.noGravity = true;
			dust.velocity = base.Projectile.velocity * 0.8f;
			dust.scale = Main.rand.NextFloat(1.1f, 1.7f);
		}
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 4f;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = TextureAssets.Projectile[base.Type].Value;
		int frameHeight = texture.Height / Main.projFrames[base.Type];
		int frameY = frameHeight * base.Projectile.frame;
		float scale = base.Projectile.scale;
		float rotation = base.Projectile.rotation;
		Rectangle rectangle = default(Rectangle);
		((Rectangle)(ref rectangle))._002Ector(0, frameY, texture.Width, frameHeight);
		Vector2 origin = rectangle.Size() / 2f;
		Main.spriteBatch.Draw(texture, base.Projectile.Center - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY), (Rectangle?)rectangle, Color.White, rotation, origin, scale, (SpriteEffects)0, 0f);
		return false;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i <= 13; i++)
		{
			Vector2 sparkVelocity = base.Projectile.velocity.RotatedByRandom(0.25) * Main.rand.NextFloat(0.3f, 1.8f);
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center - base.Projectile.velocity * 0.5f, Main.rand.NextBool(4) ? 242 : 310, sparkVelocity, 0, default(Color), Main.rand.NextFloat(2.2f, 3.5f));
			dust.noGravity = true;
			dust.fadeIn = 0.5f;
		}
		SoundEngine.PlaySound(in Hitsound, base.Projectile.Center);
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		if (base.Projectile.numHits > 0)
		{
			base.Projectile.damage = (int)((float)base.Projectile.damage * 0.95f);
		}
		if (base.Projectile.damage < 1)
		{
			base.Projectile.damage = 1;
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i <= 9; i++)
		{
			Dust.NewDust(base.Projectile.position + base.Projectile.velocity, base.Projectile.width, base.Projectile.height, 272, base.Projectile.oldVelocity.X * 0.3f, base.Projectile.oldVelocity.Y * 0.3f, 0, default(Color), Main.rand.NextFloat(1.2f, 1.6f));
		}
	}
}
