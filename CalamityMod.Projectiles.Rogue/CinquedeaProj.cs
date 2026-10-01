using System;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class CinquedeaProj : ModProjectile, ILocalizedModType, IModType
{
	public static readonly SoundStyle StealthSliceSound = new SoundStyle("CalamityMod/Sounds/Custom/SwiftSlice");

	internal float gravspin;

	private Vector2 StoredVelocity;

	private Vector2 StickOffset;

	private const int StickTime = 30;

	private int Stick;

	private int elecFrame;

	private int elecFrameCounter;

	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Items/Weapons/Rogue/Cinquedea";

	public ref float Timer => ref base.Projectile.ai[0];

	public ref float Target => ref base.Projectile.ai[1];

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 4;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 10;
		base.Projectile.height = 10;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = 2;
		base.Projectile.timeLeft = 600;
		base.Projectile.tileCollide = false;
		base.Projectile.extraUpdates = 1;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = 10;
	}

	public override void AI()
	{
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c9: Unknown result type (might be due to invalid IL or missing references)
		CalamityGlobalProjectile modProj = base.Projectile.Calamity();
		base.DrawOriginOffsetY = 11;
		base.DrawOffsetX = -22;
		gravspin = base.Projectile.velocity.Y * (0.03f * (float)base.Projectile.spriteDirection);
		Timer++;
		if (Timer > 2f)
		{
			base.Projectile.tileCollide = true;
		}
		if (Stick > 0)
		{
			base.Projectile.Center = Main.npc[(int)Target].Center + StickOffset;
			Stick--;
			if (Stick == 0)
			{
				SoundEngine.PlaySound(in StealthSliceSound, base.Projectile.Center);
				base.Projectile.velocity = StoredVelocity;
				base.Projectile.extraUpdates = 8;
				base.Projectile.timeLeft = 90;
				for (int i = 0; i < 6; i++)
				{
					GeneralParticleHandler.SpawnParticle(new ElectricSpark(base.Projectile.Center, -base.Projectile.velocity.RotatedByRandom(0.5235987901687622), Color.Aqua, Color.AliceBlue, 1.2f, 60));
				}
			}
		}
		if (modProj.stealthStrike && Stick == 0 && Timer % (float)((base.Projectile.numHits > 0) ? 1 : 3) == 0f)
		{
			GeneralParticleHandler.SpawnParticle(new ElectricSpark(base.Projectile.Center, Main.rand.NextVector2CircularEdge(3f, 3f), Color.Aqua, Color.AliceBlue, 0.9f, 25));
		}
		elecFrameCounter++;
		if (elecFrameCounter > 2)
		{
			elecFrameCounter = 0;
			elecFrame++;
			if (elecFrame > 3)
			{
				elecFrame = 0;
			}
		}
		if (((Timer <= 80f && !modProj.stealthStrike) || modProj.stealthStrike || base.Projectile.velocity.Y <= 0f) && Stick == 0)
		{
			base.Projectile.spriteDirection = (base.Projectile.direction = (base.Projectile.velocity.X > 0f).ToDirectionInt());
			base.Projectile.rotation = base.Projectile.velocity.ToRotation() + ((base.Projectile.spriteDirection == 1) ? 0f : ((float)Math.PI));
			base.Projectile.rotation += MathHelper.ToRadians(45f) * (float)base.Projectile.spriteDirection;
		}
		if (Timer > 80f && !modProj.stealthStrike)
		{
			base.Projectile.velocity.Y = base.Projectile.velocity.Y + 0.15f;
			if (base.Projectile.velocity.Y > 0f)
			{
				base.Projectile.rotation += gravspin;
			}
			if (base.Projectile.velocity.Y > 10f)
			{
				base.Projectile.velocity.Y = 10f;
			}
		}
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		Collision.HitTiles(base.Projectile.position + base.Projectile.velocity, base.Projectile.velocity, base.Projectile.width, base.Projectile.height);
		SoundEngine.PlaySound(in SoundID.Dig, base.Projectile.position);
		return true;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		return false;
	}

	public override bool? CanDamage()
	{
		return Stick == 0;
	}

	public override void ModifyDamageHitbox(ref Rectangle hitbox)
	{
		if (base.Projectile.Calamity().stealthStrike && base.Projectile.numHits > 0 && Stick == 0)
		{
			((Rectangle)(ref hitbox)).Inflate(38, 38);
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.Calamity().stealthStrike && base.Projectile.numHits == 0)
		{
			Target = target.whoAmI;
			base.Projectile.penetrate = -1;
			Stick = 30;
			StickOffset = base.Projectile.Center - target.Center;
			StoredVelocity = base.Projectile.velocity;
			base.Projectile.velocity = Vector2.Zero;
		}
	}

	public override void PostDraw(Color lightColor)
	{
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.Calamity().stealthStrike && base.Projectile.numHits > 0 && Stick == 0)
		{
			Texture2D value = ModContent.Request<Texture2D>("Terraria/Images/Projectile_443", (AssetRequestMode)2).Value;
			Rectangle frame = value.Frame(1, 4, 0, elecFrame);
			Main.EntitySpriteDraw(value, base.Projectile.Center - Main.screenPosition, frame, Color.White, 0f, frame.Size() / 2f, 1f, (SpriteEffects)0);
		}
	}
}
