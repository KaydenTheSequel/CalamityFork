using System;
using CalamityMod.Particles;
using CalamityMod.Projectiles.Melee;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class SpearofDestinyStealth : ModProjectile, ILocalizedModType, IModType
{
	public static readonly SoundStyle Hitsound = new SoundStyle("CalamityMod/Sounds/Item/BlazingCoreParry")
	{
		Volume = 0.7f,
		PitchVariance = 0.3f
	};

	public bool posthit;

	public int Time;

	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Projectiles/Rogue/LanceofDestiny";

	public ref float Timer => ref base.Projectile.ai[0];

	public override void SetDefaults()
	{
		base.Projectile.width = 23;
		base.Projectile.height = 23;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 900;
		base.Projectile.aiStyle = 0;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 50;
		base.Projectile.tileCollide = false;
		base.Projectile.extraUpdates = 3;
	}

	public override void AI()
	{
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		Timer++;
		Time++;
		base.Projectile.scale = 1.5f;
		Projectile projectile = base.Projectile;
		projectile.velocity *= 1.02f;
		if (!base.Projectile.Calamity().stealthStrike)
		{
			base.Projectile.extraUpdates = 1;
		}
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 4f;
		float radiusFactor = MathHelper.Lerp(0f, 1f, Utils.GetLerpValue(10f, 50f, Time, clamped: true));
		for (int i = 0; i < 10; i++)
		{
			float offsetRotationAngle = base.Projectile.velocity.ToRotation() + (float)Time / 20f;
			float radius = (50f + (float)Math.Cos((float)Time / 3f) * 12f) * radiusFactor;
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center + offsetRotationAngle.ToRotationVector2().RotatedBy((float)i / 5f * ((float)Math.PI * 2f)) * radius, Main.rand.NextBool() ? 279 : 159);
			dust.noGravity = true;
			dust.velocity = base.Projectile.velocity * 0.5f;
			dust.scale = 1.3f;
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in Hitsound, base.Projectile.position);
		posthit = true;
		float numberOfDusts = 35f;
		float rotFactor = 360f / numberOfDusts;
		for (int i = 0; (float)i < numberOfDusts; i++)
		{
			float rot = MathHelper.ToRadians((float)i * rotFactor);
			Vector2 offset = Utils.RotatedBy(new Vector2(18f, 0f), (double)rot, default(Vector2));
			Vector2 velOffset = Utils.RotatedBy(new Vector2(9f, 0f), (double)rot, default(Vector2));
			GeneralParticleHandler.SpawnParticle(new SparkParticle(base.Projectile.position + offset, new Vector2(velOffset.X, velOffset.Y) * Main.rand.NextFloat(1.5f, 2.3f), affectedByGravity: false, Main.rand.Next(23, 28), 1.9f, Main.rand.NextBool(5) ? Color.Gold : Color.PaleGoldenrod));
		}
		for (int j = 0; j <= 17; j++)
		{
			Dust obj = Main.dust[Dust.NewDust(target.position, base.Projectile.width, base.Projectile.height, 130, 0f, 0f, 0, default(Color), 1.5f)];
			obj.velocity.Y -= Main.rand.NextFloat(2.5f, 10.5f);
			obj.velocity.X += Main.rand.NextFloat(-3f, 3f);
		}
		Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, base.Projectile.velocity * 0f, ModContent.ProjectileType<SpearofDestinyStealthExplosion>(), base.Projectile.damage / 2, base.Projectile.knockBack * 2f, base.Projectile.owner);
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
}
