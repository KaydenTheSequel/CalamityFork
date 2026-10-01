using System;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class EarthenTidesShockwave : ModProjectile, ILocalizedModType, IModType
{
	public Particle BloomRing;

	public new string LocalizationCategory => "Projectiles.Melee";

	public override string Texture => "CalamityMod/Projectiles/Melee/TrueBiomeBlade_EarthenTidesShockwave";

	public Player Owner => Main.player[base.Projectile.owner];

	public float Timer => (60f - (float)base.Projectile.timeLeft) / 100f;

	public ref float Size => ref base.Projectile.ai[0];

	public override void SetDefaults()
	{
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.width = (base.Projectile.height = 170);
		base.Projectile.tileCollide = false;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 60;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		return Collision.CheckAABBvAABBCollision(targetHitbox.TopLeft(), targetHitbox.Size(), base.Projectile.Center - projHitbox.Size() * base.Projectile.scale * 0.5f, projHitbox.Size() * base.Projectile.scale);
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01de: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_026c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0272: Unknown result type (might be due to invalid IL or missing references)
		//IL_027c: Unknown result type (might be due to invalid IL or missing references)
		//IL_028c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0291: Unknown result type (might be due to invalid IL or missing references)
		//IL_0297: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02be: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b7: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.velocity = Vector2.Zero;
		base.Projectile.scale = (1f + (float)Math.Sin((float)base.Projectile.timeLeft / 60f * (float)Math.PI) * 0.2f) * Size;
		if (base.Projectile.timeLeft == 60)
		{
			SoundEngine.PlaySound(in SoundID.DD2_ExplosiveTrapExplode, base.Projectile.Center);
			GeneralParticleHandler.SpawnParticle(new GenericSparkle(base.Projectile.Center, Vector2.Zero, Color.White, Main.rand.NextBool() ? Color.Aqua : Color.SpringGreen, base.Projectile.scale, 20, 0.2f, 2f));
			BloomRing = new BloomRing(base.Projectile.Center, Vector2.Zero, Color.Aqua, base.Projectile.scale, 50);
			GeneralParticleHandler.SpawnParticle(BloomRing);
			GeneralParticleHandler.SpawnParticle(new StrongBloom(base.Projectile.Center, Vector2.Zero, Main.rand.NextBool() ? (Color.Aqua * 0.6f) : (Color.SpringGreen * 0.6f), base.Projectile.scale * (1f + Main.rand.NextFloat(0f, 1.5f)), 20));
			for (int i = 0; i < 10; i++)
			{
				GeneralParticleHandler.SpawnParticle(new CritSpark(base.Projectile.Center, Main.rand.NextVector2Circular(1f, 1f) * Main.rand.NextFloat(17.5f, 25f) * base.Projectile.scale, Color.White, Main.rand.NextBool() ? Color.DarkSlateBlue : Color.Chocolate, 0.1f + Main.rand.NextFloat(0f, 1.5f), 20 + Main.rand.Next(30), 1f, 3f));
			}
			for (float i2 = 0f; i2 < 1f; i2 += 0.05f)
			{
				float rotation = i2 * ((float)Math.PI * 2f);
				GeneralParticleHandler.SpawnParticle(new CritSpark(base.Projectile.Center + rotation.ToRotationVector2() * 65f * base.Projectile.scale, rotation.ToRotationVector2() * 10f, Color.White, Main.rand.NextBool() ? Color.Aqua : Color.SpringGreen, 0.1f + Main.rand.NextFloat(0f, 1.5f), 20 + Main.rand.Next(30), 1f, 3f));
			}
		}
		if (BloomRing != null)
		{
			BloomRing.Scale = base.Projectile.scale;
		}
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		if (Owner.HeldItem.ModItem is OmegaBiomeBlade sword && Main.rand.NextFloat() <= OmegaBiomeBlade.ShockwaveAttunement_ShockwaveProc)
		{
			sword.OnHitProc = true;
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		Texture2D value = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Melee/TrueBiomeBlade_EarthenTidesShockwave", (AssetRequestMode)2).Value;
		float drawAngle = base.Projectile.rotation;
		int animFrame = 6 - (int)Math.Ceiling((float)base.Projectile.timeLeft / 10f);
		Rectangle frame = default(Rectangle);
		((Rectangle)(ref frame))._002Ector(0, animFrame * 168, 170, 166);
		Main.EntitySpriteDraw(position: base.Projectile.Center - Main.screenPosition, origin: frame.Size() / 2f, texture: value, sourceRectangle: frame, color: Color.White, rotation: drawAngle, scale: base.Projectile.scale, effects: (SpriteEffects)0);
		return false;
	}
}
