using System;
using System.Collections.Generic;
using System.IO;
using CalamityMod.Items.Weapons.Magic;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class EnormousConsumingVortex : ModProjectile, ILocalizedModType, IModType
{
	public const int ExplodeTime = 45;

	public const float StartingScale = 0.0004f;

	public const float IdealScale = 2.7f;

	public new string LocalizationCategory => "Projectiles.Magic";

	public Player Owner => Main.player[base.Projectile.owner];

	public float Time
	{
		get
		{
			return base.Projectile.ai[0];
		}
		set
		{
			base.Projectile.ai[0] = value;
		}
	}

	public bool HasBeenReleased
	{
		get
		{
			return base.Projectile.ai[1] == 1f;
		}
		set
		{
			base.Projectile.ai[1] = value.ToInt();
		}
	}

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetDefaults()
	{
		base.Projectile.width = 2;
		base.Projectile.height = 2;
		base.Projectile.friendly = true;
		base.Projectile.alpha = 255;
		base.Projectile.penetrate = -1;
		base.Projectile.scale = 0.0004f;
		base.Projectile.timeLeft = 90000;
		base.Projectile.tileCollide = false;
		base.Projectile.netImportant = true;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 7;
		base.Projectile.hide = true;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(base.Projectile.scale);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		base.Projectile.scale = reader.ReadSingle();
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_026f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0217: Unknown result type (might be due to invalid IL or missing references)
		//IL_021c: Unknown result type (might be due to invalid IL or missing references)
		//IL_022d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0232: Unknown result type (might be due to invalid IL or missing references)
		//IL_0234: Unknown result type (might be due to invalid IL or missing references)
		//IL_0235: Unknown result type (might be due to invalid IL or missing references)
		//IL_023c: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0739: Unknown result type (might be due to invalid IL or missing references)
		//IL_0743: Unknown result type (might be due to invalid IL or missing references)
		//IL_0748: Unknown result type (might be due to invalid IL or missing references)
		//IL_05be: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_05fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0608: Unknown result type (might be due to invalid IL or missing references)
		//IL_060d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0612: Unknown result type (might be due to invalid IL or missing references)
		//IL_0614: Unknown result type (might be due to invalid IL or missing references)
		//IL_0619: Unknown result type (might be due to invalid IL or missing references)
		//IL_0621: Unknown result type (might be due to invalid IL or missing references)
		//IL_0626: Unknown result type (might be due to invalid IL or missing references)
		//IL_0630: Unknown result type (might be due to invalid IL or missing references)
		//IL_0635: Unknown result type (might be due to invalid IL or missing references)
		//IL_0643: Unknown result type (might be due to invalid IL or missing references)
		//IL_0648: Unknown result type (might be due to invalid IL or missing references)
		//IL_064f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0654: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0501: Unknown result type (might be due to invalid IL or missing references)
		//IL_0508: Unknown result type (might be due to invalid IL or missing references)
		//IL_050d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0516: Unknown result type (might be due to invalid IL or missing references)
		//IL_051b: Unknown result type (might be due to invalid IL or missing references)
		//IL_051d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0521: Unknown result type (might be due to invalid IL or missing references)
		//IL_0316: Unknown result type (might be due to invalid IL or missing references)
		//IL_0321: Unknown result type (might be due to invalid IL or missing references)
		//IL_0357: Unknown result type (might be due to invalid IL or missing references)
		//IL_0365: Unknown result type (might be due to invalid IL or missing references)
		//IL_036f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0374: Unknown result type (might be due to invalid IL or missing references)
		//IL_0388: Unknown result type (might be due to invalid IL or missing references)
		//IL_038d: Unknown result type (might be due to invalid IL or missing references)
		Vector2 center = base.Projectile.Center;
		Color white = Color.White;
		Lighting.AddLight(center, ((Color)(ref white)).ToVector3() * 1.3f);
		if ((!Owner.Calamity().mouseRight || Owner.noItems || Owner.CCed) && !HasBeenReleased)
		{
			if (Time >= 240f)
			{
				if (Main.myPlayer == base.Projectile.owner)
				{
					base.Projectile.velocity = base.Projectile.SafeDirectionTo(Main.MouseWorld) * 33f;
					base.Projectile.damage = (int)((float)base.Projectile.damage * 4.65f);
					HasBeenReleased = true;
					base.Projectile.netUpdate = true;
				}
			}
			else if (Time >= 56f)
			{
				if (Main.myPlayer == base.Projectile.owner)
				{
					base.Projectile.velocity = base.Projectile.SafeDirectionTo(Main.MouseWorld) * 33f;
					base.Projectile.damage = (int)((float)base.Projectile.damage * (1f + Time * 0.0152f));
					HasBeenReleased = true;
					base.Projectile.netUpdate = true;
				}
			}
			else
			{
				base.Projectile.Kill();
			}
			return;
		}
		if (HasBeenReleased && base.Projectile.timeLeft > 45 && !base.Projectile.WithinRange(Owner.Center, 2000f))
		{
			base.Projectile.Kill();
			return;
		}
		if (Time >= 56f)
		{
			Vector2 bookPosition = Owner.Center + Vector2.UnitX * (float)Owner.direction * 22f;
			if (Main.rand.NextBool())
			{
				Vector2 energyVelocity = Main.rand.NextVector2Circular(3f, 3f);
				Color energyColor = CalamityUtils.MulticolorLerp(Main.rand.NextFloat(), CalamityUtils.ExoPalette);
				GeneralParticleHandler.SpawnParticle(new SquishyLightParticle(bookPosition, energyVelocity, 0.55f, energyColor, 40, 1f, 1.5f));
			}
			NPC potentialTarget = base.Projectile.Center.ClosestNPCAt(1200f);
			if (potentialTarget != null && Time % 27f == 26f && Time < 240f && !HasBeenReleased)
			{
				bool allowContinuedUse = Owner.CheckMana(Owner.HeldItem, -1, pay: true);
				if ((Owner.Calamity().mouseRight & allowContinuedUse) && !Owner.noItems && !Owner.CCed)
				{
					SoundEngine.PlaySound(in SoundID.Item84, base.Projectile.Center);
					if (Main.myPlayer == base.Projectile.owner)
					{
						float hue = (Time - 56f) / 125f;
						Vector2 vortexVelocity = base.Projectile.SafeDirectionTo(potentialTarget.Center) * 8f;
						Projectile.NewProjectile(base.Projectile.GetSource_FromAI(), base.Projectile.Center, vortexVelocity, ModContent.ProjectileType<ExoVortex>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner, hue);
					}
					base.Projectile.netUpdate = true;
				}
			}
		}
		if (Main.rand.NextBool() && base.Projectile.Opacity > 0.6f)
		{
			float dustSpawnChance = Utils.Remap(Time, 12f, 64f, 0.25f, 0.7f);
			for (int i = 0; i < 3; i++)
			{
				if (Main.rand.NextFloat() < dustSpawnChance)
				{
					float spawnOffsetAngle = Main.rand.NextFloat((float)Math.PI * 2f);
					float hue2 = (float)Math.Sin(spawnOffsetAngle + Time / 26f) * 0.5f + 0.5f;
					float spawnOffsetFactor = Main.rand.NextFloat(0.3f, 0.95f);
					float energyScale = base.Projectile.scale * Main.rand.NextFloat(0.18f, 0.3f);
					if (energyScale > 1f)
					{
						energyScale = 1f;
					}
					Vector2 position = base.Projectile.Center + spawnOffsetAngle.ToRotationVector2() * base.Projectile.Size * spawnOffsetFactor;
					Vector2 energyVelocity2 = (spawnOffsetAngle - (float)Math.PI / 2f).ToRotationVector2() * (Main.rand.NextFloat(5f, 10f) * spawnOffsetFactor) * dustSpawnChance;
					Color energyColor2 = CalamityUtils.MulticolorLerp(hue2, CalamityUtils.ExoPalette);
					GeneralParticleHandler.SpawnParticle(new SquishyLightParticle(position, energyVelocity2, energyScale, energyColor2, 32, 1f, 1.5f));
				}
			}
		}
		if (Main.myPlayer == base.Projectile.owner && !HasBeenReleased)
		{
			float verticalOffset = Utils.Remap(Time, 0f, 90f, -30f, (float)Math.Cos((float)base.Projectile.timeLeft / 32f) * 30f);
			Vector2 hoverDestination = Owner.Top + new Vector2((float)Owner.direction * base.Projectile.scale * 30f, verticalOffset);
			hoverDestination += (Owner.ClampedMouseWorld() - hoverDestination) * 0.09f;
			Vector2 idealVelocity = Vector2.Zero.MoveTowards(hoverDestination - base.Projectile.Center, 32f);
			base.Projectile.velocity = Vector2.Lerp(base.Projectile.velocity, idealVelocity, 0.04f);
			base.Projectile.ForceNetUpdate();
		}
		base.Projectile.Opacity = Utils.GetLerpValue(0f, 20f, Time, clamped: true) * Utils.GetLerpValue(0f, 45f, base.Projectile.timeLeft, clamped: true);
		base.Projectile.scale = Utils.Remap(Time, 0f, 240f, 0.0004f, 2.7f);
		base.Projectile.scale *= Utils.Remap(base.Projectile.timeLeft, 45f, 1f, 1f, 5.4f);
		base.Projectile.ExpandHitboxBy((int)(base.Projectile.scale * 62f));
		if (base.Projectile.timeLeft < 45)
		{
			Projectile projectile = base.Projectile;
			projectile.velocity *= 0.8f;
		}
		AdjustPlayerValues();
		Time++;
	}

	public void AdjustPlayerValues()
	{
		base.Projectile.spriteDirection = (base.Projectile.direction = Owner.direction);
		Owner.heldProj = base.Projectile.whoAmI;
		Owner.itemTime = 2;
		Owner.itemAnimation = 2;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		if (HasBeenReleased && base.Projectile.timeLeft >= 45)
		{
			SoundStyle style = SubsumingVortex.ExplosionSound with
			{
				Volume = 1.3f
			};
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			base.Projectile.timeLeft = 45;
			base.Projectile.netUpdate = true;
		}
	}

	public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
	{
		behindProjectiles.Add(index);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		Main.spriteBatch.EnterShaderRegion(BlendState.Additive);
		Texture2D worleyNoise = ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/GreyscaleGradients/BlobbyNoise", (AssetRequestMode)2).Value;
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition;
		Vector2 scale = base.Projectile.Size / worleyNoise.Size() * 2f;
		float spinRotation = Main.GlobalTimeWrappedHourly * 2.4f;
		GameShaders.Misc["CalamityMod:ExoVortex"].UseOpacity(1f);
		GameShaders.Misc["CalamityMod:ExoVortex"].Apply();
		for (int i = 0; i < CalamityUtils.ExoPalette.Length; i++)
		{
			float spinDirection = ((float)i % 2f == 0f).ToDirectionInt();
			Vector2 drawOffset = ((float)Math.PI * 2f * (float)i / (float)CalamityUtils.ExoPalette.Length + Main.GlobalTimeWrappedHourly * spinDirection * 4f).ToRotationVector2() * base.Projectile.scale * 15f;
			Main.spriteBatch.Draw(worleyNoise, drawPosition + drawOffset, (Rectangle?)null, CalamityUtils.ExoPalette[i] * base.Projectile.Opacity, spinDirection * spinRotation, worleyNoise.Size() * 0.5f, scale, (SpriteEffects)0, 0f);
		}
		Main.spriteBatch.ExitShaderRegion();
		return false;
	}
}
