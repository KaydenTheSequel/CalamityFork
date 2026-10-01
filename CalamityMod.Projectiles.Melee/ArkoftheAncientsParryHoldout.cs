using System;
using System.IO;
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

public class ArkoftheAncientsParryHoldout : ModProjectile, ILocalizedModType, IModType
{
	private bool initialized;

	private const float MaxTime = 340f;

	private static float ParryTime = 15f;

	public new string LocalizationCategory => "Projectiles.Melee";

	public override string Texture => "CalamityMod/Items/Weapons/Melee/FracturedArk";

	public Vector2 DistanceFromPlayer
	{
		get
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			//IL_003a: Unknown result type (might be due to invalid IL or missing references)
			return base.Projectile.velocity * 10f * (1f + (float)Math.Sin(Timer / ParryTime * (float)Math.PI) * 0.8f);
		}
	}

	public float Timer => 340f - (float)base.Projectile.timeLeft;

	public ref float AlreadyParried => ref base.Projectile.ai[1];

	public Player Owner => Main.player[base.Projectile.owner];

	public override void SetDefaults()
	{
		base.Projectile.DamageType = DamageClass.MeleeNoSpeed;
		base.Projectile.width = (base.Projectile.height = 75);
		base.Projectile.width = (base.Projectile.height = 75);
		base.Projectile.tileCollide = false;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.noEnchantmentVisuals = true;
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = 10;
	}

	public override bool? CanDamage()
	{
		return Timer <= ParryTime && AlreadyParried == 0f;
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		float collisionPoint = 0f;
		float bladeLength = 80f * base.Projectile.scale;
		return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), Owner.Center + DistanceFromPlayer, Owner.Center + DistanceFromPlayer + base.Projectile.velocity * bladeLength, 24f, ref collisionPoint);
	}

	public void GeneralParryEffects()
	{
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		if (Owner.HeldItem.ModItem is FracturedArk sword)
		{
			sword.Charge = 10f;
		}
		SoundEngine.PlaySound(in SoundID.DD2_WitherBeastCrystalImpact);
		SoundEngine.PlaySound(in SoundID.Item67);
		CombatText.NewText(base.Projectile.Hitbox, new Color(111, 247, 200), CalamityUtils.GetTextValue("Misc.ArkParry"), dramatic: true);
		AlreadyParried = 1f;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		if (!(AlreadyParried > 0f))
		{
			GeneralParryEffects();
			if (target.damage > 0)
			{
				int arkParryIFrames = Owner.ComputeParryIFrames();
				Owner.GiveUniversalIFrames(arkParryIFrames);
			}
			Vector2 val = target.Hitbox.Size();
			Vector2 particleOrigin = ((((Vector2)(ref val)).Length() < 140f) ? target.Center : (base.Projectile.Center + base.Projectile.rotation.ToRotationVector2() * 60f));
			GeneralParticleHandler.SpawnParticle(new GenericSparkle(particleOrigin, Vector2.Zero, Color.White, Color.HotPink, 1.2f, 35, 0.1f, 2f));
			for (int i = 0; i < 10; i++)
			{
				Vector2 particleSpeed = Main.rand.NextVector2CircularEdge(1f, 1f) * Main.rand.NextFloat(2.6f, 4f);
				GeneralParticleHandler.SpawnParticle(new SquishyLightParticle(particleOrigin, particleSpeed, Main.rand.NextFloat(0.3f, 0.6f), Color.Cyan, 60, 1f, 1.5f, 3f, 0.02f));
			}
		}
	}

	public override void AI()
	{
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		if (!initialized)
		{
			base.Projectile.timeLeft = 340;
			SoundEngine.PlaySound(in SoundID.DD2_SkyDragonsFuryShot, base.Projectile.Center);
			base.Projectile.velocity = Owner.SafeDirectionTo(Owner.Calamity().mouseWorld, Vector2.Zero);
			((Vector2)(ref base.Projectile.velocity)).Normalize();
			base.Projectile.rotation = base.Projectile.velocity.ToRotation();
			initialized = true;
			base.Projectile.ForceNetUpdate();
		}
		base.Projectile.Center = Owner.Center + DistanceFromPlayer;
		base.Projectile.scale = 1.4f + (float)Math.Sin(Timer / 160f * (float)Math.PI) * 0.6f;
		Owner.heldProj = base.Projectile.whoAmI;
		Owner.ChangeDir(Math.Sign(base.Projectile.velocity.X));
		Owner.itemRotation = base.Projectile.rotation;
		if (Owner.direction != 1)
		{
			Owner.itemRotation -= (float)Math.PI;
		}
		Owner.itemRotation = MathHelper.WrapAngle(Owner.itemRotation);
		if (AlreadyParried > 0f)
		{
			AlreadyParried++;
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		//IL_0208: Unknown result type (might be due to invalid IL or missing references)
		//IL_020d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0212: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_0222: Unknown result type (might be due to invalid IL or missing references)
		//IL_0228: Unknown result type (might be due to invalid IL or missing references)
		//IL_0240: Unknown result type (might be due to invalid IL or missing references)
		//IL_024d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0252: Unknown result type (might be due to invalid IL or missing references)
		//IL_025c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0262: Unknown result type (might be due to invalid IL or missing references)
		//IL_02af: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02de: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_030e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0326: Unknown result type (might be due to invalid IL or missing references)
		//IL_032c: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		if (Timer > ParryTime)
		{
			if (Main.myPlayer == Owner.whoAmI)
			{
				Texture2D barBG = ModContent.Request<Texture2D>("CalamityMod/UI/MiscTextures/GenericBarBack", (AssetRequestMode)2).Value;
				Texture2D barFG = ModContent.Request<Texture2D>("CalamityMod/UI/MiscTextures/GenericBarFront", (AssetRequestMode)2).Value;
				Vector2 drawPos = Owner.Center - Main.screenPosition + new Vector2(0f, -36f) - barBG.Size() / 2f;
				Rectangle frame = default(Rectangle);
				((Rectangle)(ref frame))._002Ector(0, 0, (int)((Timer - ParryTime) / (340f - ParryTime) * (float)barFG.Width), barFG.Height);
				float opacity = ((Timer <= ParryTime + 25f) ? ((Timer - ParryTime) / 25f) : ((340f - Timer <= 8f) ? ((float)base.Projectile.timeLeft / 8f) : 1f));
				Color color = Main.hslToRgb(Main.GlobalTimeWrappedHourly * 0.6f % 1f, 1f, 0.85f + (float)Math.Sin(Main.GlobalTimeWrappedHourly * 3f) * 0.1f);
				Main.spriteBatch.Draw(barBG, drawPos, color * opacity);
				Main.spriteBatch.Draw(barFG, drawPos, (Rectangle?)frame, color * opacity * 0.8f);
			}
			return false;
		}
		Texture2D sword = ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Melee/FracturedArk", (AssetRequestMode)2).Value;
		Texture2D glowmask = ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Melee/FracturedArkGlow", (AssetRequestMode)2).Value;
		float drawRotation = base.Projectile.rotation + (float)Math.PI / 4f;
		Vector2 drawOrigin = default(Vector2);
		((Vector2)(ref drawOrigin))._002Ector(0f, (float)sword.Height);
		Vector2 center = Owner.Center;
		Vector2 velocity = base.Projectile.velocity;
		Vector2 distanceFromPlayer = DistanceFromPlayer;
		Vector2 drawOffset = center + velocity * ((Vector2)(ref distanceFromPlayer)).Length() - Main.screenPosition;
		Main.EntitySpriteDraw(sword, drawOffset, null, lightColor, drawRotation, drawOrigin, base.Projectile.scale, (SpriteEffects)0);
		Main.EntitySpriteDraw(glowmask, drawOffset, null, Color.Lerp(lightColor, Color.White, 0.75f), drawRotation, drawOrigin, base.Projectile.scale, (SpriteEffects)0);
		if (AlreadyParried > 0f)
		{
			((Vector2)(ref drawOrigin))._002Ector(0f, 36f);
			Rectangle frame2 = default(Rectangle);
			((Rectangle)(ref frame2))._002Ector(24, 0, 36, 36);
			Vector2 center2 = Owner.Center;
			Vector2 velocity2 = base.Projectile.velocity;
			distanceFromPlayer = DistanceFromPlayer;
			drawOffset = center2 + velocity2 * (((Vector2)(ref distanceFromPlayer)).Length() + 33f) - Main.screenPosition;
			Main.EntitySpriteDraw(glowmask, drawOffset, frame2, Main.hslToRgb(Main.GlobalTimeWrappedHourly % 1f, 1f, 0.8f) * (1f - AlreadyParried / ParryTime), drawRotation, drawOrigin, base.Projectile.scale + AlreadyParried / ParryTime, (SpriteEffects)0);
		}
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		if (Main.myPlayer == Owner.whoAmI)
		{
			SoundEngine.PlaySound(in SoundID.Item35);
		}
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(initialized);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		initialized = reader.ReadBoolean();
	}
}
