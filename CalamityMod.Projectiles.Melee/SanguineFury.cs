using System;
using System.IO;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Particles;
using CalamityMod.Sounds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class SanguineFury : ModProjectile, ILocalizedModType, IModType
{
	private bool initialized;

	private Vector2 direction;

	public const float pogoStrenght = 16f;

	public const float maxShred = 500f;

	public Projectile Wheel;

	public bool Dashing;

	public Vector2 DashStart;

	public new string LocalizationCategory => "Projectiles.Melee";

	public override string Texture => "CalamityMod/Projectiles/Melee/TrueBiomeBlade_SanguineFury";

	public ref float Shred => ref base.Projectile.ai[0];

	public float ShredRatio => MathHelper.Clamp(Shred / 250f, 0f, 1f);

	public ref float PogoCooldown => ref base.Projectile.ai[1];

	public ref float BounceTime => ref base.Projectile.localAI[0];

	public ref float ChargeSoundCooldown => ref base.Projectile.localAI[1];

	public Player Owner => Main.player[base.Projectile.owner];

	public bool CanPogo
	{
		get
		{
			if (Owner.velocity.Y != 0f)
			{
				return PogoCooldown <= 0f;
			}
			return false;
		}
	}

	public override void SetDefaults()
	{
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.width = (base.Projectile.height = 70);
		base.Projectile.tileCollide = false;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.extraUpdates = 1;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = OmegaBiomeBlade.SuperPogoAttunement_LocalIFrames;
		base.Projectile.timeLeft = OmegaBiomeBlade.SuperPogoAttunement_LocalIFrames;
	}

	public override bool? CanDamage()
	{
		return base.Projectile.timeLeft <= 2;
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		float collisionPoint = 0f;
		float bladeLength = 130f * base.Projectile.scale;
		float bladeWidth = 86f * base.Projectile.scale;
		return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), Owner.Center, Owner.Center + direction * bladeLength, bladeWidth, ref collisionPoint);
	}

	public void Pogo()
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_024d: Unknown result type (might be due to invalid IL or missing references)
		//IL_025d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0263: Unknown result type (might be due to invalid IL or missing references)
		//IL_0265: Unknown result type (might be due to invalid IL or missing references)
		//IL_027e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0283: Unknown result type (might be due to invalid IL or missing references)
		//IL_0286: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_02af: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02be: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02de: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e3: Unknown result type (might be due to invalid IL or missing references)
		if (CanPogo && Main.myPlayer == Owner.whoAmI)
		{
			Owner.velocity = -direction.SafeNormalize(Vector2.Zero) * 16f;
			Owner.fallStart = (int)(Owner.position.Y / 16f);
			PogoCooldown = 30f;
			SoundEngine.PlaySound(in SoundID.DD2_MonkStaffGroundImpact, base.Projectile.position);
			Vector2 hitPosition = Owner.Center + direction * 100f * base.Projectile.scale;
			BounceTime = 20f;
			for (int i = 0; i < 8; i++)
			{
				Vector2 hitPositionDisplace = direction.RotatedBy(1.5707963705062866) * Main.rand.NextFloat(-10f, 10f);
				Vector2 flyDirection = -direction.RotatedBy(Main.rand.NextFloat(-(float)Math.PI / 4f, (float)Math.PI / 4f));
				GeneralParticleHandler.SpawnParticle(new SmallSmokeParticle(hitPosition + hitPositionDisplace, flyDirection * 9f, Color.Crimson, new Color(130, 130, 130), Main.rand.NextFloat(1.8f, 2.6f), 155 - Main.rand.Next(30)));
				GeneralParticleHandler.SpawnParticle(new StrongBloom(hitPosition - hitPositionDisplace * 3f, -direction * 6f * Main.rand.NextFloat(0.5f, 1f), Color.Crimson * 0.5f, 0.01f + Main.rand.NextFloat(0f, 0.2f), 20 + Main.rand.Next(40)));
			}
			for (int j = 0; j < 3; j++)
			{
				Vector2 hitPositionDisplace2 = direction.RotatedBy(1.5707963705062866) * Main.rand.NextFloat(-10f, 10f);
				Vector2 flyDirection2 = -direction.RotatedBy(Main.rand.NextFloat(-(float)Math.PI / 4f, (float)Math.PI / 4f));
				GeneralParticleHandler.SpawnParticle(new StoneDebrisParticle(hitPosition - hitPositionDisplace2 * 3f, flyDirection2 * Main.rand.NextFloat(3f, 6f), Color.Beige, 1f + Main.rand.NextFloat(0f, 0.4f), 30 + Main.rand.Next(50), 0.1f));
			}
		}
	}

	public override void AI()
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0249: Unknown result type (might be due to invalid IL or missing references)
		//IL_0253: Unknown result type (might be due to invalid IL or missing references)
		//IL_0258: Unknown result type (might be due to invalid IL or missing references)
		//IL_0268: Unknown result type (might be due to invalid IL or missing references)
		//IL_0273: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03df: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0403: Unknown result type (might be due to invalid IL or missing references)
		//IL_040d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0412: Unknown result type (might be due to invalid IL or missing references)
		//IL_0387: Unknown result type (might be due to invalid IL or missing references)
		//IL_038c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0396: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0474: Unknown result type (might be due to invalid IL or missing references)
		//IL_030b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0310: Unknown result type (might be due to invalid IL or missing references)
		//IL_031c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0321: Unknown result type (might be due to invalid IL or missing references)
		//IL_053c: Unknown result type (might be due to invalid IL or missing references)
		if (!initialized)
		{
			SoundEngine.PlaySound(in SoundID.Item90, base.Projectile.Center);
			initialized = true;
			Projectile[] projectile = Main.projectile;
			foreach (Projectile proj in projectile)
			{
				if (proj.active && proj.type == ModContent.ProjectileType<SanguineFuryWheel>() && proj.owner == Owner.whoAmI)
				{
					if ((Owner.Center - Owner.Calamity().mouseWorld).AngleBetween(Owner.Center - proj.Center) > (float)Math.PI / 4f)
					{
						proj.Kill();
						break;
					}
					Wheel = proj;
					Dashing = true;
					DashStart = Owner.Center;
					Wheel.timeLeft = 60;
					Owner.GiveUniversalIFrames(OmegaBiomeBlade.SuperPogoAttunement_PlayerSliceIFrames);
					break;
				}
			}
		}
		if (Owner.CantUseHoldout())
		{
			base.Projectile.Kill();
			return;
		}
		if (Shred >= 500f)
		{
			Shred = 500f;
		}
		if (Shred < 0f)
		{
			Shred = 0f;
		}
		Lighting.AddLight(base.Projectile.Center, new Vector3(1f, 0.56f, 0.56f) * ShredRatio);
		direction = Owner.SafeDirectionTo(Owner.Calamity().mouseWorld, Vector2.Zero);
		((Vector2)(ref direction)).Normalize();
		base.Projectile.rotation = direction.ToRotation();
		base.Projectile.Center = Owner.Center + direction * 60f;
		base.Projectile.scale = 1.25f + ShredRatio * 1f;
		if ((Wheel == null || !Wheel.active) && Dashing)
		{
			Dashing = false;
			Player owner = Owner;
			owner.velocity *= 0.1f;
			SoundEngine.PlaySound(in CommonCalamitySounds.MeatySlashSound, base.Projectile.Center);
			if (Owner.whoAmI == Main.myPlayer && Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), Owner.Center - DashStart / 2f, Vector2.Zero, ModContent.ProjectileType<SanguineFuryDash>(), (int)((float)base.Projectile.damage * OmegaBiomeBlade.SuperPogoAttunement_SliceDamageMult), 0f, Owner.whoAmI).ModProjectile is SanguineFuryDash dash)
			{
				dash.DashStart = DashStart;
				dash.DashEnd = Owner.Center;
			}
		}
		Owner.Calamity().LungingDown = false;
		if (Dashing)
		{
			Owner.Calamity().LungingDown = true;
			Owner.fallStart = (int)(Owner.position.Y / 16f);
			Owner.velocity = Owner.SafeDirectionTo(Wheel.Center, Vector2.Zero) * 60f;
			if (Owner.Distance(Wheel.Center) < 60f)
			{
				Wheel.active = false;
			}
		}
		if (Collision.SolidCollision(Owner.Center + direction * 100f * base.Projectile.scale - Vector2.One * 5f, 10, 10) && !Dashing)
		{
			Pogo();
			base.Projectile.ForceNetUpdate();
		}
		Owner.heldProj = base.Projectile.whoAmI;
		Owner.ChangeDir(Math.Sign(direction.X));
		Owner.itemRotation = direction.ToRotation();
		if (Owner.direction != 1)
		{
			Owner.itemRotation -= (float)Math.PI;
		}
		Owner.itemRotation = MathHelper.WrapAngle(Owner.itemRotation);
		Owner.itemTime = 2;
		Owner.itemAnimation = 2;
		if ((double)ShredRatio > 0.25 && Owner.whoAmI == Main.myPlayer)
		{
			if (ChargeSoundCooldown <= 0f)
			{
				SoundStyle style = SoundID.DD2_SonicBoomBladeSlash with
				{
					Volume = SoundID.DD2_SonicBoomBladeSlash.Volume * 2.5f
				};
				SoundEngine.PlaySound(in style);
				ChargeSoundCooldown = 20f;
			}
		}
		else
		{
			ChargeSoundCooldown--;
		}
		Shred -= OmegaBiomeBlade.SuperPogoAttunement_ShredDecayRate;
		PogoCooldown--;
		BounceTime--;
		if (base.Projectile.timeLeft <= 2)
		{
			base.Projectile.timeLeft = 2;
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		ShredTarget();
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		ShredTarget();
	}

	private void ShredTarget()
	{
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		if (Main.myPlayer == Owner.whoAmI)
		{
			if (Owner.HeldItem.ModItem is OmegaBiomeBlade sword && Main.rand.NextFloat() <= OmegaBiomeBlade.SuperPogoAttunement_ShredderProc)
			{
				sword.OnHitProc = true;
			}
			Owner.fallStart = (int)(Owner.position.Y / 16f);
			if (PogoCooldown <= 0f)
			{
				SoundEngine.PlaySound(in SoundID.NPCHit30, base.Projectile.Center);
				Shred += 62f;
				Owner.GiveUniversalIFrames(OmegaBiomeBlade.SuperPogoAttunement_PlayerShredIFrames);
				PogoCooldown = 20f;
			}
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.NPCHit43, base.Projectile.Center);
		if ((double)ShredRatio > 0.8 && Owner.whoAmI == Main.myPlayer)
		{
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, direction * 16f, ModContent.ProjectileType<SanguineFuryWheel>(), (int)((float)base.Projectile.damage * OmegaBiomeBlade.SuperPogoAttunement_ShotDamageMult), base.Projectile.knockBack, Owner.whoAmI, Shred);
		}
		if (Dashing)
		{
			Player owner = Owner;
			owner.velocity *= 0.1f;
		}
		Owner.Calamity().LungingDown = false;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0226: Unknown result type (might be due to invalid IL or missing references)
		//IL_022c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0243: Unknown result type (might be due to invalid IL or missing references)
		//IL_024d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0252: Unknown result type (might be due to invalid IL or missing references)
		//IL_0257: Unknown result type (might be due to invalid IL or missing references)
		//IL_025c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0261: Unknown result type (might be due to invalid IL or missing references)
		//IL_0264: Unknown result type (might be due to invalid IL or missing references)
		//IL_0274: Unknown result type (might be due to invalid IL or missing references)
		//IL_027a: Unknown result type (might be due to invalid IL or missing references)
		//IL_027c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0283: Unknown result type (might be due to invalid IL or missing references)
		//IL_028d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02db: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0303: Unknown result type (might be due to invalid IL or missing references)
		//IL_030d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0317: Unknown result type (might be due to invalid IL or missing references)
		//IL_031d: Unknown result type (might be due to invalid IL or missing references)
		//IL_036d: Unknown result type (might be due to invalid IL or missing references)
		Texture2D handle = ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Melee/OmegaBiomeBlade", (AssetRequestMode)2).Value;
		Texture2D blade = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Melee/TrueBiomeBlade_SanguineFury", (AssetRequestMode)2).Value;
		int bladeAmount = 4;
		float drawRotation = direction.ToRotation() + (float)Math.PI / 4f;
		Vector2 drawOrigin = default(Vector2);
		((Vector2)(ref drawOrigin))._002Ector(0f, (float)handle.Height);
		Vector2 drawOffset = Owner.Center + direction * 10f - Main.screenPosition;
		Main.EntitySpriteDraw(handle, drawOffset, null, lightColor, drawRotation, drawOrigin, base.Projectile.scale, (SpriteEffects)0);
		Main.spriteBatch.End();
		Main.spriteBatch.Begin((SpriteSortMode)1, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, (Effect)null, Main.GameViewMatrix.TransformationMatrix);
		GameShaders.Misc["CalamityMod:BasicTint"].UseOpacity(MathHelper.Clamp(BounceTime, 0f, 20f) / 20f);
		GameShaders.Misc["CalamityMod:BasicTint"].UseColor(new Color(207, 248, 255));
		GameShaders.Misc["CalamityMod:BasicTint"].Apply();
		((Vector2)(ref drawOrigin))._002Ector(0f, (float)blade.Height);
		Main.EntitySpriteDraw(blade, drawOffset, null, Color.Lerp(Color.White, lightColor, 0.5f) * 0.9f, drawRotation, drawOrigin, base.Projectile.scale, (SpriteEffects)0);
		for (int i = 0; i < bladeAmount; i++)
		{
			blade = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Melee/TrueBiomeBlade_SanguineFuryExtra", (AssetRequestMode)2).Value;
			float num = direction.ToRotation();
			float circleCompletion = (float)Math.Sin(Main.GlobalTimeWrappedHourly * 5f + (float)i * ((float)Math.PI / 2f));
			drawRotation = num + (float)Math.PI / 4f + circleCompletion * (float)Math.PI / 10f - circleCompletion * ((float)Math.PI / 9f) * ShredRatio;
			((Vector2)(ref drawOrigin))._002Ector(0f, (float)blade.Height);
			Vector2 drawOffsetStraight = Owner.Center + direction * (float)Math.Sin(Main.GlobalTimeWrappedHourly * 7f) * 10f - Main.screenPosition;
			Vector2 drawDisplacementAngle = direction.RotatedBy(1.5707963705062866) * circleCompletion.ToRotationVector2().Y * (20f + 40f * ShredRatio);
			Vector2 drawOffsetFromBounce = direction * MathHelper.Clamp(BounceTime, 0f, 20f) / 20f * 20f;
			Main.EntitySpriteDraw(blade, drawOffsetStraight + drawDisplacementAngle + drawOffsetFromBounce, null, Color.Lerp(Color.White, lightColor, 0.5f) * 0.8f, drawRotation, drawOrigin, base.Projectile.scale, (SpriteEffects)0);
		}
		Main.spriteBatch.End();
		Main.spriteBatch.Begin((SpriteSortMode)0, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, (Effect)null, Main.GameViewMatrix.TransformationMatrix);
		return false;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		writer.Write(initialized);
		writer.WriteVector2(direction);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		initialized = reader.ReadBoolean();
		direction = reader.ReadVector2();
	}

	public SanguineFury()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		direction = Vector2.Zero;
		base._002Ector();
	}
}
