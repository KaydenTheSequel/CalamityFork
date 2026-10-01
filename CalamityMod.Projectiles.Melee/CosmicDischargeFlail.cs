using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Buffs.StatBuffs;
using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.Projectiles.BaseProjectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class CosmicDischargeFlail : BaseFlailProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Melee";

	public override Color SpecialDrawColor
	{
		get
		{
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			return new Color(150, 255, 255);
		}
	}

	public override int ExudeDustType => 67;

	public override int WhipDustType => 187;

	public override int HandleHeight => 62;

	public override int BodyType1StartY => 64;

	public override int BodyType1SectionHeight => 28;

	public override int BodyType2StartY => 94;

	public override int BodyType2SectionHeight => 18;

	public override int TailStartY => 114;

	public override int TailHeight => 84;

	public override void SetDefaults()
	{
		base.Projectile.width = 16;
		base.Projectile.height = 16;
		base.Projectile.friendly = true;
		base.Projectile.alpha = 255;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.DamageType = DamageClass.MeleeNoSpeed;
		base.Projectile.ignoreWater = true;
		base.Projectile.MaxUpdates = 2;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 6 * base.Projectile.MaxUpdates;
		base.Projectile.coldDamage = true;
	}

	public override void PostDraw(Color lightColor)
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0204: Unknown result type (might be due to invalid IL or missing references)
		//IL_021b: Unknown result type (might be due to invalid IL or missing references)
		//IL_021d: Unknown result type (might be due to invalid IL or missing references)
		//IL_021f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0227: Unknown result type (might be due to invalid IL or missing references)
		//IL_0237: Unknown result type (might be due to invalid IL or missing references)
		//IL_023c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0241: Unknown result type (might be due to invalid IL or missing references)
		Vector2 mountedCenter = Main.player[base.Projectile.owner].MountedCenter;
		Color colorAtCenter = Lighting.GetColor((int)((double)base.Projectile.position.X + (double)base.Projectile.width * 0.5) / 16, (int)(((double)base.Projectile.position.Y + (double)base.Projectile.height * 0.5) / 16.0));
		if (base.Projectile.hide && !ProjectileID.Sets.DontAttachHideToAlpha[base.Type])
		{
			colorAtCenter = Lighting.GetColor((int)mountedCenter.X / 16, (int)(mountedCenter.Y / 16f));
		}
		Color drawColor = base.Projectile.GetAlpha(colorAtCenter);
		float speed = ((Vector2)(ref base.Projectile.velocity)).Length() + 16f - 40f * base.Projectile.scale;
		Vector2 normalizedVelocity = Vector2.Normalize(base.Projectile.velocity);
		Rectangle type1BodyFrame = default(Rectangle);
		((Rectangle)(ref type1BodyFrame))._002Ector(0, BodyType1StartY, base.FlailTexture.Width, BodyType1SectionHeight);
		Vector2 bodyDrawPosition = base.Projectile.Center.Floor();
		bodyDrawPosition += normalizedVelocity * base.Projectile.scale * 33f;
		if (!(speed > 0f))
		{
			return;
		}
		float counter = 0f;
		while (counter + 1f < speed)
		{
			if (speed - counter < (float)type1BodyFrame.Height)
			{
				type1BodyFrame.Height = (int)(speed - counter);
			}
			Main.spriteBatch.Draw(base.FlailTexture, bodyDrawPosition - Main.screenPosition + Vector2.UnitY * Main.player[base.Projectile.owner].gfxOffY, (Rectangle?)type1BodyFrame, drawColor, base.Projectile.rotation + (float)Math.PI, new Vector2((float)(type1BodyFrame.Width / 2), 0f), base.Projectile.scale, (SpriteEffects)0, 0.6f);
			counter += (float)type1BodyFrame.Height * base.Projectile.scale;
			bodyDrawPosition += normalizedVelocity * (float)type1BodyFrame.Height * base.Projectile.scale;
		}
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		if (((Rectangle)(ref projHitbox)).Intersects(targetHitbox))
		{
			return true;
		}
		float zero = 0f;
		if (Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), base.Projectile.Center, base.Projectile.Center + base.Projectile.velocity, 16f * base.Projectile.scale, ref zero))
		{
			return true;
		}
		return false;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		Player obj = Main.player[base.Projectile.owner];
		target.AddBuff(ModContent.BuffType<Nightwither>(), 180);
		target.AddBuff(ModContent.BuffType<GlacialState>(), 60);
		if (base.Projectile.localAI[1] <= 0f && base.Projectile.owner == Main.myPlayer)
		{
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), target.Center.X, target.Center.Y, 0f, 0f, ModContent.ProjectileType<CosmicIceBurst>(), base.Projectile.damage, 10f, base.Projectile.owner, 0f, 0.85f + Main.rand.NextFloat() * 1.15f);
		}
		base.Projectile.localAI[1] = 4f;
		obj.AddBuff(ModContent.BuffType<CosmicFreeze>(), 300);
	}
}
