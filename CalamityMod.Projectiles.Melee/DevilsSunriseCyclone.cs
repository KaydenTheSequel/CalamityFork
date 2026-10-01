using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.NPCs;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

[PierceResistException(false)]
public class DevilsSunriseCyclone : ModProjectile, ILocalizedModType, IModType
{
	public static readonly SoundStyle HitSound = new SoundStyle("CalamityMod/Sounds/Item/MantisSwipe", 2)
	{
		Pitch = 1.25f,
		PitchVariance = 0.15f
	};

	private int greenAndBlue = 100;

	private const int MaxHits = 10;

	private float ReturnVel = 5f;

	private const float MaxReturnVel = 30f;

	private NPC targetToSlice;

	private Vector2 sliceOffset;

	public new string LocalizationCategory => "Projectiles.Melee";

	public ref float State => ref base.Projectile.ai[0];

	public ref float Timer => ref base.Projectile.ai[1];

	public override void SetDefaults()
	{
		base.Projectile.width = 30;
		base.Projectile.height = 30;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.DamageType = DamageClass.MeleeNoSpeed;
		base.Projectile.scale = 1.75f;
		base.Projectile.tileCollide = false;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 7;
	}

	public override void AI()
	{
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_030c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0312: Unknown result type (might be due to invalid IL or missing references)
		//IL_0317: Unknown result type (might be due to invalid IL or missing references)
		//IL_0322: Unknown result type (might be due to invalid IL or missing references)
		//IL_0327: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0204: Unknown result type (might be due to invalid IL or missing references)
		//IL_0234: Unknown result type (might be due to invalid IL or missing references)
		//IL_0217: Unknown result type (might be due to invalid IL or missing references)
		//IL_04af: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0365: Unknown result type (might be due to invalid IL or missing references)
		//IL_0370: Unknown result type (might be due to invalid IL or missing references)
		//IL_0375: Unknown result type (might be due to invalid IL or missing references)
		//IL_038d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0392: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0262: Unknown result type (might be due to invalid IL or missing references)
		//IL_0247: Unknown result type (might be due to invalid IL or missing references)
		//IL_0292: Unknown result type (might be due to invalid IL or missing references)
		//IL_0275: Unknown result type (might be due to invalid IL or missing references)
		//IL_0408: Unknown result type (might be due to invalid IL or missing references)
		//IL_0413: Unknown result type (might be due to invalid IL or missing references)
		//IL_0418: Unknown result type (might be due to invalid IL or missing references)
		//IL_041d: Unknown result type (might be due to invalid IL or missing references)
		//IL_042b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0444: Unknown result type (might be due to invalid IL or missing references)
		//IL_0449: Unknown result type (might be due to invalid IL or missing references)
		//IL_0451: Unknown result type (might be due to invalid IL or missing references)
		//IL_0456: Unknown result type (might be due to invalid IL or missing references)
		//IL_0460: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d5: Unknown result type (might be due to invalid IL or missing references)
		Player Owner = Main.player[base.Projectile.owner];
		Timer++;
		base.Projectile.rotation += 0.5f;
		Lighting.AddLight(base.Projectile.Center, 0.51f, 0.2f, 0.2f);
		int dust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 66, 0f, 0f, 100, new Color(255, greenAndBlue, greenAndBlue));
		Dust obj = Main.dust[dust];
		obj.velocity *= 0.3f;
		Main.dust[dust].noGravity = true;
		float state = State;
		if (state != 0f)
		{
			if (state != 1f)
			{
				if (state != 2f)
				{
					return;
				}
				base.Projectile.velocity = Vector2.Zero;
				base.Projectile.Center = targetToSlice.Center + sliceOffset + targetToSlice.velocity;
				if (Timer % 2f == 0f)
				{
					if (Timer % 4f == 0f)
					{
						float angle = (base.Projectile.Center - targetToSlice.Center).ToRotation() + (float)Math.PI / 2f;
						GeneralParticleHandler.SpawnParticle(new CustomSpark(targetToSlice.Center, Vector2.Zero, "CalamityMod/Particles/ThinEndedLine", affectedByGravity: false, 10, Main.rand.NextFloat(0.6f, 0.8f), Color.Red, new Vector2(0.5f, 1f), useAddativeBlend: true, glowCenter: false, angle));
					}
					else
					{
						for (int i = 0; i < 2; i++)
						{
							Vector2 sparkVel = Vector2.Normalize(base.Projectile.Center - targetToSlice.Center).RotatedByRandom(0.7853981852531433) * Main.rand.NextFloat(14f, 18f);
							GeneralParticleHandler.SpawnParticle(new AltLineParticle(targetToSlice.Center, sparkVel, affectedByGravity: false, 12, 0.75f, Color.OrangeRed));
						}
					}
				}
				if (!targetToSlice.CanBeChasedBy(base.Projectile))
				{
					State = 1f;
					base.Projectile.velocity = Vector2.Normalize(Owner.Center - base.Projectile.Center) * ReturnVel;
				}
				return;
			}
			ReturnVel *= 1.035f;
			if (ReturnVel > 30f)
			{
				ReturnVel = 30f;
			}
			Vector2 ownerDist = Owner.Center - base.Projectile.Center;
			if (((Vector2)(ref ownerDist)).Length() > 3000f)
			{
				base.Projectile.Kill();
			}
			ownerDist = Vector2.Normalize(ownerDist) * ReturnVel;
			if (base.Projectile.velocity.X < ownerDist.X)
			{
				base.Projectile.velocity.X = ownerDist.X;
			}
			else if (base.Projectile.velocity.X > ownerDist.X)
			{
				base.Projectile.velocity.X = ownerDist.X;
			}
			if (base.Projectile.velocity.Y < ownerDist.Y)
			{
				base.Projectile.velocity.Y = ownerDist.Y;
			}
			else if (base.Projectile.velocity.Y > ownerDist.Y)
			{
				base.Projectile.velocity.Y = ownerDist.Y;
			}
			if (Main.myPlayer == base.Projectile.owner)
			{
				Rectangle hitbox = base.Projectile.Hitbox;
				if (((Rectangle)(ref hitbox)).Intersects(Owner.Hitbox))
				{
					base.Projectile.Kill();
				}
			}
		}
		else
		{
			Projectile projectile = base.Projectile;
			projectile.velocity *= 0.965f;
			if (((Vector2)(ref base.Projectile.velocity)).Length() < 0.05f)
			{
				base.Projectile.velocity = Vector2.Zero;
			}
			if (Timer == 60f)
			{
				State = 1f;
				base.Projectile.velocity = Vector2.Normalize(Owner.Center - base.Projectile.Center) * ReturnVel;
			}
		}
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return null;
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		float radius = ((State == 2f) ? 60f : 45f);
		return CalamityUtils.CircularHitboxCollision(base.Projectile.Center, radius, targetHitbox);
	}

	public override bool? CanHitNPC(NPC target)
	{
		if (base.Projectile.numHits < 10)
		{
			return null;
		}
		return false;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in HitSound, target.Center);
		target.AddBuff(ModContent.BuffType<BrimstoneFlames>(), 180);
		Main.player[base.Projectile.owner].DoLifestealDirect(target, 1, 0.75f);
		if (State != 2f)
		{
			State = 2f;
			targetToSlice = target;
			sliceOffset = base.Projectile.Center - target.Center;
		}
		if (base.Projectile.numHits >= 9)
		{
			State = 1f;
			base.Projectile.velocity = Vector2.Normalize(Main.player[base.Projectile.owner].Center - base.Projectile.Center) * ReturnVel;
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Item88, base.Projectile.position);
		int dustAmt = 24;
		for (int i = 0; i < dustAmt; i++)
		{
			Vector2 dustVel = Vector2.UnitX.RotatedBy((float)i * ((float)Math.PI * 2f) / (float)dustAmt) * Main.rand.NextFloat(8f, 12f);
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center, 66, dustVel, 100, new Color(255, greenAndBlue, greenAndBlue));
			dust.noGravity = true;
			dust.noLight = true;
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		Texture2D cyclone = ModContent.Request<Texture2D>(Texture, (AssetRequestMode)2).Value;
		Vector2 drawPos = base.Projectile.Center - Main.screenPosition;
		SpriteEffects sp = (SpriteEffects)0;
		Main.EntitySpriteDraw(cyclone, drawPos, null, new Color(255, 50, 50), base.Projectile.rotation, cyclone.Size() / 2f, base.Projectile.scale, sp);
		Main.EntitySpriteDraw(cyclone, drawPos, null, new Color(255, 75, 75) * 0.7f, 0f - base.Projectile.rotation, cyclone.Size() / 2f, base.Projectile.scale * 1.5f, sp);
		Main.EntitySpriteDraw(cyclone, drawPos, null, new Color(255, greenAndBlue, greenAndBlue) * 0.4f, base.Projectile.rotation * 0.75f, cyclone.Size() / 2f, base.Projectile.scale * 2f, sp);
		Texture2D flashySlash = ModContent.Request<Texture2D>("CalamityMod/Particles/SlashSmear", (AssetRequestMode)2).Value;
		Main.spriteBatch.SetBlendState(BlendState.Additive);
		Main.EntitySpriteDraw(flashySlash, drawPos + Main.rand.NextVector2Circular(30f, 30f), null, new Color(255, greenAndBlue, greenAndBlue), Main.rand.NextFloat((float)Math.PI * 2f), flashySlash.Size() / 2f, 0.275f, sp);
		Main.spriteBatch.SetBlendState(BlendState.AlphaBlend);
		return false;
	}
}
