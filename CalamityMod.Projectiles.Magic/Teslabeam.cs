using System;
using System.Collections.Generic;
using System.IO;
using CalamityMod.Graphics.Primitives;
using CalamityMod.NPCs;
using CalamityMod.Projectiles.BaseProjectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.Enums;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

[PierceResistException(false)]
public class Teslabeam : BaseLaserbeamProjectile, ILocalizedModType, IModType
{
	private List<Vector2> offsetPoints = new List<Vector2>();

	private NPC Victim;

	public float damageMultiplier = 1f;

	public bool damageShouldDecay;

	public float decayGracePeriod;

	public const float MaxDamageMultiplier = 5f;

	public const float GracePeriod = 10f;

	public const float DamagePerHit = 0.1f;

	public const float DamageDecayPerFrame = 0.01f;

	public const float AimResponsiveness = 0.965f;

	public new string LocalizationCategory => "Projectiles.Magic";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public Player Owner => Main.player[base.Projectile.owner];

	public override Color LightCastColor
	{
		get
		{
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			return new Color(92, 144, 245);
		}
	}

	public override float Lifetime => 18000f;

	public override float MaxScale => 1f;

	public override float MaxLaserLength => 1600f;

	public override Texture2D LaserBeginTexture => ModContent.Request<Texture2D>("CalamityMod/Projectiles/InvisibleProj", (AssetRequestMode)1).Value;

	public override Texture2D LaserMiddleTexture => ModContent.Request<Texture2D>("CalamityMod/Projectiles/InvisibleProj", (AssetRequestMode)1).Value;

	public override Texture2D LaserEndTexture => ModContent.Request<Texture2D>("CalamityMod/Projectiles/InvisibleProj", (AssetRequestMode)1).Value;

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 1;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 10;
		base.Projectile.height = 10;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.hide = true;
		base.Projectile.timeLeft = 18000;
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = 6;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(damageMultiplier);
		writer.Write(decayGracePeriod);
		writer.Write(damageShouldDecay);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		damageMultiplier = reader.ReadSingle();
		decayGracePeriod = reader.ReadSingle();
		damageShouldDecay = reader.ReadBoolean();
	}

	public override bool PreAI()
	{
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fd: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.owner == Main.myPlayer)
		{
			Vector2 rrp = Owner.RotatedRelativePoint(Owner.MountedCenter, reverseRotation: true);
			UpdateAim(rrp);
			base.Projectile.direction = ((Main.MouseWorld.X > Owner.Center.X) ? 1 : (-1));
			base.Projectile.netUpdate = true;
		}
		int dir = Math.Sign(base.Projectile.velocity.X);
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		base.Projectile.Center = Owner.Center + base.Projectile.velocity * 56f;
		base.Projectile.timeLeft = 18000;
		Owner.ChangeDir(dir);
		Owner.heldProj = base.Projectile.whoAmI;
		Owner.itemTime = 2;
		Owner.itemAnimation = 2;
		Owner.itemRotation = ((base.Projectile.rotation + (float)Math.PI / 2f).ToRotationVector2() * (float)(-Owner.direction)).ToRotation();
		if (!Owner.channel)
		{
			base.Projectile.Kill();
			return false;
		}
		if (Owner.miscCounter % 10 == 0 && !Owner.CheckMana(Owner.HeldItem, -1, pay: true))
		{
			base.Projectile.Kill();
			return false;
		}
		base.Projectile.ai[2]++;
		if (base.Projectile.ai[2] % 5f == 0f)
		{
			SoundStyle style = SoundID.DD2_LightningBugZap with
			{
				Pitch = 1.1f
			};
			SoundEngine.PlaySound(in style, base.Projectile.Center);
		}
		if (decayGracePeriod > 0f && damageShouldDecay)
		{
			decayGracePeriod--;
		}
		if (damageShouldDecay && damageMultiplier > 1f && decayGracePeriod <= 0f)
		{
			damageMultiplier = MathHelper.Max(damageMultiplier - 0.01f, 1f);
		}
		base.Projectile.damage = (int)MathHelper.Clamp((float)base.Projectile.originalDamage * damageMultiplier, 0f, (float)base.Projectile.originalDamage * 5f);
		damageShouldDecay = true;
		return true;
	}

	internal float WidthFunction(float completionRatio, Vector2 vertexPos)
	{
		return MathHelper.Clamp(completionRatio * 15f, 1f, 1.5f);
	}

	internal Color ColorFunction(float completionRatio, Vector2 vertexPos)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		return new Color(174, 227, 244);
	}

	internal float BackgroundWidthFunction(float completionRatio, Vector2 vertexPos)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		return WidthFunction(completionRatio, vertexPos) * 4f;
	}

	internal Color BackgroundColorFunction(float completionRatio, Vector2 vertexPos)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		return new Color(92, 144, 245) * 0.6f;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		Main.spriteBatch.EnterShaderRegion();
		GameShaders.Misc["CalamityMod:TeslaTrail"].SetShaderTexture(ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Trails/ZapTrail", (AssetRequestMode)2));
		if (base.Projectile.ai[2] % 2f == 0f)
		{
			offsetPoints.Clear();
			for (int i = 0; i <= 75; i++)
			{
				Vector2 baseVec = Vector2.Zero;
				float width = 16f;
				if (i > 0)
				{
					baseVec += Main.rand.NextVector2Square(0f - width, width);
				}
				offsetPoints.Add(Main.rand.NextVector2Square(0f - width, width));
			}
		}
		if (offsetPoints.Count < 75)
		{
			return false;
		}
		List<Vector2> finalPoints = new List<Vector2>();
		for (int j = 0; j <= 75; j++)
		{
			Vector2 baseVec2 = Vector2.Lerp(base.Projectile.Center, base.Projectile.Center + base.Projectile.velocity * base.LaserLength, (float)j / 73.5f);
			_ = j / 75;
			if (j > 0)
			{
				baseVec2 += offsetPoints[j];
			}
			finalPoints.Add(baseVec2);
		}
		PrimitiveRenderer.RenderTrail(finalPoints, new PrimitiveSettings(BackgroundWidthFunction, BackgroundColorFunction, null, smoothen: false, pixelate: false, GameShaders.Misc["CalamityMod:TeslaTrail"]), 75);
		PrimitiveRenderer.RenderTrail(finalPoints, new PrimitiveSettings(WidthFunction, ColorFunction, null, smoothen: false, pixelate: false, GameShaders.Misc["CalamityMod:TeslaTrail"]), 75);
		Main.spriteBatch.ExitShaderRegion();
		return false;
	}

	private void UpdateAim(Vector2 source)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		Vector2 aimVector = Vector2.Normalize(Main.MouseWorld - source);
		if (aimVector.HasNaNs())
		{
			aimVector = -Vector2.UnitY;
		}
		aimVector = Vector2.Normalize(Vector2.Lerp(aimVector, Vector2.Normalize(base.Projectile.velocity), 0.965f));
		if (aimVector != base.Projectile.velocity)
		{
			base.Projectile.netUpdate = true;
		}
		base.Projectile.velocity = aimVector;
	}

	public override bool ShouldUpdatePosition()
	{
		return false;
	}

	public override void CutTiles()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		DelegateMethods.tilecut_0 = TileCuttingContext.AttackProjectile;
		Vector2 unit = base.Projectile.velocity;
		Utils.PlotTileLine(base.Projectile.Center, base.Projectile.Center + unit * base.LaserLength, base.Projectile.width + 16, DelegateMethods.CutTiles);
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		if (damageMultiplier <= 1f)
		{
			Victim = target;
		}
		if (Victim == target)
		{
			damageShouldDecay = false;
			decayGracePeriod = (float)base.Projectile.idStaticNPCHitCooldown + 10f;
			if (damageMultiplier < 5f)
			{
				damageMultiplier = Math.Min(damageMultiplier + 0.1f, 5f);
			}
		}
		target.AddBuff(144, 30);
	}
}
