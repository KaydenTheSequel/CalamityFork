using System;
using System.IO;
using System.Runtime.CompilerServices;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class WingmanGrenade : ModProjectile, ILocalizedModType, IModType
{
	[CompilerGenerated]
	private Color _003CmainColor_003Ek__BackingField;

	public new string LocalizationCategory => "Projectiles.Magic";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public bool exploding
	{
		get
		{
			return base.Projectile.ai[0] == 1f;
		}
		set
		{
			base.Projectile.ai[0] = (value ? 1f : 0f);
		}
	}

	public float sizeBonus { get; set; }

	public Color mainColor
	{
		[CompilerGenerated]
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return _003CmainColor_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			_003CmainColor_003Ek__BackingField = value;
		}
	}

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 14;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 20);
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 480;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.extraUpdates = 6;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		return CalamityUtils.CircularHitboxCollision(base.Projectile.Center, exploding ? (120f * sizeBonus) : (20f * sizeBonus), targetHitbox);
	}

	public override void AI()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0241: Unknown result type (might be due to invalid IL or missing references)
		//IL_024c: Unknown result type (might be due to invalid IL or missing references)
		//IL_043e: Unknown result type (might be due to invalid IL or missing references)
		//IL_044c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0465: Unknown result type (might be due to invalid IL or missing references)
		//IL_0267: Unknown result type (might be due to invalid IL or missing references)
		//IL_026c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0272: Unknown result type (might be due to invalid IL or missing references)
		//IL_027c: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0305: Unknown result type (might be due to invalid IL or missing references)
		//IL_0310: Unknown result type (might be due to invalid IL or missing references)
		//IL_0329: Unknown result type (might be due to invalid IL or missing references)
		//IL_032e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0333: Unknown result type (might be due to invalid IL or missing references)
		//IL_0339: Unknown result type (might be due to invalid IL or missing references)
		//IL_0343: Unknown result type (might be due to invalid IL or missing references)
		//IL_047f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0484: Unknown result type (might be due to invalid IL or missing references)
		//IL_048b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0490: Unknown result type (might be due to invalid IL or missing references)
		//IL_049e: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04be: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04df: Unknown result type (might be due to invalid IL or missing references)
		//IL_052a: Unknown result type (might be due to invalid IL or missing references)
		//IL_052f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0539: Unknown result type (might be due to invalid IL or missing references)
		//IL_0522: Unknown result type (might be due to invalid IL or missing references)
		//IL_053e: Unknown result type (might be due to invalid IL or missing references)
		if (mainColor == Color.White)
		{
			base.Projectile.scale = 0.4f;
			if (base.Projectile.ai[1] == -1f)
			{
				mainColor = Color.Turquoise;
			}
			if (base.Projectile.ai[1] == 0f)
			{
				mainColor = Color.Orchid;
			}
			if (base.Projectile.ai[1] == 2f)
			{
				mainColor = Color.MediumVioletRed;
				base.Projectile.scale = 0.5f;
				sizeBonus = 1.5f;
			}
		}
		if (base.Projectile.timeLeft % 2 == 0)
		{
			base.Projectile.scale = Main.rand.NextFloat(0.35f, 0.5f);
		}
		if (base.Projectile.timeLeft <= 65)
		{
			exploding = true;
		}
		if (exploding)
		{
			base.Projectile.velocity = Vector2.Zero;
			if (base.Projectile.timeLeft > 65)
			{
				base.Projectile.timeLeft = 65;
			}
			if (base.Projectile.timeLeft == 65)
			{
				GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, mainColor, "CalamityMod/Particles/HighResHollowCircleHardEdge", Vector2.One, Main.rand.NextFloat(-10f, 10f), 0f, 0.12f * sizeBonus, 15, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
				GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.Lerp(mainColor, Color.White, 0.5f), "CalamityMod/Particles/BloomCircle", Vector2.One, Main.rand.NextFloat(-10f, 10f), 3f * sizeBonus, 0f, 25, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
				SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/ArcNovaDiffuserChargeImpact");
				style.Volume = 1.25f;
				style.Pitch = -0.2f;
				style.PitchVariance = 0.15f;
				SoundEngine.PlaySound(in style, base.Projectile.Center);
			}
			if (base.Projectile.timeLeft == 65)
			{
				GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, mainColor, "CalamityMod/Particles/HighResHollowCircleHardEdge", Vector2.One, Main.rand.NextFloat(-10f, 10f), 0.12f * sizeBonus, 0.135f * sizeBonus, 50, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
			}
			if (base.Projectile.timeLeft % 4 == 0)
			{
				GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center + Utils.RotatedByRandom(new Vector2(95f, 95f), 100.0) * sizeBonus * Main.rand.NextFloat(0.7f, 1.1f), Vector2.Zero, mainColor, "CalamityMod/Particles/HighResHollowCircleHardEdge", Vector2.One, Main.rand.NextFloat(-10f, 10f), 0f, Main.rand.NextFloat(0.04f, 0.07f) * sizeBonus, 13, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
			}
		}
		Projectile projectile = base.Projectile;
		projectile.velocity *= 0.988f;
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		Vector2 center = base.Projectile.Center;
		Color newColor = mainColor;
		Lighting.AddLight(center, ((Color)(ref newColor)).ToVector3() * 0.7f);
		if (base.Projectile.timeLeft % 2 == 0 && ((!exploding && Main.rand.NextBool()) || exploding))
		{
			Vector2 dustVel = Utils.RotatedByRandom(new Vector2(4f, 4f), 100.0) * Main.rand.NextFloat(0.1f, 0.8f) * (exploding ? sizeBonus : 1f);
			Vector2 position = base.Projectile.Center + dustVel * (float)((!exploding) ? 1 : 5);
			int type = (Main.rand.NextBool(4) ? 264 : 66);
			Vector2? velocity = dustVel * (float)((!exploding) ? 1 : 5);
			newColor = default(Color);
			Dust dust = Dust.NewDustPerfect(position, type, velocity, 0, newColor, Main.rand.NextFloat(0.9f, 1.2f) * (exploding ? 1.5f : 1f));
			dust.noGravity = true;
			dust.color = (Main.rand.NextBool() ? Color.Lerp(mainColor, Color.White, 0.5f) : mainColor);
		}
		base.Projectile.ForceNetUpdate();
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		if (!exploding)
		{
			exploding = true;
		}
		if (base.Projectile.numHits > 0)
		{
			base.Projectile.damage = (int)((float)base.Projectile.damage * 0.8f);
		}
		if (base.Projectile.damage < 1)
		{
			base.Projectile.damage = 1;
		}
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		base.Projectile.tileCollide = false;
		exploding = true;
		return false;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = ModContent.Request<Texture2D>("CalamityMod/Particles/Light", (AssetRequestMode)2).Value;
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition;
		float drawRotation = base.Projectile.rotation;
		Vector2 rotationPoint = texture.Size() * 0.5f;
		if (!exploding)
		{
			CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], Color.Lerp(mainColor, Color.White, 0.5f) * 0.6f);
			Color white = mainColor;
			((Color)(ref white)).A = 0;
			Main.EntitySpriteDraw(texture, drawPosition, null, white, drawRotation, rotationPoint, base.Projectile.scale, (SpriteEffects)0);
			white = Color.White;
			((Color)(ref white)).A = 0;
			Main.EntitySpriteDraw(texture, drawPosition, null, white, drawRotation, rotationPoint, base.Projectile.scale, (SpriteEffects)0);
		}
		return false;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(sizeBonus);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		sizeBonus = reader.ReadSingle();
	}

	public WingmanGrenade()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		sizeBonus = 1f;
		mainColor = Color.White;
		base._002Ector();
	}
}
